using System;
using System.Linq;
using System.Collections.Generic;

namespace AI_Lambda2.Examples
{
    /// <summary>
    /// 進階範例 2: 複雜的 LINQ 查詢組合
    /// 學習目標: 掌握複雜的 LINQ 操作，包括 Join, GroupBy, Aggregate 等進階技巧
    /// </summary>
    public static class Advanced2_ComplexLinq
    {
        public static void Run()
        {
            Console.WriteLine("【進階範例 2: 複雜的 LINQ 查詢組合】\n");

            // 準備測試資料
            var students = GetStudents();
            var courses = GetCourses();
            var enrollments = GetEnrollments();

            // 範例 1: Join - 多資料表聯結
            Console.WriteLine("1. Join - 學生選課資訊");

            var studentCourses = from s in students
                                 join e in enrollments on s.Id equals e.StudentId
                                 join c in courses on e.CourseId equals c.Id
                                 select new
                                 {
                                     StudentName = s.Name,
                                     CourseName = c.Name,
                                     Score = e.Score
                                 };

            foreach (var item in studentCourses)
            {
                Console.WriteLine($"   {item.StudentName} - {item.CourseName}: {item.Score}分");
            }

            // 範例 2: GroupBy - 群組統計
            Console.WriteLine("\n\n2. GroupBy - 按課程分組計算平均分數");

            var courseStats = from e in enrollments
                              join c in courses on e.CourseId equals c.Id
                              group e by new { c.Id, c.Name } into g
                              select new
                              {
                                  CourseName = g.Key.Name,
                                  StudentCount = g.Count(),
                                  AverageScore = g.Average(x => x.Score),
                                  MaxScore = g.Max(x => x.Score),
                                  MinScore = g.Min(x => x.Score)
                              };

            foreach (var stat in courseStats)
            {
                Console.WriteLine($"   {stat.CourseName}:");
                Console.WriteLine($"      學生數: {stat.StudentCount}");
                Console.WriteLine($"      平均分: {stat.AverageScore:F2}");
                Console.WriteLine($"      最高分: {stat.MaxScore}");
                Console.WriteLine($"      最低分: {stat.MinScore}");
            }

            // 範例 3: SelectMany - 扁平化巢狀集合
            Console.WriteLine("\n\n3. SelectMany - 扁平化學生所有課程");

            var allStudentCourses = students.SelectMany(
                s => enrollments.Where(e => e.StudentId == s.Id),
                (s, e) => new { s.Name, e.CourseId, e.Score }
            );

            foreach (var item in allStudentCourses.Take(5))
            {
                Console.WriteLine($"   {item.Name} 修課程 {item.CourseId}, 分數 {item.Score}");
            }

            // 範例 4: Aggregate - 自訂累加邏輯
            Console.WriteLine("\n\n4. Aggregate - 計算總分與統計");

            var scores = new[] { 85, 92, 78, 95, 88 };

            // 計算總和
            int sum = scores.Aggregate((total, score) => total + score);
            Console.WriteLine($"   分數: {string.Join(", ", scores)}");
            Console.WriteLine($"   總和: {sum}");

            // 建立統計字串
            string stats = scores.Aggregate(
                "分數統計:",
                (result, score) => result + $" {score}",
                result => result + $" (總和: {scores.Sum()})"
            );
            Console.WriteLine($"   {stats}");

            // 範例 5: 複雜的鏈式查詢
            Console.WriteLine("\n\n5. 複雜鏈式查詢 - 優秀學生分析");

            var topPerformers = students
                .Select(s => new
                {
                    Student = s,
                    Enrollments = enrollments.Where(e => e.StudentId == s.Id).ToList()
                })
                .Where(x => x.Enrollments.Any())
                .Select(x => new
                {
                    x.Student.Name,
                    x.Student.Age,
                    CourseCount = x.Enrollments.Count,
                    AverageScore = x.Enrollments.Average(e => e.Score),
                    TotalScore = x.Enrollments.Sum(e => e.Score)
                })
                .Where(x => x.AverageScore >= 85)
                .OrderByDescending(x => x.AverageScore)
                .ThenByDescending(x => x.CourseCount);

            Console.WriteLine("   平均分數 >= 85 的學生 (按平均分排序):");
            foreach (var student in topPerformers)
            {
                Console.WriteLine($"   {student.Name} ({student.Age}歲):");
                Console.WriteLine($"      修課數: {student.CourseCount}");
                Console.WriteLine($"      平均分: {student.AverageScore:F2}");
                Console.WriteLine($"      總分: {student.TotalScore}");
            }

            // 範例 6: 左外連接 (Left Join)
            Console.WriteLine("\n\n6. 左外連接 - 包含未選課的學生");

            var leftJoin = from s in students
                           join e in enrollments on s.Id equals e.StudentId into studentEnrollments
                           from se in studentEnrollments.DefaultIfEmpty()
                           select new
                           {
                               StudentName = s.Name,
                               CourseId = se?.CourseId ?? 0,
                               Score = se?.Score ?? 0
                           };

            var studentSummary = leftJoin
                .GroupBy(x => x.StudentName)
                .Select(g => new
                {
                    Name = g.Key,
                    CourseCount = g.Count(x => x.CourseId != 0),
                    AverageScore = g.Where(x => x.CourseId != 0).Any()
                        ? g.Where(x => x.CourseId != 0).Average(x => x.Score)
                        : 0
                });

            foreach (var s in studentSummary)
            {
                Console.WriteLine($"   {s.Name}: {s.CourseCount} 門課, 平均 {s.AverageScore:F2}分");
            }

            // 範例 7: 分組後再查詢 (嵌套查詢)
            Console.WriteLine("\n\n7. 嵌套查詢 - 每個年齡層的最高分學生");

            var topByAge = students
                .GroupBy(s => s.Age)
                .Select(ageGroup => new
                {
                    Age = ageGroup.Key,
                    TopStudent = ageGroup
                        .Select(s => new
                        {
                            s.Name,
                            AvgScore = enrollments
                                .Where(e => e.StudentId == s.Id)
                                .Average(e => (double?)e.Score) ?? 0
                        })
                        .OrderByDescending(x => x.AvgScore)
                        .First()
                })
                .OrderBy(x => x.Age);

            foreach (var item in topByAge)
            {
                Console.WriteLine($"   {item.Age}歲: {item.TopStudent.Name} (平均 {item.TopStudent.AvgScore:F2}分)");
            }

            // 範例 8: 自訂擴展方法與 LINQ 整合
            Console.WriteLine("\n\n8. 自訂擴展方法 - 批次處理");

            var numbers = Enumerable.Range(1, 20);
            var batches = numbers.Batch(5);

            Console.WriteLine("   將 1-20 分成每批 5 個:");
            int batchNum = 1;
            foreach (var batch in batches)
            {
                Console.WriteLine($"   批次 {batchNum}: {string.Join(", ", batch)}");
                batchNum++;
            }

            // 範例 9: Zip - 合併兩個序列
            Console.WriteLine("\n\n9. Zip - 合併學生與排名");

            var topStudents = students
                .Select(s => new
                {
                    s.Name,
                    AvgScore = enrollments
                        .Where(e => e.StudentId == s.Id)
                        .Average(e => (double?)e.Score) ?? 0
                })
                .OrderByDescending(x => x.AvgScore)
                .ToList();

            var rankings = Enumerable.Range(1, topStudents.Count);
            var rankedStudents = rankings.Zip(topStudents, (rank, student) => new
            {
                Rank = rank,
                student.Name,
                student.AvgScore
            });

            Console.WriteLine("   學生排名:");
            foreach (var item in rankedStudents)
            {
                Console.WriteLine($"   第 {item.Rank} 名: {item.Name} (平均 {item.AvgScore:F2}分)");
            }

            // 範例 10: 複合條件篩選與投影
            Console.WriteLine("\n\n10. 複合條件篩選 - 進階查詢");

            var complexQuery = from s in students
                               let studentEnrollments = enrollments.Where(e => e.StudentId == s.Id)
                               let avgScore = studentEnrollments.Any()
                                   ? studentEnrollments.Average(e => e.Score)
                                   : 0
                               where s.Age >= 21 && avgScore >= 80
                               orderby avgScore descending
                               select new
                               {
                                   s.Name,
                                   s.Age,
                                   CourseCount = studentEnrollments.Count(),
                                   AverageScore = avgScore,
                                   PassedCourses = studentEnrollments.Count(e => e.Score >= 60)
                               };

            Console.WriteLine("   年齡 >= 21 且平均分 >= 80 的學生:");
            foreach (var item in complexQuery)
            {
                Console.WriteLine($"   {item.Name} ({item.Age}歲):");
                Console.WriteLine($"      修課數: {item.CourseCount}, 及格數: {item.PassedCourses}");
                Console.WriteLine($"      平均分: {item.AverageScore:F2}");
            }
        }

