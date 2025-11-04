namespace Markdown.Parsing.Nodes;

public class HeaderNode : BlockNode
{
    public int HeaderLevel { get; }
    public List<InlineNode> Inlines { get; }

    public HeaderNode(int headerLevel, List<InlineNode> inlines)
    {
        HeaderLevel = headerLevel;
        Inlines = inlines;
    }
}