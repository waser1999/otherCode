using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace p2;

public class MergeSort
{
    public static void TopDownSort(int[] a)
    {
        int[] temp = new int[a.Length];

        TopDownSort(a, temp, 0, a.Length - 1);
    }
    public static void TopDownSort(int[] a, int[] temp, int low, int high)
    {
        if (high <= low) return;
        int mid = low + (high - low) / 2;
        TopDownSort(a, temp, low, mid);
        TopDownSort(a, temp, mid + 1, high);
        //性能优化：如果左边小于右边就不排
        if (a[mid] < a[mid + 1])
        {
            return;
        }
        //性能优化：小数组使用插入排序
        else if (high - low + 1 > 15)
        {
            Merge(a, temp, low, mid, high);
        }
        else
        {
            InsertSortInMerge(a, low, high);
        }

    }
    public static void BottomUpSort(int[] a)
    {
        int N = a.Length;
        int[] temp = new int[N];
        //确定合并间隔
        for (int sz = 1; sz <= N; sz *= 2)
        {
            //将每两个块合并
            for (int j = 0; j < N - sz; j += sz * 2)
            {
                Merge(a, temp, j, j + sz - 1, Math.Min(j + 2 * sz - 1, N - 1));
            }
        }
    }
    private static void Merge(int[] a, int[] temp, int low, int mid, int high)
    {
        int leftIndex = low;
        int rightIndex = mid + 1;

        for (int i = low; i <= high; i++)
        {
            temp[i] = a[i];
        }

        for (int i = low; i <= high; i++)
        {
            if (leftIndex > mid)
            {
                a[i] = temp[rightIndex++];
            }
            else if (rightIndex > high)
            {
                a[i] = temp[leftIndex++];
            }
            else if (temp[leftIndex] < temp[rightIndex])
            {
                a[i] = temp[leftIndex++];
            }
            else
            {
                a[i] = temp[rightIndex++];
            }
        }
    }
    private static void FastMerge(int[] a, int[] temp, int low, int mid, int high)
    {
        int leftIndex = low;
        int rightIndex = high;

        for (int i = low; i <= mid; i++)
        {
            temp[i] = a[i];
        }
        for (int i = high; i >= mid + 1; i--)
        {
            temp[mid + 1 + high - i] = a[i];
        }

        for (int i = low; i <= high; i++)
        {
            if (leftIndex > mid)
            {
                a[i] = temp[rightIndex--];
            }
            else if (temp[leftIndex] < temp[rightIndex])
            {
                a[i] = temp[leftIndex++];
            }
            else
            {
                a[i] = temp[rightIndex--];
            }
        }
    }
    private static void BlockMerge(int[] a, int M)
    {
        int N = a.Length;
        int len = N / M;
        int[] temp = new int[N];

        for (int i = 0; i < N; i += M)
        {
            InsertSortInMerge(a, i, i + len - 1);
            if (i > 0)
            {
                Merge(a, temp, 0, i - 1, i + len - 1);
            }
        }
    }
    public static void InsertSortInMerge(int[] a, int low, int high)
    {
        for (int i = low + 1; i <= high; i++)
        {
            for (int j = i; j > low && a[j] < a[j - 1]; j--)
            {
                int temp = a[j];
                a[j] = a[j - 1];
                a[j - 1] = temp;
            }
        }
    }
    public static Queue<int> QueueMerge(Queue<int> queueLeft, Queue<int> queueRight)
    {
        Queue<int> result = new Queue<int>();

        while (queueLeft.Count != 0 || queueRight.Count != 0)
        {
            if (queueLeft.Count == 0)
            {
                result.Enqueue(queueRight.Dequeue());
            }
            else if (queueRight.Count == 0)
            {
                result.Enqueue(queueLeft.Dequeue());
            }

            if (queueLeft.Peek() <= queueRight.Peek())
            {
                result.Enqueue(queueLeft.Dequeue());
                result.Enqueue(queueRight.Dequeue());
            }
            else
            {
                result.Enqueue(queueRight.Dequeue());
                result.Enqueue(queueLeft.Dequeue());
            }
        }
        return result;
    }
    //用泛型嵌套队列的队列
    public static Queue<Queue<int>> QueueSort(int[] a)
    {
        Queue<Queue<int>> resQueue = new Queue<Queue<int>>();

        foreach (int i in a)
        {
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(i);
            resQueue.Enqueue(queue);
        }

        while (resQueue.Count > 1)
        {
            Queue<int> queue1 = resQueue.Dequeue();
            Queue<int> queue2 = resQueue.Dequeue();
            resQueue.Enqueue(QueueMerge(queue1, queue2));
        }
        return resQueue;
    }
    //自然归并
    private static int FindIndex(int[] a, int start)
    {
        for (int i = start + 1; i < a.Length; i++)
        {
            if (a[i] < a[i - 1])
            {
                return i - 1;
            }
        }
        return a.Length - 1;
    }
    public static void NaturalMerge(int[] a)
    {
        if (a.Length < 2) return;
        int[] temp = new int[a.Length];
        int low = 0;
        int mid;
        int high;

        while (true)
        {
            mid = FindIndex(a, low);
            if (mid == a.Length - 1)
            {
                //low == 0，middle是最后的元素，代表该数组已排完序
                if (low == 0)
                {
                    break;
                }
                else
                {
                    //如果当前的low到最后一个全部有序，检验从0开始是否有序。如有，排序完成
                    low = 0;
                    continue;
                }
            }
            high = FindIndex(a, mid + 1);
            Merge(a, temp, low, mid, high);
            //重置low，以供下个分块使用
            if (high == a.Length - 1)
            {
                low = 0;
            }
            else
            {
                low = high + 1;
            }
        }

    }
    private static void ArrayRandom(int[] a, int min, int max)
    {
        Random random = new Random();
        for (int i = 0; i < a.Length; i++)
        {
            a[i] = random.Next(min, max + 1);
        }
    }
    private static void PrintIEnumerable(int[] a)
    {
        foreach (int i in a)
        {
            Console.Write(i + " ");
        }
    }
    public static int ReversedNo(int[] a){
        //Good explanation here: http://www.geeksforgeeks.org/counting-inversions/
        int[] copy = new int[a.Length];
        Array.Copy(a, copy, a.Length);

        int[] temp = new int[a.Length];

        return ReversedNo(copy, temp, 0, a.Length - 1);
    }
    public static int ReversedNo(int[] a, int[] temp, int low, int high){
        if(low >= high) return 0;
        int mid = low + (high - low) / 2;

        int inversion = ReversedNo(a, temp, low, mid);
        inversion += ReversedNo(a, temp, mid + 1, high);
        return inversion + MergeCount(a, temp, low, mid, high);
    }
    private static int MergeCount(int[] a, int[] temp, int low, int mid, int high){
        int leftIndex = low;
        int rightIndex = mid + 1;
        int aIndex = low;
        int times = 0;

        Array.Copy(a, temp, a.Length);

        while(leftIndex <= mid || rightIndex <= high){
            if(rightIndex > high){
                a[aIndex] = temp[leftIndex];
                leftIndex++;
            }else if(leftIndex > mid){
                a[aIndex] = temp[rightIndex];
                rightIndex++;
            }
            else if(temp[leftIndex] < temp[rightIndex]){
                a[aIndex] = temp[leftIndex];
                leftIndex++;
            }else{
                times += mid - leftIndex + 1;
                a[aIndex] = temp[rightIndex];
                rightIndex++;
            }
            aIndex++;
        }
        return times;
    }
}
public class LinkMergeSort{
    public class Link : IEnumerable
    {
        public class Node{
            public int item;
            public Node? next;
            public Node(int element){
                this.item = element;
                this.next = null;
            }
        }
        private int N = 0;
        public Node? head;
        public Node? tail; 

