using System;
using System.Collections.Generic;

namespace AI_Lambda2.Examples
{
    /// <summary>
    /// 中階範例 1: Lambda 與委派 (Delegate)
    /// 學習目標: 理解 Lambda 如何與委派搭配使用，實現回呼函數和事件處理
    /// </summary>
    public static class Intermediate1_LambdaWithDelegates
    {
        // 定義自訂委派
        public delegate int MathOperation(int a, int b);
        public delegate void NotificationHandler(string message);

        public static void Run()
        {
            Console.WriteLine("【中階範例 1: Lambda 與委派】\n");

            // 範例 1: 使用自訂委派
            Console.WriteLine("1. 自訂委派 - 數學運算");
            MathOperation add = (a, b) => a + b;
            MathOperation subtract = (a, b) => a - b;
            MathOperation multiply = (a, b) => a * b;

            Console.WriteLine($"   10 + 5 = {add(10, 5)}");
            Console.WriteLine($"   10 - 5 = {subtract(10, 5)}");
            Console.WriteLine($"   10 × 5 = {multiply(10, 5)}");

            // 範例 2: 將委派作為參數傳遞
            Console.WriteLine("\n\n2. 委派作為參數 - 計算機");
            int result1 = Calculate(10, 5, (a, b) => a + b);
            int result2 = Calculate(10, 5, (a, b) => a * b);
            Console.WriteLine($"   計算 10, 5 (加法): {result1}");
            Console.WriteLine($"   計算 10, 5 (乘法): {result2}");

            // 範例 3: Action 委派 (無回傳值)
            Console.WriteLine("\n\n3. Action 委派 - 無回傳值操作");
            Action<string> greet = name => Console.WriteLine($"   你好, {name}!");
            greet("小明");
            greet("小華");

            Action<int, int> printSum = (a, b) => Console.WriteLine($"   {a} + {b} = {a + b}");
            printSum(3, 7);

            // 範例 4: Func 委派 (有回傳值)
            Console.WriteLine("\n\n4. Func 委派 - 有回傳值操作");
            Func<int, int, int> max = (a, b) => a > b ? a : b;
            Func<string, int> getLength = s => s.Length;

            Console.WriteLine($"   Max(15, 8) = {max(15, 8)}");
            Console.WriteLine($"   \"Hello\" 的長度 = {getLength("Hello")}");

            // 範例 5: Predicate 委派 (回傳布林值)
            Console.WriteLine("\n\n5. Predicate 委派 - 布林判斷");
            Predicate<int> isPositive = n => n > 0;
            Predicate<string> isLongString = s => s.Length > 5;

            Console.WriteLine($"   10 是正數? {isPositive(10)}");
            Console.WriteLine($"   -5 是正數? {isPositive(-5)}");
            Console.WriteLine($"   \"Hello\" 長度 > 5? {isLongString("Hello")}");
            Console.WriteLine($"   \"Programming\" 長度 > 5? {isLongString("Programming")}");

            // 範例 6: 多播委派 (Multicast Delegate)
            Console.WriteLine("\n\n6. 多播委派 - 事件通知");
            NotificationHandler? notifier = null;

            // 添加多個處理器
            notifier += msg => Console.WriteLine($"   [Email] {msg}");
            notifier += msg => Console.WriteLine($"   [SMS] {msg}");
            notifier += msg => Console.WriteLine($"   [推播] {msg}");

            Console.WriteLine("   觸發通知: \"訂單已送出\"");
            notifier?.Invoke("訂單已送出");

            // 範例 7: 實際應用 - 數據處理管線
            Console.WriteLine("\n\n7. 實際應用 - 數據處理管線");

            var processor = new DataProcessor();

            // 添加處理步驟
            processor.AddStep(data => data.Trim());
            processor.AddStep(data => data.ToUpper());
            processor.AddStep(data => $"[處理完成] {data}");

            string input = "  hello world  ";
            Console.WriteLine($"   輸入: \"{input}\"");

            string output = processor.Process(input);
            Console.WriteLine($"   輸出: \"{output}\"");

            // 範例 8: 回呼函數應用
            Console.WriteLine("\n\n8. 回呼函數 - 異步操作模擬");

            SimulateAsyncOperation(
                onStart: () => Console.WriteLine("   [開始] 正在處理資料..."),
                onProgress: progress => Console.WriteLine($"   [進度] {progress}%"),
                onComplete: () => Console.WriteLine("   [完成] 處理完畢！")
            );
        }

        // 輔助方法：使用委派執行計算
        private static int Calculate(int a, int b, MathOperation operation)
        {
            return operation(a, b);
        }

        // 數據處理管線類別
        private class DataProcessor
        {
            private List<Func<string, string>> steps = new List<Func<string, string>>();

            public void AddStep(Func<string, string> step)
            {
                steps.Add(step);
            }

            public string Process(string data)
            {
                string result = data;
                foreach (var step in steps)
                {
                    result = step(result);
                }
                return result;
            }
        }

        // 模擬異步操作
        private static void SimulateAsyncOperation(
            Action onStart,
            Action<int> onProgress,
            Action onComplete)
        {
            onStart();

            for (int i = 0; i <= 100; i += 25)
            {
                onProgress(i);
            }

            onComplete();
        }
    }
}
