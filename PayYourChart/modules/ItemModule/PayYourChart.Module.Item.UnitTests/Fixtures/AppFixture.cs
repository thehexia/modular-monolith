using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using PayYourChart.Module.Tests.Common;

namespace PayYourChart.Module.Item.UnitTests;

public class TestItemsApp : BaseTestApp
{
    internal readonly IItemRepository _mockItemRepo;

    public TestItemsApp()
    {
        _mockItemRepo = Substitute.For<IItemRepository>();
    }

    protected override void ConfigureServices(IServiceCollection services)
    {
        // do test service registration here
        base.ConfigureServices(services);
        services.AddScoped((provider) => _mockItemRepo);
    }
}