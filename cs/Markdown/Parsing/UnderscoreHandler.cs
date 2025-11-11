using Markdown.Tokenizing;
using Markdown.Parsing.Nodes;

namespace Markdown.Parsing;

public static class UnderscoreHandler
{
    public static void HandleUnderscores(List<InlineTypeNode> children,
        List<(TokenType Type, int ChildrenIndex)> underscores, ParserCursor cursor, IList<Token> tokens)
    {
        var token = cursor.Current;
        var isDouble = token.Type == TokenType.DoubleUnderscore;

        var previousToken = tokens.ElementAtOrDefault(cursor.Index - 1);
        var nextToken = tokens.ElementAtOrDefault(cursor.Index + 1);

        if (IsDigitBoundary(previousToken, nextToken))
        {
            AddLiteral(children, token.Value, cursor);
            return;
        }

        var canClose = !IsWhitespaceLike(previousToken);
        var nextIsWhitespaceLike = IsWhitespaceLike(nextToken);

        if (canClose)
        {
            var closeResult = TryToClose(underscores, token.Type, children);

            if (closeResult == 1)
            {
                cursor.MoveNext();
                return;
            }

            if (closeResult == -1)
            {
                AddLiteral(children, token.Value, cursor);
                return;
            }
        }

        if (nextIsWhitespaceLike || isDouble && CheckIfDoubleUnderscoreBreaks(cursor, tokens))
        {
            AddLiteral(children, token.Value, cursor);
            return;
        }

        underscores.Add((token.Type, children.Count));
        cursor.MoveNext();
    }

    public static void InsertUnmatchedUnderscores(List<InlineTypeNode> children,
        List<(TokenType Type, int ChildrenIndex)> underscores)
    {
        for (var i = underscores.Count - 1; i >= 0; i--)
        {
            var (type, index) = underscores[i];
            var literal = type == TokenType.DoubleUnderscore ? "__" : "_";
            children.Insert(index, new TextNode(literal));
        }
    }

    private static int TryToClose(List<(TokenType Type, int ChildrenIndex)> underscores, TokenType closingType,
        List<InlineTypeNode> children)
    {
        for (var j = underscores.Count - 1; j >= 0; j--)
        {
            if (underscores[j].Type != closingType)
                continue;

            var opener = underscores[j];
            var startIndex = opener.ChildrenIndex;
            var count = children.Count - startIndex;

            if (count == 0)
                return 0;

            var inner = children.GetRange(startIndex, count);

            if (closingType == TokenType.Underscore)
            {
                var insideDouble = underscores.Any(d =>
                    d.Type == TokenType.DoubleUnderscore &&
                    d.ChildrenIndex < opener.ChildrenIndex);

                if (!insideDouble &&
                    inner.Any(n => n is TextNode t && t.Text.Any(char.IsWhiteSpace)))
                    return 0;
            }

            for (var k = j + 1; k < underscores.Count; k++)
            {
                if (underscores[k].Type == closingType ||
                    underscores[k].ChildrenIndex <= opener.ChildrenIndex ||
                    underscores[k].ChildrenIndex >= children.Count) continue;

                InsertIntersection(children, underscores, opener, underscores[k], j, k);
                return -1;
            }

            children.RemoveRange(startIndex, count);
            InlineTypeNode node = closingType == TokenType.DoubleUnderscore ? new StrongNode(inner) : new EmNode(inner);

            children.Add(node);
            underscores.RemoveAt(j);

            return 1;
        }

        return 0;
    }

    private static void InsertIntersection(
        List<InlineTypeNode> children,
        List<(TokenType Type, int ChildrenIndex)> underscores,
        (TokenType Type, int ChildrenIndex) opener,
        (TokenType Type, int ChildrenIndex) inner,
        int openerIndex, int innerIndex)
    {
        var innerLiteral = inner.Type == TokenType.DoubleUnderscore ? "__" : "_";
        var openerLiteral = opener.Type == TokenType.DoubleUnderscore ? "__" : "_";

        children.Insert(inner.ChildrenIndex, new TextNode(innerLiteral));
        children.Insert(opener.ChildrenIndex, new TextNode(openerLiteral));

        underscores.RemoveAt(innerIndex);
        underscores.RemoveAt(openerIndex);
    }

    private static void AddLiteral(List<InlineTypeNode> children, string value, ParserCursor cursor)
    {
        children.Add(new TextNode(value));
        cursor.MoveNext();
    }

    private static bool IsWhitespaceLike(Token? token)
    {
        return token == null ||
               token.Type == TokenType.Whitespace ||
               token.Type == TokenType.EndOfLine ||
               token.Type == TokenType.EndOfFile;
    }

    private static bool IsDigitBoundary(Token? prev, Token? next)
    {
        var before = prev is { Type: TokenType.Text, Value.Length: > 0 } && char.IsDigit(prev.Value.Last());
        var after = next is { Type: TokenType.Text, Value.Length: > 0 } && char.IsDigit(next.Value.First());
        return before && after;
    }

    private static bool CheckIfDoubleUnderscoreBreaks(ParserCursor cursor, IList<Token> tokens)
    {
        var singleUnderscoresCount = 0;

        for (var i = cursor.Index + 1; i < tokens.Count; i++)
        {
            var currentToken = tokens[i];

            if (currentToken.Type is TokenType.EndOfLine or TokenType.EndOfFile) return false;
            if (currentToken.Type == TokenType.DoubleUnderscore) return (singleUnderscoresCount % 2) == 1;
            if (currentToken.Type != TokenType.Underscore) continue;

            var previous = tokens.ElementAtOrDefault(i - 1);
            if (previous is { Type: TokenType.Escape }) continue;

            singleUnderscoresCount++;
        }

        return false;
    }
}