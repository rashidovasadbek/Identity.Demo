using MediatR;

namespace Identity.Domain.Common.Commands;

/// <summary>
/// Represents a command in a CQRS (Command Queue Responsibility Segregation) architecture
/// </summary>
/// <typeparam name="TResult"></typeparam>
public interface ICommand<out TResult> : ICommand, IRequest<TResult>
{
    
}

public interface ICommand
{
    
}