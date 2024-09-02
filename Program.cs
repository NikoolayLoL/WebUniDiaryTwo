using Microsoft.EntityFrameworkCore;
using WebUniDiaryTwo.Services;

namespace WebUniDiaryTwo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();

            builder.Services.AddLogging();

            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(40);
            });

            builder.Services.AddDbContext<UniversityContext>(options =>
            {
                string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;
                options.UseSqlServer(connectionString);
            });

            builder.Services.AddScoped<UserService>();

            builder.Services.AddRazorPages()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSession();

            app.UseMiddleware<LoginMiddleware>();

            app.MapRazorPages();

            app.Run();
        }
    }
}
