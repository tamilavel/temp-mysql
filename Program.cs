using System.IO;
using Microsoft.AspNetCore.Hosting;


namespace LoyaltyWeb.Model
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var host = new WebHostBuilder()
             .UseKestrel()
             .UseContentRoot(Directory.GetCurrentDirectory())
             .UseStartup<Startup>()
             .UseUrls(string.Format("{0}", args[0]))
             .Build();

            host.Run();

        }
    }
}
