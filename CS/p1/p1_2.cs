namespace p1;

public class Class1
{
    class Point2D{
        //构造点对象
        public struct Point{
            public double x;
            public double y;
        }
        public static double distance(Point p1, Point p2){
            return Math.Sqrt(Math.Pow(p2.x - p1.x, 2) + Math.Pow(p2.y - p1.y, 2));
        }

        public static double minDistance(int N){
            if(N <= 1) return 0;
            Point[] point = new Point[N];
            Random random = new Random();

            for(int i = 0; i < N; i++){
                point[i].x = random.NextDouble();
                point[i].y = random.NextDouble();
            }

            double minDistance = distance(point[0], point[1]);

            for(int i = 0; i < N; i++){
                for(int j = i + 1; j < N; j++){
                    double newDistance = distance(point[i], point[j]);
                    if(newDistance < minDistance) minDistance = newDistance;
                }
            }
            return minDistance;
        }
    }
    class VisualCounter{
        private int num;
        private int max;
        private int counter = 0;
        //构造函数无需类型
        public VisualCounter(int N, int maxi){
            num = N;
            max = maxi;
        }
        public int add(){
            if(counter <= max && num > 0) {
                counter++;
                num--;
            }
            return counter;
        }

        public int minus(){
            if(counter <= max && num > 0) {
                counter--;
                num--;
            }
            return counter;
        }
    }
    class Date{
        public struct Day{
            public int year;
            public int month;
            public int day;
        }
        public int[] dayInMonth = {31, 29, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31};
        public Day day1 = new Day();

        public Date(int year, int month, int day){
            day1.year = year;
            day1.month = month;
            day1.day = day;
        }

        public bool checkDate(){
            if(day1.month > 12 || day1.month < 0) return false;
            if(day1.day < 0 || day1.day > dayInMonth[day1.month - 1]) return false;
            if(day1.year % 4 != 0 || (day1.year % 100 == 0 && day1.year % 400 != 0)){
                if(day1.day == 29) return false;
            }
            return true;
        }

        // public string dayOfWeek(){
        //     int centery = 21;
        //     string[] week = {"Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"};
        //     int w = day1.year + day1.year / 4 + centery / 4 - 2 * centery + 26 * (day1.month + 1) / 10 + day1.day - 1;
        //     w = w % 7;
        //     return week[w];
        // }
    }

    class Transaction: Date{
        //构造函数继承
        public Transaction(int year, int month, int day): base(year, month, day){

        }
    }
    class Rotation{
        public static bool isRotate(string s, string t){
            //重复一遍回文文字并拼接，必有所有的回文文字可能
            return s.Length == t.Length && (s + s).Contains(t);
        }
    }
}
