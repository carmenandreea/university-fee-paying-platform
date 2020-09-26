namespace TaxationApi.Models
{
    public class LoginResponseDTO
    {
        public int status { get; set; }
        public string payerEmail { get; set; }
        public LoginResponseDTO()
        {
            this.payerEmail = "";
            this.status = 0;
        }
        public LoginResponseDTO(int stat, string email)
        {
            this.status = stat;
            this.payerEmail = email;
        }
    }
}