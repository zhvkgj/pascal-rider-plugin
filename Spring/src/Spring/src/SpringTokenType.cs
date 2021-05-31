using System.Collections.Generic;
using JetBrains.ReSharper.Plugins.Spring.Generated;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;
using JetBrains.ReSharper.Psi.Parsing;
using JetBrains.Text;
using JetBrains.Util;

namespace JetBrains.ReSharper.Plugins.Spring
{
    public class SpringTokenType : TokenNodeType
    {
        private readonly HashSet<int> _keywords = new HashSet<int>
        {
            PascalLexer.INT_DIV, PascalLexer.MOD,
            PascalLexer.XOR, PascalLexer.OR,
            PascalLexer.AND, PascalLexer.SHL,
            PascalLexer.SHR, PascalLexer.NOT,
            PascalLexer.IN, PascalLexer.IS,
            PascalLexer.AS, PascalLexer.NIL,
            PascalLexer.GOTO, PascalLexer.BEGIN,
            PascalLexer.END, PascalLexer.FOR,
            PascalLexer.DO, PascalLexer.TO,
            PascalLexer.DOWNTO, PascalLexer.CASE,
            PascalLexer.OF, PascalLexer.IF,
            PascalLexer.THEN, PascalLexer.ELSE,
            PascalLexer.OTHERWISE, PascalLexer.REPEAT,
            PascalLexer.UNTIL, PascalLexer.WHILE,
        };

        public SpringTokenType(string s, int index) : base(s, index)
        {
        }

        public override LeafElementBase Create(IBuffer buffer, TreeOffset startOffset, TreeOffset endOffset)
        {
            var textRange = new TextRange(startOffset.Offset, endOffset.Offset);
            return new SpringToken(this, buffer.GetText(textRange));
        }

        public override bool IsWhitespace => Index == PascalLexer.WS;

        public override bool IsComment => Index == PascalLexer.SINGLE_COMMENT || 
                                          Index == PascalLexer.MultiComment1 ||
                                          Index == PascalLexer.MultiComment2;

        public override bool IsStringLiteral => Index == PascalLexer.CharacterString;
        public override bool IsConstantLiteral => Index == PascalLexer.SignedNumber;
        public override bool IsIdentifier => Index == PascalLexer.IDENT;
        public override bool IsKeyword => _keywords.Contains(Index);
        public override string TokenRepresentation => ToString();
    }
}