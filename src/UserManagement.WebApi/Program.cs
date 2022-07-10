using Serilog;

namespace QuestUserManagement
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).UseSerilog((builder,configuration) => configuration.Enrich.FromLogContext()
                .ReadFrom.Configuration(builder.Configuration));
    }
}
