using System;

namespace MyApp // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int a;
            int b;
            Console.Write("input:");
            //用户可能输入任何东西，所以得加上错误转换
            a = Convert.ToInt32(Console.ReadLine());
            Console.Write("input second number:");
            b = Convert.ToInt32(Console.ReadLine());
            int res = a * b;
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
    }
}