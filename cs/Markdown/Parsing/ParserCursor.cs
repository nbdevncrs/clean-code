using Markdown.Tokenizing;

namespace Markdown.Parsing;

public class ParserCursor
{
    public bool End => throw new NotImplementedException();
    public Token Current => throw new NotImplementedException();

    public ParserCursor(List<Token> tokens)
    {
        throw new NotImplementedException();
    }

    public void Move()
    {
        throw new NotImplementedException();
    }

    public Token Peek()
    {
        throw new NotImplementedException();
    }
}