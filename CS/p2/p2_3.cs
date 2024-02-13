namespace p2;

public class QuickSort
{
    public static void Exchange(IComparable[] a, int i, int j){
        IComparable temp = a[i];
        a[i] = a[j];
        a[j] = temp;
    }
    public static void Sort(IComparable[] a){
        if(a.Length < 2) return;
        Random random = new Random();
        random.Shuffle<IComparable>(a);

        Sort(a, 0, a.Length - 1);
    }
    public static void Sort(IComparable[] a, int low, int high){
        if(low >= high) return;
        int mid = Partition(a, low, high);
        Sort(a, low, mid - 1);
        Sort(a, mid + 1, high);
    }
    public static int Compare(IComparable a, IComparable b){
        return a.CompareTo(b);
    }
    public static int Partition(IComparable[] a, int low, int high){
        int midIndex = low + (high - low) / 2;
        IComparable mid = a[midIndex];
        int left = low;
        int right = high + 1;

        if(Compare(a[low], mid) > 0){
            Exchange(a, low, midIndex);
        }
        if(Compare(a[low], a[high]) > 0){
            Exchange(a, low, high);
        }
        if(Compare(mid, a[high]) > 0){
            Exchange(a, midIndex, high);
        }
        midIndex = low;
        mid = a[midIndex];

        while(true){
            while(Compare(a[++left], mid) < 0);
            while(Compare(a[--right], mid) > 0);
            if(left >= right) break;
            Exchange(a, left, right);
        }
        Exchange(a, midIndex, right);
        midIndex = right;
        return midIndex;
    }
    public static void Quick3Sort(IComparable[] a){
        if(a.Length < 2) return;
        Random random = new Random();
        random.Shuffle(a);
        
        Quick3Sort(a, 0, a.Length - 1);
    }
    public static void Quick3Sort(IComparable[] a, int low, int high){
        if(low >= high) return;
        int vIndex = low;
        IComparable value = a[vIndex];
        int left = low;
        int i = low + 1;
        int right = high;

        while(i <= right){
            int cmpRes =  Compare(a[i], value);
            if(cmpRes < 0){
                Exchange(a, i++, left++);
            }else if(cmpRes > 0){
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

    public static void TwoValuesSort(IComparable[] a){
        int low = 0;
        int high = a.Length - 1;
        int left = low;
        int right = high;
        int i = low + 1;
        IComparable value = a[0];

        while(i <= high){
            int cmpRes = QuickSort.Compare(a[i], value);
            if(cmpRes < 0){
                QuickSort.Exchange(a, i++, left++);
            }else if(cmpRes > 0){
                QuickSort.Exchange(a, i++, right--);
            }else{
                i++;
            }
        }
    }
        private static void PrintIEnumerable(IComparable[] a)
    {
        foreach (IComparable i in a)
        {
            Console.Write(i + " ");
        }
        Console.WriteLine("\n");
    }
}