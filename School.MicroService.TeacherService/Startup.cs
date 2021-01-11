using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using School.MicroService.TeacherService.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using School.MicroService.TeacherService.Services;
using School.MicroService.TeacherService.Repositories;
using School.MicroService.Core.Registry.Extensions;

namespace School.MicroService.TeacherService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //ע��EF������
            services.AddDbContext<TeacherContext>(p=> {
                p.UseSqlServer(Configuration.GetConnectionString("ConnStr"));
            });
            //ע�����
            services.AddScoped<ITeacherService, TeacherService.Services.TeacherService>();
            //ע��ִ�
            services.AddScoped<ITeacherRepository, TeacherRepository>();
            //ע��Consul
            services.AddConsulRegistry(Configuration);


            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            // 1��consul����ע�� ,����ע�᷽��
            app.UserConsulConfig();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
