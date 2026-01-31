using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace Saloon.Infrastructure;


public static class Global
{
    public static string StringConnection 
    { 
    //    get => @"Server=(localdb)\MSSQLLocalDB;Database=Saloon;Trusted_Connection=True;"; 
        get 
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) // importante para achar o arquivo
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
            return configuration.GetConnectionString("DefaultConnection");

        }
    }
}
