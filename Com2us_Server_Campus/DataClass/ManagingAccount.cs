namespace WebAPIServer.DataClass
{
    public class ManagingAccount
    {
        public Int64 AccountId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
    }
}
