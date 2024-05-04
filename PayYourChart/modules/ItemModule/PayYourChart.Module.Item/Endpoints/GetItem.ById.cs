using FastEndpoints;
using Microsoft.AspNetCore.Authorization;

namespace PayYourChart.Module.Item;


internal record class GetItemByIdRequest(long Id);


[HttpGet($"{ApiPath.Base}/{{id}}")]
[AllowAnonymous]
internal class GetItemById : Endpoint<GetItemByIdRequest, ItemDto>
{
    public override async Task HandleAsync(GetItemByIdRequest req, CancellationToken ct)
    {
        
        // await SendAsync(new()
        // {
            
        // });
    }
}
