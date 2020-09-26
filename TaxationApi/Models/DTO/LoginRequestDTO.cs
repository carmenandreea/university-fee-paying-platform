namespace TaxationApi.Models
{
    public class LoginRequestDTO
    {
        public string email { get; set; }
        public string password { get; set; }
        public LoginRequestDTO()
        {
            this.email = "";
            this.password = "";
        }
    }
}