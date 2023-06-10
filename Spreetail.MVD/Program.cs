using Spreetail.MVD;
using Spreetail.MVD.Services.Implementations;
using Spreetail.MVD.Services.Interfaces;

namespace Company.WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    services.AddHostedService<Worker>();
                    services.AddSingleton<ICommandService, CommandService>();
                })
                .Build();

            host.Run();
        }
    }
}