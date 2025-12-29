using System;
using System.Linq;
using System.Linq.Expressions;

namespace AI_Lambda2.Examples
{
    /// <summary>
    /// 進階範例 1: Expression Trees (表達式樹)
    /// 學習目標: 理解表達式樹的概念、建立與操作，以及在 ORM 框架中的應用
    /// </summary>
    public static class Advanced1_ExpressionTrees
    {
        public static void Run()
        {
            Console.WriteLine("【進階範例 1: Expression Trees (表達式樹)】\n");

            // 範例 1: Lambda vs Expression Tree
            Console.WriteLine("1. Lambda 與 Expression Tree 的差異");

            // 一般的 Lambda (編譯為委派)
            Func<int, int, int> lambdaAdd = (a, b) => a + b;
            Console.WriteLine($"   Lambda 結果: {lambdaAdd(3, 4)}");
            Console.WriteLine($"   Lambda 型別: {lambdaAdd.GetType().Name}");

            // Expression Tree (編譯為表達式樹)
            Expression<Func<int, int, int>> exprAdd = (a, b) => a + b;
            Console.WriteLine($"   Expression 型別: {exprAdd.GetType().Name}");
            Console.WriteLine($"   Expression 內容: {exprAdd}");

            // 可以編譯並執行 Expression
            var compiledExpr = exprAdd.Compile();
            Console.WriteLine($"   編譯後執行結果: {compiledExpr(3, 4)}");

            // 範例 2: 手動建立表達式樹
            Console.WriteLine("\n\n2. 手動建立表達式樹 - (x, y) => x * y + 10");

            // 定義參數
            ParameterExpression x = Expression.Parameter(typeof(int), "x");
            ParameterExpression y = Expression.Parameter(typeof(int), "y");

            // 建立運算式: x * y
            BinaryExpression multiply = Expression.Multiply(x, y);

            // 建立常數: 10
            ConstantExpression ten = Expression.Constant(10);

            // 建立運算式: (x * y) + 10
            BinaryExpression add = Expression.Add(multiply, ten);

            // 建立 Lambda 表達式
            Expression<Func<int, int, int>> lambda =
                Expression.Lambda<Func<int, int, int>>(add, x, y);

            Console.WriteLine($"   表達式: {lambda}");

            var func = lambda.Compile();
            Console.WriteLine($"   執行 (5, 3): {func(5, 3)}");

            // 範例 3: 解析表達式樹結構
            Console.WriteLine("\n\n3. 解析表達式樹結構");

            Expression<Func<int, bool>> expr = num => num > 5 && num < 10;
            Console.WriteLine($"   原始表達式: {expr}");
            Console.WriteLine("\n   結構分析:");

            AnalyzeExpression(expr.Body, 1);

            // 範例 4: 動態建立查詢條件
            Console.WriteLine("\n\n4. 動態建立查詢條件 (模擬 ORM)");

            var products = new[]
            {
                new Product { Id = 1, Name = "筆記型電腦", Price = 30000 },
                new Product { Id = 2, Name = "滑鼠", Price = 500 },
                new Product { Id = 3, Name = "鍵盤", Price = 1500 },
                new Product { Id = 4, Name = "螢幕", Price = 8000 }
            };

            Console.WriteLine("   所有產品:");
            foreach (var p in products)
            {
                Console.WriteLine($"   - {p.Name}: ${p.Price}");
            }

            // 動態建立條件: price > 1000
            var predicate = CreatePricePredicate(1000);
            var filtered = products.AsQueryable().Where(predicate);

            Console.WriteLine("\n   價格 > 1000 的產品:");
            foreach (var p in filtered)
            {
                Console.WriteLine($"   - {p.Name}: ${p.Price}");
            }

            // 範例 5: Expression Visitor - 修改表達式樹
            Console.WriteLine("\n\n5. Expression Visitor - 修改表達式樹");

            Expression<Func<int, int>> original = x => x + 5;
            Console.WriteLine($"   原始表達式: {original}");

            // 將所有常數 +1
            var modifier = new ConstantModifier();
            var modified = (Expression<Func<int, int>>)modifier.Visit(original);
            Console.WriteLine($"   修改後表達式: {modified}");

            Console.WriteLine($"\n   原始 (10): {original.Compile()(10)}");
            Console.WriteLine($"   修改後 (10): {modified.Compile()(10)}");

            // 範例 6: 組合多個條件
            Console.WriteLine("\n\n6. 組合多個條件 - AND/OR");

            Expression<Func<Product, bool>> condition1 = p => p.Price > 1000;
            Expression<Func<Product, bool>> condition2 = p => p.Price < 10000;

            var combinedAnd = CombinePredicates(condition1, condition2, ExpressionType.AndAlso);
            var combinedOr = CombinePredicates(condition1, condition2, ExpressionType.OrElse);

            Console.WriteLine($"   條件1: {condition1}");
            Console.WriteLine($"   條件2: {condition2}");
            Console.WriteLine($"   AND 組合: {combinedAnd}");
            Console.WriteLine($"   OR 組合: {combinedOr}");

            var andResults = products.AsQueryable().Where(combinedAnd);
            Console.WriteLine("\n   AND 結果:");
            foreach (var p in andResults)
            {
                Console.WriteLine($"   - {p.Name}: ${p.Price}");
            }

            // 範例 7: 將表達式轉換為 SQL (概念示範)
            Console.WriteLine("\n\n7. 表達式轉換為 SQL 查詢 (概念)");

            Expression<Func<Product, bool>> sqlExpr = p => p.Price > 1000 && p.Name.Contains("電");
            Console.WriteLine($"   C# 表達式: {sqlExpr}");

            string sql = ConvertToSql(sqlExpr);
            Console.WriteLine($"   轉換為 SQL: {sql}");
        }

