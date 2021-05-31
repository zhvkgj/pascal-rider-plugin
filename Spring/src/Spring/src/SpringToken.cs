using System;
using System.Text;
using Antlr4.Runtime;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;
using JetBrains.ReSharper.Psi.Parsing;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.Text;

namespace JetBrains.ReSharper.Plugins.Spring
{
    public class SpringToken : LeafElementBase, ITokenNode
    {
        private readonly string _text;
        public IToken Token { get; set; }

        public SpringToken(SpringTokenType nodeType, String text)
        {
            _text = text;
            NodeType = nodeType;
        }

        public override int GetTextLength()
        {
            return _text.Length;
        }
        
        public override string GetText()
        {
            return _text;
        }

        public override StringBuilder GetText(StringBuilder to)
        {
            return to.Append(_text);
        }
        
        public override IBuffer GetTextAsBuffer()
        {
            return new StringBuffer(_text);
        }

        public override NodeType NodeType { get; }

        public override PsiLanguageType Language => SpringLanguage.Instance;
        
        public TokenNodeType GetTokenType()
        {
            return (TokenNodeType) NodeType;
        }
    }
}