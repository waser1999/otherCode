namespace p2;

public class ArraySort
{
    private static void Exchange(int[] ints, int a, int b){
        int swap = ints[a];
        ints[a] = ints[b];
        ints[b] = swap;
    }
    public static void ChooseSort(int[] ints){
        int N = ints.Length;
        for(int i = 0; i < N; i ++){
            int min = ints[i];
            for(int j = i; j < N; j ++){
                if(ints[j] < min){
                    min = ints[j];
                }
                //写在里面负担会上升
                Exchange(ints, i, j);
            }
        }
    }
    public static void InsertSort(int[] ints){
        int N = ints.Length;
        for(int i = 1; i < N; i ++){
            //比较必须写在for语句上，否则会耗资源
            for(int j = i; j > 0 && ints[j - 1] > ints[j]; j--){
                Exchange(ints, j, j - 1);
            }
        }
    }
    public static void InsertSortWithSentinel(int[] ints){
        int m = 0;
        int min = ints[m];
        int N = ints.Length;
        for(int k = 1; k < N; k++){
            if(ints[k] < min){
                min = ints[k];
                m = k;
            }
        }
        Exchange(ints, m, 0);

        for(int i = 1; i < N; i ++){
            for(int j = i; ints[j - 1] > ints[j]; j--){
                Exchange(ints, j, j - 1);
            }
        }
    }
    public static void InsertSortWithoutExchange(int[] ints){
        int N = ints.Length;
        for(int i = 1; i < N; i ++){
            int temp = ints[i];
            int j;

            for(j = i; j > 0 && ints[j - 1] > temp; j--){
                ints[j] = ints[j - 1];    
            }
            ints[j] = temp;
        }
    }
    public static void ShellSort(int[] ints){
        int N = ints.Length;
        int h = 0;
        while(h < N / 3){
            h = 3 * h + 1;
        }

        while(h >= 1){
            for(int i = h; i < N; i++){
                for(int j = i; j >= h && ints[j] < ints[j - h]; j -= h){
                    Exchange(ints, j, j - h);
                }
            }
            h /= 3;
        }
    }
    public static void ShellStackSort(int[] ints){
        int N = ints.Length;
        int h = 0;
        Stack<int> stack = new Stack<int>();

        while(h < N / 3){
            h = 3 * h + 1;
            stack.Push(h);
        }

        while(stack.Count != 0){
            h = stack.Pop();
            for(int i = h; i < N; i++){
                for(int j = i; j >= h && ints[j] < ints[j - h]; j -= h){
                    Exchange(ints, j, j - h);
                }
            }
        }
    }
    static void Main(){
        int[] ints = {3, 6, 1, 0, 4, 7, 9, 18, 87, 34, 23};
        ArraySort.InsertSortWithoutExchange(ints);
        for(int i = 0; i < ints.Length; i++){
            Console.Write(ints[i] + " ");
        }
    }
}
