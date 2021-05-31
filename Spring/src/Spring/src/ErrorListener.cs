using System.IO;
using Antlr4.Runtime;
using JetBrains.ReSharper.Psi.TreeBuilder;

namespace JetBrains.ReSharper.Plugins.Spring
{
    public class ErrorListener : IAntlrErrorListener<IToken>
    {
        private readonly PsiBuilder _builder;

        public ErrorListener(PsiBuilder builder)
        {
            _builder = builder;
        }

        public void SyntaxError(TextWriter output, IRecognizer recognizer, IToken offendingSymbol, int line,
            int charPositionInLine,
            string msg, RecognitionException e)
        {
            _builder.Error($"{line} : {charPositionInLine} -- {msg}");
        }
    }
}