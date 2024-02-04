using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;

namespace p1;

public class QuickFindUF
{
    private int[] id;
    //树的数量
    private int count;
    public QuickFindUF(int N){
        count = N;
        id = new int[N];
        for(int i = 0; i < N; i++){
            id[i] = i;
        }
    }
    public int Count(){
        return count;
    }
    public bool Connected(int p, int q){
            return id[p] == id[q];
    }
    public int Find(int p){
        return id[p];
    }
    public void Union(int p, int q){
        int pRoot = Find(p);
        int qRoot = Find(q);

        if(pRoot == qRoot) return;

        for(int i = 0; i < id.Length; i++){
            if(id[i] == pRoot) id[i] = qRoot;
        }
        count--;
    }
    
}
public class QuickUnionUF
{
    protected int[] id;
    protected int count;
    public QuickUnionUF(int N){
        count = N;
        id = new int[N];
        for(int i = 0; i < N; i++){
            id[i] = i;
        }
    }
    public int Count(){
        return count;
    }
    public bool Connected(int p, int q){
            return id[p] == id[q];
    }
    public virtual int Find(int p){
        while(p != id[p]) p = id[p];
        return p;
    }
    //支持重写的方法
    public virtual void Union(int p, int q){
        int pRoot = Find(p);
        int qRoot = Find(q);

        if(pRoot == qRoot) return;

        id[pRoot] = qRoot;
        count --;
    }
}
public class WeightedQuickUnionUF: QuickUnionUF{
    private int[] size;
    private int[] height;
    public WeightedQuickUnionUF(int N): base(N){
        size = new int[N];
        height = new int[N];
        for(int i = 0; i < N; i++){
            size[i] = 1;
            height[i] = 1;
        }
    }
    
    public override int Find(int p)
    {
        int node = p;
        while(p != id[p]) {
            p = id[p];
        };
        //路径压缩（将所有连通发节点全连根节点）
        // while(node != id[node]){
        //     int nextNode = id[node];
        //     id[node] = p;
        //     node = nextNode;
        // }
        return p;
        
    }
    public override void Union(int p, int q){
        int pRoot = Find(p);
        int qRoot = Find(q);

        if(pRoot == qRoot) return;
        if(size[pRoot] <= size[qRoot]){
            base.id[pRoot] = qRoot;
            size[qRoot] += size[pRoot];
        }else{
            base.id[qRoot] = pRoot;
            size[pRoot] += size[qRoot];
        }
        count--;
    }
    public void HeightUnion(int p, int q){
        int pRoot = Find(p);
        int qRoot = Find(q);

        if(pRoot == qRoot) return;
        if(height[pRoot] < height[qRoot]){
            base.id[pRoot] = qRoot;
        }else if(height[pRoot] > height[qRoot]){
            base.id[qRoot] = pRoot;
        }else{
            base.id[pRoot] = qRoot;
            height[qRoot] ++;
        }
        count--;
    }
    public int Size(int p){
        return size[base.Find(p)];
    }
}
public class LinkWQuickUnion{
    public class Node{
        public int item;
        public int size = 1;
        public Node? nextLink;
        public Node? nextRoot;
    }
    //树的个数
    private int count;
    private int num;
    private Node? head;

    public LinkWQuickUnion(int n){
        num = n;
        for(int i = 0; i < n; i++){
            Node node = new Node();
            node.item = i;
            node.nextLink = head;
            node.nextRoot = node;
            count++;
            head = node;
        }
        
    }
    public int Count(){
        return count;
    }
    public bool Connected(int p, int q){
        return Find(p).Equals(Find(q));
    }
    public Node? Find(int k){
        Node? item = head;
        if(item == null) return default;
        for(int i = 0; i < num - 1 - k; i++){
            item = item.nextLink;
        }

        while(item != item.nextRoot){
            item = item.nextRoot;
        }
        return item;
    }
    public void Union(int p, int q){
        Node? pRoot = Find(p);
        Node? qRoot = Find(q);

        if(pRoot.Equals(qRoot)) return;

        if(pRoot.size <= qRoot.size){
            pRoot.nextRoot = qRoot;
            qRoot.size += pRoot.size;
        }else{
            qRoot.nextRoot = pRoot;
            pRoot.size += qRoot.size;
        }
        count--;
    }
}
class Test{
    public static int ErdosRenyi(int N){
        LinkWQuickUnion linkWQuickUnion = new LinkWQuickUnion(N);
        linkWQuickUnion.Union(2,3);
        linkWQuickUnion.Union(3,4);
        return linkWQuickUnion.Count();
    }
    static void Main(){
        Console.WriteLine(ErdosRenyi(10));
    }
}
