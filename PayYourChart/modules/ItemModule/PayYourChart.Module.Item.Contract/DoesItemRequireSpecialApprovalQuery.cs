using FluentResults;
using MediatR;

namespace PayYourChart.Module.Item.Contracts;

public record class DoesItemRequireSpecialApprovalQuery(long Id) : IRequest<Result<DoesItemRequireSpecialApprovalResponse>>;