namespace Markdown.Parsing.Nodes;

public class RootNode : INode
{
    public List<BlockNode> Blocks { get; }

    public RootNode(List<BlockNode> blocks)
    {
        Blocks = blocks;
    }
}