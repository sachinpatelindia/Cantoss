using Cantoss.Azure.Library;
using Cantoss.Azure.Library.Cosmos;
using Cantoss.Service.Courses;
using Cantoss.Service.Portals;
using Cantoss.Service.SEO;
using Cantoss.Web.Framework.MVC.Routing;

namespace Cantoss.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddScoped(typeof(SlugRouteTransformer));
            CosmosDbDependencyRegistrar registrar = new CosmosDbDependencyRegistrar(builder.Services, builder.Configuration);
            builder.Services.AddScoped<IPortalService, PortalService>();
            builder.Services.AddScoped<ICourseService, CourseService>();
            builder.Services.AddScoped<IUrlRecordService, UrlRecordService>();
            builder.Services.AddScoped(typeof(ICosmosDbHandler<>), typeof(CosmosDbHandler<>));
            builder.Services.AddSingleton<IRoutePublisher, RoutePublisher>();
    

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();
            
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(name: "areaRoute", pattern: "{area:exists}/{controller=Course}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(name: "areaRoute", pattern: "{area:exists}/{controller=values}/{action=Index}/{id?}");
                app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
                
                var result = app.Services.GetService<IRoutePublisher>();
                result.RegisterRoutes(endpoints);
            });



            app.Run();
        }
    }
}
