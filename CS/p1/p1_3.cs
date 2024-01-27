using System.Collections;
using System.Reflection;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;

namespace p1;

public class FixedCapacityStackOfThings
{
    private string[] a;
    private int N = 5;
    private int max;
    public FixedCapacityStackOfThings(int cap)
    {
        a = new string[cap];
        max = cap;
    }
    public bool isFull()
    {
        return N >= (max - 1);
    }
}
//继承迭代器接口。由于迭代器可能返回空类型，故泛型需要可空
public class Stack<T> : IEnumerable<T?>
{
    //protected可以为该类与子类访问
    protected class Node
    {
        //里面的变量要public，否则"."运算探测不到
        public T? item;
        public Node? next;
    }
    //定义节点指针
    protected Node? head;
    protected Node? tail;
    protected Node? pointer;
    protected int N;

    public void push(T? item)
    {
        Node node = new Node();
        node.next = head;
        node.item = item;
        head = node;
        N++;
        if(N == 1){
            tail = head;
        }
    }

    public T? pop()
    {
        T? item = head!.item;
        head = head.next;
        N--;
        return item;
    }
    public bool isEmpty()
    {
        return N == 0;
    }
    public int size()
    {
        return N;
    }
    public T? peek()
    {
        return head!.item;
    }

    public IEnumerator<T?> GetEnumerator()
    {
        pointer = head;
        if (pointer == null)
        {
            yield return pointer!.item;
        }
        for (int i = 0; i < N; i++)
        {
            yield return pointer!.item;
            pointer = pointer.next;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

}
public class Deque<T>: Stack<T>{
    public T? deleteTail(){
        pointer = head;
        T? res;
        if(pointer == null) return default;
        if(pointer.next == null){
            res = pop();
            return res;
        }
        while(pointer.next!.next != null){
            pointer = pointer.next;
        }
        res = pointer.next.item;
        pointer.next = null;
        N--;
        return res;
    }
    public T? delete(int k){
        pointer = head;
        if(k > N) return default;
        if(k == 1) return pop();
        if(k == N) return deleteTail();
        for(int i = 0; i < k - 2; i++){
            pointer = pointer!.next;
        }
        T? res = pointer.next.item;
        pointer.next = pointer.next.next;
        N--;
        return res;
    }
    public void enquene(T? element){
        if(N == 0) {
            push(element);
        }else{
            Node node = new Stack<T>.Node();
            node.item = element;
            tail.next = node;
            tail = node;
            N++;
        }
    }
}
public class ArrayQuene<T>: IEnumerable<T?>
{
    private static int N = 2;
    private T[] quene = new T[N];
    private int num = 0;
    public bool isEmpty()
    {
        return num == 0;
    }
    public int size()
    {
        return num;
    }
    public void enquene(T element)
    {
        
        quene[num] = element;
        num++;
        if(num * 4 >= N){
            T[] temp = new T[N * 2];
            for(int i = 0; i < num; i++){
                temp[i] = quene[i];
            }
            //将新数组的地址传给老数组
            quene = temp;
            N *= 2;
        }
    }
    public T? dequene()
    {
        if(isEmpty()) return default;
        T? res = quene[0];
        for (int i = 1; i < N - 1; i++)
        {
            quene[i - 1] = quene[i];
        }
        num--;
        return res;
    }
    public T? GetValue(int index){
        return quene[index - 1];
    }

    public IEnumerator<T?> GetEnumerator()
    {
        if(num == 0) yield return default;
        for(int i = 0; i < num; i++){
            yield return quene[i];
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        throw new NotImplementedException();
    }
}
public class CircleQuene<T>{
    protected class Node
    {
        public T? item;
        public Node? next;
    }
    private Node? last;
    private int N;
    public bool isEmpty(){
        return N == 0;
    }
    public int size(){
        return N;
    }
    public void enquene(T? element){
        Node node = new Node();
        node.item = element;
        if(N == 0){
            last = node;
            node.next = last;
        }else{
            node.next = last.next;
            last.next = node;
            last = node;
        }
        N++;
    }
    public T? dequene(){
        T? res;
        if(N == 1){
            res = last.item;
            last = null;
        }else{
            res = last.next.item;
            last.next = last.next.next;
        }
        N--;
        return res;
    }
}
class StringCopy
{
    private static Stack<string> temp = new Stack<string>();
    private static Stack<string> strings = new Stack<string>();

    public static Stack<string> Copy(Stack<string> stack)
    {
        foreach (string? c in stack)
        {
            temp.push(c);
        }

        foreach (string? c in temp)
        {
            strings.push(c);
        }
        return strings;
    }
}
class FindLink{
    public static bool Find(Link<string> link, string s){
        int size = link.size();
        foreach(string? c in link){
            if(s.Equals(c)){
                return true;
            }
        }
        return false;
    }
    public static void remove(Link<string> link, string key){
        int count = 1;
        foreach(string? c in link){
            if(key.Equals(c)){
                link.delete(count);
            }
            count++;
        }
    }
    public static int max(Link<int> link){
        if(link == null) return 0;
        int max = 0;
        foreach(int c in link){
            if(c > max) max = c;
        }
        return max;
    }
    
}
class Parentheses
{
    public static bool completed(string? s)
    {
        if (s == "") return true;
        Stack<char> stack = new Stack<char>();
        foreach (char c in s!)
        {
            if (stack.isEmpty())
            {
                stack.push(c);
                continue;
            }
            char i = stack.peek();
            if (i.Equals('(') && c.Equals(')') ||
                i.Equals('[') && c.Equals(']') ||
                i.Equals('{') && c.Equals('}'))
            {
                stack.pop();
                continue;
            }
            else
            {
                stack.push(c);
            }

        }
        return stack.isEmpty();
    }
}
class Fix
{
    //操作数栈
    private static Stack<string>? operand = new Stack<string>();
    //运算符栈
    private static Stack<string>? operation = new Stack<string>();

    public static string leftToMid(string s)
    {
        string[] res = s.Split(" ");

        foreach (string c in res)
        {
            if (c.Equals("("))
            {
                continue;
            }
            else if (c.Equals("+") || c.Equals("-") || c.Equals("*") || c.Equals("/"))
            {
                operation.push(c);
            }
            else if (c.Equals(")"))
            {
                string? signal = operation.pop();
                string? value2 = operand.pop();
                string? value1 = operand.pop();
                string newValue = "( " + value1 + " " + signal + " " + value2 + " )";
                operand.push(newValue);
            }
            else
            {
                operand.push(c);
            }
        }
        return operand.pop();
    }

    public static string InfixToPostfix(string s)
    {
        string[] res = s.Split(" ");

        foreach (string c in res)
        {
            if (c.Equals("("))
            {
                continue;
            }
            else if (c.Equals("+") || c.Equals("-") || c.Equals("*") || c.Equals("/"))
            {
                operation.push(c);
            }
            else if (c.Equals(")"))
            {
                string? signal = operation.pop();
                string? value2 = operand.pop();
                string? value1 = operand.pop();
                string newValue = value1 + " " + value2 + " " + signal;
                operand.push(newValue);
            }
            else
            {
                operand.push(c);
            }
        }
        return operand.pop();
    }
    public static int EvaluatePostfix(string s)
    {
        string a = InfixToPostfix(s);
        string[] res = a.Split(" ");

        int? value;
        int? value1;
        int? value2;
        foreach (string c in res)
        {
            if (c.Equals("+"))
            {
                value2 = int.Parse(operand.pop());
                value1 = int.Parse(operand.pop());
                value = value1 + value2;
                operand.push(value.ToString());
            }
            else if (c.Equals("-"))
            {
                value2 = int.Parse(operand.pop());
                value1 = int.Parse(operand.pop());
                value = value1 - value2;
                operand.push(value.ToString());
            }
            else if (c.Equals("*"))
            {
                value2 = int.Parse(operand.pop());
                value1 = int.Parse(operand.pop());
                value = value1 * value2;
                operand.push(value.ToString());
            }
            else if (c.Equals("/"))
            {
                value2 = int.Parse(operand.pop());
                value1 = int.Parse(operand.pop());
                value = value1 / value2;
                operand.push(value.ToString());
            }
            else
            {
                operand.push(c);
            }
        }
        return int.Parse(operand.pop());
    }

}
class Test{
    static void Main(){
        // Link<string> ints = new Link<string>();
        // ints.push("10");
        // ints.push("20");
        // ints.push("30");
        // FindLink.remove(ints, "30");
        // Console.WriteLine(FindLink.Find(ints, "30"));
        Link<int> ints = new Link<int>();
        ints.push(10);
        ints.push(20);
        ints.enquene(30);
        ints.enquene(50);
        Console.WriteLine(ints.pop());
    }
}