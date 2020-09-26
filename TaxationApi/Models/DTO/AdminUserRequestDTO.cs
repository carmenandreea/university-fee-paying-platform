using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaxationApi.Models
{
    public class AdminUserRequestDTO
    {

        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string institutionName { get; set; }

        public Institution institution { get; set; }
        public AdminUserRequestDTO()
        {
            this.name = new string("");
            this.email = new string("");
            this.password = new string("");
            this.institutionName = "";
        }
        public AdminUserRequestDTO(AdminUser a, string iName)
        {
            this.name = a.name;
            this.email = a.email;
            this.password = a.password;
            this.institutionName = iName;
        }

    }
}