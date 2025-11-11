using System;
using System.Text;
using Markdown.Parsing.Nodes;

namespace Markdown.Rendering;

public class HtmlRenderer
{
    public static string Render(RootNode document)
    {
        ArgumentNullException.ThrowIfNull(document);

        var sb = new StringBuilder();
        foreach (var block in document.Blocks)
        {
            RenderBlock(block, sb);
        }

        return sb.ToString();
    }

    private static void RenderBlock(BlockTypeNode block, StringBuilder sb)
    {
        switch (block)
        {
            case ParagraphNode p:
                sb.Append('<').Append('p').Append('>');
                foreach (var inline in p.Inlines)
                    RenderInline(inline, sb);
                sb.Append("</p>");
                break;

            case HeaderNode h:
                sb.Append('<').Append('h').Append('1').Append('>');
                foreach (var inline in h.Inlines)
                    RenderInline(inline, sb);
                sb.Append("</").Append('h').Append('1').Append('>');
                break;
        }
    }

    private static void RenderInline(InlineTypeNode node, StringBuilder sb)
    {
        switch (node)
        {
            case TextNode t:
                sb.Append(EscapeHtml(t.Text));
                break;

            case EmNode em:
                sb.Append("<em>");
                foreach (var child in em.Children)
                    RenderInline(child, sb);
                sb.Append("</em>");
                break;

            case StrongNode strong:
                sb.Append("<strong>");
                foreach (var child in strong.Children)
                    RenderInline(child, sb);
                sb.Append("</strong>");
                break;

            case EscapedNode esc:
                sb.Append(EscapeHtml(esc.EscapedChar.ToString()));
                break;

            case LinkNode link:
                var href = EscapeHtmlAttribute(link.Url);
                sb.Append("<a href=\"").Append(href).Append("\">");
                foreach (var child in link.Children)
                    RenderInline(child, sb);
                sb.Append("</a>");
                break;

            default:
                sb.Append(EscapeHtml(node.ToString() ?? string.Empty));
                break;
        }
    }

    private static string EscapeHtml(string? text)
    {
        if (string.IsNullOrEmpty(text)) return string.Empty;

        var sb = new StringBuilder(text.Length);
        foreach (var c in text)
        {
            switch (c)
            {
                case '&': sb.Append("&amp;"); break;
                case '<': sb.Append("&lt;"); break;
                case '>': sb.Append("&gt;"); break;
                case '"': sb.Append("&quot;"); break;
                case '\'': sb.Append("&#39;"); break;
                default: sb.Append(c); break;
            }
        }

        return sb.ToString();
    }

    private static string EscapeHtmlAttribute(string? value) => EscapeHtml(value);
}