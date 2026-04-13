namespace ServiceHandle;

public interface IServiceHandleProcessor
{
    ValueTask<TResponse> ProcessAsync<TRequest, TResponse>(TRequest request)
        where TRequest : IRequestService<TResponse>;
}
