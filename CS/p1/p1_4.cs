using System.Configuration.Assemblies;

namespace p1;

public class Search
{
    //二分查找输入数组必须有序
    public static int binarySearch(int num, int[] ints, int low, int high)
    {
        while (low <= high)
        {
            int mid = low + (high - low) / 2;
            if (num < ints[mid])
            {
                high = mid - 1;
            }
            else if (num > ints[mid])
            {
                low = mid + 1;
            }
            else
            {
                return mid;
            }
        }
        return -1;
    }
    public static int binaryRightSearch(int num, int[] ints, int low, int high)
    {
        while (low <= high)
        {
            int mid = low + (high - low) / 2;
            if (num > ints[mid])
            {
                high = mid - 1;
            }
            else if (num < ints[mid])
            {
                low = mid + 1;
            }
            else
            {
                return mid;
            }
        }
        return -1;
    }
    public static int arrayMinerElement(int[] ints)
    {
        int N = ints.Length;
        int i = N / 2;
        if (N == 1) return ints[i];
        if (N == 2)
        {
            if (ints[0] < ints[1])
            {
                return ints[0];
            }
            else
            {
                return ints[1];
            }
        }
        while (i > 0 && i < N - 1)
        {
            if (ints[i] < ints[i + 1] && ints[i] < ints[i - 1])
            {
                return ints[i];
            }
            else if (ints[i - 1] < ints[i + 1])
            {
                i = (i - 1) / 2;
            }
            else
            {
                i = (i + N) / 2;
            }
        }
        if (ints[0] < ints[1])
        {
            return ints[0];
        }
        else if (ints[ints.Length - 1] < ints[ints.Length - 2])
        {
            return ints[ints.Length - 1];
        }
        return -1;
    }
    public static bool findInt(int[] a, int n)
    {
        int low = 0;
        int high = a.Length - 1;
        int max;
        int maxPos = 0;

        while (low <= high)
        {
            int mid = low + (high - low) / 2;
            if (a[mid] < a[mid + 1])
            {
                high = mid - 1;
            }
            else if (a[mid] < a[mid - 1])
            {
                low = mid + 1;
            }
            else
            {
                max = a[mid];
                maxPos = mid;
                break;
            }
        }

        if(binarySearch(n, a, low, maxPos) == -1 && binaryRightSearch(n, a, maxPos, high) == -1){
            return false;
        }else{
            return true;
        }
    }
    class Sum
    {
        public static int fourSum(int[] ints)
        {
            int count = 0;
            int len = ints.Length;
            for (int i = 0; i < len; i++)
            {
                for (int j = i + 1; j < len; j++)
                {
                    for (int k = j + 1; k < len; k++)
                    {
                        if (Search.binarySearch(-ints[i] - ints[j] - ints[k], ints, 0, ints.Length) > k)
                        {
                            count++;
                        }
                    }
                }
            }
            return count;
        }
        public static double[] closestDouble(double[] doubles)
        {
            double[] res = new double[2];
            int N = doubles.Length;
            Array.Sort(doubles);
            double min = doubles[N - 1] - doubles[0];
            for (int i = 0; i < N - 1; i++)
            {
                if (doubles[i + 1] - doubles[i] < min)
                {
                    min = doubles[i + 1] - doubles[i];
                    res[0] = doubles[i];
                    res[1] = doubles[i + 1];
                }
            }
            return res;
        }

        static void Main()
        {
            int[] ints = {1, 2, 3, 4, -1, -2, -3};
            double[] doubles = { -5.2, 9.4, 20, -10, 21.1, 40, 50, -20 };
            // Array.Sort(ints);
            Console.WriteLine(Search.findInt(ints, -2));
        }
    }
}