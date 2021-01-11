using School.MicroService.TeacherService.Models;
using School.MicroService.TeacherService.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace School.MicroService.TeacherService.Services
{
    public class TeacherService : ITeacherService
    {
        private ITeacherRepository _reprository;
        public TeacherService(ITeacherRepository reprository)
        {
            _reprository = reprository;
        }
        public int Create(Teacher teacher)
        {
            return _reprository.Create(teacher);
        }

        public int Delete(Teacher teacher)
        {
            return _reprository.Delete(teacher);
        }

        public Teacher GetTeacherById(int id)
        {
            return _reprository.GetTeacherById(id);
        }

        public IEnumerable<Teacher> GetTeachers()
        {
            return _reprository.GetTeachers();
        }

        public int Update(Teacher teacher)
        {
            return _reprository.Update(teacher);
        }
    }
}
