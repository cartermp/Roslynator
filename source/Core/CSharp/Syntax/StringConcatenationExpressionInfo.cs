﻿// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using Roslynator.Text;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Roslynator.CSharp.Syntax
{
    //XTODO: pub
    internal readonly struct StringConcatenationExpressionInfo : IEquatable<StringConcatenationExpressionInfo>
    {
        private StringConcatenationExpressionInfo(
            BinaryExpressionSyntax binaryExpression,
            TextSpan? span = null)
        {
            BinaryExpression = binaryExpression;
            Span = span;
        }

        private static StringConcatenationExpressionInfo Default { get; } = new StringConcatenationExpressionInfo();

        public BinaryExpressionSyntax BinaryExpression { get; }

        public TextSpan? Span { get; }

        public bool Success
        {
            get { return BinaryExpression != null; }
        }

        internal StringConcatenationAnalysis Analyze()
        {
            return StringConcatenationAnalysis.Create(this);
        }

        internal static StringConcatenationExpressionInfo Create(
            BinaryExpressionSyntax binaryExpression,
            SemanticModel semanticModel,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (binaryExpression?.Kind() != SyntaxKind.AddExpression)
                return Default;

            if (semanticModel == null)
                throw new ArgumentNullException(nameof(semanticModel));

            if (!IsStringConcatenation(binaryExpression, semanticModel, cancellationToken))
                return Default;

            return new StringConcatenationExpressionInfo(binaryExpression);
        }

        internal static StringConcatenationExpressionInfo Create(
            BinaryExpressionSelection binaryExpressionSelection,
            SemanticModel semanticModel,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            BinaryExpressionSyntax binaryExpression = binaryExpressionSelection.BinaryExpression;

            if (binaryExpression?.Kind() != SyntaxKind.AddExpression)
                return Default;

            if (semanticModel == null)
                throw new ArgumentNullException(nameof(semanticModel));

            foreach (ExpressionSyntax expression in binaryExpressionSelection.Expressions)
            {
                if (!CSharpUtility.IsStringExpression(expression, semanticModel, cancellationToken))
                    return Default;
            }

            return new StringConcatenationExpressionInfo(binaryExpression, binaryExpressionSelection.Span);
        }

        private static bool IsStringConcatenation(
            BinaryExpressionSyntax binaryExpression,
            SemanticModel semanticModel,
            CancellationToken cancellationToken)
        {
            while (true)
            {
                if (binaryExpression.Right != null)
                {
                    ExpressionSyntax left = binaryExpression.Left;

                    if (left != null
                        && CSharpUtility.IsStringConcatenation(binaryExpression, semanticModel, cancellationToken))
                    {
                        if (left.Kind() == SyntaxKind.AddExpression)
                        {
                            binaryExpression = (BinaryExpressionSyntax)left;
                        }
                        else
                        {
                            return true;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        internal IEnumerable<ExpressionSyntax> Expressions(bool leftToRight = false)
        {
            ThrowInvalidOperationIfNotInitialized();

            return BinaryExpressionInfo.Create(BinaryExpression).Expressions(leftToRight);
        }

        public InterpolatedStringExpressionSyntax ToInterpolatedString()
        {
            ThrowInvalidOperationIfNotInitialized();

            StringBuilder sb = StringBuilderCache.GetInstance();

            sb.Append('$');

            bool containsRegular = Analyze().ContainsRegularExpression;

            if (!containsRegular)
                sb.Append('@');

            sb.Append('"');

            foreach (ExpressionSyntax expression in Expressions(leftToRight: true))
            {
                SyntaxKind kind = expression.Kind();

                StringLiteralExpressionInfo stringLiteral = SyntaxInfo.StringLiteralExpressionInfo(expression);

                if (stringLiteral.Success)
                {
                    int startIndex = sb.Length;

                    if (containsRegular
                        && stringLiteral.IsVerbatim)
                    {
                        sb.Append(stringLiteral.ValueText);
                        sb.Replace(@"\", @"\\", startIndex);
                        sb.Replace("\"", @"\" + "\"", startIndex);
                        sb.Replace("{", "{{", startIndex);
                        sb.Replace("}", "}}", startIndex);
                        sb.Replace("\n", @"\n", startIndex);
                        sb.Replace("\r", @"\r", startIndex);
                    }
                    else
                    {
                        sb.Append(stringLiteral.InnerText);
                        sb.Replace("{", "{{", startIndex);
                        sb.Replace("}", "}}", startIndex);
                    }
                }
                else if (kind == SyntaxKind.InterpolatedStringExpression)
                {
                    var interpolatedString = (InterpolatedStringExpressionSyntax)expression;

                    bool isVerbatimInterpolatedString = interpolatedString.IsVerbatim();

                    foreach (InterpolatedStringContentSyntax content in interpolatedString.Contents)
                    {
                        Debug.Assert(content.IsKind(SyntaxKind.Interpolation, SyntaxKind.InterpolatedStringText), content.Kind().ToString());

                        switch (content.Kind())
                        {
                            case SyntaxKind.InterpolatedStringText:
                                {
                                    var text = (InterpolatedStringTextSyntax)content;

                                    if (containsRegular
                                        && isVerbatimInterpolatedString)
                                    {
                                        int startIndex = sb.Length;
                                        sb.Append(text.TextToken.ValueText);
                                        sb.Replace(@"\", @"\\", startIndex);
                                        sb.Replace("\"", @"\" + "\"", startIndex);
                                        sb.Replace("\n", @"\n", startIndex);
                                        sb.Replace("\r", @"\r", startIndex);
                                    }
                                    else
                                    {
                                        sb.Append(content.ToString());
                                    }

                                    break;
                                }
                            case SyntaxKind.Interpolation:
                                {
                                    sb.Append(content.ToString());
                                    break;
                                }
                        }
                    }
                }
                else
                {
                    sb.Append('{');
                    sb.Append(expression.ToString());
                    sb.Append('}');
                }
            }

            sb.Append("\"");

            return (InterpolatedStringExpressionSyntax)ParseExpression(StringBuilderCache.GetStringAndFree(sb));
        }

        public LiteralExpressionSyntax ToStringLiteral()
        {
            ThrowInvalidOperationIfNotInitialized();

            StringConcatenationAnalysis analysis = Analyze();

            if (analysis.ContainsNonStringLiteral)
                throw new InvalidOperationException();

            StringBuilder sb = StringBuilderCache.GetInstance();

            if (!analysis.ContainsRegularExpression)
                sb.Append('@');

            sb.Append('"');

            foreach (ExpressionSyntax expression in Expressions(leftToRight: true))
            {
                StringLiteralExpressionInfo literal = SyntaxInfo.StringLiteralExpressionInfo(expression);

                if (literal.Success)
                {
                    if (analysis.ContainsRegularExpression && literal.IsVerbatim)
                    {
                        int startIndex = sb.Length;
                        sb.Append(literal.ValueText);
                        sb.Replace(@"\", @"\\", startIndex);
                        sb.Replace("\"", @"\" + "\"", startIndex);
                        sb.Replace("\n", @"\n", startIndex);
                        sb.Replace("\r", @"\r", startIndex);
                    }
                    else
                    {
                        sb.Append(literal.InnerText);
                    }
                }
            }

            sb.Append('"');

            return (LiteralExpressionSyntax)ParseExpression(StringBuilderCache.GetStringAndFree(sb));
        }

        public LiteralExpressionSyntax ToMultilineStringLiteral()
        {
            ThrowInvalidOperationIfNotInitialized();

            if (Analyze().ContainsNonStringLiteral)
                throw new InvalidOperationException();

            StringBuilder sb = StringBuilderCache.GetInstance();

            sb.Append('@');
            sb.Append('"');

            ExpressionSyntax[] expressions = Expressions(leftToRight: true).ToArray();

            for (int i = 0; i < expressions.Length; i++)
            {
                if (expressions[i].IsKind(SyntaxKind.StringLiteralExpression))
                {
                    var literal = (LiteralExpressionSyntax)expressions[i];

                    int length = sb.Length;

                    sb.Append(literal.Token.ValueText);

                    sb.Replace("\"", "\"\"", length);

                    if (sb.Length > length
                        && sb[sb.Length - 1] == '\n')
                    {
                        sb.Remove(sb.Length - 1, 1);

                        if (sb.Length - length > 1
                            && sb[sb.Length - 1] == '\r')
                        {
                            sb.Remove(sb.Length - 1, 1);
                        }

                        sb.AppendLine();
                    }
                    else if (i < expressions.Length - 1)
                    {
                        TextSpan span = TextSpan.FromBounds(expressions[i].Span.End, expressions[i + 1].SpanStart);

                        if (BinaryExpression.SyntaxTree.IsMultiLineSpan(span))
                            sb.AppendLine();
                    }
                }
            }

            sb.Append('"');

            return (LiteralExpressionSyntax)ParseExpression(StringBuilderCache.GetStringAndFree(sb));
        }

        public override string ToString()
        {
            return (Span != null)
                ? BinaryExpression.ToString(Span.Value)
                : BinaryExpression.ToString();
        }

        private void ThrowInvalidOperationIfNotInitialized()
        {
            if (BinaryExpression == null)
                throw new InvalidOperationException($"{nameof(StringConcatenationExpressionInfo)} is not initalized.");
        }

        public override bool Equals(object obj)
        {
            return obj is StringConcatenationExpressionInfo other && Equals(other);
        }

        public bool Equals(StringConcatenationExpressionInfo other)
        {
            return EqualityComparer<BinaryExpressionSyntax>.Default.Equals(BinaryExpression, other.BinaryExpression)
                && EqualityComparer<TextSpan?>.Default.Equals(Span, other.Span);
        }

        public override int GetHashCode()
        {
            return Hash.Combine(Span.GetHashCode(), Hash.Create(BinaryExpression));
        }

        public static bool operator ==(StringConcatenationExpressionInfo info1, StringConcatenationExpressionInfo info2)
        {
            return info1.Equals(info2);
        }

        public static bool operator !=(StringConcatenationExpressionInfo info1, StringConcatenationExpressionInfo info2)
        {
            return !(info1 == info2);
        }
    }
}
