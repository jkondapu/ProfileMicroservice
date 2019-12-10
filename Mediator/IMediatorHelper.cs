using System.Threading;
using System.Threading.Tasks;

namespace ProfileMicroservice.Mediator
{
    public interface IMediatorHelper
    {
        /// <summary>
        /// Call Command handler attached to respective Command
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TResponse> Send<TResponse>(ICommand<TResponse> command, CancellationToken cancellationToken = default);

        /// <summary>
        /// Publish any object and respective event handlers will be called
        /// </summary>
        /// <param name="notification"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task Publish(object notification, CancellationToken cancellationToken = default);

        /// <summary>
        /// publish event object and respective event handlers will be called
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <param name="notification"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task Publish<TEvent>(IEvent notification, CancellationToken cancellationToken = default);

        /// <summary>
        /// publish event object ,respective event handlers will be called and awaiting
        /// for the response
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="notification"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<EventResponse> AwaitablePublish(IEvent notification, CancellationToken cancellationToken = default);
    }
}
