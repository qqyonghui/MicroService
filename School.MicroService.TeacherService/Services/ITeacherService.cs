using School.MicroService.TeacherService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace School.MicroService.TeacherService.Services
{
    public interface ITeacherService
    {
        IEnumerable<Teacher> GetTeachers();

        Teacher GetTeacherById(int id);

        int Create(Teacher teacher);

        int Update(Teacher teacher);

        int Delete(Teacher teacher);
    }
}
