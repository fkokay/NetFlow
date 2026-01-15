namespace NetFlow.Blazor.Web.Security
{
    public interface ITokenStore
    {
        string? GetToken();
    }
}
