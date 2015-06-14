namespace Cookies
{
    using System;
    using Nancy.Hosting.Self;

    class Program
    {
        static void Main(string[] args)
        {
            var uri =
                new Uri("http://localhost:3579");

            var config = new HostConfiguration();
            config.UrlReservations.CreateAutomatically = true;
            var host = new NancyHost(config, uri);
            try {
                host.Start();

                Console.WriteLine("Your application is running on " + uri);
                Console.WriteLine("Press any [Enter] to close the host.");
                Console.ReadLine();
            } finally {
                host.Dispose();
            }
        }
    }
}
