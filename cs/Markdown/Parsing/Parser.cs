using Markdown.Parsing.Nodes;
using Markdown.Tokenizing;

namespace Markdown.Parsing;

public class Parser
{
    public RootNode Parse(List<Token> tokens)
    {
        throw new NotImplementedException();
    }
    
    private BlockNode ParseBlock(ParserCursor cursor)
    {
        throw new NotImplementedException();
    }

    private List<InlineNode> ParseInlines(ParserCursor cursor)
    {
        throw new NotImplementedException();
    }
}