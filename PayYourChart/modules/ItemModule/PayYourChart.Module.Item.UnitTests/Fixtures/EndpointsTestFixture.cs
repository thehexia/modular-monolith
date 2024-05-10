using AutoFixture;
using FastEndpoints.Testing;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using PayYourChart.Module.Tests.Common;

namespace PayYourChart.Module.Item.UnitTests;

[DisableWafCache]
public class EndpointsTestFixture : BaseTestApp
{
    public readonly Fixture f = new();
    internal readonly IItemRepository ItemRepository;
    internal readonly IItemDtoMapper Mapper = new ItemDtoMapper();

    public EndpointsTestFixture()
    {
        ItemRepository = Substitute.For<IItemRepository>();
        f.Register(() => ItemRepository);
        f.Register(() => Mapper);
    }

    protected override void ConfigureServices(IServiceCollection services)
    {
        // do test service registration here
        base.ConfigureServices(services);
        services.AddScoped((provider) => ItemRepository);
    }
}