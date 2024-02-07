using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace p2;

public class MergeSort
{
    public static void TopDownSort(int[] a){
        int[] temp = new int[a.Length];

        TopDownSort(a, temp, 0, a.Length - 1);
    }
    public static void TopDownSort(int[] a, int[] temp, int low, int high){
        if(high <= low) return;
        int mid = low + (high - low) / 2;
        TopDownSort(a, temp, low, mid);
        TopDownSort(a, temp, mid + 1, high);
        //性能优化：如果左边小于右边就不排
        if(a[mid] < a[mid + 1]){
            return;
        }
        //性能优化：小数组使用插入排序
        else if(high - low + 1 > 15){
            Merge(a, temp, low, mid, high);
        }else{
            InsertSortInMerge(a, low, high);
        }
        
    }
    public static void BottomUpSort(int[] a){
        int N = a.Length;
        int[] temp = new int[N];
        //确定合并间隔
        for(int sz = 1; sz <= N; sz *= 2){
            //将每两个块合并
            for(int j = 0; j < N - sz; j += sz * 2){
                Merge(a, temp, j, j + sz - 1, Math.Min(j + 2 * sz - 1, N - 1));
            }
        }
    }
    private static void Merge(int[] a, int[] temp, int low, int mid, int high){
        int leftIndex = low;
        int rightIndex = mid + 1;
        
        for(int i = low; i <= high; i++){
            temp[i] = a[i];
        }

        for(int i = low; i <= high; i++){
            if(leftIndex > mid){
                a[i] = temp[rightIndex++];
            }else if(rightIndex > high){
                a[i] = temp[leftIndex++];
            }else if(temp[leftIndex] < temp[rightIndex]){
                a[i] = temp[leftIndex++];
            }else{
                a[i] = temp[rightIndex++];
            }
        }
    }
    private static void FastMerge(int[] a, int[] temp, int low, int mid, int high){
        int leftIndex = low;
        int rightIndex = high;
        
        for(int i = low; i <= mid; i++){
            temp[i] = a[i];
        }
        for(int i = high; i >= mid + 1; i--){
            temp[mid + 1 + high - i] = a[i];
        }

        for(int i = low; i <= high; i++){
            if(leftIndex > mid){
                a[i] = temp[rightIndex--];
            }else if(temp[leftIndex] < temp[rightIndex]){
                a[i] = temp[leftIndex++];
            }else{
                a[i] = temp[rightIndex--];
            }
        }
    }
    private static void SpaceMerge(int[] a, int M){
        int N = a.Length;
        int len = N / M;
        int[] temp = new int[N];

        for(int i = 0; i < N; i += M){
            InsertSortInMerge(a, i, i + len - 1);
            if(i > 0){
                Merge(a, temp, 0, i - 1, i + len - 1);
            }
        }
    }
    public static void InsertSortInMerge(int[] a, int low, int high){
        for(int i = low + 1; i <= high; i++){
            for(int j = i; j > low && a[j] < a[j - 1]; j--){
                int temp = a[j];
                a[j] = a[j - 1];
                a[j - 1] = temp;
            }
        }
    }
    public static Queue<int> QueueMerge(Queue<int> queueLeft, Queue<int> queueRight){
        Queue<int> result = new Queue<int>();

        while(queueLeft.Count != 0 || queueRight.Count != 0){
            if(queueLeft.Count == 0){
                result.Enqueue(queueRight.Dequeue());
            }else if(queueRight.Count == 0){
                result.Enqueue(queueLeft.Dequeue());
            }
            
            if(queueLeft.Peek() <= queueRight.Peek()){
                result.Enqueue(queueLeft.Dequeue());
                result.Enqueue(queueRight.Dequeue());
            }else{
                result.Enqueue(queueRight.Dequeue());
                result.Enqueue(queueLeft.Dequeue());
            }
        }
        return result;
    }
    //用泛型嵌套队列的队列
    public static Queue<Queue<int>> QueueSort(int[] a){
        Queue<Queue<int>> resQueue = new Queue<Queue<int>>();

        foreach(int i in a){
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(i);
            resQueue.Enqueue(queue);
        }

        while(resQueue.Count > 1){
            Queue<int> queue1 = resQueue.Dequeue();
            Queue<int> queue2 = resQueue.Dequeue();
            resQueue.Enqueue(QueueMerge(queue1, queue2));
        }
        return resQueue;
    }
    private static void ArrayRandom(int[] a, int min, int max){
        Random random = new Random();
        for(int i = 0; i < a.Length; i++){
            a[i] = random.Next(min, max + 1);
        }
    }
    static void Main(){
        int[] ints = new int[10];
        ArrayRandom(ints, 0, 100);
        BottomUpSort(ints);
        foreach(int i in ints){
            Console.Write(i + " ");
        }
    }
}
