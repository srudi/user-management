using UserManagement.Application;
using UserManagement.Infrastructure;
using UserManagement.WebAPI.ExceptionHandling;

namespace QuestUserManagement
{
    public class Startup
    {
        private readonly IWebHostEnvironment _environment;
        private readonly IConfiguration _configuration;


        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            _configuration = configuration;
            _environment = environment;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(options => options.Filters.Add(new ExceptionFilter(_environment)));
            services.AddTransient<ExceptinoHandler>();
            services.AddApplication();
            services.AddInfrastructure(_configuration);
            services.AddSwaggerDocument(configure => configure.Title = "User Management WebAPI");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            if (_environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<ExceptinoHandler>();
            app.UseOpenApi();
            app.UseSwaggerUi3(settings => settings.DocExpansion = "list");

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller}/{action=Index}/{id?}");
            });
        }
    }
}
