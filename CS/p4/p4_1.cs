using System.Collections;

namespace p4;

public class Graph
{
    private int VertaxNum;
    private int EdgeNum;
    private ArrayList[]? bag;

    public Graph(int V){
        VertaxNum = V;
        EdgeNum = 0;
        bag = new ArrayList[VertaxNum];
        for(int i = 0; i < VertaxNum; i++){
            bag[i] = new ArrayList();
        }
    }

    public Graph(FileInfo file){
        using(StreamReader streamReader = file.OpenText()){
            VertaxNum = int.Parse(streamReader.ReadLine());
            EdgeNum = int.Parse(streamReader.ReadLine());

            bag = new ArrayList[VertaxNum];
            for(int i = 0; i < VertaxNum; i++){
                bag[i] = new ArrayList();
            }

            while(!streamReader.EndOfStream){
                string[] s = streamReader.ReadLine().Split(" ");
                if(s != null) AddEdge(int.Parse(s[0]), int.Parse(s[1]));
                EdgeNum++;
            }
        }
    }

    public int V(){
        return VertaxNum;
    }
    public int E(){
        return EdgeNum;
    }
    public void AddEdge(int a, int b){
        bag[b].Add(a);
        bag[a].Add(b);
        EdgeNum++;
    }
    public ArrayList Adj(int v){
        return bag[v];
    }
}

public class DepthFristSearch{
    private bool[] marked;
    private int[] parentEdge;
    private int count;
    private int start;
    public DepthFristSearch(Graph graph, int start){
        marked = new bool[graph.V()];
        parentEdge = new int[graph.V()];
        this.start = start;
        DFS(graph, start);
    }
    private void DFS(Graph graph, int root){
        marked[root] = true;
        count++;
        foreach(int next in graph.Adj(root)){
            if(!marked[next]){
                parentEdge[next] = root;
                DFS(graph, next);
            }
        }
    }
    public int Count(){
        return count;
    }
    public bool hasRouteTo(int v){
        return marked[v];
    }
    public Stack<int>? Route(int end){
        if(hasRouteTo(end) == false) return null;
        Stack<int> route = new Stack<int>();
        for(int i = end; i != start; i = parentEdge[i]){
            route.Push(i);
        }
        route.Push(start);
        return route;
    }
}
public class BreadthFirstSearch{
    private bool[] marked;
    private int[] parentEdge;
    private int start;

    public BreadthFirstSearch(Graph graph, int start){
        this.start = start;
        marked = new bool[graph.V()];
        parentEdge = new int[graph.V()];
        bfs(graph, start);
    }
    private void bfs(Graph graph, int root){
        Queue<int> queue = new Queue<int>();
        queue.Enqueue(root);
        marked[root] = true;
        
        while(queue.Count != 0){
            int v = queue.Dequeue();
            foreach(int c in graph.Adj(v)){
                if(!marked[c]){
                    queue.Enqueue(c);
                    marked[c] = true;
                    parentEdge[c] = v;
                } 
            }
        }
    }
    public bool hasRouteTo(int v){
        return marked[v];
    }
    public Stack<int>? Route(int end){
        if(hasRouteTo(end) == false) return null;
        Stack<int> route = new Stack<int>();
        for(int i = end; i != start; i = parentEdge[i]){
            route.Push(i);
        }
        route.Push(start);
        return route;
    }
}
class T1{
    static void Main(){
        FileInfo file = new FileInfo("D:\\waser\\Documents\\Program\\otherCode\\CS\\p4\\graph");
        Graph graph = new Graph(file);
        BreadthFirstSearch breadthFirstSearch = new BreadthFirstSearch(graph, 1);
        Stack<int>? ints = breadthFirstSearch.Route(9);

        if(ints == null){
            Console.WriteLine("null");
        }else {
            foreach(int c in ints) Console.Write(c + "-");
        }
        
    }
}