using System.IO;
using ICSharpCode.Decompiler;

namespace NetSpy
{
    internal class FilteredTextOutput : ITextOutput
    {
        private readonly ITextOutput _wrapped;
        private readonly bool _zeroLineNumber = true;

        public FilteredTextOutput(TextWriter writer)
        {
            _wrapped = new PlainTextOutput(writer);
        }

        public void Indent()
        {
            _wrapped.Indent();
        }

        public void Unindent()
        {
            _wrapped.Unindent();
        }

        public void Write(char ch)
        {
            _wrapped.Write(ch);
        }

        public void Write(string text)
        {
            _wrapped.Write(text);
        }

        public void WriteLine()
        {
            _wrapped.WriteLine();
        }

        private const string Prefix = "IL_";

        public void WriteDefinition(string text, object definition, bool isLocal = true)
        {
            if (_zeroLineNumber && text.StartsWith(Prefix) && text.Length == 7)
                text = Prefix + "0000";
            _wrapped.WriteDefinition(text, definition, isLocal);
        }

        public void WriteReference(string text, object reference, bool isLocal = false)
        {
            _wrapped.WriteReference(text, reference, isLocal);
        }

        public void AddDebugSymbols(MethodDebugSymbols methodDebugSymbols)
        {
            _wrapped.AddDebugSymbols(methodDebugSymbols);
        }

        public void MarkFoldStart(string collapsedText = "...", bool defaultCollapsed = false)
        {
            _wrapped.MarkFoldStart(collapsedText, defaultCollapsed);
        }

        public void MarkFoldEnd()
        {
            _wrapped.MarkFoldEnd();
        }

        public ICSharpCode.NRefactory.TextLocation Location => _wrapped.Location;
    }
}