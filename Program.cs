using System;
using AI_Lambda2.Examples;

namespace AI_Lambda2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            bool running = true;

            while (running)
            {
                Console.Clear();
                Console.WriteLine("═══════════════════════════════════════════════════════");
                Console.WriteLine("    C# Lambda 表達式教學範例 (.NET 8)");
                Console.WriteLine("═══════════════════════════════════════════════════════");
                Console.WriteLine();
                Console.WriteLine("初階範例:");
                Console.WriteLine("  1. 基本 Lambda 表達式");
                Console.WriteLine("  2. Lambda 與 LINQ 基礎");
                Console.WriteLine();
                Console.WriteLine("中階範例:");
                Console.WriteLine("  3. Lambda 與委派 (Delegate)");
                Console.WriteLine("  4. Lambda 閉包與變數捕獲");
                Console.WriteLine();
                Console.WriteLine("進階範例:");
                Console.WriteLine("  5. Expression Trees (表達式樹)");
                Console.WriteLine("  6. 複雜的 LINQ 查詢組合");
                Console.WriteLine();
                Console.WriteLine("  0. 離開");
                Console.WriteLine("═══════════════════════════════════════════════════════");
                Console.Write("請選擇範例 (0-6): ");

                string? choice = Console.ReadLine();

                Console.WriteLine();
                Console.WriteLine("───────────────────────────────────────────────────────");
                Console.WriteLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            Beginner1_BasicLambda.Run();
                            break;
                        case "2":
                            Beginner2_LambdaWithLinq.Run();
                            break;
                        case "3":
                            Intermediate1_LambdaWithDelegates.Run();
                            break;
                        case "4":
                            Intermediate2_LambdaClosures.Run();
                            break;
                        case "5":
                            Advanced1_ExpressionTrees.Run();
                            break;
                        case "6":
                            Advanced2_ComplexLinq.Run();
                            break;
                        case "0":
                            running = false;
                            Console.WriteLine("感謝使用！再見！");
                            continue;
                        default:
                            Console.WriteLine("無效的選擇，請重試。");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"發生錯誤: {ex.Message}");
                }

                if (running)
                {
                    Console.WriteLine();
                    Console.WriteLine("───────────────────────────────────────────────────────");
                    Console.WriteLine("按任意鍵繼續...");
                    Console.ReadKey();
                }
            }
        }
    }
}
