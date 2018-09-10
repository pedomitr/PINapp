using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Phonebook.Data;
using Phonebook.Models;
using Phonebook.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Razor;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using System.Threading;

namespace Phonebook
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

			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

			services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddMvc();

            services.AddDbContext<NewContactContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("NewContactContext")));

			services.AddLocalization(options => options.ResourcesPath = "Resources");

			services.AddMvc()
				.AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
				.AddDataAnnotationsLocalization();

		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Shared/Error");
            }

			var supportedCultures = new []
			{
				new CultureInfo("en-GB"),
				new CultureInfo("en-US"),
				new CultureInfo("hr")
			};

			var options = new RequestLocalizationOptions
			{
				DefaultRequestCulture = new RequestCulture("hr"),
				SupportedCultures = supportedCultures,
				SupportedUICultures = supportedCultures,
			};
			options.DefaultRequestCulture = new RequestCulture("hr", "hr");

			options.SupportedCultures = supportedCultures;

			options.SupportedUICultures = supportedCultures;


			app.UseRequestLocalization(options);


			app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
