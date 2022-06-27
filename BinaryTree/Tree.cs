namespace BinaryTree
{
    public class Tree
    {
        private uint _deepestLevel;
        private uint _nodeCount;
        private uint _lastFindComparisonCount;

        /// <summary>
        /// Creates a new tree with <see cref="root"/> as the root node.
        /// </summary>
        /// <param name="root">The root node</param>
        public Tree(Node root)
        {
            Root = root;
        }

        /// <summary>
        /// The root node.
        /// </summary>
        public Node Root { get; }
        /// <summary>
        /// The deepest level of the tree.
        /// </summary>
        public uint DeepestLevel => _deepestLevel;
        /// <summary>
        /// The amount of nodes in the tree.
        /// </summary>
        public uint NodeCount => _nodeCount;
        /// <summary>
        /// How many comparisons have been done in the last Find operation.
        /// </summary>
        public uint LastFindComparisonCount => _lastFindComparisonCount;

        /// <summary>
        /// Insert a new node with the key <see cref="key"/> and the value <see cref="value"/>.
        /// </summary>
        /// <param name="key">The key of the new node.</param>
        /// <param name="value">The value of the new node.</param>
        /// <returns>The inserted node.</returns>
        public Node Insert(short key, string value)
        {
            var node = new Node(key, value);
            InsertNode(node, Root);
            _nodeCount++;

            if (_deepestLevel < node.Level)
                _deepestLevel = node.Level;

            return node;
        }

        /// <summary>
        /// Recursive method to insert a new node under a parent node.
        /// </summary>
        /// <param name="node">The node to be inserted</param>
        /// <param name="parent">The current potential parent of the inserted node</param>
        private void InsertNode(Node node, Node parent)
        {
            node.Level++;
            if (node.Key > parent.Key)
            {
                if (parent.RightChild == null)
                {
                    parent.RightChild = node;
                    node.Parent = parent;
                }
                else
                    InsertNode(node, parent.RightChild);
            }
            else
            {
                if (parent.LeftChild == null)
                {
                    parent.LeftChild = node;
                    node.Parent = parent;
                }
                else
                    InsertNode(node, parent.LeftChild);
            }
        }

        /// <summary>
        /// Searches a node with the key <see cref="key"/> and returns it if it exists.
        /// </summary>
        /// <param name="key"></param>
        /// <returns>A node with the key <see cref="key"/>, if it exists.</returns>
        public Node Find(short key)
        {
            _lastFindComparisonCount = 0;
            return FindNode(key, Root);
        }

        /// <summary>
        /// Recursive method to find a node.
        /// </summary>
        /// <param name="key">The key to search for.</param>
        /// <param name="searchNode">The current node that's being evaluated.</param>
        /// <returns>A node with the key <see cref="key"/>, if it exists.</returns>
        private Node FindNode(short key, Node searchNode)
        {
            _lastFindComparisonCount++;
            if (searchNode.Key == key)
                return searchNode;

            _lastFindComparisonCount++;
            if (key > searchNode.Key)
            {
                if (searchNode.RightChild == null)
                    return null;
                return FindNode(key, searchNode.RightChild);
            }

            if (searchNode.LeftChild == null)
                return null;
            return FindNode(key, searchNode.LeftChild);
        }

        /// <summary>
        /// Removes a node from the tree based on its key, if it exists.
        /// If the node has multiple children, they and the subsequent children will be removed as well.
        /// If the node only has one child, it will be set as the new child of the removed node's parent.
        /// </summary>
        /// <param name="key"></param>
        public void Remove(short key)
        {
            var node = Find(key);
            if (node != null)
            {
                if (node == node.Parent.RightChild)
                {
                    node.Parent.RightChild = null;
                    var loneChild = GetLoneChild(node);

                    // If node has only one child, set that as the new child of the parent.
                    if (loneChild != null)
                    {
                        node.Parent.RightChild = loneChild;
                        ResetLevels(node.Parent);
                    }
                }
                else
                {
                    node.Parent.LeftChild = null;
                    var loneChild = GetLoneChild(node);

                    // If node has only one child, set that as the new child of the parent.
                    if (loneChild != null)
                    {
                        node.Parent.LeftChild = loneChild;
                        ResetLevels(node.Parent);
                    }
                }
            }

            _nodeCount--;
        }

        /// <summary>
        /// Reset the levels of a node and its children.
        /// </summary>
        /// <param name="node"></param>
        private void ResetLevels(Node node)
        {
            if (node == null)
                return;

            node.Level = node.Parent.Level + 1;
            ResetLevels(node.RightChild);
            ResetLevels(node.LeftChild);
        }

        /// <summary>
        /// Returns the child of a node if it has exactly one child. If there are none or two children, null will be returned.
        /// </summary>
        /// <param name="node">The node who's children are to be evaluated.</param>
        /// <returns>The lone child of a node.</returns>
        private Node GetLoneChild(Node node)
        {
            if (node.RightChild == null && node.LeftChild == null)
                return null;

            if (node.RightChild != null)
            {
                if (node.LeftChild != null)
                    return null;
                return node.RightChild;
            }
            
            if (node.LeftChild != null)
            {
                if (node.RightChild != null)
                    return null;
                return node.LeftChild;
            }

            return null;
        }

        /// <summary>
        /// Recalculates tree depth
        /// </summary>
        private void RecalculateDepth(Node node = null)
        {
            node ??= Root;

            if (_deepestLevel < node.Level)
                _deepestLevel = node.Level;

            if (node.RightChild != null)
                RecalculateDepth(node.RightChild);

            if (node.LeftChild != null)
                RecalculateDepth(node.LeftChild);
        }
    }

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
}
