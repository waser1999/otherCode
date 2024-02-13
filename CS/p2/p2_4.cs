namespace p2;

public interface IHeap
{
    bool IsEmpty();
    int Size();
    void Insert(IComparable element);
    IComparable DelMin();
}

public class HeapSort : IHeap
{
    private IComparable[] heap;
    private int N = 0;
    public HeapSort(int n)
    {
        heap = new IComparable[n + 1];
    }
    public HeapSort(IComparable[] a)
    {
        heap = new IComparable[a.Length + 1];
        for (int i = 1; i <= a.Length; i++) Insert(a[i - 1]);
    }
    private void Exchange(int i, int j)
    {
        IComparable temp = heap[i];
        heap[i] = heap[j];
        heap[j] = temp;
    }
    private int Compare(int i, int j)
    {
        return heap[i].CompareTo(heap[j]);
    }
    private void Swim(int k)
    {
        while (k > 1 && Compare(k / 2, k) > 0)
        {
            Exchange(k, k / 2);
            k = k / 2;
        }
    }
    private void Sink(int k)
    {
        while (2 * k <= N)
        {
            int j = 2 * k;
            //排最小值null比一些值小
            if (heap[j + 1] != null && Compare(j, j + 1) > 0) j++;
            if (Compare(k, j) > 0)
            {
                Exchange(k, j);
            }
            else
            {
                break;
            }
            k = j;
        }
    }
    private void Resize()
    {
        int len = heap.Length;
        IComparable[] tempArray = new IComparable[len * 2];
        for (int i = 0; i < heap.Length; i++)
        {
            tempArray[i] = heap[i];
        }
        heap = tempArray;
    }
    public bool IsEmpty()
    {
        return N == 0;
    }
    public int Size()
    {
        return N;
    }
    public void Insert(IComparable element)
    {
        heap[++N] = element;
        if (N > heap.Length / 4) Resize();
        Swim(N);
    }
    public IComparable DelMin()
    {
        IComparable min = heap[1];
        Exchange(1, N--);
        heap[N + 1] = null;
        Sink(1);
        return min;
    }
}
public class ArrayHeapSort
{
    protected IComparable[] heap;
    protected int N = 0;
    public ArrayHeapSort(IComparable[] a)
    {
        heap = new IComparable[a.Length];
        for (int i = 0; i < a.Length; i++) Insert(a[i]);
    }
    protected void Exchange(int i, int j)
    {
        IComparable temp = heap[i];
        heap[i] = heap[j];
        heap[j] = temp;
    }
    protected int Compare(int i, int j)
    {
        return heap[i].CompareTo(heap[j]);
    }
    public bool IsEmpty()
    {
        return N == 0;
    }
    public int Size()
    {
        return N;
    }
    public virtual void Insert(IComparable element)
    {
        heap[N++] = element;
    }
    public virtual IComparable DelMax()
    {
        int maxIndex = 0;
        for (int i = 0; i < N; i++)
        {
            if (Compare(maxIndex, i) < 0) maxIndex = i;
        }
        IComparable max = heap[maxIndex];
        Exchange(maxIndex, N - 1);
        heap[N - 1] = null;
        N--;
        return max;
    }

}
public class ArrayLinedSort : ArrayHeapSort
{
    public ArrayLinedSort(IComparable[] a) : base(a)
    {

    }

    public override IComparable DelMax()
    {
        IComparable max = heap[N - 1];
        heap[N - 1] = null;
        N--;
        return max;
    }

    public override void Insert(IComparable element)
    {
        heap[N++] = element;
        for (int i = N - 1; i > 0 && Compare(i, i - 1) < 0; i--)
        {
            Exchange(i, i - 1);
        }
    }
}
public class StackHeapSort
{
    private class Node
    {
        public IComparable item;
        public Node? next;
        public Node(IComparable item)
        {
            this.item = item;
            this.next = null;
        }
    }
    private int N = 0;
    private Node? head;
    public StackHeapSort(IComparable[] a)
    {
        for (int i = 0; i < a.Length; i++)
        {
            Push(a[i]);
        }
    }
    public bool isEmpty()
    {
        return N == 0;
    }
    public int Size()
    {
        return N;
    }
    public void Push(IComparable element)
    {
        Node node = new Node(element);
        node.next = head;
        head = node;
        N++;
    }
    public IComparable Pop()
    {
        Node? temp = head.next;
        Node res = head;
        while (temp != null)
        {
            if ((res.item).CompareTo(temp.item) < 0) res = temp;
            temp = temp.next;
        }
        IComparable tempNum = res.item;
        head.item = res.item;
        res.item = tempNum;
        head = head.next;
        N--;
        return tempNum;
    }
}
public class Ans
{
    public static bool HeapCheck(IComparable[] a)
    {
        int N = a.Length - 1;
        for (int i = 1; i < N / 2; i++)
        {
            if (2 * i <= N && a[i].CompareTo(a[2 * i]) > 0)
            {
                return false;
            }
            else if (2 * i + 1 <= N && a[i].CompareTo(a[2 * i + 1]) > 0)
            {
                return false;
            }
        }
        return true;
    }
    public class CubeNum : IComparable
    {
        public int sumCube;
        public int a;
        public int b;
        public CubeNum(int a, int b)
        {
            this.a = a;
            this.b = b;
            this.sumCube = a * a * a + b * b * b;
        }
        public int CompareTo(object? obj)
        {
            if(obj == null) return 1;
            //对象类型判别
            if(obj is CubeNum cubeNum){
                return this.sumCube.CompareTo(cubeNum.sumCube);
            }
            throw new NotImplementedException();
        }
        //重写对象打印方法
        public override string ToString()
        {
            return this.sumCube.ToString();
        }
    }
    public static void CubeSum(int n){
        HeapSort heapSort = new HeapSort(n);
        for(int i = 0; i <= n; i++){
            CubeNum cubeNum = new CubeNum(i, 0);
            heapSort.Insert(cubeNum);
        }
        for(int i = 0; i <= n; i++){
            for(int j = 1; j <= n; j++){
                CubeNum cubeNum = new CubeNum(i, j);
                Console.Write(heapSort.DelMin() + " ");
                heapSort.Insert(cubeNum);
            }
        }
    }
    
}
