namespace BinaryTree;

public class Node
{
    public Node(short key, string value)
    {
        Key = key;
        Value = value;
    }

    public short Key { get; }
    public string Value { get; }

    public Node Parent { get; set; }
    public Node LeftChild { get; set; }
    public Node RightChild { get; set; }
    public uint Level { get; set; }
}