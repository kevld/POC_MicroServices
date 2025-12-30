namespace POC_MivroServices.Authentication.Models
{
    public class AuthToken
    {
        public string? Token { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
