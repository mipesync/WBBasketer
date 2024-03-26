namespace WBBasketer.Options;

public class HttpOptions
{
    public string BearerToken { get; }
    public string SessionId { get; }
    
    public HttpOptions(string bearerToken, string sessionId)
    {
        BearerToken = bearerToken;
        SessionId = sessionId;
    }
}