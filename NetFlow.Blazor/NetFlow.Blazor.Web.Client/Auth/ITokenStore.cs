namespace NetFlow.Blazor.Web.Client.Auth
{
    public interface ITokenStore
    {
        Task<string?> GetAsync();
        Task SetAsync(string token);
        Task ClearAsync();
    }
}
