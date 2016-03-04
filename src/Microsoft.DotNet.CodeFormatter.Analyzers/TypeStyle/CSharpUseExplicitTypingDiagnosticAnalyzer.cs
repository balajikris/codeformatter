// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System.Composition;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.CodeStyle.TypeStyle;
using Microsoft.CodeAnalysis.CSharp.Extensions;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Options;
using Microsoft.CodeAnalysis.Shared.Extensions;
using Microsoft.CodeAnalysis.Text;
using Microsoft.DotNet.CodeFormatter.Analyzers;
using Microsoft.DotNet.CodeFormatter.Analyzers.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Diagnostics.TypingStyles
{
    [Export(typeof(DiagnosticAnalyzer))]
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    internal sealed class CSharpUseExplicitTypingDiagnosticAnalyzer : CSharpTypingStyleDiagnosticAnalyzerBase
    {
        private static readonly LocalizableString s_Title =
            new LocalizableResourceString(nameof(Resources.UseExplicitTypeDiagnosticTitle), Resources.ResourceManager, typeof(Resources));

        private static readonly LocalizableString s_Message =
            new LocalizableResourceString(nameof(Resources.UseExplicitType), Resources.ResourceManager, typeof(Resources));

        public CSharpUseExplicitTypingDiagnosticAnalyzer()
            : base(diagnosticId: AnalyzerIds.ProvideExplicitVariableType,
                   title: s_Title,
                   message: s_Message)
        {
        }

        public const string AnalyzerName = AnalyzerIds.ProvideExplicitVariableType + "." + nameof(AnalyzerIds.ProvideExplicitVariableType);

        protected override bool IsStylePreferred(SemanticModel semanticModel, OptionSet optionSet, State state, CancellationToken cancellationToken)
        {
            var stylePreferences = state.TypeStyle;
            var shouldNotify = state.ShouldNotify();

            // If notification preference is None, don't offer the suggestion.
            if (!shouldNotify)
            {
                return false;
            }

            if (state.IsInIntrinsicTypeContext)
            {
                return !stylePreferences.HasFlag(TypeStyle.ImplicitTypeForIntrinsicTypes);
            }
            else if (state.IsTypingApparentInContext)
            {
                return !stylePreferences.HasFlag(TypeStyle.ImplicitTypeWhereApparent);
            }
            else
            {
                return !stylePreferences.HasFlag(TypeStyle.ImplicitTypeWherePossible);
            }
        }

        protected override bool TryAnalyzeVariableDeclaration(TypeSyntax typeName, SemanticModel semanticModel, OptionSet optionSet, CancellationToken cancellationToken, out TextSpan issueSpan)
        {
            issueSpan = default(TextSpan);

            // If it is currently not var, explicit typing exists, return. 
            // this also takes care of cases where var is mapped to a named type via an alias or a class declaration.
            if (!typeName.IsTypeInferred(semanticModel))
            {
                return false;
            }

            if (typeName.Parent.IsKind(SyntaxKind.VariableDeclaration) &&
                typeName.Parent.Parent.IsKind(SyntaxKind.LocalDeclarationStatement, SyntaxKind.ForStatement, SyntaxKind.UsingStatement))
            {
                // check assignment for variable declarations.
                var variable = ((VariableDeclarationSyntax)typeName.Parent).Variables.First();
                if (!AssignmentSupportsStylePreference(variable.Identifier, typeName, variable.Initializer, semanticModel, optionSet, cancellationToken))
                {
                    return false;
                }
            }

            issueSpan = typeName.Span;
            return true;
        }

        /// <summary>
        /// Analyzes the assignment expression and rejects a given declaration if it is unsuitable for explicit typing.
        /// </summary>
        /// <returns>
        /// false, if explicit typing cannot be used.
        /// true, otherwise.
        /// </returns>
        protected override bool AssignmentSupportsStylePreference(SyntaxToken identifier, TypeSyntax typeName, EqualsValueClauseSyntax initializer, SemanticModel semanticModel, OptionSet optionSet, CancellationToken cancellationToken)
        {
            // is or contains an anonymous type
            // cases :
            //        var anon = new { Num = 1 };
            //        var enumerableOfAnons = from prod in products select new { prod.Color, prod.Price };
            var declaredType = semanticModel.GetTypeInfo(typeName, cancellationToken).Type;
            if (declaredType.ContainsAnonymousType())
            {
                return false;
            }

            // cannot find type if initializer resolves to an ErrorTypeSymbol
            var initializerTypeInfo = semanticModel.GetTypeInfo(initializer.Value, cancellationToken);
            return !initializerTypeInfo.Type.IsErrorType();
        }
    }
}