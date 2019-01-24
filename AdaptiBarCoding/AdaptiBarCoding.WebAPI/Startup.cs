using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text;

using AdaptiBarCoding.Common.Configuration;
using AdaptiBarCoding.WebAPI.Models;
using AdaptiBarCoding.DataAccess;
using AdaptiBarCoding.ApplicationServices;

using AsyncFriendlyStackTrace;
using Swashbuckle.AspNetCore.Swagger;
using System;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace AdaptiBarCoding.WebAPI
{
    public class Startup
    {
        #region Private Fields

        private AppSettings _appSettings;
     
        #endregion

        #region Constructors
        public Startup(IHostingEnvironment environment, IConfiguration configuration)
        {
            Environment = environment;
            Configuration = configuration;
        }
        #endregion


        #region Public Properties

        public IConfiguration Configuration { get; }

        public IHostingEnvironment Environment { get; }

        #endregion

        #region Public Members
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            Console.WriteLine("ConfigureServices");
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
         
            services.AddDbContext<ApplicationDbContext>(
                options =>
                {
                    options.UseSqlServer(Configuration.GetConnectionString("DBConnectionString"));
                    options.EnableSensitiveDataLogging();
                });

            var corsBuilder = new CorsPolicyBuilder();
            corsBuilder.AllowAnyHeader();
            corsBuilder.AllowAnyMethod();
            corsBuilder.AllowAnyOrigin();
            corsBuilder.AllowCredentials();

            services.AddCors(options => { options.AddPolicy("ApiCorsPolicy", corsBuilder.Build()); });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddOptions();

            if (Environment.IsDevelopment())
            {
                services.AddSwaggerGen(c => c.SwaggerDoc("v1", new Info { Title = "AdaptiBarCoding API", Version = "v1" }));
            }

            ConfigureDependencyInjections(services);
        }

     
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            Console.WriteLine("Configure");
            _appSettings = app.ApplicationServices.GetService<IOptions<AppSettings>>().Value;

            app.UseExceptionHandler(
               builder =>
               {
                   builder.Run(
                       async context =>
                       {
                           context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                           context.Response.ContentType = "application/json";

                           var error = context.Features.Get<IExceptionHandlerFeature>();

                           if (error != null)
                           {
                               var err = JsonConvert.SerializeObject(
                                   new WebApiError
                                   {
                                       Message = error.Error.Message,
                                       StackTrace = error.Error.ToAsyncString()
                                   });

                               await context.Response.Body.WriteAsync(Encoding.ASCII.GetBytes(err), 0, err.Length)
                                   .ConfigureAwait(false);
                           }
                       });
               });

            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(
                    c =>
                    {
                        c.SwaggerEndpoint("/swagger/v1/swagger.json", "AdaptiBarCoding API V1");
                                               
                    });
            }

            app.UseCors("ApiCorsPolicy");
            app.UseMvc();
        }
        #endregion

        #region Private Methods

        private void ConfigureDependencyInjections(IServiceCollection services)
        {
            services.AddScoped<IBlogPostRepository, BlogPostRepository>();
            services.AddScoped<IBlogPostService, BlogPostService>();            
        }

        #endregion
    }


}
