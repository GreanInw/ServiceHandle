using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace ServiceHandle;

internal class PipelineServiceHandleProcessor
{
    private readonly IServiceProvider _provider;

    public PipelineServiceHandleProcessor(IServiceProvider provider)
    {
        _provider = provider;
    }

    protected IHttpContextAccessor HttpContextAccessor { get; }
    protected HttpContext Context => HttpContextAccessor.HttpContext;

    public async ValueTask<TResponse> ProcessAsync<TRequest, TResponse>(TRequest request)
        where TRequest : IRequestService<TResponse>
    {
        var serviceHandle = _provider.GetService<IRequestServiceHandle<TRequest, TResponse>>();
        return await serviceHandle.ProcessAsync(request);
    }
}
