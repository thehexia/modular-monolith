using AutoFixture;
using FastEndpoints;
using FastEndpoints.Testing;
using NSubstitute;
using System.Net;

using NSubstitute.ReturnsExtensions;

namespace PayYourChart.Module.Item.UnitTests;

public class ItemServiceTests(TestItemsApp App)  : TestBase<TestItemsApp>
{
    readonly TestItemsApp _sut = App;
    readonly Fixture _fixture = new();
    readonly ItemDtoMapper _mapper = new();


    [Fact]
    public async void GetItemById_returns_dto_if_in_database()
    {
        // Arrange
        const long itemId = 1;
        Item item = _fixture
            .Build<Item>()
            .With(dto => dto.Id, itemId)
            .Create();
        _sut._mockItemRepo.GetItemAsync(itemId).Returns(item);

        // Act
        TestResult<ItemDto> result = await _sut.Client.GETAsync<GetItem, GetItemByIdRequest, ItemDto>(new(1));

        // Assert
        Assert.Equal(HttpStatusCode.OK, result.Response.StatusCode);
        Assert.Equal(_mapper.Get().Map<ItemDto>(item), result.Result);
    }


    [Fact]
    public async void GetItemById_returns_no_content_if_not_in_database()
    {
        // Arrange
        const long itemId = 1;
        _sut._mockItemRepo.GetItemAsync(itemId).ReturnsNull();

        // Act
        TestResult<ItemDto> result = await _sut.Client.GETAsync<GetItem, GetItemByIdRequest, ItemDto>(new(1));

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, result.Response.StatusCode);
    }
}
