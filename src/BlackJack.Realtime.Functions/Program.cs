using Azure.Identity;
using BlackJack.Realtime.Functions;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

await new BlackJackRealtimeFunctionStartup().RunAsync();

var host = new HostBuilder()
    .ConfigureAppConfiguration(c =>
    {
        c.AddEnvironmentVariables();
        var azureAppConfigurationUrl = Environment.GetEnvironmentVariable("Azure:AppConfiguration");
        var credential = new ChainedTokenCredential(
            new ManagedIdentityCredential(),
            new EnvironmentCredential(),
            new AzureCliCredential());

        if (!string.IsNullOrWhiteSpace(azureAppConfigurationUrl))
        {
            try
            {
                c.AddAzureAppConfiguration(options =>
                {
                    options.Connect(new Uri(azureAppConfigurationUrl), credential)
                        .ConfigureKeyVault(kv => { kv.SetCredential(credential); });
                });
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to configure service using Azure App Configuration service", ex);
            }
        }
    })
    .ConfigureFunctionsWorkerDefaults(builder =>
    {
        builder.Services.AddApplicationInsightsTelemetryWorkerService();
    })
    .Build();

host.Run();
