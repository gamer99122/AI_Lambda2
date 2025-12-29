using System;
using System.Linq;
using System.Collections.Generic;

namespace AI_Lambda2.Examples
{
    /// <summary>
    /// 初階範例 2: Lambda 與 LINQ 基礎
    /// 學習目標: 理解 Lambda 在 LINQ 查詢中的應用
    /// </summary>
    public static class Beginner2_LambdaWithLinq
    {
        public static void Run()
        {
            Console.WriteLine("【初階範例 2: Lambda 與 LINQ 基礎】\n");

            // 準備測試資料
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            string[] fruits = { "蘋果", "香蕉", "櫻桃", "芭樂", "奇異果", "檸檬" };

            // 範例 1: Where - 篩選偶數
            Console.WriteLine("1. Where() - 篩選偶數");
            Console.WriteLine($"   原始數字: {string.Join(", ", numbers)}");

            var evenNumbers = numbers.Where(n => n % 2 == 0);
            Console.WriteLine($"   偶數: {string.Join(", ", evenNumbers)}");

            // 範例 2: Select - 轉換資料
            Console.WriteLine("\n\n2. Select() - 將數字轉換為平方");
            var squares = numbers.Select(n => n * n);
            Console.WriteLine($"   原始: {string.Join(", ", numbers)}");
            Console.WriteLine($"   平方: {string.Join(", ", squares)}");

            // 範例 3: Where + Select 組合
            Console.WriteLine("\n\n3. Where + Select 組合 - 篩選奇數並乘以 10");
            var result = numbers
                .Where(n => n % 2 != 0)
                .Select(n => n * 10);
            Console.WriteLine($"   結果: {string.Join(", ", result)}");

            // 範例 4: OrderBy - 排序
            Console.WriteLine("\n\n4. OrderBy() - 字串長度排序");
            Console.WriteLine($"   原始水果: {string.Join(", ", fruits)}");

            var sortedFruits = fruits.OrderBy(f => f.Length);
            Console.WriteLine($"   按長度排序: {string.Join(", ", sortedFruits)}");

            // 範例 5: Any - 檢查是否存在
            Console.WriteLine("\n\n5. Any() - 檢查條件");
            bool hasLargeNumber = numbers.Any(n => n > 5);
            bool hasNegative = numbers.Any(n => n < 0);
            Console.WriteLine($"   是否有大於 5 的數字? {hasLargeNumber}");
            Console.WriteLine($"   是否有負數? {hasNegative}");

            // 範例 6: Count - 計數
            Console.WriteLine("\n\n6. Count() - 計數符合條件的元素");
            int evenCount = numbers.Count(n => n % 2 == 0);
            int oddCount = numbers.Count(n => n % 2 != 0);
            Console.WriteLine($"   偶數數量: {evenCount}");
            Console.WriteLine($"   奇數數量: {oddCount}");

            // 範例 7: First 和 FirstOrDefault
            Console.WriteLine("\n\n7. First() / FirstOrDefault() - 取得第一個符合條件的元素");
            int firstEven = numbers.First(n => n % 2 == 0);
            string? longFruit = fruits.FirstOrDefault(f => f.Length > 3);
            Console.WriteLine($"   第一個偶數: {firstEven}");
            Console.WriteLine($"   第一個長度大於 3 的水果: {longFruit}");

            // 範例 8: 使用物件集合
            Console.WriteLine("\n\n8. 物件集合的 LINQ 操作");

            var students = new List<Student>
            {
                new Student { Name = "小明", Age = 20, Score = 85 },
                new Student { Name = "小華", Age = 22, Score = 92 },
                new Student { Name = "小美", Age = 21, Score = 78 },
                new Student { Name = "小強", Age = 23, Score = 88 }
            };

            Console.WriteLine("   所有學生:");
            foreach (var s in students)
            {
                Console.WriteLine($"   - {s.Name}: {s.Age}歲, 分數 {s.Score}");
            }

            // 篩選分數 >= 85 的學生
            var topStudents = students.Where(s => s.Score >= 85);
            Console.WriteLine("\n   分數 >= 85 的學生:");
            foreach (var s in topStudents)
            {
                Console.WriteLine($"   - {s.Name}: 分數 {s.Score}");
            }

            // 取得學生名稱列表
            var names = students.Select(s => s.Name);
            Console.WriteLine($"\n   所有學生姓名: {string.Join(", ", names)}");

            // 按分數排序
            var sortedByScore = students.OrderByDescending(s => s.Score);
            Console.WriteLine("\n   按分數排序 (高到低):");
            foreach (var s in sortedByScore)
            {
                Console.WriteLine($"   - {s.Name}: {s.Score}");
            }
        }

        // 學生類別
        private class Student
        {
            public string Name { get; set; } = "";
            public int Age { get; set; }
            public int Score { get; set; }
        }
    }
}
