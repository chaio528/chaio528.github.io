using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Course> courseList = new List<Course>()
            {
              new Course() { CourseId = "A001", Name = "C#", Teacher = "Bill", Classroom = "L107", Credit = 3 },
              new Course() { CourseId = "A002", Name = "HTML/CSS", Teacher = "Amos", Classroom = "L104", Credit = 2 },
              new Course() { CourseId = "A003", Name = "JavaScript/jQuery", Teacher = "奚江華", Classroom = "L104", Credit = 3 },
              new Course() { CourseId = "A004", Name = "MSSQL", Teacher = "Jimmy", Classroom = "L202", Credit = 3 },
              new Course() { CourseId = "A005", Name = "MVC5/CoreMVC", Teacher = "奚江華", Classroom = "L107", Credit = 6 },
              new Course() { CourseId = "B001", Name = "VueJS", Teacher = "Jimmy", Classroom = "L104", Credit = 2 },
              new Course() { CourseId = "B002", Name = "DevOps", Teacher = "David", Classroom = "L107", Credit = 3 },
              new Course() { CourseId = "B003", Name = "MongoDB", Teacher = "Ben", Classroom = "L202", Credit = 2 },
              new Course() { CourseId = "B004", Name = "Redis", Teacher = "Ben", Classroom = "L202", Credit = 2 },
              new Course() { CourseId = "B005", Name = "Git", Teacher = "Andy", Classroom = "L107", Credit = 1 },
              new Course() { CourseId = "B006", Name = "Git", Teacher = "Jimmy", Classroom = "L107", Credit = 1 }
            };

            List<Student> studentList = new List<Student>()
            {
              new Student() { StudentId = "S0001", Name = "小新", Gender = GenderOption.Male, CourseList = new List<string>() { "A001", "A004", "B002", "B003", "B004", "B005" } },
              new Student() { StudentId = "S0002", Name = "妮妮", Gender = GenderOption.Female, CourseList = new List<string>() { "A002", "A003", "B001", "B002", "B005" } },
              new Student() { StudentId = "S0003", Name = "風間", Gender = GenderOption.Male, CourseList = new List<string>() { "A001", "A002", "A003", "A004", "A005", "B001", "B002", "B003", "B004", "B005"  } },
              new Student() { StudentId = "S0004", Name = "阿呆", Gender = GenderOption.Male, CourseList = new List<string>() { "A001", "A002", "A003", "A004", "A005" } },
              new Student() { StudentId = "S0005", Name = "正男", Gender = GenderOption.Male, CourseList = new List<string>() { "B001", "B002", "B003", "B004", "B005" } },
              new Student() { StudentId = "S0006", Name = "小丸子", Gender = GenderOption.Female, CourseList = new List<string>() { "A005" } },
              new Student() { StudentId = "S0007", Name = "小玉", Gender = GenderOption.Female, CourseList = new List<string>() { "A005", "B002", "B003", "B004" } },
            };

            #region 第1題
            // 1. 列出所有課程的名稱
            Console.WriteLine("1. 列出所有課程的名稱");
            {
                var result1 = courseList.Select((c, index) => $"第{index + 1}： {c.Name}");
                Display(result1);
            }

            Console.WriteLine($"{Environment.NewLine}");
            #endregion

            #region 第2題
            // 2. 列出所有在"L107"教室上課的課程ID
            Console.WriteLine("2. 列出所有在'L107'教室上課的課程ID");
            {
                var result2 = courseList.Where(c => c.Classroom == "L107").Select((o, index) => $"第{index + 1}： {o.CourseId}");
                Display(result2);

            }

            Console.WriteLine($"{Environment.NewLine}");
            #endregion

            #region 第3題
            // 3. 列出所有在'L107'教室上課，並且學分為3的課程ID
            Console.WriteLine("3. 列出所有在'L107'教室上課，並且學分為3的課程ID");
            {
                var result3 = courseList.Where(c => c.Classroom == "L107" && c.Credit == 3).Select((o, index) => $"第{index + 1}： {o.CourseId}");
                Display(result3);

            }

            Console.WriteLine($"{Environment.NewLine}");
            #endregion

            #region 第4題
            // 4. 列出所有老師的名字(名字不能重複出現)
            Console.WriteLine("4. 列出所有老師的名字(名字不能重複出現)");
            {
                var result4 = string.Join(Environment.NewLine, courseList.Select((c, index) => $"第{index + 1}： {c.Teacher}").Distinct());
                Console.Write(result4);
            }

            Console.WriteLine($"{Environment.NewLine}");
            #endregion

            #region 第5題
            // 5. 列出所有 有在'L202'上課的老師名字(名字不能重複出現)
            Console.WriteLine("5. 列出所有有在'L202'上課的老師名字(名字不能重複出現)");
            {
                var result5 = string.Join(Environment.NewLine, courseList.Where(c => c.Classroom == "L202").Select((c, index) => $"第{index + 1}：{c.Teacher}").Distinct());
                Console.Write(result5);
            }

            Console.WriteLine($"{Environment.NewLine}");
            #endregion

            #region 第6題
            // 6. 列出所有女性學生的名字
            Console.WriteLine("6. 列出所有女性學生的名字");
            {
                var result6 = studentList.Where(s => s.Gender == GenderOption.Female).Select(s => s.Name);
                Display(result6);
            }

            Console.WriteLine($"{Environment.NewLine}");
            #endregion

            #region 第7題
            // 7. 列出有上'Git'這門課的學員名字
            Console.WriteLine("7. 列出有上'Git'這門課的學員名字");
            {
                string courseName = "Git";

                var result7 = studentList.Where(s => s.CourseList.Any(c => courseList.Any(cc => cc.Name == courseName && c == cc.CourseId))).Select(s => s.Name);

                Console.WriteLine($"選修了課程名為'{courseName}'的學員名字：");
                Display(result7);
            }

            Console.WriteLine($"{Environment.NewLine}");
            #endregion

            #region 第8題
            // 8. 列出每個學員上的每一堂課
            // ex:
            /*
                       小玉: 
                            MVC5/CoreMVC
                            DevOps
                            MongoDB
                            Redis
                    */
            Console.WriteLine("8. 列出每個學員上的每一堂課");
            {
                var result88 = studentList.GroupBy(student => student.Name)
                .Select(group => $"{group.Key}:{Environment.NewLine}{string.Join(Environment.NewLine, group.SelectMany(student => student.CourseList).Join(courseList, courseId => courseId, course => course.CourseId, (courseId, course) => $"\t{course.Name}"))}");

                Console.Write(string.Join(Environment.NewLine, result88));
            }

            Console.WriteLine($"{Environment.NewLine}");
            #endregion

            #region 第9題
            // 9. 找出誰上的課數量最少
            Console.WriteLine("9. 找出誰上的課數量最少");
            {
                var courseCount = studentList.ToDictionary(s => s.Name, s => s.CourseList.Count);
                var minCourses = courseCount.OrderBy(c => c.Value).First();
                Console.Write($"上最少課的是{minCourses.Key},總共上了{minCourses.Value}門課");
            }

            Console.WriteLine($"{Environment.NewLine}");
            #endregion

            #region 第10題
            // 10. 找出誰修的學分總和小於10
            Console.WriteLine("10. 找出誰修的學分總和小於10");
            {// FirstOrDefault 是 LINQ 中的一個方法，從集合中檢索滿足指定條件的第一個元素。在這個特定的情境下，它被用來在 courseList 中查找與特定 courseId 匹配的課程。
             // ?前面的事情成立的話,就執行 ?? 前面的事情(也就是取得 Credit 的數字,並加總上去), 不成立的話就執行 ?? 後面的事情 ( 也就是將0加總上去 )
                var credits = studentList
                .Where(student => student.CourseList
                .Sum(courseId => courseList.FirstOrDefault(course => course.CourseId == courseId)?.Credit ?? 0) < 10)
                .Select(student => student.Name);
                Display(credits);
            }

            Console.WriteLine($"{Environment.NewLine}");
            #endregion

            #region 第11題
            // 11. 找出誰最後獲得學分數最高
            Console.WriteLine("11. 找出誰最後獲得學分數最高");
            {
                var best = studentList
                    .OrderByDescending(student => student.CourseList
                    .Sum(courseId => courseList.FirstOrDefault(course => course.CourseId == courseId)?.Credit ?? 0))
                    .FirstOrDefault();
                Console.Write(best.Name);
            }

            Console.WriteLine($"{Environment.NewLine}");
            #endregion

            #region 第12題(加分題)
            // 12. 十二生肖自定義排序
            Console.WriteLine("12. 十二生肖自定義排序");
            {
                var zoo = new List<string> { "龍", "鼠", "馬", "豬", "羊" }; //答案: 鼠、龍、馬、羊、豬
                Console.WriteLine($"排序前: {string.Join("、", zoo)}{Environment.NewLine}");
                Console.Write("排序後: ");
                //作答區

            }

            #endregion

            Console.ReadLine();
        }

        private static void Display(IEnumerable<string> result)
        {
            Console.Write(string.Join(Environment.NewLine, result));
        }
    }
}
