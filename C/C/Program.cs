using C;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpMidTerm
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Course> courseList = new List<Course>()
            {
                new Course() { CourseId = "A001", Name = "C#", Teacher = "Bill", Classroom = "L107", Credit = 3 },
                new Course() { CourseId = "A002", Name = "HTML/CSS", Teacher = "Amos", Classroom = "L104", Credit = 2 },
                new Course()
                    { CourseId = "A003", Name = "JavaScript/jQuery", Teacher = "奚江華", Classroom = "L104", Credit = 3 },
                new Course() { CourseId = "A004", Name = "MSSQL", Teacher = "Jimmy", Classroom = "L202", Credit = 3 },
                new Course()
                    { CourseId = "A005", Name = "MVC5/CoreMVC", Teacher = "奚江華", Classroom = "L107", Credit = 6 },
                new Course() { CourseId = "B001", Name = "VueJS", Teacher = "Jimmy", Classroom = "L104", Credit = 2 },
                new Course() { CourseId = "B002", Name = "DevOps", Teacher = "David", Classroom = "L107", Credit = 3 },
                new Course() { CourseId = "B003", Name = "MongoDB", Teacher = "Ben", Classroom = "L202", Credit = 2 },
                new Course() { CourseId = "B004", Name = "Redis", Teacher = "Ben", Classroom = "L202", Credit = 2 },
                new Course() { CourseId = "B005", Name = "Git", Teacher = "Andy", Classroom = "L107", Credit = 1 },
                new Course() { CourseId = "B006", Name = "Git", Teacher = "Jimmy", Classroom = "L107", Credit = 1 }
            };

            List<Student> studentList = new List<Student>()
            {
                new Student()
                {
                    StudentId = "S0001", Name = "小新", Gender = GenderOption.Male,
                    CourseList = new List<string>() { "A001", "A004", "B002", "B003", "B004", "B005" }
                },
                new Student()
                {
                    StudentId = "S0002", Name = "妮妮", Gender = GenderOption.Female,
                    CourseList = new List<string>() { "A002", "A003", "B001", "B002", "B005" }
                },
                new Student()
                {
                    StudentId = "S0003", Name = "風間", Gender = GenderOption.Male,
                    CourseList = new List<string>()
                        { "A001", "A002", "A003", "A004", "A005", "B001", "B002", "B003", "B004", "B005" }
                },
                new Student()
                {
                    StudentId = "S0004", Name = "阿呆", Gender = GenderOption.Male,
                    CourseList = new List<string>() { "A001", "A002", "A003", "A004", "A005" }
                },
                new Student()
                {
                    StudentId = "S0005", Name = "正男", Gender = GenderOption.Male,
                    CourseList = new List<string>() { "B001", "B002", "B003", "B004", "B005" }
                },
                new Student()
                {
                    StudentId = "S0006", Name = "小丸子", Gender = GenderOption.Female,
                    CourseList = new List<string>() { "A005" }
                },
                new Student()
                {
                    StudentId = "S0007", Name = "小玉", Gender = GenderOption.Female,
                    CourseList = new List<string>() { "A005", "B002", "B003", "B004" }
                },
            };

            #region 第1題

            // 1. 列出所有課程的名稱
            Console.WriteLine("1. 列出所有課程的名稱");
            {
                // 建議：將變數重新命名為有意義的名稱，例如 courseNames 或 courseNameList。
                var courseNames = courseList.Select((c, index) => $"第{index + 1}： {c.Name}");
                Display(courseNames);
            }

            Console.WriteLine($"{Environment.NewLine}");

            #endregion

            #region 第2題

            // 2. 列出所有在"L107"教室上課的課程ID
            Console.WriteLine("2. 列出所有在'L107'教室上課的課程ID");
            {
                // 建議：將變數重新命名為有意義的名稱。
                var courseID = courseList.Where(c => c.Classroom == "L107")
                    .Select((o, index) => $"第{index + 1}： {o.CourseId}");
                Display(courseID);
            }

            Console.WriteLine($"{Environment.NewLine}");

            #endregion

            #region 第3題

            // 3. 列出所有在'L107'教室上課，並且學分為3的課程ID
            Console.WriteLine("3. 列出所有在'L107'教室上課，並且學分為3的課程ID");
            {
                var creditThreeCourseID = courseList.Where(c => c.Classroom == "L107" && c.Credit == 3)
                    .Select((o, index) => $"第{index + 1}： {o.CourseId}");
                Display(creditThreeCourseID);
            }

            Console.WriteLine($"{Environment.NewLine}");

            #endregion

            #region 第4題

            // 4. 列出所有老師的名字(名字不能重複出現)
            Console.WriteLine("4. 列出所有老師的名字(名字不能重複出現)");
            {
                var teacherName = string.Join(Environment.NewLine,
                    courseList.Select((c, index) => $"第{index + 1}： {c.Teacher}").Distinct());
                Console.Write(teacherName);
            }

            Console.WriteLine($"{Environment.NewLine}");

            #endregion

            #region 第5題

            // 5. 列出所有 有在'L202'上課的老師名字(名字不能重複出現)
            Console.WriteLine("5. 列出所有有在'L202'上課的老師名字(名字不能重複出現)");
            {
                var atL202TeacherName = string.Join(Environment.NewLine,
                    courseList.Where(c => c.Classroom == "L202").Select((c, index) => $"第{index + 1}：{c.Teacher}")
                        .Distinct());
                Console.Write(atL202TeacherName);
            }

            Console.WriteLine($"{Environment.NewLine}");

            #endregion

            #region 第6題

            // 6. 列出所有女性學生的名字
            Console.WriteLine("6. 列出所有女性學生的名字");
            {
                var femaleStudents = studentList.Where(s => s.Gender == GenderOption.Female).Select(s => s.Name);
                Display(femaleStudents);
            }

            Console.WriteLine($"{Environment.NewLine}");

            #endregion

            #region 第7題

            // 7. 列出有上'Git'這門課的學員名字
            Console.WriteLine("7. 列出有上'Git'這門課的學員名字");
            {
                string courseName = "Git";

                // Q: Explain Code Below 解釋下面的程式碼。
                /*
                 回答:
                cc 代表 courseList 課程列表
                c  代表 s.CourseList 學生課程列表

                從學生列表裡過濾出   ,當"學生的課程列表" 與 "關聯的課程列表"  有任何
                "課程列表"裡 有名字與 courseName(也就是"Git")符合,同時 "學生課程列表" 有課程ID與 "課程列表"的ID 一樣時
                選取符合條件的學生的名字
                 */
                var gitStudentsName = studentList.Where(s =>s.CourseList.Any(c => courseList.Any(cc => cc.Name == courseName && c == cc.CourseId)))
                    .Select(s => s.Name);

                Console.WriteLine($"選修了課程名為'{courseName}'的學員名字：");
                Display(gitStudentsName);
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
                // Q: 為什麼使用 GroupBy？
                // Q: 解釋下面的程式碼。
                // 可以參考 C# Linq 助教版作業，印出 personList 每一位 person 所有喜愛的電影這題的寫法
                // 先用 foreach 寫看看，再改用 Linq 寫看看
                /*
                 回答:
                ( 因為題目的主語是學員,所以我用學員的名字分群 )
                用學生的名字分群組 ,分出來的 group.Key 就是學生的名字   ,string.Join(Environment.NewLine 把我後面要選出來的東西 組成字串,並換行
                group.SelectMany 這段,用已分組的學生名字 ,選出該學生的課程列表 ,但因為是 List<string> 所以要用 SelectMany 展平
                Join             這段,聯結了 courseList 列表
                courseId => courseId        是聯結 student.CourseList 裡的課程ID
                course => course.CourseId   是聯結 courseList         裡的課程ID
                (courseId, course)          符合這兩個條件的
                {course.Name}               輸出課程名稱
                 */
                var courses = studentList.GroupBy(student => student.Name)
                    .Select(group =>
                        $"{group.Key}:{Environment.NewLine}{string.Join(Environment.NewLine, group.SelectMany(student => student.CourseList).Join(courseList, courseId => courseId, course => course.CourseId, (courseId, course) => $"\t{course.Name}"))}");

                Console.Write(string.Join(Environment.NewLine, courses));
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
            {
                //  解釋下面的程式碼。

                // FirstOrDefault 被用來在 courseList 中查找與特定 courseId 匹配的課程。
                // ?前面的事情成立的話,就執行 ?? 前面的事情(也就是取得 Credit 的數字,並加總上去), 不成立(也就是找不到,也就是null)的話就執行 ?? 後面的事情 ( 也就是將0加總上去 )
                /*
                 回答:
                從"學生列表裡" 過濾出 ,"學生的課程列表" 的學分總和
                藉由學生列表的 courseId  ,從 courseList 裡找出第一個相符合的 course.CourseId ,如果有找到的話 ,就取得該 courseId 的Credit值 ,並加總
                                                                                           ,如果沒有符合條件的話 ,就用??後面的 0 加總
                選取出加總後 < 10 的學生名字
                 */

                var credits = studentList
                    .Where(student => student.CourseList
                    .Sum(courseId =>courseList
                    .FirstOrDefault(course => course.CourseId == courseId)?.Credit ?? 0) < 10)
                    .Select(student => student.Name);
                Display(credits);
            }

            Console.WriteLine($"{Environment.NewLine}");

            #endregion

            #region 第11題

            // 11. 找出誰最後獲得學分數最高
            Console.WriteLine("11. 找出誰最後獲得學分數最高");
            {
                // 解釋下面的程式碼。
                // 建議：將變數重新命名為有意義的名稱。
                /*
                回答:
                從學生列表裡 ,將學分總和 從大到小排列 ,並取出排列後的第一個人的名字FirstOrDefault( 也就是學分總和最高的人 )

                藉由學生課程代號 courseId => 去 "課程列表" 裡找出第一個符合課程代號的
                有找到符合的 就取他的 .Credit學分屬性的值 ,並加總
                沒找到      ,就依照??後面的 0 去加總
                 */
                var topCredit = studentList
                    .OrderByDescending(student => student.CourseList
                        .Sum(courseId => courseList.FirstOrDefault(course => course.CourseId == courseId)?.Credit ?? 0))
                    .FirstOrDefault();
                Console.Write(topCredit.Name);
            }

            Console.WriteLine($"{Environment.NewLine}");

            #endregion

            #region 第12題(加分題)

            // 12. 十二生肖自定義排序
            // 問題：如何定義自定義的排序順序？
            // https://dotblogs.com.tw/billchung/2018/05/02/030556
            // https://learn.microsoft.com/zh-tw/troubleshoot/developer/visualstudio/csharp/language-compilers/use-icomparable-icomparer
            // 問題：閱讀上面的參考資料，並為中國十二生肖實現自定義的排序順序。
            // 然後試著解釋下面的程式碼。
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