namespace ServiceHandle;

public sealed class ServiceHandleProcessor : IServiceHandleProcessor
{
    private readonly PipelineServiceHandleProcessor _processor;
    public ServiceHandleProcessor(IServiceProvider provider)
    {
        _processor = new(provider);
    }

    public async ValueTask<TResponse> ProcessAsync<TRequest, TResponse>(TRequest request)
        where TRequest : IRequestService<TResponse>
        => await _processor.ProcessAsync<TRequest, TResponse>(request);
}
