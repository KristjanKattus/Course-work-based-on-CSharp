using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using BLL.App;
using Contracts.BLL.App;
using Contracts.DAL.App;
using DAL.App.EF;
using DAL.App.EF.AppDataInit;
using Domain.App.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WebApp
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
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();
            
            
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services
                .AddAuthentication()
                .AddCookie(options =>
                    {
                        options.SlidingExpiration = true;
                    }
                    )
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidIssuer = Configuration["JWT:Issuer"],
                        ValidAudience = Configuration["JWT:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Key"])),
                        ClockSkew = TimeSpan.Zero
                    };
                });

            services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddDefaultUI()
                .AddEntityFrameworkStores<AppDbContext>();
            services.AddControllersWithViews();

            services.AddScoped<IAppUnitOfWork, AppUnitOfWork>();
            services.AddScoped<IAppBLL, AppBLL>();

            // Add support for API versioning
            services.AddApiVersioning(options => options.ReportApiVersions = true);

            // Add support for m2m API documentation
            services.AddVersionedApiExplorer(options => options.GroupNameFormat = "'v'VVV");
            
            // Add support to generate human readable documentation from m2m docs
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSwaggerGen();


            services.AddAutoMapper(
                typeof(DAL.App.DTO.MappingProfiles.AutoMapperProfile),
                typeof(BLL.App.DTO.MappingProfiles.AutoMapperProfile),
                typeof(PublicApi.DTO.v1.MappingProfiles.AutoMapperProfile)
                );

        }
        
        
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider apiVersionDescriptionProvider)
        {
            SetupAppData(app, env, Configuration);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();


            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                foreach (var apiVersionDescription in apiVersionDescriptionProvider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint(
                        $"/swagger/{apiVersionDescription.GroupName}/swagger.json",
                        apiVersionDescription.GroupName.ToUpperInvariant()
                        );
                }
            }
            );
            
            
            
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            
            


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "area",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }

        private static void SetupAppData(IApplicationBuilder app, IWebHostEnvironment env, IConfiguration configuration)
        {
            using var serviceScope =
                app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            using var ctx = serviceScope.ServiceProvider.GetService<AppDbContext>();
            if (ctx != null)
            {
                if (configuration.GetValue<bool>("AppData:DropDataBase"))
                {
                    DataInit.DropDataBase(ctx);
                }
                if (configuration.GetValue<bool>("AppData:Migrate"))
                {
                    DataInit.MigrateDataBase(ctx);
                }
                if (configuration.GetValue<bool>("AppData:SeedIdentity"))
                {
                    //TODO
                }
                if (configuration.GetValue<bool>("AppData:SeedData"))
                {
                    DataInit.SeedAppData(ctx);
                }
                
                ctx.Database.EnsureDeleted();
                ctx.Database.Migrate();
            }
        }
    }
}