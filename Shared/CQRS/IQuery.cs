using MediatR;

namespace Shared.CQRS;
public interface IQuery<TResponse> : IRequest<TResponse>;
