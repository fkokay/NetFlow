namespace NetFlow.Api.Auth
{
    public class LoginRequestDto
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FirmCode { get; set; } = string.Empty;
    }
}
