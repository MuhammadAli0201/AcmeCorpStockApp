using AcmeCorpStockApp.BLL.Interfaces;
using AcmeCorpStockApp.BLL.Services;
using AcmeCorpStockApp.DAL;
using AcmeCorpStockApp.DAL.Interfaces;
using AcmeCorpStockApp.DAL.Repositories;
using AcmeCorpStockApp.Services.Infrastructure;
using AcmeCorpStockApp.Services.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Recaptcha.Web.Configuration;

namespace AcmeCorpStockApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private readonly IConfiguration Configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            //CODING STANDARDS THAT I FOLLOW
            //https://www.aspdotnet-suresh.com/2010/04/c-coding-standards-and-best-programming.html#google_vignette

            RecaptchaConfigurationManager.SetConfiguration(Configuration);
            services.AddControllersWithViews();

            //REPOSITORIES

            services.AddScoped<IManageStockAppUserRepo, ManageStockAppUserRepo>();
            services.AddScoped<IManageProductsRepository, ManageProductsRepository>();
            services.AddScoped<IManageStockAppCookiesRepo, ManageStockAppCookieRepo>();
            services.AddScoped(typeof(IGenericRepo<>), typeof(GenericSQLRepo<>));

            //SERVICES

            services.AddScoped<IManageStockAppUserService, ManageStockAppUserService>();
            services.AddScoped<IManageProductsService, ManageProductService>();

            //OTHERS

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddDbContextPool<AcmeCorpAppDBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("AcmeCorpStockApp")));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error/ExceptionHandler");
                app.UseStatusCodePagesWithReExecute("/Error/HttpStatusCodeHandler/{0}");

                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}