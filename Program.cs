using System;

namespace MyApp // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Beep();
            Console.Title = "Formula Studio.";
            //命令行打印
            Console.WriteLine("Hello, what's your name?");
            //在变量后打问号，代表此字符串可空
            string? userName = Console.ReadLine();
            Console.WriteLine(userName);

            //关闭程序前悬停
            Console.ReadKey();
        }
    }
}