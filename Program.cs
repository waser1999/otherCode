//引入namespace
using System;
//本namespace提供了list数组类
using System.Collections.Generic;

namespace MyApp // Note: actual namespace depends on the project name.
{
    //声明一个类，public可以外部访问，private不能（默认）
    //static关键字可以在一次运行后继续计数
    class cat{
        public string? name;
        public int age;

        public void meow(){
            Console.WriteLine("meow!");
        }
    }
    internal class Program
    {   
        static void Main(string[] args)
        {
            int a;
            int b;
            //数组定义方式，与C不同
            string[] names = {
                "jobs",
                "cook",
                "nahida"
            };
            //定义一个空数组，预定义后不能更改长度
            string[] movies = new string[4];
            //实例化一个类以及设置属性
            cat cat1 = new cat();
            cat1.age = 12;
            //定义一个list
            List<string> newList = new List<string>();
            //向list中添加、删除项
            newList.Add("a");
            newList.Remove("a");
            //显示list中项的数目
            a = newList.Count;

            Console.Write("input:");
            //用户可能输入任何东西，所以得加上错误转换
            a = Convert.ToInt32(Console.ReadLine());
            Console.Write("input second number:");
            b = Convert.ToInt32(Console.ReadLine());
            int res = multiply(a, b);
            Console.WriteLine("res = " + res);

            //命令行打印
            Console.WriteLine("Hello, what's your name?");
            //在变量后打问号，代表此字符串可空
            string? userName = Console.ReadLine();
            Console.WriteLine(userName);
            for (int i = 0; i < 2; i++)
            {
                Console.WriteLine(i);
            }

            //关闭程序前悬停
            Console.ReadKey();
        }
        //函数定义
        static int multiply(int a, int b){
            return a * b;
        }
    }
}