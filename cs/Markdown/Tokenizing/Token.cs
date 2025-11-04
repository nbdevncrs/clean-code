namespace Markdown.Tokenizing;

public class Token
{
    public TokenType Type { get; }
    public TokenPosition Position { get; }
    public string Value { get; }

    public Token(TokenType type, TokenPosition position, string value)
    {
        Type = type;
        Position = position;
        Value = value;
    }
}