namespace p2;

public class QuickSort
{
    public static void Exchange(int[] a, int i, int j){
        int temp = a[i];
        a[i] = a[j];
        a[j] = temp;
    }
    public static void Sort(int[] a){
        if(a.Length < 2) return;
        Random random = new Random();
        random.Shuffle(a);

        Sort(a, 0, a.Length - 1);
    }
    public static void Sort(int[] a, int low, int high){
        if(low >= high) return;
        int mid = Partition(a, low, high);
        Sort(a, low, mid - 1);
        Sort(a, mid + 1, high);
    }
    public static int Partition(int[] a, int low, int high){
        int midIndex = low + (high - low) / 2;
        int mid = a[midIndex];
        int left = low;
        int right = high + 1;

        if(a[low] > mid){
            Exchange(a, low, midIndex);
        }
        if(a[low] > a[high]){
            Exchange(a, low, high);
        }
        if(mid > a[high]){
            Exchange(a, midIndex, high);
        }
        midIndex = low;
        mid = a[midIndex];

        while(true){
            while(a[++left] < mid);
            while(a[--right] > mid);
            if(left >= right) break;
            Exchange(a, left, right);
        }
        Exchange(a, midIndex, right);
        midIndex = right;
        return midIndex;
    }
    public static void Quick3Sort(int[] a){
        if(a.Length < 2) return;
        Random random = new Random();
        random.Shuffle(a);
        
        Quick3Sort(a, 0, a.Length - 1);
    }
    public static void Quick3Sort(int[] a, int low, int high){
        if(low >= high) return;
        int vIndex = low;
        int value = a[vIndex];
        int left = low;
        int i = low + 1;
        int right = high;

        while(i <= right){
            if(a[i] < value){
                Exchange(a, i++, left++);
            }else if(a[i] > value){
                Exchange(a, i, right--);
            }else{
                i++;
            }
        }
        
        Quick3Sort(a, low, left - 1);
        Quick3Sort(a, right + 1,high);
    }
}
public class QuickSortPractice{

    public static void TwoValuesSort(int[] a){
        int low = 0;
        int high = a.Length - 1;
        int left = low;
        int right = high;
        int i = low + 1;
        int value = a[0];

        while(i <= high){
            if(a[i] < value){
                QuickSort.Exchange(a, i++, left++);
            }else if(a[i] > value){
                QuickSort.Exchange(a, i++, right--);
            }else{
                i++;
            }
        }
    }
    
    private static void PrintIEnumerable(int[] a)
    {
        foreach (int i in a)
        {
            Console.Write(i + " ");
        }
        Console.WriteLine("\n");
    }
    static void Main(){
        int[] ints = {0,0,0,1,2,4,0,2,1,0};
        QuickSort.Sort(ints);
        PrintIEnumerable(ints);
    }
}