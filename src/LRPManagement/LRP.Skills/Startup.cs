using LRP.Skills.Data;
using LRP.Skills.Data.CharacterSkills;
using LRP.Skills.Data.Skills;
using LRPManagement.Data.CharacterSkills;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Reflection;

namespace LRP.Skills
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
            services.AddDbContext<SkillDbContext>
            (
                options =>
                {
                    options.UseSqlServer
                    (
                        Configuration.GetConnectionString("SkillDb"),
                        sqlOptions =>
                        {
                            sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                            // Resiliency
                            sqlOptions.EnableRetryOnFailure
                                (5, TimeSpan.FromSeconds(30), null);
                        }
                    );
                }
            );

            services.AddControllers();
            services.AddScoped<ISkillRepository, SkillRepository>();
            services.AddScoped<ICharacterSkillRepository, CharacterSkillRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                logger.LogInformation("In Development Environment");
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}