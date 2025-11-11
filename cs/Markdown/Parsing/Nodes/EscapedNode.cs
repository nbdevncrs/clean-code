namespace Markdown.Parsing.Nodes;

public class EscapedNode : InlineTypeNode
{
    public char EscapedChar { get; }

    public EscapedNode(char escapedChar)
    {
        EscapedChar = escapedChar;
    }
}