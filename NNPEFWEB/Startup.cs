using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using NNPEFWEB.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NNPEFWEB.Models;
using NETCore.MailKit.Extensions;
using NETCore.MailKit.Infrastructure.Internal;
using NNPEFWEB.Service;
using NNPEFWEB.Repository;
using Wkhtmltopdf.NetCore;
using Microsoft.Extensions.Logging;
using System.IO;
using Microsoft.AspNetCore.DataProtection;

namespace NNPEFWEB
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
            services.AddDataProtection();
             //.PersistKeysToDbContext<DbContext>()


            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
                    services.AddIdentity<ApplicationUser, IdentityRole>()
                    .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddDefaultUI()
                    .AddDefaultTokenProviders();

            services.AddWkhtmltopdf("wkhtmltopdf");
            services.AddAutoMapper(typeof(Startup));
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddScoped<IUnitOfWorks, UnitOfWorks>();
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IPersonInfoService, PersonInfoService>();
            services.AddScoped<IDashboard, Dashboard>();
            services.AddScoped<ICommandDashboard, CommandDashboard>();
            services.AddScoped<ISystemsInfoService, SystemsInfoService>();
            services.AddSession();

            services.AddMvc();

            services.AddAuthorizationCore(options =>
            {
                options.AddPolicy("SuperAdmin", policy => policy.RequireRole("SuperAdmin"));
                options.AddPolicy("Commander", policy => policy.RequireRole("Commander"));
                options.AddPolicy("Operator", policy => policy.RequireRole("Operator"));
                options.AddPolicy("Secertariat", policy => policy.RequireRole("Secertariat"));
                options.AddPolicy("Section A", policy => policy.RequireRole("Section A"));
                options.AddPolicy("Section B", policy => policy.RequireRole("Section B"));
                options.AddPolicy("Section C", policy => policy.RequireRole("Section C"));
                options.AddPolicy("Section D", policy => policy.RequireRole("Section D"));
                options.AddPolicy("Section E", policy => policy.RequireRole("Section E"));
                options.AddPolicy("Officers Section", policy => policy.RequireRole("Officers Section"));
            });
            //services.AddMailKit(optionBuilder =>
            //{
            //    optionBuilder.UseMailKit(new MailKitOptions()
            //    {
            //        //get options from sercets.json
            //        Server = Configuration["Server"],
            //        Port = Convert.ToInt32(Configuration["Port"]),
            //        SenderName = Configuration["SenderName"],
            //        SenderEmail = Configuration["SenderEmail"],

            //        // can be optional with no authentication 
            //        Account = Configuration["Account"],
            //        Password = Configuration["Password"],
            //        // enable ssl or tls
            //        Security = true
            //    });
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,ILoggerFactory loggerFactory)
        {
            var path = Directory.GetCurrentDirectory();
            loggerFactory.AddFile($"{path}\\Logs\\Log.txt");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSession();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=HomePage}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
