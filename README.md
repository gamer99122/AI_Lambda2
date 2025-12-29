# C# Lambda 表達式教學範例 (.NET 8)

這是一個專為初學者設計的 C# Lambda 表達式教學專案，包含從基礎到進階的完整範例。

## 專案結構

```
AI_Lambda2/
├── Program.cs                                    # 主程式（互動式選單）
├── Examples/
│   ├── Beginner1_BasicLambda.cs                 # 初階範例 1
│   ├── Beginner2_LambdaWithLinq.cs              # 初階範例 2
│   ├── Intermediate1_LambdaWithDelegates.cs     # 中階範例 1
│   ├── Intermediate2_LambdaClosures.cs          # 中階範例 2
│   ├── Advanced1_ExpressionTrees.cs             # 進階範例 1
│   └── Advanced2_ComplexLinq.cs                 # 進階範例 2
└── README.md                                     # 本說明文件
```

## 環境需求

- .NET 8.0 SDK 或更高版本
- 支援 C# 的 IDE（Visual Studio, VS Code, Rider 等）

## 如何執行

### 方法 1: 使用 dotnet CLI

```bash
# 進入專案目錄
cd AI_Lambda2

# 執行專案
dotnet run
```

### 方法 2: 使用 Visual Studio

1. 開啟專案資料夾
2. 按 F5 執行或點擊「開始」按鈕

## 範例說明

### 📘 初階範例

#### 1️⃣ 基本 Lambda 表達式 (Beginner1_BasicLambda.cs)

**學習目標**: 理解 Lambda 的基本語法和用途

**內容包含**:
- ✅ 簡單運算 - Lambda 取代傳統方法
- ✅ 單一參數的 Lambda
- ✅ 無參數的 Lambda
- ✅ 多行 Lambda（使用大括號）
- ✅ 布林判斷的 Lambda (Predicate)
- ✅ 字串處理的 Lambda

**核心概念**:
```csharp
// Lambda 語法: (參數) => 運算式
Func<int, int, int> add = (a, b) => a + b;

// 單一參數可省略括號
Func<int, int> square = x => x * x;

// 多行需要大括號和 return
Func<int, string> getGrade = score =>
{
    if (score >= 90) return "A";
    return "F";
};
```

#### 2️⃣ Lambda 與 LINQ 基礎 (Beginner2_LambdaWithLinq.cs)

**學習目標**: 理解 Lambda 在 LINQ 查詢中的應用

**內容包含**:
- ✅ Where() - 篩選資料
- ✅ Select() - 轉換資料
- ✅ OrderBy() - 排序
- ✅ Any() - 檢查是否存在
- ✅ Count() - 計數
- ✅ First() / FirstOrDefault() - 取得第一個元素
- ✅ 物件集合的 LINQ 操作

**核心概念**:
```csharp
// 篩選偶數
var evenNumbers = numbers.Where(n => n % 2 == 0);

// 轉換為平方
var squares = numbers.Select(n => n * n);

// 鏈式查詢
var result = numbers
    .Where(n => n % 2 != 0)
    .Select(n => n * 10)
    .OrderBy(n => n);
```

### 📙 中階範例

#### 3️⃣ Lambda 與委派 (Intermediate1_LambdaWithDelegates.cs)

**學習目標**: 理解 Lambda 如何與委派搭配使用，實現回呼函數和事件處理

**內容包含**:
- ✅ 自訂委派 (Delegate)
- ✅ 委派作為參數傳遞
- ✅ Action 委派（無回傳值）
- ✅ Func 委派（有回傳值）
- ✅ Predicate 委派（回傳布林值）
- ✅ 多播委派 (Multicast Delegate)
- ✅ 數據處理管線應用
- ✅ 回呼函數應用

