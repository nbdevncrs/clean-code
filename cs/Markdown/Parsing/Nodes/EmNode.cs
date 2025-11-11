namespace Markdown.Parsing.Nodes;

public class EmNode : InlineTypeNode
{
    public List<InlineTypeNode> Children { get; }

    public EmNode(List<InlineTypeNode> children)
    {
        Children = children;
    }
}