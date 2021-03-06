﻿// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

// <auto-generated>

using System.Collections.Generic;
using Roslynator.CSharp;

namespace Roslynator.VisualStudio
{
    public partial class CodeFixesOptionsPage
    {
        protected override string DisabledByDefault
        {
            get;
        }

        = $"{CodeFixIdentifiers.RemoveReturnExpression},{CodeFixIdentifiers.RemoveReturnKeyword}";
        protected override string MaxId
        {
            get;
        }

        = CodeFixIdentifiers.MarkDeclarationAsNonCLSCompliant;
        protected override void Fill(ICollection<BaseModel> codeFixes)
        {
            codeFixes.Clear();
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.AddBreakStatementToSwitchSection, "Add break statement to switch section (fixes CS0163, CS8070)", IsEnabled(CodeFixIdentifiers.AddBreakStatementToSwitchSection)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.CreateSingletonArray, "Create singleton array (fixes CS0266)", IsEnabled(CodeFixIdentifiers.CreateSingletonArray)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.AddOutModifierToArgument, "Add 'out' modifier to argument (fixes CS1620)", IsEnabled(CodeFixIdentifiers.AddOutModifierToArgument)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.MoveBaseClassBeforeAnyInterface, "Base base class before any interface (fixes CS1722)", IsEnabled(CodeFixIdentifiers.MoveBaseClassBeforeAnyInterface)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.AddOverrideModifier, "Add 'override' modifier (fixes CS0114)", IsEnabled(CodeFixIdentifiers.AddOverrideModifier)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.AddNewModifier, "Add 'new' modifier (fixes CS0114)", IsEnabled(CodeFixIdentifiers.AddNewModifier)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.ExtractDeclarationFromUsingStatement, "Extract declaration from using statement (fixes CS1674)", IsEnabled(CodeFixIdentifiers.ExtractDeclarationFromUsingStatement)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.AddBracesToDeclarationOrLabeledStatement, "Add braces to declaration or labeled statement (fixes CS1023)", IsEnabled(CodeFixIdentifiers.AddBracesToDeclarationOrLabeledStatement)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.MarkOperatorAsPublicAndStatic, "Mark operator as 'public' and 'static' (fixes CS0558)", IsEnabled(CodeFixIdentifiers.MarkOperatorAsPublicAndStatic)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.RemoveDuplicateModifier, "Remove duplicate modifier (fixes CS1004)", IsEnabled(CodeFixIdentifiers.RemoveDuplicateModifier)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.RemoveDuplicateAttribute, "Remove duplicate attribute (fixes CS0579)", IsEnabled(CodeFixIdentifiers.RemoveDuplicateAttribute)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.RemoveNewModifier, "Remove new modifier (fixes CS0109)", IsEnabled(CodeFixIdentifiers.RemoveNewModifier)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.RemoveUnusedLabel, "Remove unused label (fixes CS0164)", IsEnabled(CodeFixIdentifiers.RemoveUnusedLabel)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.AddDocumentationComment, "Add documentation comment (fixes CS1591)", IsEnabled(CodeFixIdentifiers.AddDocumentationComment)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.RemoveUnreachableCode, "Unreachable code detected (fixes CS0162)", IsEnabled(CodeFixIdentifiers.RemoveUnreachableCode)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.ChangeMethodReturnType, "Change method return type (fixes CS0508, CS0766, CS1624, CS1983)", IsEnabled(CodeFixIdentifiers.ChangeMethodReturnType)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.MemberTypeMustMatchOverriddenMemberType, "Member type must match overridden member type (fixes CS1715)", IsEnabled(CodeFixIdentifiers.MemberTypeMustMatchOverriddenMemberType)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.AddReturnStatementThatReturnsDefaultValue, "Add return statement that returns default value (fixes CS0161, CS1643)", IsEnabled(CodeFixIdentifiers.AddReturnStatementThatReturnsDefaultValue)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.UseYieldReturnInsteadOfReturn, "Use yield return instead of return (fixes CS0029, CS1622)", IsEnabled(CodeFixIdentifiers.UseYieldReturnInsteadOfReturn)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.ReplaceStringLiteralWithCharacterLiteral, "Replace string literal with character literal (fixes CS0029)", IsEnabled(CodeFixIdentifiers.ReplaceStringLiteralWithCharacterLiteral)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.AddComparisonWithBooleanLiteral, "Add comparison with boolean literal (fixes CS0019, CS0266)", IsEnabled(CodeFixIdentifiers.AddComparisonWithBooleanLiteral)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.RemovePropertyOrFieldInitializer, "Remove property or field initializer (fixes CS0573, CS8050)", IsEnabled(CodeFixIdentifiers.RemovePropertyOrFieldInitializer)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.AddPartialModifier, "Add 'partial' modifier (fixes CS0101, CS0102, CS0260, CS0751)", IsEnabled(CodeFixIdentifiers.AddPartialModifier)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.AddBody, "Add body (fixes CS0501, CS0756, CS8112)", IsEnabled(CodeFixIdentifiers.AddBody)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.RemoveRefModifier, "Remove 'ref' modifier (fixes CS0192, CS1615, CS1988, CS1623)", IsEnabled(CodeFixIdentifiers.RemoveRefModifier)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.RemoveRedundantAssignment, "Remove redundant assignment (fixes CS1717)", IsEnabled(CodeFixIdentifiers.RemoveRedundantAssignment)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.ChangeAccessibility, "Change accessibility (fixes CS0261, CS0442, CS0507, CS0628, CS1057, CS1527)", IsEnabled(CodeFixIdentifiers.ChangeAccessibility)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.ChangeTypeOfParamsParameter, "Change type of 'params' parameter (fixes CS0225)", IsEnabled(CodeFixIdentifiers.ChangeTypeOfParamsParameter)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.UseUncheckedExpression, "Use unchecked expression (fixes CS0221)", IsEnabled(CodeFixIdentifiers.UseUncheckedExpression)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.RemoveImplementationFromAbstractMember, "Remove implementation from abstract member (fixes CS0069, CS0500, CS0531)", IsEnabled(CodeFixIdentifiers.RemoveImplementationFromAbstractMember)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.AddStaticModifier, "Add 'static' modifier (fixes CS0708, CS0710, CS1105, CS1106)", IsEnabled(CodeFixIdentifiers.AddStaticModifier)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.MakeContainingClassAbstract, "Make containing class abstract (fixes CS0513)", IsEnabled(CodeFixIdentifiers.MakeContainingClassAbstract)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.SynchronizeAccessibility, "SynchronizeAccessibility (fixes CS0262)", IsEnabled(CodeFixIdentifiers.SynchronizeAccessibility)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.RemoveArgumentList, "Remove argument list (fixes CS1955)", IsEnabled(CodeFixIdentifiers.RemoveArgumentList)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.FixMemberAccessName, "Fix member access name (fixes CS1061)", IsEnabled(CodeFixIdentifiers.FixMemberAccessName)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.AddArgumentList, "Add argument list (fixes CS0023, CS0119, CS0428, CS0201, CS1526)", IsEnabled(CodeFixIdentifiers.AddArgumentList)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.InitializeLocalVariableWithDefaultValue, "Initialize local variable with default value (fixes CS0165)", IsEnabled(CodeFixIdentifiers.InitializeLocalVariableWithDefaultValue)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.ChangeTypeOfLocalVariable, "Change type of local variable (fixes CS0123, CS0407, CS0815)", IsEnabled(CodeFixIdentifiers.ChangeTypeOfLocalVariable)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.MakeMemberNonStatic, "Make member non-static (fixes CS0120)", IsEnabled(CodeFixIdentifiers.MakeMemberNonStatic)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.RemoveConstraintClauses, "Remove constraint clauses (fixes CS0080)", IsEnabled(CodeFixIdentifiers.RemoveConstraintClauses)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.AddTypeArgument, "Add type argument (fixes CS0305)", IsEnabled(CodeFixIdentifiers.AddTypeArgument)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.RemoveConstModifier, "Remove 'const' modifier (fixes CS0131, CS0133)", IsEnabled(CodeFixIdentifiers.RemoveConstModifier)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.RemoveEmptySwitchStatement, "Remove empty switch statement (fixes CS1522)", IsEnabled(CodeFixIdentifiers.RemoveEmptySwitchStatement)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.IntroduceLocalVariable, "Introduce local variable (fixes CS0201)", IsEnabled(CodeFixIdentifiers.IntroduceLocalVariable)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.UseExplicitTypeInsteadOfVar, "Use explicit type instead of 'var' (fixes CS0819, CS0822)", IsEnabled(CodeFixIdentifiers.UseExplicitTypeInsteadOfVar)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.RemoveAwaitKeyword, "Remove 'await' keyword (fixes CS1061)", IsEnabled(CodeFixIdentifiers.RemoveAwaitKeyword)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.WrapInUnsafeStatement, "Wrap in unsafe statement (fixes CS0214)", IsEnabled(CodeFixIdentifiers.WrapInUnsafeStatement)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.MakeContainingDeclarationUnsafe, "Make containing declaration unsafe (fixes CS0214)", IsEnabled(CodeFixIdentifiers.MakeContainingDeclarationUnsafe)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.RemoveReturnKeyword, "Remove 'return' keyword (fixes CS0127, CS1997)", IsEnabled(CodeFixIdentifiers.RemoveReturnKeyword)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.RemoveParametersFromStaticConstructor, "Remove parameters from static constructor (fixes CS0132)", IsEnabled(CodeFixIdentifiers.RemoveParametersFromStaticConstructor)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.OrderModifiers, "Order modifiers (fixes CS0267)", IsEnabled(CodeFixIdentifiers.OrderModifiers)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.RemoveInvalidModifier, "Remove invalid modifier (fixes CS0107, CS0112, CS0115, CS0275, CS0441, CS0515, CS0678, CS0750, CS0753, CS0759, CS1609, CS1994)", IsEnabled(CodeFixIdentifiers.RemoveInvalidModifier)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.RemoveMemberDeclaration, "Remove member declaration (fixes CS0114, CS0541, CS0525, CS0567, CS0568, CS0575)", IsEnabled(CodeFixIdentifiers.RemoveMemberDeclaration)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.RemoveReturnExpression, "Remove return expression (fixes CS0127, CS1997)", IsEnabled(CodeFixIdentifiers.RemoveReturnExpression)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.ReplaceNullLiteralExpressionWithDefaultValue, "Replace 'null' with default value (fixes CS0037, CS0403, CS1503, CS1750)", IsEnabled(CodeFixIdentifiers.ReplaceNullLiteralExpressionWithDefaultValue)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.ReplaceVariableDeclarationWithAssignment, "Replace variable declaration with assignment (fixes CS0128, CS0136)", IsEnabled(CodeFixIdentifiers.ReplaceVariableDeclarationWithAssignment)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.RemoveThisModifier, "Remove 'this' modifier (fixes CS1100, CS1105, CS1106)", IsEnabled(CodeFixIdentifiers.RemoveThisModifier)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.RemoveTypeParameter, "Remove type parameter (fixes CS0693)", IsEnabled(CodeFixIdentifiers.RemoveTypeParameter)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.MakeContainingClassNonStatic, "Make containing class non-static (fixes CS0708, CS0710)", IsEnabled(CodeFixIdentifiers.MakeContainingClassNonStatic)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.AddCastExpression, "Add cast expression (fixes CS0173)", IsEnabled(CodeFixIdentifiers.AddCastExpression)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.MoveConstraint, "Move constraint (fixes CS0401, CS0449)", IsEnabled(CodeFixIdentifiers.MoveConstraint)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.RemoveConstraint, "Remove constraint (fixes CS0405, CS0450, CS0451, CS0714)", IsEnabled(CodeFixIdentifiers.RemoveConstraint)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.CombineConstraintClauses, "Combine constraint clauses (fixes CS0409)", IsEnabled(CodeFixIdentifiers.CombineConstraintClauses)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.AssignDefaultValueToOutParameter, "Assign default value to 'out' parameter (fixes CS0177)", IsEnabled(CodeFixIdentifiers.AssignDefaultValueToOutParameter)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.MakeClassNonStatic, "Make class non-static (fixes CS0713, CS0714, CS0718)", IsEnabled(CodeFixIdentifiers.MakeClassNonStatic)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.RemoveYieldKeyword, "Remove 'yield' keyword (fixes CS1621)", IsEnabled(CodeFixIdentifiers.RemoveYieldKeyword)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.RemoveOutModifier, "Remove 'out' modifier (fixes CS0192, CS1988, CS1623)", IsEnabled(CodeFixIdentifiers.RemoveOutModifier)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.RemoveAttribute, "Remove attribute (fixes CS0592, CS1689)", IsEnabled(CodeFixIdentifiers.RemoveAttribute)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.RemoveJumpStatement, "Remove jump statement (fixes CS0139)", IsEnabled(CodeFixIdentifiers.RemoveJumpStatement)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.UseCoalesceExpression, "Use coalesce expression (fixes CS0266)", IsEnabled(CodeFixIdentifiers.UseCoalesceExpression)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.ReplaceAsExpressionWithCastExpression, "Replace as expression with cast expression (fixes CS0077)", IsEnabled(CodeFixIdentifiers.ReplaceAsExpressionWithCastExpression)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.RemoveConditionThatIsAlwaysEqualToTrueOrFalse, "Remove condition that is always equal to true/false (fixes CS0472)", IsEnabled(CodeFixIdentifiers.RemoveConditionThatIsAlwaysEqualToTrueOrFalse)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.IntroduceField, "Introduce field (fixes CS0201)", IsEnabled(CodeFixIdentifiers.IntroduceField)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.ReplaceElementAccessWithInvocation, "Replace element access with invocation (fixes CS0021)", IsEnabled(CodeFixIdentifiers.ReplaceElementAccessWithInvocation)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.AddVariableType, "Add variable type (fixes CS0103)", IsEnabled(CodeFixIdentifiers.AddVariableType)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.ReplaceBreakWithContinue, "Replace 'break' with 'continue' (fixes CS0139)", IsEnabled(CodeFixIdentifiers.ReplaceBreakWithContinue)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.ChangeMemberTypeAccordingToReturnExpression, "Change member type according to return expression (fixes CS0029, CS0127, CS0201, CS0266, CS1997)", IsEnabled(CodeFixIdentifiers.ChangeMemberTypeAccordingToReturnExpression)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.ReturnDefaultValue, "Return default value (fixes CS0126)", IsEnabled(CodeFixIdentifiers.ReturnDefaultValue)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.DefineMatchingOperator, "Define matching operator (fixes CS0216)", IsEnabled(CodeFixIdentifiers.DefineMatchingOperator)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.DefineObjectEquals, "Define object.Equals (fixes CS0660)", IsEnabled(CodeFixIdentifiers.DefineObjectEquals)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.DefineObjectGetHashCode, "Define object.GetHashCode (fixes CS0659, CS0661)", IsEnabled(CodeFixIdentifiers.DefineObjectGetHashCode)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.ChangeArrayType, "Change array type (fixes CS0246)", IsEnabled(CodeFixIdentifiers.ChangeArrayType)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.AddMissingType, "Add missing type (fixes CS1031)", IsEnabled(CodeFixIdentifiers.AddMissingType)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.ReplaceConditionalExpressionWithIfElse, "Replace conditional expression with if-else (fixes CS0201)", IsEnabled(CodeFixIdentifiers.ReplaceConditionalExpressionWithIfElse)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.RemoveSemicolon, "Remove semicolon (fixes CS1597)", IsEnabled(CodeFixIdentifiers.RemoveSemicolon)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.RemoveConditionalAccess, "Remove conditional access (fixes CS0023)", IsEnabled(CodeFixIdentifiers.RemoveConditionalAccess)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.ChangeTypeAccordingToInitializer, "Change type according to initializer (fixes CS0029)", IsEnabled(CodeFixIdentifiers.ChangeTypeAccordingToInitializer)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.ChangeForEachType, "Change foreach type (fixes CS0030)", IsEnabled(CodeFixIdentifiers.ChangeForEachType)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.ChangeTypeToVar, "Change type to var (fixes CS0029, CS0030)", IsEnabled(CodeFixIdentifiers.ChangeTypeToVar)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.RemoveDefaultValueFromParameter, "Remove default value from parameter (fixes CS1751, CS1743)", IsEnabled(CodeFixIdentifiers.RemoveDefaultValueFromParameter)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.ReplaceConstantWithField, "Replace constant with field (fixes CS0133)", IsEnabled(CodeFixIdentifiers.ReplaceConstantWithField)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.ReplaceStructWithClass, "Replace struct with class (fixes CS0527)", IsEnabled(CodeFixIdentifiers.ReplaceStructWithClass)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.ReplaceYieldReturnWithForEach, "Replace yield return with foreach (fixes CS0029)", IsEnabled(CodeFixIdentifiers.ReplaceYieldReturnWithForEach)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.ReplaceComparisonWithAssignment, "Replace comparison with assignment (fixes CS0201)", IsEnabled(CodeFixIdentifiers.ReplaceComparisonWithAssignment)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.AddModifierAbstract, "Add modifier 'abstract' (fixes CS0501)", IsEnabled(CodeFixIdentifiers.AddModifierAbstract)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.MakeMemberReadOnly, "Make member read-only' (fixes CS8340)", IsEnabled(CodeFixIdentifiers.MakeMemberReadOnly)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.RemoveSealedModifier, "Remove 'sealed' modifier (fixes CS0238)", IsEnabled(CodeFixIdentifiers.RemoveSealedModifier)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.RemoveVirtualModifier, "Remove 'virtual' modifier (fixes CS0549)", IsEnabled(CodeFixIdentifiers.RemoveVirtualModifier)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.MakeContainingClassUnsealed, "Make containing class unsealed (fixes CS0549)", IsEnabled(CodeFixIdentifiers.MakeContainingClassUnsealed)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.RemoveSwitchLabel, "RemoveSwitchLabel (fixes CS0152)", IsEnabled(CodeFixIdentifiers.RemoveSwitchLabel)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.RenameDestructorToMatchClassName, "Rename destructor to match class name (fixes CS0574)", IsEnabled(CodeFixIdentifiers.RenameDestructorToMatchClassName)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.RemoveBaseList, "Remove base list (fixes CS0713, CS0714)", IsEnabled(CodeFixIdentifiers.RemoveBaseList)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.AddDefaultValueToParameter, "Add default value to parameter (fixes CS1737)", IsEnabled(CodeFixIdentifiers.AddDefaultValueToParameter)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.AddMissingComma, "Add missing comma (fixes CS1003)", IsEnabled(CodeFixIdentifiers.AddMissingComma)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.RemoveParentheses, "Remove parentheses (fixes CS0210)", IsEnabled(CodeFixIdentifiers.RemoveParentheses)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.RemoveParameter, "Remove parameter (fixes CS0136)", IsEnabled(CodeFixIdentifiers.RemoveParameter)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.RenameTupleElement, "Rename tuple element to match overridden member (fixes CS8139)", IsEnabled(CodeFixIdentifiers.RenameTupleElement)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.ChangeParameterType, "Change parameter type (fixes CS1750)", IsEnabled(CodeFixIdentifiers.ChangeParameterType)));
            codeFixes.Add(new BaseModel(CodeFixIdentifiers.MarkDeclarationAsNonCLSCompliant, "Mark declaration as non-CLS-compliant (fixes CS3000, CS3001, CS3002, CS3003, CS3005, CS3006, CS3007, CS3008, CS3009, CS3016, CS3024, CS3027)", IsEnabled(CodeFixIdentifiers.MarkDeclarationAsNonCLSCompliant)));
        }
    }
}