**核心概念**:
```csharp
// Action - 無回傳值
Action<string> greet = name => Console.WriteLine($"你好, {name}!");

// Func - 有回傳值
Func<int, int, int> max = (a, b) => a > b ? a : b;

// Predicate - 回傳布林值
Predicate<int> isPositive = n => n > 0;

// 多播委派
NotificationHandler notifier = null;
notifier += msg => Console.WriteLine($"[Email] {msg}");
notifier += msg => Console.WriteLine($"[SMS] {msg}");
notifier?.Invoke("訊息內容");
```

#### 4️⃣ Lambda 閉包與變數捕獲 (Intermediate2_LambdaClosures.cs)

**學習目標**: 理解閉包概念、變數捕獲機制及常見陷阱

**內容包含**:
- ✅ 基本閉包 - 捕獲外部變數
- ✅ 計數器閉包 - 保持狀態
- ✅ 迴圈中的閉包陷阱（常見錯誤）
- ✅ 迴圈中的閉包（正確做法）
- ✅ 函數工廠 - 使用閉包建立客製化函數
- ✅ 閉包與物件狀態
- ✅ 多層閉包
- ✅ 延遲執行與快取應用

**核心概念**:
```csharp
// 閉包捕獲外部變數
int multiplier = 3;
Func<int, int> multiply = x => x * multiplier;

// ❌ 迴圈陷阱
for (int i = 0; i < 5; i++)
{
    actions.Add(() => Console.Write($"{i} ")); // 全部都是 5
}

// ✅ 正確做法
for (int i = 0; i < 5; i++)
{
    int localCopy = i;
    actions.Add(() => Console.Write($"{localCopy} ")); // 0 1 2 3 4
}

// 函數工廠
Func<int, int> CreateAdder(int valueToAdd)
{
    return x => x + valueToAdd; // 捕獲 valueToAdd
}
```

### 📕 進階範例

#### 5️⃣ Expression Trees 表達式樹 (Advanced1_ExpressionTrees.cs)

**學習目標**: 理解表達式樹的概念、建立與操作，以及在 ORM 框架中的應用

**內容包含**:
- ✅ Lambda vs Expression Tree 的差異
- ✅ 手動建立表達式樹
- ✅ 解析表達式樹結構
- ✅ 動態建立查詢條件（模擬 ORM）
- ✅ Expression Visitor - 修改表達式樹
- ✅ 組合多個條件（AND/OR）
- ✅ 表達式轉換為 SQL 查詢（概念示範）

**核心概念**:
```csharp
// 一般 Lambda（編譯為委派）
Func<int, int, int> lambdaAdd = (a, b) => a + b;

// Expression Tree（編譯為表達式樹，可分析結構）
Expression<Func<int, int, int>> exprAdd = (a, b) => a + b;

// 手動建立表達式: (x, y) => x * y + 10
ParameterExpression x = Expression.Parameter(typeof(int), "x");
ParameterExpression y = Expression.Parameter(typeof(int), "y");
BinaryExpression multiply = Expression.Multiply(x, y);
ConstantExpression ten = Expression.Constant(10);
BinaryExpression add = Expression.Add(multiply, ten);
Expression<Func<int, int, int>> lambda =
    Expression.Lambda<Func<int, int, int>>(add, x, y);

// 動態建立查詢（ORM 常用）
Expression<Func<Product, bool>> predicate = p => p.Price > 1000;
var filtered = products.AsQueryable().Where(predicate);
```

**實際應用**:
- Entity Framework 的動態查詢
- AutoMapper 的屬性對應
- 規則引擎
- 動態 SQL 生成

#### 6️⃣ 複雜的 LINQ 查詢組合 (Advanced2_ComplexLinq.cs)

**學習目標**: 掌握複雜的 LINQ 操作，包括 Join, GroupBy, Aggregate 等進階技巧

