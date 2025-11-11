namespace Markdown.Parsing.Nodes;

public class ParagraphNode : BlockTypeNode
{
    public List<InlineTypeNode> Inlines { get; }

    public ParagraphNode(List<InlineTypeNode> inlines)
    {
        Inlines = inlines;
    }
}