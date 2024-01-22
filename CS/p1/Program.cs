using System;
using System.Globalization;

namespace p1{
    class Answer{
        public static string exr1(int n){
            if(n<=0) return "";
            return exr1(n - 3) + n + exr1(n - 2) + n;
        }

        public static int mystery(int a, int b){
            if(b == 0) return 0;
            if(b % 2 == 0) return mystery(a + a, b / 2);
            return mystery(a + a, b / 2) + a;
        }

        private static int factorial(int n){
            if(n == 0 || n == 1) return 1;
            return factorial(n - 1) * n;
        }

        public static double lnFactorial(int n){
            return Math.Log(factorial(n));
        }
        //欧几里得算法
        public static int gcd(int p, int q){
            if(q == 0) return p;
            int r = p % q;
            // Console.WriteLine($"p = {p}, q = {q}");
            return gcd(q, r);
        }

        public static bool[,] primal(int N){
            bool[,] a = new bool[N, N];
            for(int i = 0; i < N ; i++){
                for(int j = 0; j < N; j++){
                    if(gcd(i,j) == 1){
                        a[i,j] = true;
                    }else{
                        a[i,j] = false;
                    }
                    Console.WriteLine($"a[{i}][{j}] = {a[i,j]}");
                }
            }
            
            return a;
        }
    }
    //执行类
    class Test{
        static void Main(){
            Answer.primal(5);
        }
    }
}