using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C
{
    public class Student
    {
        // 學生姓名
        public string Name { get; set; }

        // 學生性別
        public GenderOption Gender { get; set; }

        // 學生學號
        public string StudentId { get; set; }

        // 學生選課
        public List<string> CourseList { get; set; }
    }

    public enum GenderOption
    {
        Male,
        Female
    }
}
