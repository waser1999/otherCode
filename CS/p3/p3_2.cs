﻿namespace p3;

public class BST<Value>
{
    protected class Node{
        public IComparable key;
        public Value value;
        public Node? left, right;
        public int N;
        public Node(IComparable key, Value value, int N, int layer){
            this.key = key;
            this.value = value;
            this.N = N;
        }
    }
    protected Node? root;

    public virtual int Size(){
        return Size(root);
    }
    private int Size(Node? node){
        if(node == null) return 0;
        return node.N;
    }
    public bool isEmpty(){
        return root == null;
    }
    public virtual void Put(IComparable key, Value value){
        root = Put(key, value, root);
    }
    private Node? Put(IComparable key, Value value, Node? root){
        if(root == null) return new Node(key, value, 1, 1);
        int cmp = root.key.CompareTo(key);
        if(cmp > 0){
            root.left = Put(key, value, root.left);
        }else if(cmp < 0){
            root.right = Put(key, value, root.right);
        }else{
            root.value = value;
        }
        root.N = Size(root.left) + Size(root.right) + 1;
        return root;
    }
    public virtual Value? Get(IComparable key){
        Node? x = Get(key, root);
        if(x == null) return default(Value);
        return x.value;
    }
    private Node? Get(IComparable key, Node? root){
        if(root == null) return null;
        int cmp = root.key.CompareTo(key);
        if(cmp > 0){
            return Get(key, root.left);
        }else if(cmp < 0){
            return Get(key, root.right);
        }
        return root;
    }
    public void DeleteMin(){
        root = DeleteMin(root);
    }
    private Node? DeleteMin(Node? node){
        if(node.left == null) return node.right;
        node.left = DeleteMin(node.left);
        node.N = Size(node.left) + Size(node.right) + 1;
        return node;
    }
    public void Delete(IComparable key){
        root = Delete(key, root);
    }

    private Node? Delete(IComparable key, Node? root)
    {
        
        if(root == null) return null;
        int cmp = root.key.CompareTo(key);

        if(cmp > 0){
            root.left = Delete(key, root.left);
        }else if(cmp < 0){
            root.right = Delete(key, root.right);
        }else{
            if(root.left == null) return root.right;
            if(root.right == null) return root.left;
            Node? newRoot = Min(root.right);
            newRoot.left = root.left;
            newRoot.right = DeleteMin(root.right);
            newRoot.N = Size(newRoot.left) + Size(newRoot.right) + 1;
            return newRoot;
        }
        return root;
    }
    public IComparable? Min(){
        Node? x = Min(root);
        if(x == null) return null;
        return x.key;
    }
    private Node? Min(Node? root){
        if(root == null) return null;
        if(root.left == null) return root;
        return Min(root.left);
    }
    public IComparable? Floor(IComparable key){
        Node? x = Floor(key, root);
        if(x == null) return null;
        return x.key;
    }

    private Node? Floor(IComparable key, Node? root)
    {
        if(root == null) return null;
        int cmp = root.key.CompareTo(key);
        if(cmp == 0){
            return root;
        }else if(cmp > 0){
            return Floor(key, root.left);
        }else{
            Node? node = Floor(key, root.right);
            if(node != null) return node;
            return root;
        }
    }

    public IComparable? Select(int k){
        Node? x = Select(k, root);
        if(x == null) return default;
        return x.key;
    }

    private Node? Select(int k, Node? root)
    {
        if(root == null) return null;
        int cmp = k.CompareTo(Size(root.left));
        if(cmp == 0) {
            return root;
        }else if(cmp < 0){
            return Select(k, root.left);
        }else{
            return Select(k - Size(root.left) - 1, root.right);
        }
    }
    public int Height(){
        return Height(root);
    }
    private int Height(Node? root){
        if(root == null) return 0;
        int leftHeight = Height(root.left);
        int rightHeight = Height(root.right);
        if(leftHeight <= rightHeight){
            return rightHeight + 1;
        }else{
            return leftHeight + 1;
        }
    }
    public int AvgCompare(){
        return TreeRouteLength()/Size() + 1;
    }

    private int TreeRouteLength()
    {
        return TreeRouteLength(root);
    }

    private int TreeRouteLength(Node? root)
    {
        if(root == null) return 0;
        int child;
        if(root.left == null && root.right == null){
            return 0;
        }else if(root.left == null || root.right == null){
            child = 1;
        }else{
            child = 2;
        }
        return TreeRouteLength(root.left) + TreeRouteLength(root.right) + child;
    }
}
public class ModifiedBST<Value>: BST<Value>{
    public override int Size()
    {
        return Size(root);
    }

    private int Size(Node? root)
    {
        if(root == null) return 0;
        return Size(root.left) + Size(root.right) + 1;
    }
    private Node? Max(Node? root)
    {
        if(root == null) return null;
        Node? node = root;
        while(node.right != null) node = node.right;
        return node;
    }
    public IComparable? Ceiling(IComparable key){
        if(root == null) return null;
        Node? node = root;
        IComparable? ceiling = null;

        while(node != null){
            int cmp = node.key.CompareTo(key);
            if(cmp == 0){
                return node.key;
            }else if(cmp < 0){
                node = node.right;
            }else{
                ceiling = node.key;
                node = node.left;    
            }
        }
        return ceiling;
        
        
    }
    public int Rank(IComparable key){
        int rank = 0;
        if(root == null) return rank;

        Node? node = root;
        while(node != null){
            int cmp = node.key.CompareTo(key);
            if(cmp == 0){
                rank += Size(node.left);
                break;
            }else if(cmp > 0){
                node = node.left;
            }else{
                rank += Size(node.left) + 1;
                node = node.right;
            }
        }
        return rank;
    }
    public override Value? Get(IComparable key)
    {
        if(root == null) return default;
        
        Node? node = root;
        while(node != null){
            int cmp = node.key.CompareTo(key);
            if(cmp > 0){
                node = node.left;
            }else if(cmp < 0){
                node = node.right;
            }else{
                return node.value;
            }
        }
        return default;
    }
}
public class Test{
    static void Main(){
        ModifiedBST<int> bST = new ModifiedBST<int>();
        bST.Put(2, 10);
        bST.Put(1, 8);
        bST.Put(3, 25);
        bST.Put(-4, 9);
        bST.Put(0, 2);
        Console.WriteLine(bST.Rank(-4));
    }
}