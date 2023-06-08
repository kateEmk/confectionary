using Confectionery.Mapers;
using LibraryDatabaseCoffe.Models.DB.Context.Connection;
using LibraryDatabaseCoffe.Models.DB.Context.ConnectionProviders;
using LibraryDatabaseCoffe.Models.DB.Context.@interface;
using LibraryDatabaseCoffe.Models.DB.Request.Repositories;
using MapsterMapper;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Confectionery
{
    public class Startup : AdapterConfig
    {
        public IConfiguration Configuration { get; set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddSingleton(Configuration);
            services.AddScoped<IDapperConnectionProvider, DapperConnectionProvider>();

            ConnectionOptions connectionOptions = new ConnectionOptions();
            services.Configure<ConnectionOptions>(
                Configuration.GetSection(ConnectionOptions.NameConnection)
                );

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = new PathString("/Home/Autorisation");
                    options.AccessDeniedPath = new PathString("/Home/Autorisation");
                });
            services.AddAuthorization();

            services.AddSingleton(Register());
            services.AddScoped<IMapper, ServiceMapper>();

            services.AddTransient<Mapsters>();

            services.AddTransient<CompanyRepository>();
            services.AddTransient<DescriptionOrderRepository>();
            services.AddTransient<OrderRepository>();
            services.AddTransient<SweetStaffRepository>();
            services.AddTransient<UserRepository>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment hostEnvironment)
        {
            if (hostEnvironment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints => {
                    endpoints.MapControllerRoute(
                        name: "default",
                        pattern: "{controller=Home}/{action=Main}/{id?}");
            });
        }


    }
}