        public bool isEmpty(){
            return N == 0;
        }
        public int Size(){
            return N;
        }
        public void Add(int element){
            Node node = new Node(element);
            if(isEmpty()){
                tail = node;
                head = node;
            }else{
                tail.next = node;
                tail = node;
            }
            N++;
        }
        public void AddArray(int[] elements){
            foreach(int i in elements){
                Add(i);
            }
        }
        public IEnumerator GetEnumerator()
        {
            Node node = head;
            for(int i = 0; i < N; i++){
                yield return node.item;
                node = node.next;
            }
        }
    }
    public static Link.Node? LinkSort(Link.Node head){
        if(head == null || head.next == null){
            return head;
        }
        Link.Node? linkLeft = head;
        Link.Node? linkRight = CutLink(linkLeft);
        
        Link.Node? Lleft = LinkSort(linkLeft);
        Link.Node? Lright = LinkSort(linkRight);
        return LinkMerge(Lleft, Lright);
    }
    public static Link.Node? CutLink(Link.Node head){
        if(head == null || head.next == null){
            return head;
        }

        //定义快慢指针用以表示左右分量；慢指针是第二个链表的头节点
        Link.Node? linkFast = head; 
        Link.Node? linkSlow = head;
        //要切的点在慢指针左边，故须保存它的地址
        Link.Node? preNode = linkSlow;

        while(linkFast != null && linkFast.next != null){
            linkFast = linkFast.next.next;
            preNode = linkSlow;
            linkSlow = linkSlow.next;
        }
        //将两个子节点切掉
        preNode.next = null;
        return linkSlow;
    }
    public static Link.Node? LinkMerge(Link.Node left, Link.Node right){
        Link.Node fakeHead = new Link.Node(0);
        Link.Node tail = fakeHead;
        //链表不像数组，当一个排完后可以直接将另一个链接上
        while(left != null && right != null){
            if(left.item <= right.item){
                tail.next = left;
                left = left.next;
            }else{
                tail.next = right;
                right = right.next;
            }
            tail = tail.next;
        }
        //必定有一个空节点，一个非空节点
        if(left == null){
            tail.next = right;
        }else{
            tail.next = left;
        }
        return fakeHead.next;
    }
    private static void PrintIEnumerable(Link.Node? a)
    {
        while(a != null){
            Console.Write(a.item + " ");
            a = a.next;
        }
    }
}