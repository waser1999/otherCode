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
    public Graph(Graph graph){
        VertaxNum = graph.V();
        bag = new ArrayList[VertaxNum];
        for(int i = 0; i < VertaxNum; i++){
            bag[i] = new ArrayList();
            foreach(int j in graph.Adj(i)){
                bag[i].Add(j);
            }
        }
    }
    public Graph(FileInfo file){
        using(StreamReader streamReader = file.OpenText()){
            VertaxNum = int.Parse(streamReader.ReadLine());

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

    public Graph(FileInfo symbolGraph, int E){
        using(StreamReader streamReader = symbolGraph.OpenText()){
            VertaxNum = int.Parse(streamReader.ReadLine());

            bag = new ArrayList[VertaxNum];
            for(int i = 0; i < VertaxNum; i++){
                bag[i] = new ArrayList();
            }

            while(!streamReader.EndOfStream){
                string[] s = streamReader.ReadLine().Split(" ");
                if(s != null) {
                    int root = int.Parse(s[0]);
                    for(int i = 1; i < s.Length; i++){
                        AddEdge(root, int.Parse(s[i]));
                        EdgeNum++;
                    }
                }
                
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
        if(a == b) return;
        if(HasEdge(a, b)) return;
        bag[b].Add(a);
        bag[a].Add(b);
        EdgeNum++;
    }
    public ArrayList Adj(int v){
        return bag[v];
    }
    public bool HasEdge(int v, int w){
        foreach(int i in Adj(v)){
            if(i.Equals(w)) return true;
        }
        return false;
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
    private int[] length;

    public BreadthFirstSearch(Graph graph, int start){
        this.start = start;
        marked = new bool[graph.V()];
        parentEdge = new int[graph.V()];
        length = new int[graph.V()];
        length[start] = 0;
        bfs(graph, start);
    }
    private void bfs(Graph graph, int root){
        Queue<int> queue = new Queue<int>();
        queue.Enqueue(root);
        marked[root] = true;
        
        while(queue.Count != 0){
            int v = queue.Dequeue();
            int count = length[v] + 1;
            foreach(int c in graph.Adj(v)){
                if(!marked[c]){
                    queue.Enqueue(c);
                    marked[c] = true;
                    parentEdge[c] = v;
                    length[c] = count;
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
    public int DistTo(int v){
        return length[v];
    }
}
public class CC{
    private bool[] marked;
    private int[] id;
    private int count;

    public CC(Graph graph){
        marked = new bool[graph.V()];
        id = new int[graph.V()];
        for(int i = 0; i < marked.Length; i++){
            if(!marked[i]){
                DFS(graph, i);
                count++;
            }
        }
    }

    private void DFS(Graph graph, int root)
    {
        marked[root] = true;
        id[root] = count;
        foreach(int i in graph.Adj(root)){
            if(!marked[i]) DFS(graph, i);
        }
    }
    public int Count(){
        return count;
    }
    public bool isConnected(int a, int b){
        return id[a] == id[b];
    }
}
public class GraphProperties{
    private int[] eccentricities;
    private int maxE = 0;
    //设一个数为最大值
    private int minE = int.MaxValue;
    private int center;

    public GraphProperties(Graph graph){
        CC cC = new CC(graph);
        eccentricities = new int[graph.V()];

        if(cC.Count() != 1) throw new Exception("Not a connected graph");
        GetAllEccentricity(graph);
    }

    private void GetAllEccentricity(Graph graph)
    {
        for(int vertax = 0; vertax < graph.V(); vertax++){
            BreadthFirstSearch breadthFirstSearch = new BreadthFirstSearch(graph, vertax);
            for(int root = 0; root < graph.V(); root++){
                if(root == vertax) continue;
                
                eccentricities[vertax] = Math.Max(eccentricities[vertax], breadthFirstSearch.DistTo(root));
            }

            if(eccentricities[vertax] > maxE) maxE = eccentricities[vertax];
            if(eccentricities[vertax] < minE){
                minE = eccentricities[vertax];
                center = vertax;
            }
        }
    }
    public int Eccentricity(int v){
        return eccentricities[v];
    }
    public int Diameter(){
        return maxE;
    }
    public int Radius(){
        return minE;
    }
    public int Center(){
        return center;
    }
}

class T1{
    static void Main(){
        FileInfo file = new FileInfo("D:\\waser\\Documents\\Program\\otherCode\\CS\\p4\\graph");
        Graph graph = new Graph(file);
        BreadthFirstSearch breadthFirstSearch = new BreadthFirstSearch(graph, 0);
        GraphProperties graphProperties = new GraphProperties(graph);
        Console.WriteLine(graphProperties.Diameter());
    }
}