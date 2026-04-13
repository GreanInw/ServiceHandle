namespace ServiceHandle;

public interface IRequestServiceHandle { }

public interface IRequestServiceHandle<TRequest, TResponse> : IRequestServiceHandle
    where TRequest : IRequestService<TResponse>
{
    ValueTask<TResponse> ProcessAsync(TRequest request);
}