using Antlr4.Runtime;
using JetBrains.ReSharper.Plugins.Spring.Generated;
using JetBrains.ReSharper.Psi.Parsing;
using JetBrains.Text;

namespace JetBrains.ReSharper.Plugins.Spring
{
    public class SpringLexer : ILexer
    {
        private readonly Lexer _lexer;
        public IBuffer Buffer { get; }

        private SpringToken _currentToken;

        public SpringLexer(IBuffer buffer)
        {
            Buffer = buffer;
            var inputStream = new AntlrInputStream(Buffer.GetText());
            _lexer = new PascalLexer(inputStream);
        }
        
        public int TokenStart => _currentToken.Token.StartIndex;

        public int TokenEnd => _currentToken.Token.StopIndex + 1;

        public object CurrentPosition { get; set; }

        public TokenNodeType TokenType => _currentToken?.GetTokenType();

        public void Start()
        {
            Advance();
        }

        public void Advance()
        {
            if (_lexer.HitEOF)
            {
                _currentToken = null;
            }
            else
            {
                var nextToken = _lexer.NextToken();
                var nextTokenTypeName = _lexer.Vocabulary.GetLiteralName(nextToken.Type) ??
                                        _lexer.Vocabulary.GetSymbolicName(nextToken.Type);
                CurrentPosition = nextToken.StartIndex;
                _currentToken =
                    new SpringToken(new SpringTokenType(nextTokenTypeName, nextToken.Type), nextToken.Text)
                    {
                        Token = nextToken
                    };
            }
        }
    }
}