namespace Markdown.Parsing.Nodes;

public class EscapedNode : InlineNode
{
    public char EscapedChar { get; }

    public EscapedNode(char escapedChar)
    {
        EscapedChar = escapedChar;
    }
}