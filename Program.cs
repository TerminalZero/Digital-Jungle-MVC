Microsoft.AspNetCore.WebHost.CreateDefaultBuilder<Digital_Jungle_Startup>(args)
    .ConfigureKestrel((context, kestrelOptions) => {
        if (context.HostingEnvironment.IsProduction()) {
            string? KeyPath = context.Configuration.GetValue<string?>("Kestrel:Endpoints:Https:Certificate:KeyPath");
            string? Password = context.Configuration.GetValue<string?>("Kestrel:Endpoints:Https:Certificate:Password");
            int? Port = context.Configuration.GetValue<int?>("Kestrel:Endpoints:Https:Port");

            if (KeyPath != null && Password != null) {
                System.Net.IPAddress current_IP = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList
                    .First(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);

                kestrelOptions.Listen(current_IP, Port ?? 443, configure => {
                    configure.UseHttps(KeyPath, Password);
                    Console.WriteLine("Website is Live!");
                });
            }
            else {
                Console.WriteLine("Certificate not found in appsettings. Visitors can't connect.");
            }
        }
        else {
            Console.WriteLine("Non-production environment. Visitors can't connect.");
        }
    })
    .Build()
    .Run();