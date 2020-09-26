namespace TaxationApi.Models
{
    public class AdminUserResponseDTO
    {

        public long userId { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string institutionName { get; set; }
        public AdminUserResponseDTO(AdminUser a, string iName)
        {
            this.userId = a.userId;
            this.name = a.name;
            this.email = a.email;
            this.institutionName = iName;

        }
        public AdminUserResponseDTO()
        {
            this.name = null;
            this.email = null;
            this.institutionName = "";
            this.userId = -1;

        }

    }
}