**內容包含**:
- ✅ Join - 多資料表聯結
- ✅ GroupBy - 群組統計
- ✅ SelectMany - 扁平化巢狀集合
- ✅ Aggregate - 自訂累加邏輯
- ✅ 複雜的鏈式查詢
- ✅ 左外連接 (Left Join)
- ✅ 嵌套查詢
- ✅ 自訂擴展方法與 LINQ 整合
- ✅ Zip - 合併兩個序列
- ✅ 複合條件篩選與投影

**核心概念**:
```csharp
// Join - 聯結多個資料表
var result = from s in students
             join e in enrollments on s.Id equals e.StudentId
             join c in courses on e.CourseId equals c.Id
             select new { s.Name, c.Name, e.Score };

// GroupBy - 群組統計
var stats = enrollments
    .GroupBy(e => e.CourseId)
    .Select(g => new
    {
        CourseId = g.Key,
        Count = g.Count(),
        Average = g.Average(x => x.Score),
        Max = g.Max(x => x.Score)
    });

// SelectMany - 扁平化
var allCourses = students.SelectMany(
    s => enrollments.Where(e => e.StudentId == s.Id),
    (s, e) => new { s.Name, e.CourseId }
);

// Aggregate - 自訂累加
int sum = scores.Aggregate((total, score) => total + score);

// 複雜鏈式查詢
var topPerformers = students
    .Select(s => new { Student = s, Enrollments = GetEnrollments(s.Id) })
    .Where(x => x.Enrollments.Any())
    .Select(x => new { x.Student.Name, Avg = x.Enrollments.Average(e => e.Score) })
    .Where(x => x.Avg >= 85)
    .OrderByDescending(x => x.Avg);
```

**實際應用**:
- 資料庫查詢優化
- 報表統計分析
- 數據轉換與整合
- 複雜業務邏輯處理

## 學習建議

### 📚 學習路徑

1. **初學者** → 從範例 1、2 開始
   - 理解基本 Lambda 語法
   - 熟悉常用的 LINQ 方法

2. **有基礎者** → 進階到範例 3、4
   - 掌握委派與回呼函數
   - 了解閉包機制與常見陷阱

3. **進階學習** → 挑戰範例 5、6
   - 深入理解 Expression Trees
   - 精通複雜的 LINQ 查詢

### 💡 實踐建議

- ✅ **逐一執行範例**: 透過互動式選單依序執行每個範例
- ✅ **閱讀程式碼**: 每個範例都有詳細的註解說明
- ✅ **修改嘗試**: 試著修改參數和邏輯，觀察結果變化
- ✅ **實作練習**: 參考範例，嘗試解決實際問題

### ⚠️ 常見陷阱

1. **迴圈中的閉包**: 注意變數捕獲問題（見範例 4）
2. **延遲執行**: LINQ 查詢是延遲執行的，要注意時間點
3. **Lambda vs Expression**: 理解何時使用哪一種
4. **效能考量**: 複雜查詢可能影響效能，適時使用 `ToList()`

## 延伸學習資源

- [Microsoft C# Lambda 官方文件](https://learn.microsoft.com/zh-tw/dotnet/csharp/language-reference/operators/lambda-expressions)
- [LINQ 官方文件](https://learn.microsoft.com/zh-tw/dotnet/csharp/linq/)
- [Expression Trees 官方文件](https://learn.microsoft.com/zh-tw/dotnet/csharp/advanced-topics/expression-trees/)

## 關鍵字索引

`Lambda`, `LINQ`, `Func`, `Action`, `Predicate`, `Delegate`, `Closure`, `Expression Trees`, `Where`, `Select`, `GroupBy`, `Join`, `Aggregate`, `SelectMany`, `閉包`, `委派`, `表達式樹`

## 版本資訊

- **.NET Version**: 8.0
- **C# Version**: 12.0
- **最後更新**: 2025

---

## 快速開始

```bash
# 複製或下載專案後
cd AI_Lambda2

# 執行
dotnet run

# 選擇範例編號 (1-6) 開始學習
```

祝你學習愉快！如有問題歡迎隨時提問。
