namespace Markdown.Parsing.Nodes;

public class EmNode : InlineNode
{
    public List<InlineNode> Children { get; }

    public EmNode(List<InlineNode> children)
    {
        Children = children;
    }
}