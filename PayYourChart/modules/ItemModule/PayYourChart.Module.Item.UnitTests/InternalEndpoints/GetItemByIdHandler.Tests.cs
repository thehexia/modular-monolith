using AutoFixture;
using FluentResults;
using NSubstitute;
using PayYourChart.Module.Item.Contracts;

namespace PayYourChart.Module.Item.UnitTests;


public class GetItemByIdHandlerTests(InternalEndpointHandlersTestFixture app) : IClassFixture<InternalEndpointHandlersTestFixture>
{
    readonly InternalEndpointHandlersTestFixture _app = app;

    [Fact]
    public async Task GetItemByIdHandler_returns_item_with_no_special_approval_in_description_if_price_below_10_000()
    {
        // Arrange
        const long itemId = 1;
        Item item = _app.f
            .Build<Item>()
            .With(item => item.Id, itemId)
            .With(item => item.Price, _app.f.Create<int>() % 10_000)
            .Create();
        
        _app.ItemRepository.GetItemAsync(itemId).Returns(item);
        GetItemByIdHandler sut = _app.f.Create<GetItemByIdHandler>();

        // Act
        Result<GetItemResponse> result = await sut.Handle(new(itemId));

        // Assert
        Assert.True(result.IsSuccess);
        Assert.False(result.Value.SpecialApprovalRequired);
        Assert.Null(result.Value.SpecialApprovalReason);
    }
}
