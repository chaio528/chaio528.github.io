using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C
{
    public class Course
    {
        // 課程ID
        public string CourseId { get; set; }

        // 課程名稱
        public string Name { get; set; }

        // 課程指導教師
        public string Teacher { get; set; }

        // 課程教室
        public string Classroom { get; set; }

        // 課程學分
        public int Credit { get; set; }
    }
}
