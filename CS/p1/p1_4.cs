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
    public static int FibonacciSearch(int num, int[] a){
        int low = 0;
        int high = a.Length - 1;
        int fn = 1;
        int fn_1 = 0;
        int temp;
        //求最大fn
        while(fn < high){
            temp = fn + fn_1;
            fn_1 = fn;
            fn = temp;
        }

        while(low <= high){
            //每次改上下限均产生新的n-1和fn-2
            while(fn_1 > 0 && fn >= high - low){
                temp = fn_1;
                fn_1 = fn - fn_1;
                fn = temp;
            }

            int mid = low + fn_1;
            if(a[mid] < num){
                low = mid + 1;
            }else if(a[mid] > num){
                high = mid - 1;
            }else{
                return mid;
            }
        }
        return -1;
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

}
class EggThrow{
    public static int ThrowEgg(int[] floors){
        int low = 0;
        int high  = floors.Length - 1;

        while(low <= high){
            int mid = low + (high - low) / 2;
            if(low == high){
                return low + 1;
            }
            else if(floors[mid] == 0){
                low = mid + 1;
            }else if(floors[mid] == 1){
                high = mid - 1;
            }
        }
        return high + 1;
    }
    public static int Throw2EggSqrtN(int[] floors){
        const int INTECT = 0;
        int sqrtN = (int)Math.Sqrt(floors.Length);
        int firstEggTimes = 0;
        int secondEggBegin = 0;

        int high = 0;
        while(high < floors.Length - 1 && floors[high] == INTECT){
            firstEggTimes++;
            high = firstEggTimes * sqrtN;
        }
        if(firstEggTimes != 0){
            secondEggBegin = firstEggTimes - 1;
        }else if(high > floors.Length){
            high = floors.Length;
        }

        for(int i = secondEggBegin * sqrtN; i < high; i++){
            if(floors[i] == 1) return i;
        }
        if(floors[floors.Length - 1] == 1) return  floors.Length - 1;
        return -1;
    }
    public static int Throw2EggSqrtF(int[] floors){
        const int INTECT = 0;
        int n = 0;
        int sum = 0;
        int low = 0;

        while(sum < floors.Length && floors[sum] == INTECT){
            sum += n;
            n ++;
        }

        if(sum > floors.Length){
            sum = floors.Length;
        }else if(sum != 0){
            low = sum - n + 1;
        }else if(floors[sum] == 1){
            return 0;
        }

        for(int i = low; i < sum; i++){
            if(floors[i] == 1) return i;
        }
        return -1;
    }
}
class StackQuene<T>{
    private Stack<T> pushStack = new Stack<T>();
    private Stack<T> popStack = new Stack<T>();

    public bool isEmpty(){
        return pushStack.isEmpty();
    }
    public int size(){
        return pushStack.size();
    }
    public void enquene(T element){
        pushStack.push(element);
    }
    public T dequene(){
        int size = pushStack.size();
        for(int i = 0; i < size; i++){
            popStack.push(pushStack.pop());
        }
        return popStack.pop();
    }
}
class QueneStack<T>{
    private Queue<T> quene = new Queue<T>();
    public bool isEmpty(){
        return quene.Count == 0;
    }
    public int size(){
        return quene.Count;
    }
    public void push(T element){
        quene.Enqueue(element);
    }
    public T pop(){
        int size = this.size();
        for(int i = 0; i < size - 1; i++){
            quene.Enqueue(quene.Dequeue());
        }
        return quene.Dequeue();
    }
}
class Steque<T>{
    Stack<T> stack = new Stack<T>();
    Stack<T> tempStack = new Stack<T>();
    public int size(){
        return stack.size();
    }
    public void push(T element){
        stack.push(element);
    }
    public T pop(){
        return stack.pop();
    }
    public void enquene(T element){
        int size = stack.size();
        for(int i = 0; i < size; i ++){
            tempStack.push(stack.pop());
        }
        stack.push(element);
        for(int i = 0; i < size; i ++){
            stack.push(tempStack.pop());
        }
    }
}
class NewDeque<T>{
    Stack<T> stack = new Stack<T>();
    Steque<T> steque = new Steque<T>();
    public void pushLeft(T element){
        steque.push(element);
    }
    public T popLeft(){
        return steque.pop();
    }
    public void pushRight(T element){
        steque.enquene(element);
    }
    public T popRight(){
        int size = steque.size();
        T res;
        for(int i = 0; i < size; i++){
            stack.push((steque.pop()));
        }
        res = stack.pop();
        for(int i = 0; i < size - 1; i++){
            steque.push(stack.pop());
        }
        return res;
    }
}