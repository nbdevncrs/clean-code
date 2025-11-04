namespace Markdown.Parsing.Nodes;

public class ParagraphNode : BlockNode
{
    public List<InlineNode> Inlines { get; }

    public ParagraphNode(List<InlineNode> inlines)
    {
        Inlines = inlines;
    }
}