        // 測試資料
        private static List<Student> GetStudents()
        {
            return new List<Student>
            {
                new Student { Id = 1, Name = "王小明", Age = 20 },
                new Student { Id = 2, Name = "李小華", Age = 22 },
                new Student { Id = 3, Name = "張小美", Age = 21 },
                new Student { Id = 4, Name = "陳小強", Age = 23 },
                new Student { Id = 5, Name = "林小芳", Age = 20 }
            };
        }

        private static List<Course> GetCourses()
        {
            return new List<Course>
            {
                new Course { Id = 1, Name = "C# 程式設計" },
                new Course { Id = 2, Name = "資料結構" },
                new Course { Id = 3, Name = "演算法" },
                new Course { Id = 4, Name = "資料庫系統" }
            };
        }

        private static List<Enrollment> GetEnrollments()
        {
            return new List<Enrollment>
            {
                new Enrollment { StudentId = 1, CourseId = 1, Score = 85 },
                new Enrollment { StudentId = 1, CourseId = 2, Score = 90 },
                new Enrollment { StudentId = 2, CourseId = 1, Score = 92 },
                new Enrollment { StudentId = 2, CourseId = 3, Score = 88 },
                new Enrollment { StudentId = 2, CourseId = 4, Score = 95 },
                new Enrollment { StudentId = 3, CourseId = 2, Score = 78 },
                new Enrollment { StudentId = 3, CourseId = 3, Score = 82 },
                new Enrollment { StudentId = 4, CourseId = 1, Score = 88 },
                new Enrollment { StudentId = 4, CourseId = 4, Score = 91 }
            };
        }

        // 資料類別
        private class Student
        {
            public int Id { get; set; }
            public string Name { get; set; } = "";
            public int Age { get; set; }
        }

        private class Course
        {
            public int Id { get; set; }
            public string Name { get; set; } = "";
        }

        private class Enrollment
        {
            public int StudentId { get; set; }
            public int CourseId { get; set; }
            public int Score { get; set; }
        }
    }

    // 擴展方法
    public static class LinqExtensions
    {
        public static IEnumerable<IEnumerable<T>> Batch<T>(this IEnumerable<T> source, int batchSize)
        {
            var batch = new List<T>(batchSize);
            foreach (var item in source)
            {
                batch.Add(item);
                if (batch.Count == batchSize)
                {
                    yield return batch;
                    batch = new List<T>(batchSize);
                }
            }
            if (batch.Count > 0)
            {
                yield return batch;
            }
        }
    }
}
