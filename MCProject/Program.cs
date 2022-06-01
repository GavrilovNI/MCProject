using System.Net;
using System.Net.Sockets;

namespace MCProject;

public class Program
{
    public static string GetLocalIPAddress()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach(var ip in host.AddressList)
        {
            if(ip.AddressFamily == AddressFamily.InterNetwork)
            {
                return ip.ToString();
            }
        }
        throw new Exception("No network adapters with an IPv4 address in the system!");
    }

    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseUrls("http://localhost:80",
                                       "https://localhost:443",
                                       "https://localhost:5001",
                                       "http://" + GetLocalIPAddress() + ":80",
                                       "https://" + GetLocalIPAddress() + ":443"
                                       );
                    webBuilder.UseStartup<Startup>();
                });
}