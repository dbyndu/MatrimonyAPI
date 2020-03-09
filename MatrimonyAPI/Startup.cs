using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Matrimony.Helper;
using Matrimony.Service.Auth;
using Matrimony.Service.Contracts;
using Matrimony.Service.User;
using MatrimonyAPI.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace MatrimonyAPI
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
            services.AddControllers();
            //this.ValidateToken(Configuration, services);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidAudience = Configuration["Jwt:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                    };
                });
            
            services.AddDbContext<Matrimony.Data.MatrimonyContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("MatrimonyDB")));
            //services.AddCors(options =>
            //{
            //    options.AddPolicy("AllowAll",
            //        builder =>
            //        {
            //            builder
            //            .AllowAnyOrigin()
            //            .AllowAnyMethod()
            //            .AllowAnyHeader();
            //        });
            //});
            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "MatriMama API", Version = "v1" });
            //});
            services.AddMvc(options => options.EnableEndpointRouting = false);
            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddTransient<IUserDetailsService, UserDetailsService>();
            //services.AddTransient<IAuthService, AuthService>();
            services.Configure<JwtAuthentication>(Configuration.GetSection(ConfigurationHelper.JWTAUTHENTICATIONKEY));
            //services.AddSingleton<IPostConfigureOptions<JwtBearerOptions>, ConfigureJwtBearerOptions>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //services.AddAuthentication(x =>
            //{
            //    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            //}).AddJwtBearer();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();


            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMvc();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //app.UseSwaggerUI(c =>
            //{
            //    c.SwaggerEndpoint("/swagger/v1/swagger.json", "MatriMama API V1");
            //});
            //app.UseAuthentication();
            //app.UseCors("AllowAll");

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello From matrimonyAPI");
            //});

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllers();
            //});

        }

        private void ValidateToken(IConfiguration configuration, IServiceCollection services)
        {
            var audianceConfig = configuration.GetSection("JwtAuthentication");
            var key = audianceConfig["SecurityKey"];
            var issuer = audianceConfig["ValidIssuer"];
            var audience = audianceConfig["ValidAudience"];

            var signingKey = new SymmetricSecurityKey(Convert.FromBase64String(key));
            //var keyByteArray = Encoding.ASCII.GetBytes(key);
            //var signingKey = new SymmetricSecurityKey(keyByteArray);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,
                ValidateIssuer = true,
                ValidIssuer = issuer,
                ValidateAudience = true,
                ValidAudience = audience,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(option => {
                option.TokenValidationParameters = tokenValidationParameters;
            });
        }
    }
}
