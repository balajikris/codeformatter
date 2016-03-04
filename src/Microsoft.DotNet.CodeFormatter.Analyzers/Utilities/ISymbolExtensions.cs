using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;

namespace Microsoft.DotNet.CodeFormatter.Analyzers.Utilities
{
    internal static class ISymbolExtensions
    {
        public static ImmutableArray<ITypeSymbol> GetTypeArguments(this ISymbol symbol)
        {
            var methodSymbol = symbol as IMethodSymbol;
            if (methodSymbol != null)
            {
                return methodSymbol.TypeArguments;
            }

            var typeSymbol = symbol as INamedTypeSymbol;
            if (typeSymbol != null)
            {
                return typeSymbol.TypeArguments;
            }

            return ImmutableArray.Create<ITypeSymbol>();
        }
    }
}
