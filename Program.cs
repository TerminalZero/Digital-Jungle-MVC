using System.Net;

Microsoft.AspNetCore.WebHost.CreateDefaultBuilder<Digital_Jungle_Startup>(args)
    .ConfigureKestrel((context, options) => {

        IPAddress local_IP = Dns.GetHostEntry(Dns.GetHostName())
            .AddressList.First(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);

        options.Listen(local_IP, 443, configure => {
            string KeyPath = context.Configuration.GetValue<string>("Kestrel:Endpoints:Https:Certificate:KeyPath");
            string Password = context.Configuration.GetValue<string>("Kestrel:Endpoints:Https:Certificate:Password");
            
            if (string.IsNullOrEmpty(KeyPath) || string.IsNullOrEmpty(Password)){
                Console.WriteLine("Cert not found");
                return;
            }
            configure.UseHttps(KeyPath, Password); 
        });
    })
    .Build()
    .Run();