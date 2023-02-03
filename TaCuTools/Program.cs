using AccountingDB;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shane32.ConsoleDI;
using TacuDataAccess;

namespace TaCuTools;

public class Program
{
    public static async Task Main(string[] args)
        => await ConsoleHost.RunMainMenu(args, CreateHostBuilder, "ZBDB Console");

    // this function is necessary for Entity Framework Core tools to perform migrations, etc
    // do not change signature!!
    public static IHostBuilder CreateHostBuilder(string[] args)
        => ConsoleHost.CreateHostBuilder(args, ConfigureServices);

    private static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
    {
        // register your services or Entity Framework data contexts here
        services.AddDbContext<AccountingContext>(options =>
            options.UseSqlServer(context.Configuration.GetConnectionString("AccountingDB")));

        services.AddDbContext<TacuMoneyContext>(options =>
            options.UseSqlServer(context.Configuration.GetConnectionString("TacuDB")));



    }
}
        

   