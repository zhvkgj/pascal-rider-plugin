using System.Collections.Generic;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using JetBrains.ReSharper.Psi.TreeBuilder;

namespace JetBrains.ReSharper.Plugins.Spring
{
    public class TreeListener : IParseTreeListener
    {
        private readonly PsiBuilder _psiBuilder;
        private readonly LinkedList<int> _markers;

        public TreeListener(PsiBuilder psiBuilder)
        {
            _psiBuilder = psiBuilder;
            _markers = new LinkedList<int>();
        }

        public void VisitTerminal(ITerminalNode node)
        {
            while (!_psiBuilder.Eof())
            {
                var tokenType = _psiBuilder.AdvanceLexer();
                if (!tokenType.IsComment && !tokenType.IsWhitespace)
                {
                    break;
                }
            }
        }

        public void VisitErrorNode(IErrorNode node)
        {
            while (!_psiBuilder.Eof())
            {
                var tokenType = _psiBuilder.AdvanceLexer();
                if (!tokenType.IsComment && !tokenType.IsWhitespace)
                {
                    break;
                }
            }
        }

        public void EnterEveryRule(ParserRuleContext ctx)
        {
            _markers.AddFirst(_psiBuilder.Mark());
        }

        public void ExitEveryRule(ParserRuleContext ctx)
        {
            var oldMarker = _markers.First.Value;
            _markers.RemoveFirst();
            _psiBuilder.Done(oldMarker, SpringCompositeNodeType.BLOCK, null);
        }
    }
}