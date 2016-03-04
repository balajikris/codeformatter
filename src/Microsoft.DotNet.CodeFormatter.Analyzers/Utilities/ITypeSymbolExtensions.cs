using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;

namespace Microsoft.DotNet.CodeFormatter.Analyzers.Utilities
{
    internal static class ITypeSymbolExtensions
    {
        public static bool IsSpecialType(this ITypeSymbol symbol)
        {
            if (symbol != null)
            {
                switch (symbol.SpecialType)
                {
                    case SpecialType.System_Object:
                    case SpecialType.System_Void:
                    case SpecialType.System_Boolean:
                    case SpecialType.System_SByte:
                    case SpecialType.System_Byte:
                    case SpecialType.System_Decimal:
                    case SpecialType.System_Single:
                    case SpecialType.System_Double:
                    case SpecialType.System_Int16:
                    case SpecialType.System_Int32:
                    case SpecialType.System_Int64:
                    case SpecialType.System_Char:
                    case SpecialType.System_String:
                    case SpecialType.System_UInt16:
                    case SpecialType.System_UInt32:
                    case SpecialType.System_UInt64:
                        return true;
                }
            }

            return false;
        }

        public static bool IsNullable(this ITypeSymbol symbol)
        {
            return symbol?.OriginalDefinition.SpecialType == SpecialType.System_Nullable_T;
        }

        public static bool ContainsAnonymousType(this ITypeSymbol symbol)
        {
            var arrayTypeSymbol = symbol as IArrayTypeSymbol;
            if (arrayTypeSymbol != null)
            {
                return ContainsAnonymousType(arrayTypeSymbol.ElementType);
            }

            var pointerTypeSymbol = symbol as IPointerTypeSymbol;
            if (pointerTypeSymbol != null)
            {
                return ContainsAnonymousType(pointerTypeSymbol.PointedAtType);
            }

            var namedTypeSymbol = symbol as INamedTypeSymbol;
            if (namedTypeSymbol != null)
            {
                return ContainsAnonymousType(namedTypeSymbol);
            }

            return false;
        }

        private static bool ContainsAnonymousType(INamedTypeSymbol type)
        {
            if (type.IsAnonymousType)
            {
                return true;
            }

            foreach (var typeArg in type.GetAllTypeArguments())
            {
                if (ContainsAnonymousType(typeArg))
                {
                    return true;
                }
            }

            return false;
        }

        public static IEnumerable<ITypeSymbol> GetAllTypeArguments(this INamedTypeSymbol symbol)
        {
            var stack = GetContainmentStack(symbol);
            return stack.SelectMany(n => n.TypeArguments);
        }

        private static Stack<INamedTypeSymbol> GetContainmentStack(INamedTypeSymbol symbol)
        {
            var stack = new Stack<INamedTypeSymbol>();
            for (var current = symbol; current != null; current = current.ContainingType)
            {
                stack.Push(current);
            }

            return stack;
        }

        public static bool IsErrorType(this ITypeSymbol symbol)
        {
            return symbol?.TypeKind == TypeKind.Error;
        }

    }
}
