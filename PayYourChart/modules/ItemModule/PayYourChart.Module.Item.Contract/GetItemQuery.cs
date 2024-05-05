using FluentResults;
using MediatR;

namespace PayYourChart.Module.Item.Contracts;

public record class GetItemQuery(long Id) : IRequest<Result<GetItemResponse>>;
