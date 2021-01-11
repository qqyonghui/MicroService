using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace School.MicroService.TeacherService.Models
{
    /// <summary>
    /// 讲师类
    /// </summary>
    public class Teacher
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Sex { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

    }
}
