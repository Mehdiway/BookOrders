using MediatR;

namespace Shared.CQRS;
public interface ICommand<TResponse> : IRequest<TResponse>;
public interface ICommand : IRequest;