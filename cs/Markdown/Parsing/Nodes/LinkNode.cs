namespace Markdown.Parsing.Nodes;

public class LinkNode : InlineNode
{
    public List<InlineNode> Children { get; }
    public string Url { get; }

    public LinkNode(List<InlineNode> children, string url)
    {
        Children = children;
        Url = url;
    }
}