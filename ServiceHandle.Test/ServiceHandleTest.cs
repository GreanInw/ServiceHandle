using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ServiceHandle.Test;

public class ServiceHandleTest
{
    internal static Assembly CurrentAssembly => typeof(ServiceHandleTest).Assembly;

    private readonly ITestOutputHelper _testOutputHelper;

    public ServiceHandleTest(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact(DisplayName = "Test - Register/Get services not null.")]
    public void Test_GetService()
    {
        IServiceCollection services = new ServiceCollection();
        services.AddHandleServices(CurrentAssembly);
        services.AddScoped<IUserService, UserService>();

        using var provider = services.BuildServiceProvider();
        var userService = provider.GetRequiredService<IUserService>();
        var serviceHandlerProcessor = provider.GetRequiredService<IServiceHandleProcessor>();

        Assert.NotNull(userService);
        Assert.NotNull(serviceHandlerProcessor);
    }

    [Fact(DisplayName = "Test - User service process.")]
    public async Task Test_UserServiceProcess()
    {
        IServiceCollection services = new ServiceCollection();
        services.AddHandleServices(CurrentAssembly);
        services.AddScoped<IUserService, UserService>();

        using var provider = services.BuildServiceProvider();
        var userService = provider.GetRequiredService<IUserService>();

        Assert.NotNull(userService);
        var request = new UserRequest { Id = "1" };
        var response = await userService.ProcessAsync(request);

        Assert.Equal(request.Id, response.Id);
        _testOutputHelper.WriteLine("======== Response Data ========");
        _testOutputHelper.WriteLine("Id : {0}", response.Id);
        _testOutputHelper.WriteLine("State : {0}", response.State);
    }

    [Fact(DisplayName = "Test - ServiceHandler processor.")]
    public async Task Test_ServiceHandlerProcessor()
    {
        IServiceCollection services = new ServiceCollection();
        services.AddHandleServices(CurrentAssembly);
        services.AddScoped<IUserService, UserService>();

        using var provider = services.BuildServiceProvider();
        var serviceHandlerProcessor = provider.GetRequiredService<IServiceHandleProcessor>();

        Assert.NotNull(serviceHandlerProcessor);

        var request = new UserRequest { Id = Guid.NewGuid().ToString() };
        var response = await serviceHandlerProcessor.ProcessAsync<UserRequest, UserResponse>(request);

        Assert.Equal(request.Id, response.Id);
        _testOutputHelper.WriteLine("======== Response Data ========");
        _testOutputHelper.WriteLine("Id : {0}", response.Id);
        _testOutputHelper.WriteLine("State : {0}", response.State);
    }
}
