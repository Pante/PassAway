using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using PassAway.Models;
using PassAway.Models.Shared;
using PassAway.Models.ViewModels;
using PassAway.Infrastructure;


namespace PassAway {

    public class Startup {

        public IConfigurationRoot Configuration { get; }

        public Startup(IHostingEnvironment env) {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }


        public void ConfigureServices(IServiceCollection services) {
            services.AddTransient<IPasswordValidator<User>, CustomPasswordValidator>();
            services.AddTransient<IUserValidator<User>, CustomUserValidator>();
            services.AddDbContext<AppIdentityDbContext>(options => options.UseSqlServer(Configuration["Data:IdentityUsers:ConnectionString"]));
            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<AppIdentityDbContext>();
            services.AddIdentity<User, IdentityRole>(opts => {
                    opts.User.RequireUniqueEmail = true;
                    opts.Password.RequiredLength = 6;
                    opts.Password.RequireNonAlphanumeric = false;
                    opts.Password.RequireLowercase = false;
                    opts.Password.RequireUppercase = false;
                    opts.Password.RequireDigit = false;
            }).AddEntityFrameworkStores<AppIdentityDbContext>();

            services.AddDbContext<ApplicationContext>(options =>
            options.UseSqlServer(
            Configuration["Data:PassAway:ConnectionString"]));

            services.AddMvc();
            services.AddTransient<IElementViewModelRepository, ListElementViewModelRepository>();
            services.AddTransient<ProductRepository, ProductRepository>();
        }


        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory) {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
                app.UseStaticFiles();
                app.UseIdentity();
                app.UseMvc(routes =>
                    routes.MapRoute(
                        name: "default",
                        template: "{controller=Home}/{action=Index}/{id?}"
                    )
                );
                AppIdentityDbContext.CreateAdminAccount(app.ApplicationServices, Configuration).Wait();
        }
    }
}
