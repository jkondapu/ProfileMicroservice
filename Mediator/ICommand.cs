using MediatR;

namespace ProfileMicroservice.Mediator
{
    public interface ICommand<out T> : IRequest<T>
    {

    }
}