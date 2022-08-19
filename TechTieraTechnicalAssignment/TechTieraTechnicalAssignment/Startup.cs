using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechTieraTechnicalAssignment.Interfaces;
using TechTieraTechnicalAssignment.Mapper;
using TechTieraTechnicalAssignment.Models;
using TechTieraTechnicalAssignment.Services;

namespace TechTieraTechnicalAssignment
{
	public class Startup
	{
		public Startup(IWebHostEnvironment env, IConfiguration configuration)
		{
			Configuration = configuration;
			var builder = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
				.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
				.AddEnvironmentVariables();

			this.Configuration = builder.Build();
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllersWithViews();
			services.AddControllers();
			services.AddApiVersioning();
			services.AddMvc();
			services.AddSingleton<IFileService, FileService>();
			services.AddSingleton<IGetByCurrency, GetByCurrencyService>();
			services.AddSingleton<IGetByDateRange, GetByDateRangeService>();
			services.AddSingleton<IGetByStatus, GetByStatusService>();
			services.AddSingleton<IDataService, DataService>();
			services.AddAutoMapper(typeof(TransactionProfile).Assembly);

			services.Configure<ApiConfig>(Configuration.GetSection("ApiConfig"));
			services.Configure<DBConnection>(Configuration.GetSection("ConnectionStrings"));
			services.AddSingleton(cfg => cfg.GetService<IOptions<ApiConfig>>().Value);
			services.AddSingleton(cfg => cfg.GetService<IOptions<DBConnection>>().Value);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
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
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
