using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Microsoft.DotNet.CodeFormatter.Analyzers.Utilities
{
    internal static class TypeSyntaxExtensions
    {
        /// <summary>
        /// Determines whether the specified TypeSyntax is actually 'var'.
        /// </summary>
        public static bool IsTypeInferred(this TypeSyntax typeSyntax, SemanticModel semanticModel)
        {
            if (!typeSyntax.IsVar)
            {
                return false;
            }

            if (semanticModel.GetAliasInfo(typeSyntax) != null)
            {
                return false;
            }

            var type = semanticModel.GetTypeInfo(typeSyntax).Type;
            if (type == null)
            {
                return false;
            }

            if (type.Name == "var")
            {
                return false;
            }

            return true;
        }
    }
}
