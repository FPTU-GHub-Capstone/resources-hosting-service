namespace WebApiLayer.UserFeatures.Response;

public class IdpGetUsersResponse
{
    public IdpUser[] users { get; set; } 
}

public class IdpUser
{
    public string? username { get; set; }
    public string? email { get; set; }
    public string uid { get; set; }
}