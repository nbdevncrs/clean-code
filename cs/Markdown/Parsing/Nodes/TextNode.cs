namespace Markdown.Parsing.Nodes;

public class TextNode : InlineTypeNode
{
    public string Text { get; }

    public TextNode(string text)
    {
        Text = text;
    }
}