using System;
using System.IO;
using System.Linq;
using System.Threading;
using ICSharpCode.Decompiler;
using ICSharpCode.Decompiler.Disassembler;
using Mono.Cecil;

namespace NetSpy
{
    internal static class Wringer
    {
        public static int Disassemble(ITextOutput output, WringOptions opts)
        {
            var files = opts.Files.Select(Path.GetFullPath).ToArray();
            var dis = new ReflectionDisassembler(output, true, CancellationToken.None);
            var resolver = new DefaultAssemblyResolver();
            var dirs = files.Select(Path.GetDirectoryName).Distinct().ToArray();
            Array.ForEach(dirs, resolver.AddSearchDirectory);

            var rparm = new ReaderParameters
            {
                AssemblyResolver = resolver,
                ReadSymbols = true
            };
            foreach (var file in files)
            {
                var ass = AssemblyDefinition.ReadAssembly(file, rparm);
                output.WriteLine();
                output.WriteLine($"{ass.FullName} ({file})");
                output.WriteLine();
                dis.WriteAssemblyHeader(ass);
                output.WriteLine();
                foreach (var module in ass.Modules)
                {
                    dis.WriteModuleHeader(module);
                    output.WriteLine();
                    dis.WriteAssemblyReferences(module);
                    output.WriteLine();
                    dis.WriteModuleContents(module);
                }
            }
            return 0;
        }
    }
}