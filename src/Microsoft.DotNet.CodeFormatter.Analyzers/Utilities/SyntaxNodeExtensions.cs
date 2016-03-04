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
    internal static class SyntaxNodeExtensions
    {
        public static bool IsKind(this SyntaxNode node, SyntaxKind kind1, SyntaxKind kind2, SyntaxKind kind3)
        {
            if (node == null)
            {
                return false;
            }

            var csharpKind = node.Kind();
            return csharpKind == kind1 || csharpKind == kind2 || csharpKind == kind3;
        }

        public static ConditionalAccessExpressionSyntax GetParentConditionalAccessExpression(this SyntaxNode node)
        {
            var parent = node.Parent;
            while (parent != null)
            {
                // Because the syntax for conditional access is right associate, we cannot
                // simply take the first ancestor ConditionalAccessExpression. Instead, we 
                // must walk upward until we find the ConditionalAccessExpression whose
                // OperatorToken appears left of the MemberBinding.
                if (parent.IsKind(SyntaxKind.ConditionalAccessExpression) &&
                    ((ConditionalAccessExpressionSyntax)parent).OperatorToken.Span.End <= node.SpanStart)
                {
                    return (ConditionalAccessExpressionSyntax)parent;
                }

                parent = parent.Parent;
            }

            return null;
        }
    }
}
