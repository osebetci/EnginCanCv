
using AutoMapper;
using EnginCan.Bll.EntityCore.Abstract.Abouts;
using EnginCan.Bll.EntityCore.Abstract.Systems;
using EnginCan.Bll.EntityCore.Abstract.Users;
using EnginCan.Bll.EntityCore.Concrete.Abouts;
using EnginCan.Bll.EntityCore.Concrete.Systems;
using EnginCan.Bll.EntityCore.Concrete.Users;
using EnginCan.Caching.Microsoft;
using EnginCan.Caching;
using EnginCan.Core.Middleware;
using EnginCan.Dal.EfCore;
using EnginCan.DependencyResolvers;
using EnginCan.Extensions;
using EnginCan.Utilities.IoC;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

namespace YurtYonetim.Api
{
    public class Startup
    {
        /// <summary>
        /// Confirations
        /// </summary>
        public IConfiguration _configuration { get; }

        /// <summary>
        /// Starter
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration) => _configuration = configuration;

        private static void _addServices(ref IServiceCollection services)
        {
            #region Abouts
            services.AddScoped<IAboutRepository, AboutRepository>();
            #endregion

            #region User
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            #endregion

            #region Systems
            services.AddScoped<ICustomHttpContextAccessor, CustomHttpContextAccessor>();
            services.AddScoped<ILookupRepository, LookupRepository>();
            services.AddScoped<ILookupTypeRepository, LookupTypeRepository>();
            services.AddScoped<IPageRepository, PageRepository>();
            services.AddScoped<IPagePermissionRepository, PagePermissionRepository>();
            #endregion

            services.AddMemoryCache();
            services.AddSingleton<ICacheManager, MemoryCacheManager>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddAutoMapper(typeof(Startup));
            services.AddDbContext<EnginCanContext>(options =>
                                                         options.UseSqlServer(_configuration.GetConnectionString("Default")), ServiceLifetime.Transient);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
         {
             options.RequireHttpsMetadata = false; // https var ise true ya çek
             options.TokenValidationParameters = new TokenValidationParameters
             {
                 ValidateIssuer = false, //  server bu tokený yaratmýþmý yani geçerli mi diye kontrol eder
                 ValidateAudience = false, // tokenýn audience dan alýnýp alýnmayacaðýný kontrol eder. bir nevi bu domain yetkilimi der.
                 ValidateLifetime = true, // token ölmüþ mü ölmemiþ mi ve bu token geçerli bir token mý diye kontrol eder
                 ValidateIssuerSigningKey = true, // gelen token güvenilir anahtarlar barýndýrýyormu diye kontrol eder
                 ValidIssuer = _configuration["Jwt:Issuer"],
                 ValidAudience = _configuration["Jwt:Issuer"],
                 IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]))
             };

             options.Events = new JwtBearerEvents
             {
                 OnMessageReceived = context =>
                 {
                     var accessToken = context.Request.Query["access_token"];

                     // If the request is for our hub...
                     var path = context.HttpContext.Request.Path;
                     if (!string.IsNullOrEmpty(accessToken)
                         && (path.StartsWithSegments("/hub")))
                     {
                         // Read the token out of the query string
                         context.Token = accessToken;
                     }
                     return Task.CompletedTask;
                 }
             };
         });

            services.AddControllers()
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
            });

            services.AddDependencyResolvers(new ICoreModule[]
            {
                new CoreModule()
             });

            //#region Services
            //_addServices(ref services);
            //#endregion

            services.AddCors(options =>
            {
                options.AddPolicy("ApiCorsPolicy",
                    p => p.WithOrigins(_configuration.GetSection("Host:AllowedOrigins").Get<string[]>()).
                    AllowAnyMethod().
                    AllowAnyHeader().
                    AllowCredentials());
            });


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Engin Can",
                    Contact = new OpenApiContact
                    {
                        Name = "Engin CAN",
                        Email = "canengin757@gmail.com",
                        Url = new Uri("http://localhost:5000")
                    }
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var cultureInfo = new CultureInfo("en-US");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
            app.UseCors("ApiCorsPolicy");
            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Happy Farmer Application Api Swagger");
            });

        }
    }
}