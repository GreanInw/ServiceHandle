namespace ServiceHandle.Test;

public class UserRequest : IRequestService<UserResponse>
{
    public string Id { get; set; }
}

public class UserResponse
{
    public string Id { get; set; }
    public string State { get; set; }
}
