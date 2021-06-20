using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using MyCms.Data.Context;
using MyCms.Data.Mongo;
using MyCms.Extensions.Consts;
using MyCms.IoC.DependencyInjections;
using System.IO;
using System.Text;

namespace MyCms.Api
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
            #region Database

            services.AddDbContext<MyCmsContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("MyCmsConnection"));
            });

            services.Configure<MongoOptions>(Configuration.GetSection("Mongo"));
            services.AddSingleton<MongoClient>(c =>
            {
                var options = c.GetService<IOptions<MongoOptions>>();
                return new MongoClient(options.Value.ConnectionString);
            });

            services.AddScoped<IMongoDatabase>(c =>
            {
                var options = c.GetService<IOptions<MongoOptions>>();
                var client = c.GetService<MongoClient>();
                return client.GetDatabase(options.Value.Database);
            });
            #endregion

            services.AddControllers();

            #region Swagger

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "MyCms.Api",
                    Version = "v1"
                });
                c.IncludeXmlComments(Path.Combine(Directory.GetCurrentDirectory(), @"bin\Debug\net5.0", "MyCms.Api.xml"));
            });

            #endregion

            #region Add IoC

            RegisterServices(services);

            #endregion

            #region JWT

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Const.SiteUrl,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Const.VerifyCodeJwt))
                    };
                });

            services.AddCors(options =>
            {

                options.AddPolicy("EnableCors", builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        //.AllowCredentials()
                    .Build();
                });
            });

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                #region Swagger

                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyCms.Api v1"));

                #endregion
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("EnableCors");
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors(policy =>
                policy.WithOrigins("http://localhost:63950", "https://localhost:44396")
                    .AllowAnyMethod()
                    .WithHeaders(HeaderNames.ContentType));
            app.UseCors(policy =>
                policy.WithOrigins("http://localhost:53221", "https://localhost:44380")
                    .AllowAnyMethod()
                    .WithHeaders(HeaderNames.ContentType));

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.ApplicationServices.GetService<IDatabaseInitializer>().InitializerAsync();
        }

        public static void RegisterServices(IServiceCollection service)
        {
            DependencyContainer.RegisterServices(service);
        }
    }
}
