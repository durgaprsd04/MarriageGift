using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using MarriageGift.Logger;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace MarriageGiftAPI
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
            services.AddCors(options =>
            {
              options.AddPolicy(name: "policy1",
                              builder =>
                              {
                                  builder.AllowAnyHeader()
                                  .AllowAnyMethod()
                                  .AllowCredentials()
                                  .SetIsOriginAllowed(origin => true)
                                  .WithOrigins("https://localhost:5001/CustomerAction/login",
                                        "http://localhost:5000/CustomerAction/login");
                              });
          });
          services.AddControllers().AddNewtonsoftJson();
          services.AddAuthorization(options =>
          {
              options.AddPolicy("AllUsers", policy => policy.RequireAuthenticatedUser());
          });
          services.AddCors();
        }
        public void ConfigureContainer(ContainerBuilder builder)
        {
            // Add any Autofac modules or registrations.
            // This is called AFTER ConfigureServices so things you
            // register here OVERRIDE things registered in ConfigureServices.
            //
            // You must have the call to `UseServiceProviderFactory(new AutofacServiceProviderFactory())`
            // when building the host or this won't be called.
            builder.RegisterModule(new AutofacModule());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            loggerFactory.AddLog4Net("log4net.config");

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors("policy1");

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
              endpoints.MapControllers();
            });
        }
    }
}
