using System;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Xml;

namespace p1_3
{
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
    public class Stack<T>
    {
        private class Node
        {
            //里面的变量要public，否则"."运算探测不到
            public T? item;
            public Node? next;
        }
        //定义头节点，该节点只有指针
        private Node? head;
        private int N;

        public void push(T item)
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
        public T? peek(){
            return head!.item;
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
                if(stack.isEmpty()){
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
                }else{
                    stack.push(c);
                }

            }
            return stack.isEmpty();
        }
    }
    
}