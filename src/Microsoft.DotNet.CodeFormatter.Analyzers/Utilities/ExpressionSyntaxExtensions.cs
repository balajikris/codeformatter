using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Microsoft.DotNet.CodeFormatter.Analyzers.Utilities
{
    internal static class ExpressionSyntaxExtensions
    {
        public static ExpressionSyntax WalkDownParentheses(this ExpressionSyntax expression)
        {
            while (expression.IsKind(SyntaxKind.ParenthesizedExpression))
            {
                expression = ((ParenthesizedExpressionSyntax)expression).Expression;
            }

            return expression;
        }

        public static bool IsAnyLiteralExpression(this ExpressionSyntax expression)
        {
            return
                expression.IsKind(SyntaxKind.CharacterLiteralExpression) ||
                expression.IsKind(SyntaxKind.FalseLiteralExpression) ||
                expression.IsKind(SyntaxKind.NullLiteralExpression) ||
                expression.IsKind(SyntaxKind.NumericLiteralExpression) ||
                expression.IsKind(SyntaxKind.StringLiteralExpression) ||
                expression.IsKind(SyntaxKind.TrueLiteralExpression);
        }

        public static bool IsRightSideOfDot(this ExpressionSyntax name)
        {
            return IsMemberAccessExpressionName(name) || IsRightSideOfQualifiedName(name) || IsQualifiedCrefName(name);
        }

        public static bool IsMemberAccessExpressionName(this ExpressionSyntax expression)
        {
            return (expression.Parent.IsKind(SyntaxKind.SimpleMemberAccessExpression) && ((MemberAccessExpressionSyntax)expression.Parent).Name == expression) ||
                   (IsMemberBindingExpressionName(expression));
        }

        public static bool IsQualifiedCrefName(this ExpressionSyntax expression)
        {
            return expression.Parent.IsKind(SyntaxKind.NameMemberCref) && expression.Parent.Parent.IsKind(SyntaxKind.QualifiedCref);
        }

        public static bool IsRightSideOfQualifiedName(this ExpressionSyntax expression)
        {
            return expression.Parent.IsKind(SyntaxKind.QualifiedName) && ((QualifiedNameSyntax)expression.Parent).Right == expression;
        }

        private static bool IsMemberBindingExpressionName(this ExpressionSyntax expression)
        {
            return expression.Parent.IsKind(SyntaxKind.MemberBindingExpression) &&
                ((MemberBindingExpressionSyntax)expression.Parent).Name == expression;
        }

        public static SimpleNameSyntax GetRightmostName(this ExpressionSyntax node)
        {
            var memberAccess = node as MemberAccessExpressionSyntax;
            if (memberAccess != null && memberAccess.Name != null)
            {
                return memberAccess.Name;
            }

            var qualified = node as QualifiedNameSyntax;
            if (qualified != null && qualified.Right != null)
            {
                return qualified.Right;
            }

            var simple = node as SimpleNameSyntax;
            if (simple != null)
            {
                return simple;
            }

            var conditional = node as ConditionalAccessExpressionSyntax;
            if (conditional != null)
            {
                return conditional.WhenNotNull.GetRightmostName();
            }

            var memberBinding = node as MemberBindingExpressionSyntax;
            if (memberBinding != null)
            {
                return memberBinding.Name;
            }

            return null;
        }
    }
}
