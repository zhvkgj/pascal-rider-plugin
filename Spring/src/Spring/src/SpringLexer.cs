using Antlr4.Runtime;
using JetBrains.ReSharper.Plugins.Spring.Generated;
using JetBrains.ReSharper.Psi.Parsing;
using JetBrains.Text;

namespace JetBrains.ReSharper.Plugins.Spring
{
    public class SpringLexer : ILexer
    {
        public Lexer Lexer { get; }
        private SpringToken _currentToken;

        public SpringLexer(IBuffer buffer)
        {
            Buffer = buffer;
            var inputStream = new AntlrInputStream(buffer.GetText());
            Lexer = new PascalLexer(inputStream);
        }

        public void Start()
        {
            Lexer.Reset();
            Lexer.SetInputStream(new AntlrInputStream(Buffer.GetText()));
            Advance();
        }

        public void Advance()
        {
            if (Lexer.HitEOF)
            {
                _currentToken = null;
            }
            else
            {
                var antlrToken = Lexer.NextToken();
                var curTypename = Lexer.Vocabulary.GetLiteralName(antlrToken.Type) ??
                              Lexer.Vocabulary.GetSymbolicName(antlrToken.Type);
                var curType = new SpringTokenType(curTypename, antlrToken.Type);
                _currentToken = new SpringToken(curType, antlrToken.Text);
            }
        }

        public object CurrentPosition { get; set; }

        public TokenNodeType TokenType => _currentToken?.GetTokenType();

        public int TokenStart => _currentToken.Token.StartIndex;

        public int TokenEnd => _currentToken.Token.StopIndex + 1;
        public IBuffer Buffer { get; }
    }
}