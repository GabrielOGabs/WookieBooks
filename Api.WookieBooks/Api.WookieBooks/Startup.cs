using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Services.WookieBooks;
using Microsoft.EntityFrameworkCore;
using Services.WookieBooks.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MockData.WookieBooks;
using System.Reflection;
using System.IO;
using Microsoft.OpenApi.Models;

namespace Api.WookieBooks
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
            services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("WookieBooks"));

            services.AddScoped<IBooksService, BooksService>();
            services.AddScoped<IUsersService, UsersService>();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("WookieBooksAPISpec", 
                    new Microsoft.OpenApi.Models.OpenApiInfo
                    {
                        Title = "Wookie Books API",
                        Version = "1",
                        Description = "Test API for Savvyy.ai position of Backend engineer.",
                        Contact = new OpenApiContact
                        {
                            Name = "Gabriel de Paula Santos",
                            Email = "gabriel.santos@outlook.com"
                        },
                        License = new OpenApiLicense
                        {
                            Name = "MIT License",
                            Url = new Uri("https://en.wikipedia.org/wiki/MIT_License")
                        }
                    });

                var xmlFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlFullPath = Path.Combine(AppContext.BaseDirectory, xmlFileName);
                options.IncludeXmlComments(xmlFullPath);
            });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                MockDataInitializer.Initialize(serviceScope.ServiceProvider);
            }

            app.UseHttpsRedirection();

            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/WookieBooksAPISpec/swagger.json", "Wookie Books API");
                options.RoutePrefix = string.Empty;
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
