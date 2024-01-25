using System.Collections;
using System.Reflection;
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
public class Stack<T>: IEnumerable<T>
{
    private class Node
    {
        //里面的变量要public，否则"."运算探测不到
        public T? item;
        public Node? next;
    }
    //定义节点指针
    private Node? head;
    private Node? pointer;
    private int N;

    public void push(T? item)
    {
        Node node = new Node();
        node.next = head;
        node.item = item;
        head = node;
        N++;
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

    public IEnumerator<T> GetEnumerator()
    {
        pointer = head;
        if(pointer == null){
            yield return pointer.item;
        }
        for(int i = 0; i < N; i ++){
            yield return pointer.item;
            pointer = pointer.next;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    // public Stack<T>? copy(Stack<T> origin){
    //     Stack<T> copy = new Stack<T>();
    //     foreach(T t in origin){

    //     }
    // }
    static void Main(){
        Stack<int> ints = new Stack<int>();
        ints.push(10);
        ints.push(20);
        foreach(int i in ints){
            Console.WriteLine(i);
        }
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
class Fix{
    //操作数栈
    private static Stack<string>? operand = new Stack<string>();
    //运算符栈
    private static Stack<string>? operation = new Stack<string>();

    public static string leftToMid(string s){
        string[] res = s.Split(" ");

        foreach(string c in res){
            if(c.Equals("(")){
                continue;
            }else if(c.Equals("+") || c.Equals("-") || c.Equals("*") || c.Equals("/")){
                operation.push(c);
            }else if(c.Equals(")")){
                string? signal = operation.pop();
                string? value2 = operand.pop();
                string? value1 = operand.pop();
                string newValue = "( " + value1 + " " + signal + " " + value2 + " )";
                operand.push(newValue);
            }else{
                operand.push(c);
            }
        }
        return operand.pop();
    }

    public static string InfixToPostfix(string s){
        string[] res = s.Split(" ");

        foreach(string c in res){
            if(c.Equals("(")){
                continue;
            }else if(c.Equals("+") || c.Equals("-") || c.Equals("*") || c.Equals("/")){
                operation.push(c);
            }else if(c.Equals(")")){
                string? signal = operation.pop();
                string? value2 = operand.pop();
                string? value1 = operand.pop();
                string newValue = value1 + " " + value2 + " " + signal;
                operand.push(newValue);
            }else{
                operand.push(c);
            }
        }
        return operand.pop();
    }
    public static int EvaluatePostfix(string s){
        string a = InfixToPostfix(s);
        string[] res = a.Split(" ");

        int? value;
        int? value1;
        int? value2;
        foreach(string c in res){
            if(c.Equals("+")){
                value2 = int.Parse(operand.pop());
                value1 = int.Parse(operand.pop());
                value = value1 + value2;
                operand.push(value.ToString());
            }else if(c.Equals("-")){
                value2 = int.Parse(operand.pop());
                value1 = int.Parse(operand.pop());
                value = value1 - value2;
                operand.push(value.ToString());
            }else if(c.Equals("*")){
                value2 = int.Parse(operand.pop());
                value1 = int.Parse(operand.pop());
                value = value1 * value2;
                operand.push(value.ToString());
            }else if(c.Equals("/")){
                value2 = int.Parse(operand.pop());
                value1 = int.Parse(operand.pop());
                value = value1 / value2;
                operand.push(value.ToString());
            }else{
                operand.push(c);
            }
        }
        return int.Parse(operand.pop());
    }
    
}
