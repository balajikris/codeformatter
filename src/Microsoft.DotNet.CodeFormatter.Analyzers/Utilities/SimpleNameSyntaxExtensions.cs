using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace Microsoft.DotNet.CodeFormatter.Analyzers.Utilities
{
    internal static class SimpleNameSyntaxExtensions
    {
        public static ExpressionSyntax GetLeftSideOfDot(this SimpleNameSyntax name)
        {
            Contract.Requires(name.IsMemberAccessExpressionName() || name.IsRightSideOfQualifiedName() || name.Parent.IsKind(SyntaxKind.NameMemberCref));
            if (name.IsMemberAccessExpressionName())
            {
                var conditionalAccess = name.GetParentConditionalAccessExpression();
                if (conditionalAccess != null)
                {
                    return conditionalAccess.Expression;
                }
                else
                {
                    return ((MemberAccessExpressionSyntax)name.Parent).Expression;
                }
            }
            else if (name.IsRightSideOfQualifiedName())
            {
                return ((QualifiedNameSyntax)name.Parent).Left;
            }
            else
            {
                return ((QualifiedCrefSyntax)name.Parent.Parent).Container;
            }
        }
    }
}
