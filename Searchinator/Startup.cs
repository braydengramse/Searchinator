namespace Searchinator
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.OpenApi.Models;

    using Newtonsoft.Json;

    using Searchinator.Bootstrapping;
    using Searchinator.Configuration;
    using Searchinator.Middleware;

    public class Startup
    {
        private readonly SearchinatorConfigurationManager searchinatorConfigurationManager;

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
            this.searchinatorConfigurationManager = new SearchinatorConfigurationManager(configuration);
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddNewtonsoftJson(o => o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
            services.AddSwaggerGen(
                c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "Searchinator", Version = "v1" }); });

            services.AddSearchinator(this.searchinatorConfigurationManager);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Searchinator v1"));
                app.UseMiddleware<SimulatedLatencyMiddleware>();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}