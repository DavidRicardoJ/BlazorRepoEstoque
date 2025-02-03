using Blazored.LocalStorage;
using BlazorRepoEstoque.Data;
using BlazorRepoEstoque.Data.IRepository;
using BlazorRepoEstoque.Data.Repositories;
using BlazorRepoEstoque.Services;
using BlazorRepoEstoque.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MudBlazor.Services;
using System;
using System.Net.Http;

namespace BlazorRepoEstoque
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages().AddRazorRuntimeCompilation();
            services.AddServerSideBlazor();
            services.AddMudServices();
            services.AddScoped<HttpClient>();
            services.AddScoped<IGrupoRepository, GrupoRepository>();
            services.AddScoped<IGrupoServices, GrupoServices>();
            services.AddScoped<IFiltrosServices, FiltrosServices>();
            services.AddScoped<IEncryptString, EncryptString>();
            services.AddScoped<DataSharedService>();
            services.AddScoped<IProdutoEstoqueMinimoService, ProdutoEstoqueMinimoService>();
            services.AddScoped<IMedicamentoService, MedicamentoService>();

            services.AddSingleton<ListSharedService>();
            services.AddBlazoredLocalStorage();


            #region DbContext
           
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseMySql(Configuration.GetConnectionString("DefaultConnection2"),
                     new MySqlServerVersion(new Version(8, 0, 21)));
            });
            #endregion
            services.AddDatabaseDeveloperPageExceptionFilter();
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
