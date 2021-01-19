using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using engine_plugin_backend.Models;
using engine_plugin_backend.Services;
using Microsoft.Extensions.Options;
using engine_plugin_backend.Interfaces;

namespace engine_plugin_backend
{
    public class Startup
    {
        public static readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // enable CORS for template-engine front-end
            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                      builder => builder.WithOrigins(Configuration.GetValue<string>("FrontEndOrigin"),
                       Configuration.GetValue<string>("PluginOrigin")).AllowAnyHeader()
                      .AllowAnyMethod()
                      .AllowCredentials());
            });
            // get scaffold settings model from appsettings.json
            services.Configure<ScaffoldDatabaseSettingsModel>(
                Configuration.GetSection(nameof(ScaffoldDatabaseSettingsModel)));

            //register settigs class to be a singleton service
            services.AddSingleton<IBaseSettingsModels>(sp =>
                sp.GetRequiredService<IOptions<ScaffoldDatabaseSettingsModel>>().Value);

            //register scaffold service as singleton service
            // as it uses MongoDb client and it's good practice that multiple
            // clients should not be created
            services.AddSingleton<ScaffoldService>();

            //register webScrapper to be a singleton service
            services.AddHostedService<ScaffoldService>().AddSingleton<IWebScrapper, CompanySiteWebScrapper>();

            services.AddControllers();
            _ = services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "engine_plugin_backend", Version = "v1" }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "engine_plugin_backend v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(MyAllowSpecificOrigins);

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
