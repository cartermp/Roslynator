﻿// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

// <auto-generated>

using System;
using Microsoft.CodeAnalysis;

namespace Roslynator.CSharp
{
    public static partial class DiagnosticDescriptors
    {
        [Obsolete("", error: true)]
        public static readonly DiagnosticDescriptor RemoveRedundantBraces = new DiagnosticDescriptor(id: DiagnosticIdentifiers.RemoveRedundantBraces, title: "Remove redundant braces.", messageFormat: "Remove redundant braces.", category: DiagnosticCategories.Redundancy, defaultSeverity: DiagnosticSeverity.Info, isEnabledByDefault: true, description: null, helpLinkUri: $"{HelpLinkUriRoot}{DiagnosticIdentifiers.RemoveRedundantBraces}", customTags: _noTags);
        [Obsolete("", error: true)]
        public static readonly DiagnosticDescriptor RemoveRedundantBracesFadeOut = RemoveRedundantBraces.CreateFadeOut();
        [Obsolete("", error: true)]
        public static readonly DiagnosticDescriptor ReplaceForEachWithFor = new DiagnosticDescriptor(id: DiagnosticIdentifiers.ReplaceForEachWithFor, title: "Replace foreach statement with for statement.", messageFormat: "Replace foreach statement with for statement.", category: DiagnosticCategories.General, defaultSeverity: DiagnosticSeverity.Hidden, isEnabledByDefault: false, description: null, helpLinkUri: $"{HelpLinkUriRoot}{DiagnosticIdentifiers.ReplaceForEachWithFor}", customTags: _noTags);
        [Obsolete("", error: true)]
        public static readonly DiagnosticDescriptor ReplaceForEachWithForFadeOut = ReplaceForEachWithFor.CreateFadeOut();
        [Obsolete("", error: true)]
        public static readonly DiagnosticDescriptor MergeLocalDeclarationWithReturnStatement = new DiagnosticDescriptor(id: DiagnosticIdentifiers.MergeLocalDeclarationWithReturnStatement, title: "Merge local declaration with return statement.", messageFormat: "Merge local declaration with return statement.", category: DiagnosticCategories.Simplification, defaultSeverity: DiagnosticSeverity.Hidden, isEnabledByDefault: true, description: null, helpLinkUri: $"{HelpLinkUriRoot}{DiagnosticIdentifiers.MergeLocalDeclarationWithReturnStatement}", customTags: _noTags);
        [Obsolete("", error: true)]
        public static readonly DiagnosticDescriptor MergeLocalDeclarationWithReturnStatementFadeOut = MergeLocalDeclarationWithReturnStatement.CreateFadeOut();
        [Obsolete("", error: true)]
        public static readonly DiagnosticDescriptor ReplaceReturnStatementWithExpressionStatement = new DiagnosticDescriptor(id: DiagnosticIdentifiers.ReplaceReturnStatementWithExpressionStatement, title: "Replace yield/return statement with expression statement.", messageFormat: "Replace {0} statement with expression statement.", category: DiagnosticCategories.ErrorFix, defaultSeverity: DiagnosticSeverity.Hidden, isEnabledByDefault: true, description: null, helpLinkUri: $"{HelpLinkUriRoot}{DiagnosticIdentifiers.ReplaceReturnStatementWithExpressionStatement}", customTags: WellKnownDiagnosticTags.Unnecessary);
        [Obsolete("", error: true)]
        public static readonly DiagnosticDescriptor AddBreakStatementToSwitchSection = new DiagnosticDescriptor(id: DiagnosticIdentifiers.AddBreakStatementToSwitchSection, title: "Add break statement to switch section.", messageFormat: "Add break statement to switch section.", category: DiagnosticCategories.ErrorFix, defaultSeverity: DiagnosticSeverity.Hidden, isEnabledByDefault: true, description: null, helpLinkUri: $"{HelpLinkUriRoot}{DiagnosticIdentifiers.AddBreakStatementToSwitchSection}", customTags: _noTags);
        [Obsolete("", error: true)]
        public static readonly DiagnosticDescriptor AddReturnStatementThatReturnsDefaultValue = new DiagnosticDescriptor(id: DiagnosticIdentifiers.AddReturnStatementThatReturnsDefaultValue, title: "Add return statement that returns default value.", messageFormat: "Add return statement that returns default value.", category: DiagnosticCategories.ErrorFix, defaultSeverity: DiagnosticSeverity.Hidden, isEnabledByDefault: true, description: null, helpLinkUri: $"{HelpLinkUriRoot}{DiagnosticIdentifiers.AddReturnStatementThatReturnsDefaultValue}", customTags: _noTags);
        [Obsolete("", error: true)]
        public static readonly DiagnosticDescriptor AddMissingSemicolon = new DiagnosticDescriptor(id: DiagnosticIdentifiers.AddMissingSemicolon, title: "Add missing semicolon.", messageFormat: "Add missing semicolon.", category: DiagnosticCategories.ErrorFix, defaultSeverity: DiagnosticSeverity.Hidden, isEnabledByDefault: false, description: null, helpLinkUri: $"{HelpLinkUriRoot}{DiagnosticIdentifiers.AddMissingSemicolon}", customTags: _noTags);
        [Obsolete("", error: true)]
        public static readonly DiagnosticDescriptor MarkMemberAsStatic = new DiagnosticDescriptor(id: DiagnosticIdentifiers.MarkMemberAsStatic, title: "Mark member as static.", messageFormat: "Mark member as static.", category: DiagnosticCategories.ErrorFix, defaultSeverity: DiagnosticSeverity.Hidden, isEnabledByDefault: true, description: null, helpLinkUri: $"{HelpLinkUriRoot}{DiagnosticIdentifiers.MarkMemberAsStatic}", customTags: _noTags);
        [Obsolete("", error: true)]
        public static readonly DiagnosticDescriptor ReplaceReturnWithYieldReturn = new DiagnosticDescriptor(id: DiagnosticIdentifiers.ReplaceReturnWithYieldReturn, title: "Replace return with yield return.", messageFormat: "Replace return with yield return.", category: DiagnosticCategories.ErrorFix, defaultSeverity: DiagnosticSeverity.Hidden, isEnabledByDefault: true, description: null, helpLinkUri: $"{HelpLinkUriRoot}{DiagnosticIdentifiers.ReplaceReturnWithYieldReturn}", customTags: _noTags);
        [Obsolete("", error: true)]
        public static readonly DiagnosticDescriptor AddDocumentationComment = new DiagnosticDescriptor(id: DiagnosticIdentifiers.AddDocumentationComment, title: "Add documentation comment to publicly visible type or member.", messageFormat: "Add documentation comment to publicly visible type or member.", category: DiagnosticCategories.Maintainability, defaultSeverity: DiagnosticSeverity.Hidden, isEnabledByDefault: true, description: null, helpLinkUri: $"{HelpLinkUriRoot}{DiagnosticIdentifiers.AddDocumentationComment}", customTags: _noTags);
        [Obsolete("", error: true)]
        public static readonly DiagnosticDescriptor MarkContainingClassAsAbstract = new DiagnosticDescriptor(id: DiagnosticIdentifiers.MarkContainingClassAsAbstract, title: "Mark containing class as abstract.", messageFormat: "Mark containing class as abstract.", category: DiagnosticCategories.ErrorFix, defaultSeverity: DiagnosticSeverity.Hidden, isEnabledByDefault: true, description: null, helpLinkUri: $"{HelpLinkUriRoot}{DiagnosticIdentifiers.MarkContainingClassAsAbstract}", customTags: _noTags);
        [Obsolete("", error: true)]
        public static readonly DiagnosticDescriptor RemoveInapplicableModifier = new DiagnosticDescriptor(id: DiagnosticIdentifiers.RemoveInapplicableModifier, title: "Remove inapplicable modifier.", messageFormat: "Remove inapplicable modifier.", category: DiagnosticCategories.ErrorFix, defaultSeverity: DiagnosticSeverity.Hidden, isEnabledByDefault: true, description: null, helpLinkUri: $"{HelpLinkUriRoot}{DiagnosticIdentifiers.RemoveInapplicableModifier}", customTags: WellKnownDiagnosticTags.Unnecessary);
        [Obsolete("", error: true)]
        public static readonly DiagnosticDescriptor RemoveUnreachableCode = new DiagnosticDescriptor(id: DiagnosticIdentifiers.RemoveUnreachableCode, title: "Remove unreachable code.", messageFormat: "Remove unreachable code.", category: DiagnosticCategories.General, defaultSeverity: DiagnosticSeverity.Hidden, isEnabledByDefault: true, description: null, helpLinkUri: $"{HelpLinkUriRoot}{DiagnosticIdentifiers.RemoveUnreachableCode}", customTags: WellKnownDiagnosticTags.Unnecessary);
        [Obsolete("", error: true)]
        public static readonly DiagnosticDescriptor RemoveImplementationFromAbstractMember = new DiagnosticDescriptor(id: DiagnosticIdentifiers.RemoveImplementationFromAbstractMember, title: "Remove implementation from abstract member.", messageFormat: "Remove implementation from {0}.", category: DiagnosticCategories.ErrorFix, defaultSeverity: DiagnosticSeverity.Hidden, isEnabledByDefault: true, description: null, helpLinkUri: $"{HelpLinkUriRoot}{DiagnosticIdentifiers.RemoveImplementationFromAbstractMember}", customTags: WellKnownDiagnosticTags.Unnecessary);
        [Obsolete("", error: true)]
        public static readonly DiagnosticDescriptor MemberTypeMustMatchOverriddenMemberType = new DiagnosticDescriptor(id: DiagnosticIdentifiers.MemberTypeMustMatchOverriddenMemberType, title: "Member type must match overriden member type.", messageFormat: "Member type must match overriden member type.", category: DiagnosticCategories.ErrorFix, defaultSeverity: DiagnosticSeverity.Hidden, isEnabledByDefault: true, description: null, helpLinkUri: $"{HelpLinkUriRoot}{DiagnosticIdentifiers.MemberTypeMustMatchOverriddenMemberType}", customTags: _noTags);
        [Obsolete("", error: true)]
        public static readonly DiagnosticDescriptor OverridingMemberCannotChangeAccessModifiers = new DiagnosticDescriptor(id: DiagnosticIdentifiers.OverridingMemberCannotChangeAccessModifiers, title: "Overriding member cannot change access modifiers.", messageFormat: "Overriding member cannot change access modifiers.", category: DiagnosticCategories.ErrorFix, defaultSeverity: DiagnosticSeverity.Hidden, isEnabledByDefault: true, description: null, helpLinkUri: $"{HelpLinkUriRoot}{DiagnosticIdentifiers.OverridingMemberCannotChangeAccessModifiers}", customTags: _noTags);
    }
}