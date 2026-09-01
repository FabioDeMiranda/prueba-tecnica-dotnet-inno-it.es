using InterviewTest.Application;
using InterviewTest.Infrastructure;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace InterviewTest.Tests;

public class ApplicationServicesTests
{
    // Los controladores despachan Commands/Queries a través de ISender (MediatR).
    // Este test comprueba que el contenedor de servicios es capaz de resolverlo.
    [Fact]
    public void El_contenedor_deberia_resolver_el_dispatcher_de_peticiones()
    {
        var services = new ServiceCollection();
        services.AddLogging();

        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["ConnectionStrings:Default"] = "DataSource=:memory:"
            })
            .Build();

        services.AddApplication();
        services.AddInfrastructure(configuration);

        using var provider = services.BuildServiceProvider();

        Assert.NotNull(provider.GetService<ISender>());
    }
}
