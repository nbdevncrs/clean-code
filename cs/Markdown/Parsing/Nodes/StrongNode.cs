namespace Markdown.Parsing.Nodes;

public class StrongNode : InlineNode
{
    public List<InlineNode> Children { get; }

    public StrongNode(List<InlineNode> children)
    {
        Children = children;
    }
}