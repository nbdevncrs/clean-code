namespace Markdown.Parsing.Nodes;

public class TextNode : InlineNode
{
    public string Text { get; }

    public TextNode(string text)
    {
        Text = text;
    }
}