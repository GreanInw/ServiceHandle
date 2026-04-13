namespace ServiceHandle.Test;

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
