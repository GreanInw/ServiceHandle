# Service Handle Project
 **Service Handle** คือตัวจัดการ Services อยู่ระหว่าง Controller กับ Services
 > **ตัวอย่าง**
 >
 > Controller -> ServiceHandle -> UserService

## ตัวอย่างขั้นตอนการใช้งาน 
- สร้าง Request และ Response

```csharp
public class UserRequest : IRequestService<UserResponse>
{
    public string Id { get; set; }
}

public class UserResponse
{
    public string Id { get; set; }
    public string State { get; set; }
}

```
- สร้าง Service ชื่อ UserService, IUserService

```csharp
public class UserService : IUserService
{
    public async ValueTask<UserResponse> ProcessAsync(UserRequest request)
    {
        return await ValueTask.FromResult(new UserResponse
        {
            Id = request.Id,
            State = "Process data successfully."
        });
    }
}

public interface IUserService : IRequestServiceHandle<UserRequest, UserResponse>
{

}

```
- ทำการ Register(DI) เพื่อใช้งาน

```csharp
//File : Program.cs
var builder = WebApplication.CreateSlimBuilder(args);
builder.Services.AddHandleServices(typeof(ServiceAssembly).Assembly);

//Assembly of service.
public class ServiceAssembly { }
```

- Controller
```csharp
[ApiController, Route("example")]
public class ExampleController : ControllerBase
{
    public ExampleController(IServiceHandleProcessor serviceHandleProcessor) 
    {
        ServiceHandleProcessor = serviceHandleProcessor;
    }

    public IServiceHandleProcessor ServiceHandleProcessor { get; }
    
    [HttpGet]
    public async Task<IActionResult> Get(UserRequest request)
        => await ServiceHandleProcessor.ProcessAsync<UserRequest, UserResponse>(request);
}
```