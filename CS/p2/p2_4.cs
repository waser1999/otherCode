using System.Collections;

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
    protected IComparable[] heap;
    protected int N = 0;
    private IComparable? max;

    public HeapSort(int n)
    {
        heap = new IComparable[n + 1];
    }
    public HeapSort(IComparable[] a)
    {
        heap = new IComparable[a.Length + 1];
        for (int i = 1; i <= a.Length; i++) Insert(a[i - 1]);
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
    protected virtual void Swim(int k)
    {
        while (k > 1 && Compare(k / 2, k) > 0)
        {
            Exchange(k, k / 2);
            k = k / 2;
        }
    }
    protected virtual void Sink(int k)
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
        
        if(element.CompareTo(max) > 0){
            max = element;
        }
    }
    public IComparable DelMin()
    {
        IComparable min = heap[1];
        Exchange(1, N--);
        heap[N + 1] = null;
        Sink(1);
        if(N == 0) max = null;
        return min;
    }
    public IComparable? Max(){
        return max;
    }
}
public class OptimizedHeapSort : HeapSort
{
    public OptimizedHeapSort(IComparable[] n) : base(n)
    {
    }
    protected override void Swim(int k)
    {
        IComparable temp = heap[k];
        bool isExchanged = false;

        while(k > 1 && temp.CompareTo(heap[k/2]) < 0){
            heap[k] = heap[k/2];
            k = k/2;
            isExchanged = true;
        }
        if(isExchanged){
            heap[k] = temp;
        }
    }
    protected override void Sink(int k)
    {
        IComparable temp = heap[k];
        bool isExchanged = false;

        while(2 * k <= N){
            int j = 2 * k;
            if(heap[j + 1] != null && Compare(j, j + 1) > 0) j++;
            if(temp.CompareTo(heap[j]) > 0){
                heap[k] = heap[j];
                isExchanged = true;
            }else{
                break;
            }
            k = j;
        }
        if(isExchanged){
            heap[k] = temp;
        }
    }
}
public class FastHeapSort : HeapSort
{
    public FastHeapSort(int n) : base(n)
    {
    }
    public FastHeapSort(IComparable[] a): base(a){
    }
    private int FindSwimTimes(int k){
        int down = (int)Math.Log2(k) + 1;
        int lowLevel = down;
        int up = 1;
        int middle;

        while(down > up){
            middle = up + (down - up) / 2;
            //得到在该层的父节点地址
            int middleIndex = k / (1 << (lowLevel - middle));
            int compare = Compare(middleIndex, k);
            if(compare > 0){
                down = middle;
            }else{
                up = middle + 1;
            }
        }
        return lowLevel - up;
    }
    protected override void Swim(int k)
    {
        int times = FindSwimTimes(k);
        for(int i = 0; i < times; i++){
            Exchange(k, k/2);
            k = k/2;
        }
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
public class FindMedian{
    private PriorityQueue<int, int> smallHeap = new PriorityQueue<int, int>();
    //大顶堆
    private PriorityQueue<int, int> largeHeap = new PriorityQueue<int, int>(Comparer<int>.Create((a, b) => b.CompareTo(a)));
    private int N = 0;
    public FindMedian(int[] ints){
        for(int i = 0; i < ints.Length; i++){
            Insert(ints[i]);
        }
    }

    public void Insert(int a){
        if(N == 0 || a < largeHeap.Peek()){
            largeHeap.Enqueue(a, a);
        }else{
            smallHeap.Enqueue(a, a);
        }

        int temp;
        if(largeHeap.Count > smallHeap.Count + 1){
            temp = largeHeap.Dequeue();
            smallHeap.Enqueue(temp, temp);
        }else if(smallHeap.Count > largeHeap.Count + 1){
            temp = smallHeap.Dequeue();
            largeHeap.Enqueue(temp, temp);
        }
        N++;
    }
    public int GetMedian(){
        if(largeHeap.Count == smallHeap.Count){
            return (largeHeap.Peek() + smallHeap.Peek())/ 2;
        }else if (largeHeap.Count > smallHeap.Count){
            return largeHeap.Peek();
        }else{
            return smallHeap.Peek();
        }
    }
    public int DeleteMedian(){
        if (largeHeap.Count > smallHeap.Count){
            return largeHeap.Dequeue();
        }else{
            return smallHeap.Dequeue();
        }
    }
}
public class IndexMinPQ{
    private int N;
    private IComparable[] keys;
    private int[] pq;
    private int[] qp;
    public IndexMinPQ(int maxN){
        keys = new IComparable[maxN + 1];
        pq = new int[maxN + 1];
        qp = new int[maxN + 1];
        for(int i = 0; i <= maxN; i++){
            qp[i] = -1;
        }
    }
    public bool isEmpty(){
        return N == 0;
    }
    public int Size(){
        return N;
    }
    public bool Contains(int index){
        return qp[index] != -1;
    }
    private void Exchange(int index1, int index2){
        int temp = pq[index1];
        pq[index1] = pq[index2];
        pq[index2] = temp;

        qp[pq[index1]] = index1;
        qp[pq[index2]] = index2;
    }
    private int Compare(int index1, int index2){
        return keys[pq[index1]].CompareTo(keys[pq[index2]]);
    }
    private void Swim(int k)
    {
        while (k > 1 && Compare(k / 2, k) > 0)
        {
            Exchange(k, k / 2);
            k = k / 2;
        }
    }
    protected virtual void Sink(int k)
    {
        while (2 * k <= N)
        {
            int j = 2 * k;
            //排最小值null比一些值小
            if (keys[pq[j]] != null && Compare(j, j + 1) > 0) j++;
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
    public void Insert(int index, IComparable element){
        N++;
        //注意，keys按index的位置放
        keys[index] = element;
        pq[N] = index;
        qp[index] = N;
        Swim(index);
    }
    public IComparable Min(){
        return keys[pq[1]];
    }
    public int MinIndex(){
        return pq[1];
    }
    public IComparable DelMinIndex(){
        IComparable minIndex = pq[1];
        Exchange(1, N--);
        Sink(1);
        keys[pq[N + 1]] = null;
        qp[pq[N + 1]] = -1;
        return minIndex;
    }
    public void Change(int index, IComparable value){
        if(qp[index] == -1) return;
        keys[index] = value;
        Swim(qp[index]);
        Sink(qp[index]);
    }
    public void Delete(int index){
        Exchange(index, N--);
        Swim(index);
        Sink(index);
        keys[pq[N + 1]] = null;
        qp[pq[N + 1]] = -1;
    }
}