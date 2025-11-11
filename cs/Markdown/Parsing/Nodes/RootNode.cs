namespace Markdown.Parsing.Nodes;

public class RootNode : INode
{
    public List<BlockTypeNode> Blocks { get; }

    public RootNode(List<BlockTypeNode> blocks)
    {
        Blocks = blocks;
    }
}