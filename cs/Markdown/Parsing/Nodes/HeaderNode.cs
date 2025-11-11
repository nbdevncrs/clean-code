namespace Markdown.Parsing.Nodes;

public class HeaderNode(List<InlineTypeNode> inlines) : BlockTypeNode
{
    public List<InlineTypeNode> Inlines { get; } = inlines;
}