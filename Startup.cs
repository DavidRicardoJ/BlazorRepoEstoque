using Blazored.LocalStorage;
using BlazorRepoEstoque.Data;
using BlazorRepoEstoque.Services;
using BlazorRepoEstoque.Services.Interfaces;
using BlazorRepoEstoque.Shared.SharedState;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MudBlazor.Services;
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
            services.AddScoped<IFiltrosServices, FiltrosServices>();
            services.AddScoped<IEncryptString, EncryptString>();            
            services.AddScoped<IProdutoEstoqueMinimoService, ProdutoEstoqueMinimoService>();
            services.AddScoped<IMedicamentoService, MedicamentoService>();
            services.AddScoped<IGroupService, NomeGrupoService>();
            services.AddScoped<ManagerStateAppService>();           
            services.AddScoped<ImportExcelService>();
            services.AddBlazoredLocalStorage();



            #region DbContext

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseMySql(Configuration.GetConnectionString("DefaultConnection2"),
                       ServerVersion.AutoDetect(Configuration.GetConnectionString("DefaultConnection2")));
            });
            #endregion
            services.AddDatabaseDeveloperPageExceptionFilter();

            //using (var scope = services.BuildServiceProvider().CreateScope()) 
            //{
            //    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            //    dbContext.Database.Migrate();  // Aplica as migrations pendentes
            //}
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
