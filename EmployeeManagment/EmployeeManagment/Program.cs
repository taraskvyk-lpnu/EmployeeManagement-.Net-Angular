using EmployeeManagment.Data;
using EmployeeManagment.Filters;
using EmployeeManagment.Middlewares;
using EmployeeManagment.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagment
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<AppDbContext>(
                options => 
                    options.UseInMemoryDatabase("EmployeeDb")
                    );

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<ValidationFilter>();
            builder.Services.AddScoped<ActionFilter>();
            builder.Services.AddScoped<ResultFilter>();
            builder.Services.AddScoped<ResourseFilter>();
            builder.Services.AddScoped<AuthorizationFilter>();
            
            builder.Services.AddCors(
                options => options.AddPolicy("AllowAll", 
                    builder => builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader())
                );

            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddControllers();
            
            var app = builder.Build();
            
            app.MapGet("/", () => Results.Redirect("swagger/index.html"));

            app.UseMiddleware<CustomMiddleware1>();
            app.UseMiddleware<CustomMiddleware2>();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    //c.RoutePrefix = string.Empty;
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "EmployeeManagment v1");
                });
            }

            app.MapControllers();
            app.UseCors("AllowAll");

            app.Run();
        }
    }
}
