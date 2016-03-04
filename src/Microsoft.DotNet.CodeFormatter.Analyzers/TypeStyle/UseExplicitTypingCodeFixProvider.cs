// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System;
using System.Collections.Immutable;
using System.Composition;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp.Extensions;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Editing;
using Microsoft.CodeAnalysis.Shared.Extensions;
using Microsoft.CodeAnalysis.Simplification;
using Microsoft.DotNet.CodeFormatter.Analyzers;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.CodeFixes.UseImplicitTyping
{
    [ExportCodeFixProvider(LanguageNames.CSharp)]
    internal class UseExplicitTypingCodeFixProvider : CodeFixProvider
    {
        public override ImmutableArray<string> FixableDiagnosticIds =>
            ImmutableArray.Create(AnalyzerIds.ProvideExplicitVariableType);

        public override FixAllProvider GetFixAllProvider() => WellKnownFixAllProviders.BatchFixer;

        public override async Task RegisterCodeFixesAsync(CodeFixContext context)
        {
            var document = context.Document;
            var span = context.Span;
            var root = await document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false);
            var node = root.FindNode(span, getInnermostNodeForTie: true);

            var codeAction = CodeAction.Create(
                Resources.UseExplicitType,
                c => HandleDeclarationAsync(document, root, node, context.CancellationToken));

            context.RegisterCodeFix(codeAction, context.Diagnostics.First());
        }

        private async Task<Document> HandleDeclarationAsync(Document document, SyntaxNode root, SyntaxNode node, CancellationToken cancellationToken)
        {
            var semanticModel = await document.GetSemanticModelAsync(cancellationToken).ConfigureAwait(false);
            var declarationContext = node.Parent;

            TypeSyntax typeSyntax = null;
            if (declarationContext is VariableDeclarationSyntax)
            {
                typeSyntax = ((VariableDeclarationSyntax)declarationContext).Type;
            }
            else if (declarationContext is ForEachStatementSyntax)
            {
                typeSyntax = ((ForEachStatementSyntax)declarationContext).Type;
            }
            else
            {
                Debug.Fail($"unhandled kind {declarationContext.Kind().ToString()}");
                return document;
            }

            var typeSymbol = semanticModel.GetTypeInfo(typeSyntax).ConvertedType;
            var documentEditor = await DocumentEditor.CreateAsync(document, cancellationToken);

            var typeName = documentEditor.Generator.TypeExpression(typeSymbol)
                                     .WithAdditionalAnnotations(Simplifier.Annotation)
                                     .WithLeadingTrivia(node.GetLeadingTrivia())
                                     .WithTrailingTrivia(node.GetTrailingTrivia());

            Debug.Assert(!typeName.ContainsDiagnostics, "Explicit type replacement likely introduced an error in code");

            var newRoot = root.ReplaceNode(node, typeName);
            return document.WithSyntaxRoot(newRoot);
        }
    }
}