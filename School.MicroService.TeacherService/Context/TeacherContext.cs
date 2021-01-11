using Microsoft.EntityFrameworkCore;
using School.MicroService.TeacherService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace School.MicroService.TeacherService.Context
{
    public class TeacherContext:DbContext
    {
        public TeacherContext(DbContextOptions<TeacherContext> options) :base(options)
        {

        }

        public DbSet<Teacher> Teachers { get; set; }

    }
}
