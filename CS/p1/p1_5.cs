using System.Diagnostics.CodeAnalysis;
using System.Drawing;

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
class Test{
    public static int ErdosRenyi(int N){
        WeightedQuickUnionUF weightedQuickUnionUF = new WeightedQuickUnionUF(N);
        Random random = new Random();
        int step = 0;
        while(weightedQuickUnionUF.Count() > 1){
            int p = random.Next(0, N);
            int q = random.Next(0, N);
            if(!weightedQuickUnionUF.Connected(p, q)){
                weightedQuickUnionUF.Union(p, q);
                step++;
            }
        }
        return step;
    }
    static void Main(){
        Console.WriteLine(ErdosRenyi(200));
    }
}
