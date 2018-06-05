﻿// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using Roslynator.CSharp;

namespace Roslynator.CSharp.Analysis
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class UseAutoPropertyAnalyzer : BaseDiagnosticAnalyzer
    {
        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics
        {
            get
            {
                return ImmutableArray.Create(
                    DiagnosticDescriptors.UseAutoProperty,
                    DiagnosticDescriptors.UseAutoPropertyFadeOut);
            }
        }

        public override void Initialize(AnalysisContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            base.Initialize(context);

            context.RegisterSyntaxNodeAction(AnalyzePropertyDeclaration, SyntaxKind.PropertyDeclaration);
        }

        public static void AnalyzePropertyDeclaration(SyntaxNodeAnalysisContext context)
        {
            var property = (PropertyDeclarationSyntax)context.Node;

            if (property.ContainsDiagnostics)
                return;

            SemanticModel semanticModel = context.SemanticModel;
            CancellationToken cancellationToken = context.CancellationToken;

            IFieldSymbol fieldSymbol = null;

            AccessorDeclarationSyntax getter = null;
            AccessorDeclarationSyntax setter = null;

            ArrowExpressionClauseSyntax expressionBody = property.ExpressionBody;

            if (expressionBody != null)
            {
                IdentifierNameSyntax identifierName = GetIdentifierNameFromExpression(expressionBody.Expression);

                if (identifierName != null)
                    fieldSymbol = GetBackingFieldSymbol(identifierName, semanticModel, cancellationToken);
            }
            else
            {
                getter = property.Getter();

                if (getter != null)
                {
                    setter = property.Setter();

                    if (setter != null)
                    {
                        fieldSymbol = GetBackingFieldSymbol(getter, setter, semanticModel, cancellationToken);
                    }
                    else
                    {
                        IdentifierNameSyntax identifierName = GetIdentifierNameFromGetter(getter);

                        if (identifierName != null)
                            fieldSymbol = GetBackingFieldSymbol(identifierName, semanticModel, cancellationToken);
                    }
                }
            }

            if (fieldSymbol == null)
                return;

            var variableDeclarator = (VariableDeclaratorSyntax)fieldSymbol.GetSyntax(cancellationToken);

            if (variableDeclarator.SyntaxTree != property.SyntaxTree)
                return;

            IPropertySymbol propertySymbol = semanticModel.GetDeclaredSymbol(property, cancellationToken);

            if (propertySymbol?.IsStatic != fieldSymbol.IsStatic)
                return;

            if (!propertySymbol.ExplicitInterfaceImplementations.IsDefaultOrEmpty)
                return;

            if (!propertySymbol.Type.Equals(fieldSymbol.Type))
                return;

            if (propertySymbol.ContainingType?.Equals(fieldSymbol.ContainingType) != true)
                return;

            if (setter == null
                && propertySymbol.IsOverride
                && propertySymbol.OverriddenProperty?.SetMethod != null)
            {
                return;
            }

            foreach (AttributeData attributeData in fieldSymbol.GetAttributes())
            {
                if (attributeData.AttributeClass.HasMetadataName(MetadataNames.System_NonSerializedAttribute))
                    return;
            }

            if (HasStructLayoutAttributeWithExplicitKind(propertySymbol.ContainingType))
                return;

            if (!IsFixableBackingField(property, fieldSymbol, context.Compilation, semanticModel, cancellationToken))
                return;

            if (!CheckPreprocessorDirectives(property, variableDeclarator))
                return;

            context.ReportDiagnostic(DiagnosticDescriptors.UseAutoProperty, property.Identifier);

            if (property.ExpressionBody != null)
            {
                context.ReportNode(DiagnosticDescriptors.UseAutoPropertyFadeOut, property.ExpressionBody);
            }
            else
            {
                if (getter != null)
                    FadeOut(getter);

                if (setter != null)
                    FadeOut(setter);
            }

            void FadeOut(AccessorDeclarationSyntax accessor)
            {
                BlockSyntax body = accessor.Body;

                if (body != null)
                {
                    switch (body.Statements.First())
                    {
                        case ReturnStatementSyntax returnStatement:
                            {
                                context.ReportToken(DiagnosticDescriptors.UseAutoPropertyFadeOut, returnStatement.ReturnKeyword);
                                context.ReportNode(DiagnosticDescriptors.UseAutoPropertyFadeOut, returnStatement.Expression);
                                break;
                            }
                        case ExpressionStatementSyntax expressionStatement:
                            {
                                context.ReportNode(DiagnosticDescriptors.UseAutoPropertyFadeOut, expressionStatement.Expression);
                                break;
                            }
                    }

                    context.ReportBraces(DiagnosticDescriptors.UseAutoPropertyFadeOut, body);
                }
                else
                {
                    context.ReportNode(DiagnosticDescriptors.UseAutoPropertyFadeOut, accessor.ExpressionBody);
                }
            }
        }

        private static bool HasStructLayoutAttributeWithExplicitKind(INamedTypeSymbol typeSymbol)
        {
            AttributeData attribute = typeSymbol.GetAttribute(MetadataNames.System_Runtime_InteropServices_StructLayoutAttribute);

            if (attribute != null)
            {
                TypedConstant typedConstant = attribute.ConstructorArguments.SingleOrDefault(shouldThrow: false);

                return typedConstant.Type?.HasMetadataName(MetadataNames.System_Runtime_InteropServices_LayoutKind) == true
                    && (((LayoutKind)typedConstant.Value) == LayoutKind.Explicit);
            }

            return false;
        }

        private static bool IsFixableBackingField(
            PropertyDeclarationSyntax propertyDeclaration,
            IFieldSymbol fieldSymbol,
            Compilation compilation,
            SemanticModel semanticModel,
            CancellationToken cancellationToken)
        {
            UseAutoPropertyWalker walker = UseAutoPropertyWalkerCache.GetInstance();

            INamedTypeSymbol containingType = fieldSymbol.ContainingType;

            bool shouldSearchForReferenceInInstanceConstructor = !containingType.IsSealed
                && propertyDeclaration.Modifiers.ContainsAny(SyntaxKind.VirtualKeyword, SyntaxKind.OverrideKeyword);

            ImmutableArray<SyntaxReference> syntaxReferences = containingType.DeclaringSyntaxReferences;

            if (syntaxReferences.Length == 1)
            {
                walker.SetValues(fieldSymbol, shouldSearchForReferenceInInstanceConstructor, default(Compilation), semanticModel, cancellationToken);

                walker.Visit(propertyDeclaration.Parent);

                bool isFixable = !walker.IsReferencedInInstanceConstructor && !walker.IsUsedInRefOrOutArgument;

                UseAutoPropertyWalkerCache.Free(walker);

                return isFixable;
            }
            else
            {
                foreach (SyntaxReference syntaxReference in syntaxReferences)
                {
                    SyntaxNode typeDeclaration = syntaxReference.GetSyntax(cancellationToken);

                    if (typeDeclaration.SyntaxTree == semanticModel.SyntaxTree)
                    {
                        walker.SetValues(fieldSymbol, shouldSearchForReferenceInInstanceConstructor, default(Compilation), semanticModel, cancellationToken);
                    }
                    else
                    {
                        walker.SetValues(fieldSymbol, shouldSearchForReferenceInInstanceConstructor, compilation, default(SemanticModel), cancellationToken);
                    }

                    walker.Visit(typeDeclaration);

                    bool isFixable = !walker.IsReferencedInInstanceConstructor && !walker.IsUsedInRefOrOutArgument;

                    if (!isFixable)
                    {
                        UseAutoPropertyWalkerCache.Free(walker);
                        return false;
                    }
                }

                UseAutoPropertyWalkerCache.Free(walker);
                return true;
            }
        }

        private static IFieldSymbol GetBackingFieldSymbol(
            IdentifierNameSyntax identifierName,
            SemanticModel semanticModel,
            CancellationToken cancellationToken)
        {
            ISymbol symbol = semanticModel.GetSymbol(identifierName, cancellationToken);

            if (symbol?.DeclaredAccessibility == Accessibility.Private
                && symbol.Kind == SymbolKind.Field)
            {
                var fieldSymbol = (IFieldSymbol)symbol;

                if (fieldSymbol.IsReadOnly
                    && !fieldSymbol.IsVolatile)
                {
                    return fieldSymbol;
                }
            }

            return null;
        }

        private static IFieldSymbol GetBackingFieldSymbol(
            AccessorDeclarationSyntax getter,
            AccessorDeclarationSyntax setter,
            SemanticModel semanticModel,
            CancellationToken cancellationToken)
        {
            IdentifierNameSyntax getterName = GetIdentifierNameFromGetter(getter);

            if (getterName == null)
                return null;

            IdentifierNameSyntax setterName = GetIdentifierNameFromSetter(setter);

            if (setterName == null)
                return null;

            ISymbol getterSymbol = semanticModel.GetSymbol(getterName, cancellationToken);

            if (getterSymbol?.DeclaredAccessibility != Accessibility.Private)
                return null;

            if (getterSymbol.Kind != SymbolKind.Field)
                return null;

            var fieldSymbol = (IFieldSymbol)getterSymbol;

            if (fieldSymbol.IsVolatile)
                return null;

            ISymbol setterSymbol = semanticModel.GetSymbol(setterName, cancellationToken);

            if (fieldSymbol.Equals(setterSymbol))
                return fieldSymbol;

            return null;
        }

        private static IdentifierNameSyntax GetIdentifierNameFromGetter(AccessorDeclarationSyntax getter)
        {
            if (getter != null)
            {
                BlockSyntax body = getter.Body;

                if (body != null)
                {
                    StatementSyntax statement = body.Statements.SingleOrDefault(shouldThrow: false);

                    if (statement.IsKind(SyntaxKind.ReturnStatement))
                    {
                        var returnStatement = (ReturnStatementSyntax)statement;

                        return GetIdentifierNameFromExpression(returnStatement.Expression);
                    }
                }
                else
                {
                    return GetIdentifierNameFromExpression(getter.ExpressionBody?.Expression);
                }
            }

            return null;
        }

        private static IdentifierNameSyntax GetIdentifierNameFromSetter(AccessorDeclarationSyntax setter)
        {
            if (setter != null)
            {
                BlockSyntax body = setter.Body;

                if (body != null)
                {
                    StatementSyntax statement = body.Statements.SingleOrDefault(shouldThrow: false);

                    if (statement.IsKind(SyntaxKind.ExpressionStatement))
                    {
                        var expressionStatement = (ExpressionStatementSyntax)statement;

                        ExpressionSyntax expression = expressionStatement.Expression;

                        return GetIdentifierName(expression);
                    }
                }
                else
                {
                    return GetIdentifierName(setter.ExpressionBody?.Expression);
                }
            }

            return null;

            IdentifierNameSyntax GetIdentifierName(ExpressionSyntax expression)
            {
                if (expression?.Kind() == SyntaxKind.SimpleAssignmentExpression)
                {
                    var assignment = (AssignmentExpressionSyntax)expression;
                    ExpressionSyntax right = assignment.Right;

                    if (right.IsKind(SyntaxKind.IdentifierName)
                        && ((IdentifierNameSyntax)right).Identifier.ValueText == "value")
                    {
                        return GetIdentifierNameFromExpression(assignment.Left);
                    }
                }

                return null;
            }
        }

        private static IdentifierNameSyntax GetIdentifierNameFromExpression(ExpressionSyntax expression)
        {
            switch (expression?.Kind())
            {
                case SyntaxKind.IdentifierName:
                    {
                        return (IdentifierNameSyntax)expression;
                    }
                case SyntaxKind.SimpleMemberAccessExpression:
                    {
                        var memberAccess = (MemberAccessExpressionSyntax)expression;

                        if (memberAccess.Expression?.Kind() == SyntaxKind.ThisExpression)
                        {
                            SimpleNameSyntax name = memberAccess.Name;

                            if (name.IsKind(SyntaxKind.IdentifierName))
                                return (IdentifierNameSyntax)name;
                        }

                        break;
                    }
            }

            return null;
        }

        private static bool CheckPreprocessorDirectives(
            PropertyDeclarationSyntax property,
            VariableDeclaratorSyntax declarator)
        {
            ArrowExpressionClauseSyntax expressionBody = property.ExpressionBody;

            if (expressionBody != null)
            {
                if (expressionBody.SpanContainsDirectives())
                    return false;
            }
            else if (property.AccessorList.Accessors.Any(f => f.SpanContainsDirectives()))
            {
                return false;
            }

            var variableDeclaration = (VariableDeclarationSyntax)declarator.Parent;

            if (variableDeclaration.Variables.Count == 1)
            {
                if (variableDeclaration.Parent.SpanContainsDirectives())
                    return false;
            }
            else if (declarator.SpanContainsDirectives())
            {
                return false;
            }

            return true;
        }

        private class UseAutoPropertyWalker : CSharpSyntaxWalker
        {
            private bool _isInInstanceConstructor;

            public IFieldSymbol FieldSymbol { get; private set; }

            public bool ShouldSearchForReferenceInInstanceConstructor { get; private set; }

            public Compilation Compilation { get; private set; }

            public SemanticModel SemanticModel { get; private set; }

            public CancellationToken CancellationToken { get; private set; }

            public bool IsUsedInRefOrOutArgument { get; private set; }

            public bool IsReferencedInInstanceConstructor { get; private set; }

            public void SetValues(
                IFieldSymbol fieldSymbol,
                bool shouldSearchForReferenceInInstanceConstructor,
                Compilation compilation,
                SemanticModel semanticModel,
                CancellationToken cancellationToken)
            {
                FieldSymbol = fieldSymbol;
                ShouldSearchForReferenceInInstanceConstructor = shouldSearchForReferenceInInstanceConstructor;
                Compilation = compilation;
                SemanticModel = semanticModel;
                CancellationToken = cancellationToken;
                IsUsedInRefOrOutArgument = false;
                IsReferencedInInstanceConstructor = false;
            }

            public void Clear()
            {
                SetValues(
                    default(IFieldSymbol),
                    false,
                    default(Compilation),
                    default(SemanticModel),
                    default(CancellationToken));
            }

            public override void Visit(SyntaxNode node)
            {
                if (!IsUsedInRefOrOutArgument
                    && !IsReferencedInInstanceConstructor)
                {
                    base.Visit(node);
                }
            }

            public override void VisitArgument(ArgumentSyntax node)
            {
                if (node.RefOrOutKeyword.IsKind(SyntaxKind.RefKeyword, SyntaxKind.OutKeyword))
                {
                    ExpressionSyntax expression = node.Expression;

                    switch (expression?.Kind())
                    {
                        case SyntaxKind.IdentifierName:
                            {
                                IsUsedInRefOrOutArgument = IsBackingFieldReference((IdentifierNameSyntax)expression);
                                return;
                            }
                        case SyntaxKind.SimpleMemberAccessExpression:
                            {
                                var memberAccessExpression = (MemberAccessExpressionSyntax)expression;

                                if (memberAccessExpression.Expression.IsKind(SyntaxKind.ThisExpression))
                                {
                                    SimpleNameSyntax name = memberAccessExpression.Name;

                                    if (name.IsKind(SyntaxKind.IdentifierName))
                                    {
                                        IsUsedInRefOrOutArgument = IsBackingFieldReference((IdentifierNameSyntax)name);
                                        return;
                                    }
                                }

                                break;
                            }
                    }
                }

                base.VisitArgument(node);
            }

            public override void VisitConstructorDeclaration(ConstructorDeclarationSyntax node)
            {
                Debug.Assert(!_isInInstanceConstructor);

                if (ShouldSearchForReferenceInInstanceConstructor
                    && !node.Modifiers.Contains(SyntaxKind.StaticKeyword))
                {
                    _isInInstanceConstructor = true;
                }

                base.VisitConstructorDeclaration(node);
                _isInInstanceConstructor = false;
            }

            public override void VisitIdentifierName(IdentifierNameSyntax node)
            {
                if (_isInInstanceConstructor
                    && IsBackingFieldReference(node))
                {
                    IsReferencedInInstanceConstructor = true;
                }

                base.VisitIdentifierName(node);
            }

            private bool IsBackingFieldReference(IdentifierNameSyntax identifierName)
            {
                if (string.Equals(identifierName.Identifier.ValueText, FieldSymbol.Name, StringComparison.Ordinal))
                {
                    if (SemanticModel == null)
                        SemanticModel = Compilation.GetSemanticModel(identifierName.SyntaxTree);

                    if (SemanticModel.GetSymbol(identifierName, CancellationToken)?.Equals(FieldSymbol) == true)
                        return true;
                }

                return false;
            }
        }

        private static class UseAutoPropertyWalkerCache
        {
            [ThreadStatic]
            private static UseAutoPropertyWalker _cachedInstance;

            public static UseAutoPropertyWalker GetInstance()
            {
                UseAutoPropertyWalker walker = _cachedInstance;

                if (walker != null)
                {
                    _cachedInstance = null;
                }
                else
                {
                    walker = new UseAutoPropertyWalker();
                }

                return walker;
            }

            public static void Free(UseAutoPropertyWalker walker)
            {
                walker.Clear();
                _cachedInstance = walker;
            }
        }
    }
}
