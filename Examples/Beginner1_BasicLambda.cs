using System;

namespace AI_Lambda2.Examples
{
    /// <summary>
    /// 初階範例 1: 基本 Lambda 表達式
    /// 學習目標: 理解 Lambda 的基本語法和用途
    /// </summary>
    public static class Beginner1_BasicLambda
    {
        public static void Run()
        {
            Console.WriteLine("【初階範例 1: 基本 Lambda 表達式】\n");

            // 範例 1: 最簡單的 Lambda - 加法運算
            Console.WriteLine("1. 簡單運算 - Lambda 取代傳統方法");
            Console.WriteLine("   傳統方法:");
            int traditionalResult = AddTraditional(5, 3);
            Console.WriteLine($"   5 + 3 = {traditionalResult}");

            Console.WriteLine("\n   Lambda 方法:");
            Func<int, int, int> add = (a, b) => a + b;
            int lambdaResult = add(5, 3);
            Console.WriteLine($"   5 + 3 = {lambdaResult}");

            // 範例 2: 單一參數的 Lambda
            Console.WriteLine("\n\n2. 單一參數 - 計算平方");
            Func<int, int> square = x => x * x;
            Console.WriteLine($"   5 的平方 = {square(5)}");
            Console.WriteLine($"   10 的平方 = {square(10)}");

            // 範例 3: 無參數的 Lambda
            Console.WriteLine("\n\n3. 無參數 - 產生隨機數字");
            Func<int> getRandomNumber = () => new Random().Next(1, 101);
            Console.WriteLine($"   隨機數字 1: {getRandomNumber()}");
            Console.WriteLine($"   隨機數字 2: {getRandomNumber()}");

            // 範例 4: 多行 Lambda (使用大括號)
            Console.WriteLine("\n\n4. 多行 Lambda - 判斷成績等級");
            Func<int, string> getGrade = score =>
            {
                if (score >= 90) return "A";
                if (score >= 80) return "B";
                if (score >= 70) return "C";
                if (score >= 60) return "D";
                return "F";
            };

            Console.WriteLine($"   分數 95 的等級: {getGrade(95)}");
            Console.WriteLine($"   分數 75 的等級: {getGrade(75)}");
            Console.WriteLine($"   分數 55 的等級: {getGrade(55)}");

            // 範例 5: 布林判斷的 Lambda (Predicate)
            Console.WriteLine("\n\n5. 布林判斷 - 檢查是否為偶數");
            Func<int, bool> isEven = n => n % 2 == 0;

            int[] numbers = { 1, 2, 3, 4, 5, 6 };
            Console.Write("   數字陣列: ");
            foreach (var num in numbers)
            {
                Console.Write($"{num} ");
            }
            Console.WriteLine();

            Console.Write("   偶數判斷: ");
            foreach (var num in numbers)
            {
                Console.Write($"{num}={isEven(num)} ");
            }
            Console.WriteLine();

            // 範例 6: 字串處理的 Lambda
            Console.WriteLine("\n\n6. 字串處理 - 格式化名稱");
            Func<string, string> formatName = name => $"你好，{name}先生/小姐！";

            Console.WriteLine($"   {formatName("王小明")}");
            Console.WriteLine($"   {formatName("李小華")}");
        }

        // 傳統方法（用於比較）
        private static int AddTraditional(int a, int b)
        {
            return a + b;
        }
    }
}
