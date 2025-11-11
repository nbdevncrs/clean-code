namespace Markdown.Parsing.Nodes;

public class LinkNode : InlineTypeNode
{
    public List<InlineTypeNode> Children { get; }
    public string Url { get; }

    public LinkNode(List<InlineTypeNode> children, string url)
    {
        Children = children;
        Url = url;
    }
}