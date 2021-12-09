using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using RulesEngine.API.Utils;
using RulesEngine.Core.Config;
using RulesEngine.Core.Repositories;
using RulesEngine.Core.Services;
using RulesEngine.Core.Services.Actions;
using RulesEngine.Infrastructure.Provider;
using System;

namespace RulesEngine
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
            services.Configure<RulesConfig>(options => Configuration.GetSection("RulesResource").Bind(options));

            services.AddTransient<IResourceLoader, ResourceLoader>();
            services.AddTransient<IRuleRepository, RuleRepository>();
            services.AddTransient <IRuleService, RuleService> ();
            services.AddTransient <IRulesValidatorService, RulesValidatorService> ();
            services.AddTransient <IRulesActionCoordinatorService, RulesActionCoordinatorService> ();

            services.RegisterAllTypes<IRuleTriggerAction>(AppDomain.CurrentDomain.GetAssemblies(), ServiceLifetime.Transient);

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RulesEngine", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RulesEngine v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
