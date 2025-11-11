namespace Markdown.Parsing.Nodes;

public class StrongNode : InlineTypeNode
{
    public List<InlineTypeNode> Children { get; }

    public StrongNode(List<InlineTypeNode> children)
    {
        Children = children;
    }
}