        // 分析表達式結構
        private static void AnalyzeExpression(Expression expr, int level)
        {
            string indent = new string(' ', level * 3);
            Console.WriteLine($"{indent}型別: {expr.NodeType}, 類別: {expr.GetType().Name}");

            if (expr is BinaryExpression binary)
            {
                Console.WriteLine($"{indent}左側:");
                AnalyzeExpression(binary.Left, level + 1);
                Console.WriteLine($"{indent}右側:");
                AnalyzeExpression(binary.Right, level + 1);
            }
            else if (expr is ParameterExpression param)
            {
                Console.WriteLine($"{indent}參數名稱: {param.Name}");
            }
            else if (expr is ConstantExpression constant)
            {
                Console.WriteLine($"{indent}常數值: {constant.Value}");
            }
        }

        // 動態建立價格條件
        private static Expression<Func<Product, bool>> CreatePricePredicate(int minPrice)
        {
            ParameterExpression param = Expression.Parameter(typeof(Product), "p");
            MemberExpression property = Expression.Property(param, "Price");
            ConstantExpression constant = Expression.Constant(minPrice);
            BinaryExpression comparison = Expression.GreaterThan(property, constant);

            return Expression.Lambda<Func<Product, bool>>(comparison, param);
        }

        // 組合兩個條件
        private static Expression<Func<T, bool>> CombinePredicates<T>(
            Expression<Func<T, bool>> expr1,
            Expression<Func<T, bool>> expr2,
            ExpressionType logicalOperator)
        {
            var parameter = Expression.Parameter(typeof(T), "x");

            var leftVisitor = new ReplaceParameterVisitor(expr1.Parameters[0], parameter);
            var left = leftVisitor.Visit(expr1.Body);

            var rightVisitor = new ReplaceParameterVisitor(expr2.Parameters[0], parameter);
            var right = rightVisitor.Visit(expr2.Body);

            var combined = logicalOperator == ExpressionType.AndAlso
                ? Expression.AndAlso(left, right)
                : Expression.OrElse(left, right);

            return Expression.Lambda<Func<T, bool>>(combined, parameter);
        }

        // 簡化的 SQL 轉換 (僅供示範)
        private static string ConvertToSql(Expression<Func<Product, bool>> expr)
        {
            return $"SELECT * FROM Products WHERE {expr.Body.ToString().Replace("p.", "").Replace("AndAlso", "AND")}";
        }

        // Expression Visitor - 修改常數
        private class ConstantModifier : ExpressionVisitor
        {
            protected override Expression VisitConstant(ConstantExpression node)
            {
                if (node.Type == typeof(int))
                {
                    int value = (int)node.Value!;
                    return Expression.Constant(value + 1);
                }
                return base.VisitConstant(node);
            }
        }

        // Expression Visitor - 替換參數
        private class ReplaceParameterVisitor : ExpressionVisitor
        {
            private readonly ParameterExpression _oldParameter;
            private readonly ParameterExpression _newParameter;

            public ReplaceParameterVisitor(ParameterExpression oldParameter, ParameterExpression newParameter)
            {
                _oldParameter = oldParameter;
                _newParameter = newParameter;
            }

            protected override Expression VisitParameter(ParameterExpression node)
            {
                return node == _oldParameter ? _newParameter : base.VisitParameter(node);
            }
        }

        // 產品類別
        private class Product
        {
            public int Id { get; set; }
            public string Name { get; set; } = "";
            public int Price { get; set; }
        }
    }
}
