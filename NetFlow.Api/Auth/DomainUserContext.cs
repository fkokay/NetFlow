using NetFlow.Domain.Identity;

namespace NetFlow.Api.Auth
{
    public static class DomainUserContext
    {
        private static readonly object Key = new();

        public static void Set(HttpContext ctx, User user)
            => ctx.Items[Key] = user;

        public static User Get(HttpContext ctx)
            => (User)ctx.Items[Key]!;
    }
}
