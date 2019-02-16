﻿// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using DotMarkdown;
using Microsoft.CodeAnalysis;
using Roslynator.Documentation;
using static Roslynator.Logger;

namespace Roslynator.CommandLine
{
    internal class ListSymbolsCommand : MSBuildWorkspaceCommand
    {
        public ListSymbolsCommand(
            ListSymbolsCommandLineOptions options,
            SymbolFilterOptions symbolFilterOptions,
            string rootDirectoryUrl,
            in ProjectFilter projectFilter) : base(projectFilter)
        {
            Options = options;
            SymbolFilterOptions = symbolFilterOptions;
            RootDirectoryUrl = rootDirectoryUrl;
        }

        public ListSymbolsCommandLineOptions Options { get; }

        public SymbolFilterOptions SymbolFilterOptions { get; }

        public string RootDirectoryUrl { get; }

        public override async Task<CommandResult> ExecuteAsync(ProjectOrSolution projectOrSolution, CancellationToken cancellationToken = default)
        {
            AssemblyResolver.Register();

            var format = new DefinitionListFormat(
                omitContainingNamespace: Options.OmitContainingNamespace,
                indentChars: Options.IndentChars,
                nestNamespaces: Options.NestNamespaces,
                emptyLineBetweenMembers: Options.EmptyLineBetweenMembers,
                formatAttributes: Options.FormatAttributes,
                formatParameters: Options.FormatParameters,
                formatBaseList: Options.FormatBaseList,
                formatConstraints: Options.FormatConstraints,
                includeAttributeArguments: !Options.NoAttributeArguments,
                omitIEnumerable: true,
                includeAssemblyAttributes: Options.IncludeAssemblyAttributes);

            ImmutableArray<Compilation> compilations = await GetCompilationsAsync(projectOrSolution, cancellationToken);

            string text = null;

            SymbolDefinitionComparer comparer = SymbolDefinitionComparer.SystemNamespaceFirstInstance;

            IEnumerable<IAssemblySymbol> assemblies = compilations.Select(f => f.Assembly);
#if DEBUG
            SymbolDefinitionWriter textWriter = new SymbolDefinitionTextWriter(
                ConsoleOut,
                filter: SymbolFilterOptions,
                format: format,
                comparer: comparer);

            textWriter.WriteDocument(assemblies);

            WriteLine();

            using (XmlWriter xmlWriter = XmlWriter.Create(ConsoleOut, new XmlWriterSettings() { Indent = true, IndentChars = Options.IndentChars }))
            {
                SymbolDefinitionWriter writer = new SymbolDefinitionXmlWriter(xmlWriter, SymbolFilterOptions, format, comparer);

                writer.WriteDocument(assemblies);
            }

            WriteLine();

            using (MarkdownWriter markdownWriter = MarkdownWriter.Create(ConsoleOut))
            {
                SymbolDefinitionWriter writer = new SymbolDefinitionMarkdownWriter(markdownWriter, SymbolFilterOptions, format, comparer, RootDirectoryUrl);

                writer.WriteDocument(assemblies);
            }

            WriteLine();
#endif
            using (var stringWriter = new StringWriter())
            {
                SymbolDefinitionWriter writer = new SymbolDefinitionTextWriter(
                    stringWriter,
                    filter: SymbolFilterOptions,
                    format: format,
                    comparer: comparer);

                writer.WriteDocument(assemblies);

                text = stringWriter.ToString();
            }

            WriteLine(Verbosity.Minimal);
            WriteLine(text, Verbosity.Minimal);

            foreach (string path in Options.Output)
            {
                WriteLine($"Save '{path}'", ConsoleColor.DarkGray, Verbosity.Diagnostic);

                string extension = Path.GetExtension(path);

                if (string.Equals(extension, ".xml", StringComparison.OrdinalIgnoreCase))
                {
                    var xmlWriterSettings = new XmlWriterSettings() { Indent = true, IndentChars = Options.IndentChars };

                    using (XmlWriter xmlWriter = XmlWriter.Create(path, xmlWriterSettings))
                    {
                        SymbolDefinitionWriter writer = new SymbolDefinitionXmlWriter(xmlWriter, SymbolFilterOptions, format, comparer);

                        writer.WriteDocument(assemblies);
                    }
                }
                else if (string.Equals(extension, ".md", StringComparison.OrdinalIgnoreCase))
                {
                    var markdownWriterSettings = new MarkdownWriterSettings();

                    using (var fileStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Read))
                    using (var streamWriter = new StreamWriter(fileStream, Encodings.UTF8NoBom))
                    using (MarkdownWriter markdownWriter = MarkdownWriter.Create(streamWriter, markdownWriterSettings))
                    {
                        SymbolDefinitionWriter writer = new SymbolDefinitionMarkdownWriter(markdownWriter, SymbolFilterOptions, format, comparer, RootDirectoryUrl);

                        writer.WriteDocument(assemblies);
                    }
                }
                else
                {
                    File.WriteAllText(path, text, Encoding.UTF8);
                }
            }

            if (ShouldWrite(Verbosity.Normal))
                WriteSummary(assemblies, SymbolFilterOptions, Verbosity.Normal);

            return CommandResult.Success;
        }

        private static void WriteSummary(IEnumerable<IAssemblySymbol> assemblies, SymbolFilterOptions filter, Verbosity verbosity)
        {
            WriteLine(verbosity);

            WriteLine($"{assemblies.Count()} assemblies", verbosity);

            INamedTypeSymbol[] types = assemblies
                .SelectMany(a => a.GetTypes(t => filter.IsVisibleType(t)))
                .ToArray();

            IEnumerable<INamespaceSymbol> namespaces = types
                .Select(f => f.ContainingNamespace)
                .Distinct(MetadataNameEqualityComparer<INamespaceSymbol>.Instance)
                .Where(f => filter.IsVisibleNamespace(f));

            WriteLine($"  {namespaces.Count()} namespaces", verbosity);

            WriteLine($"    {types.Length} types", verbosity);

            foreach (IGrouping<SymbolGroup, INamedTypeSymbol> grouping in types.GroupBy(f => f.GetSymbolGroup()))
            {
                WriteLine($"      {grouping.Count()} {grouping.Key.GetPluralText()}", verbosity);
            }

            WriteLine(verbosity);

            ISymbol[] members = types
                .Where(f => f.TypeKind.Is(TypeKind.Class, TypeKind.Struct, TypeKind.Interface))
                .SelectMany(t => t.GetMembers().Where(m => filter.IsVisibleMember(m)))
                .ToArray();

            WriteLine($"    {members.Length} members", verbosity);

            foreach (IGrouping<SymbolGroup, ISymbol> grouping in members.GroupBy(f => f.GetSymbolGroup()))
            {
                WriteLine($"      {grouping.Count()} {grouping.Key.GetPluralText()}", verbosity);
            }

            WriteLine(verbosity);
        }
    }
}