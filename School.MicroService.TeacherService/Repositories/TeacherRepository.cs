using School.MicroService.TeacherService.Context;
using School.MicroService.TeacherService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace School.MicroService.TeacherService.Repositories
{
    public class TeacherRepository : ITeacherRepository
    {
        private TeacherContext _teacherContext;
        public TeacherRepository(TeacherContext teacherContext)
        {
            this._teacherContext = teacherContext;
        }
        public int Create(Teacher teacher)
        {
            _teacherContext.Teachers.Add(teacher);
            return _teacherContext.SaveChanges();
        }

        public int Delete(Teacher teacher)
        {
            _teacherContext.Entry(teacher).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            return _teacherContext.SaveChanges();
        }

        /// <summary>
        /// 根据Id获取教师
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Teacher GetTeacherById(int id)
        {
            Teacher teacher=_teacherContext.Teachers.Find(id);
            return teacher;
        }

        public IEnumerable<Teacher> GetTeachers()
        {
            return _teacherContext.Teachers.ToList();
        }

        public int Update(Teacher teacher)
        {
            _teacherContext.Entry(teacher).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return _teacherContext.SaveChanges();
        }
    }
}
