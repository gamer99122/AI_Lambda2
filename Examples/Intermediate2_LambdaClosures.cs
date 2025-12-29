using System;
using System.Collections.Generic;

namespace AI_Lambda2.Examples
{
    /// <summary>
    /// 中階範例 2: Lambda 閉包與變數捕獲
    /// 學習目標: 理解閉包概念、變數捕獲機制及常見陷阱
    /// </summary>
    public static class Intermediate2_LambdaClosures
    {
        public static void Run()
        {
            Console.WriteLine("【中階範例 2: Lambda 閉包與變數捕獲】\n");

            // 範例 1: 基本閉包 - 捕獲外部變數
            Console.WriteLine("1. 基本閉包 - 捕獲外部變數");
            int multiplier = 3;
            Func<int, int> multiply = x => x * multiplier;

            Console.WriteLine($"   multiplier = {multiplier}");
            Console.WriteLine($"   5 × multiplier = {multiply(5)}");

            // 修改外部變數
            multiplier = 5;
            Console.WriteLine($"\n   修改 multiplier = {multiplier}");
            Console.WriteLine($"   5 × multiplier = {multiply(5)} (注意：結果改變了！)");

            // 範例 2: 計數器閉包
            Console.WriteLine("\n\n2. 計數器閉包 - 保持狀態");
            var counter = CreateCounter();

            Console.WriteLine($"   呼叫 1: {counter()}");
            Console.WriteLine($"   呼叫 2: {counter()}");
            Console.WriteLine($"   呼叫 3: {counter()}");
            Console.WriteLine($"   呼叫 4: {counter()}");

            // 範例 3: 迴圈中的閉包陷阱（錯誤示範）
            Console.WriteLine("\n\n3. 迴圈中的閉包 - 常見陷阱");
            Console.WriteLine("   ❌ 錯誤示範 (所有 Lambda 都捕獲同一個變數):");

            var actions = new List<Action>();
            for (int i = 0; i < 5; i++)
            {
                // 錯誤：所有 lambda 都捕獲同一個變數 i
                actions.Add(() => Console.Write($"{i} "));
            }

            Console.Write("   輸出: ");
            foreach (var action in actions)
            {
                action();
            }
            Console.WriteLine("  <- 全部都是 5！");

            // 範例 4: 迴圈中的閉包（正確做法）
            Console.WriteLine("\n   ✅ 正確做法 (使用區域變數複製):");

            var correctActions = new List<Action>();
            for (int i = 0; i < 5; i++)
            {
                int localCopy = i;  // 建立區域變數複製
                correctActions.Add(() => Console.Write($"{localCopy} "));
            }

            Console.Write("   輸出: ");
            foreach (var action in correctActions)
            {
                action();
            }
            Console.WriteLine("  <- 正確！");

            // 範例 5: 函數工廠 - 使用閉包建立客製化函數
            Console.WriteLine("\n\n5. 函數工廠 - 建立客製化函數");

            var add10 = CreateAdder(10);
            var add100 = CreateAdder(100);

            Console.WriteLine($"   add10(5) = {add10(5)}");
            Console.WriteLine($"   add100(5) = {add100(5)}");

            // 範例 6: 閉包與物件狀態
            Console.WriteLine("\n\n6. 閉包與物件狀態 - 購物車範例");

            var cart = new ShoppingCart();
            cart.AddItem("蘋果", 30);
            cart.AddItem("香蕉", 20);
            cart.AddItem("橘子", 25);

            Console.WriteLine("   所有商品:");
            cart.PrintAll();

            Console.WriteLine("\n   價格 >= 25 的商品:");
            cart.PrintFiltered(price => price >= 25);

            // 範例 7: 多層閉包
            Console.WriteLine("\n\n7. 多層閉包 - 函數組合");

            var messageBuilder = CreateMessageBuilder("系統", "重要");
            var message1 = messageBuilder("登入成功");
            var message2 = messageBuilder("資料已儲存");

            Console.WriteLine($"   {message1}");
            Console.WriteLine($"   {message2}");

            // 範例 8: 實際應用 - 延遲執行與快取
            Console.WriteLine("\n\n8. 實際應用 - 延遲執行與快取");

            var expensive = CreateCachedFunction(ExpensiveCalculation);

            Console.WriteLine("   第一次呼叫 (需計算):");
            Console.WriteLine($"   結果: {expensive(5)}");

            Console.WriteLine("\n   第二次呼叫 (使用快取):");
            Console.WriteLine($"   結果: {expensive(5)}");

            Console.WriteLine("\n   呼叫不同參數 (需計算):");
            Console.WriteLine($"   結果: {expensive(10)}");
        }

        // 建立計數器函數
        private static Func<int> CreateCounter()
        {
            int count = 0;
            return () => ++count;
        }

        // 函數工廠 - 建立加法器
        private static Func<int, int> CreateAdder(int valueToAdd)
        {
            return x => x + valueToAdd;
        }

        // 多層閉包 - 訊息建構器
        private static Func<string, string> CreateMessageBuilder(string category, string level)
        {
            return content => $"[{category}] [{level}] {content}";
        }

        // 模擬耗時計算
        private static int ExpensiveCalculation(int n)
        {
            Console.WriteLine($"      -> 執行耗時計算: {n}");
            return n * n;
        }

        // 建立帶快取的函數
        private static Func<int, int> CreateCachedFunction(Func<int, int> func)
        {
            var cache = new Dictionary<int, int>();

            return input =>
            {
                if (cache.ContainsKey(input))
                {
                    Console.WriteLine("      -> 從快取取得結果");
                    return cache[input];
                }

                var result = func(input);
                cache[input] = result;
                return result;
            };
        }

        // 購物車類別
        private class ShoppingCart
        {
            private List<(string Name, int Price)> items = new List<(string, int)>();

            public void AddItem(string name, int price)
            {
                items.Add((name, price));
            }

            public void PrintAll()
            {
                items.ForEach(item => Console.WriteLine($"   - {item.Name}: ${item.Price}"));
            }

            public void PrintFiltered(Func<int, bool> filter)
            {
                items.ForEach(item =>
                {
                    if (filter(item.Price))
                    {
                        Console.WriteLine($"   - {item.Name}: ${item.Price}");
                    }
                });
            }
        }
    }
}
