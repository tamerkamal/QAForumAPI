using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QAForumAPI.DAL.Repositories;
using QAForumAPI.BOL.Models;
using QAForumAPI.DAL;
using QAForumAPI.MiddleWares;

namespace QAForumAPI
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
            services.AddMvc(options =>
            {
                options.EnableEndpointRouting = false;
            });

            services.AddDbContext<QAForumContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddControllersWithViews()
                    .AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling =
            Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            AddModelServices(services);
            AddRepositoryServices(services);
            AddSession(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            //app.UseDeveloperExceptionPage();
            //}
            //else
            //{
            app.UseExceptionHandler(new ExceptionHandlerOptions
            {
                ExceptionHandler = new JsonExceptionMiddleware().Invoke
            });
            //}
            //app.UseHttpsRedirection();   
            app.UseSession();
            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });
            //app.UseAuthorization();
        }

        void AddModelServices(IServiceCollection services)
        {
            services.AddTransient<User>();
            services.AddTransient<Question>();
            services.AddTransient<Answer>();
            services.AddTransient<Vote>();
        }
        void AddRepositoryServices(IServiceCollection services)
        {
            services.AddScoped(typeof(IUsersRepository), typeof(UsersRepository));
            services.AddScoped(typeof(IQuestionsRepository), typeof(QuestionsRepository));
            services.AddScoped(typeof(IAnswersRepository), typeof(AnswersRepository));
            services.AddScoped(typeof(IAnswerVotesRepository), typeof(AnswerVotesRepository));
        }
        void AddSession(IServiceCollection services)
        {
            services.AddSession(options =>
            {
                options.Cookie.Name = ".QAForum.Session";
                options.IdleTimeout = TimeSpan.FromDays(0.5); //long IdleTimeout just for simplicity
                options.Cookie.IsEssential = true;
            });
        }
    }
}
