using System.Collections.Generic;
using CommandLine;

namespace NetSpy
{
    [Verb("wring", HelpText = "Extract the juicy IL from assemblies.")]
    internal class WringOptions
    {
        [Value(0, MetaName = "inputs", HelpText = "Files to wring")]
        public IEnumerable<string> Files { get; set; }
    }
}