using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using OptimizationLabs.API.Contexts;

namespace OptimizationLabs.API
{
    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            
            services.AddDbContext<DataContext>(options =>
            {
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                options.UseSqlServer(connectionString);
            });

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", policy =>
                {
                    var origins = configuration.GetSection("CorsOrigin:Links").Get<string[]>();
                    policy.WithOrigins(origins).AllowAnyHeader().AllowAnyMethod();
                });
            });
            
            services.AddEndpointsApiExplorer();

            services.AddRouting(options =>
            {
                options.LowercaseUrls = true;
            });
            
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
            });
            
            services.AddLogging(builder =>
            {
                builder.AddConsole();
                builder.AddDebug();
            });
        }

        public void Configure(IApplicationBuilder application)
        {
            application.UseCors("CorsPolicy");
            
            application.UseHttpsRedirection();
            
            application.UseRouting();
            
            application.UseAuthorization();
            
            application.UseSwagger();
            
            application.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
            });
            
            application.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}