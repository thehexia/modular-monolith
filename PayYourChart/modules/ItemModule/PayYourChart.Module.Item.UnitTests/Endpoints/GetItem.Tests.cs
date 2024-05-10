using AutoFixture;
using FastEndpoints;
using FastEndpoints.Testing;
using NSubstitute;
using System.Net;

using NSubstitute.ReturnsExtensions;

namespace PayYourChart.Module.Item.UnitTests;

public class ItemServiceTests(EndpointsTestFixture app)  : TestBase<EndpointsTestFixture>
{
    readonly EndpointsTestFixture _app = app;

    [Fact]
    public async void GetItemById_returns_dto_if_in_database()
    {
        // Arrange
        const long itemId = 1;
        Item item = _app.f
            .Build<Item>()
            .With(item => item.Id, itemId)
            .Create();
        _app.ItemRepository.GetItemAsync(itemId).Returns(item);

        // Act
        TestResult<ItemDto> result = await _app.Client.GETAsync<GetItem, GetItemByIdRequest, ItemDto>(new(itemId));

        // Assert
        Assert.Equal(HttpStatusCode.OK, result.Response.StatusCode);
        Assert.Equal(_app.Mapper.Get().Map<ItemDto>(item), result.Result);
    }


    [Fact]
    public async void GetItemById_returns_no_content_if_not_in_database()
    {
        // Arrange
        const long itemId = 2;
        _app.ItemRepository.GetItemAsync(itemId).ReturnsNull();

        // Act
        TestResult<ItemDto> result = await _app.Client.GETAsync<GetItem, GetItemByIdRequest, ItemDto>(new(itemId));

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, result.Response.StatusCode);
    }
}
