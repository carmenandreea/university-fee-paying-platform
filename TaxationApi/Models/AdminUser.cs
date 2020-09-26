using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaxationApi.Models
{
    public class AdminUser
    {
        [Key]
        public long userId { get; set; }

        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public bool hasReadingPermission { get; set; }
        public bool hasWritingPermission { get; set; }
        [ForeignKey("Institution")]
        public long institutionId { get; set; }

        public Institution institution { get; set; }
        public AdminUser()
        {
            this.name = new string("");
            this.email = new string("");
            this.password = new string("");
            this.hasReadingPermission = true;
            this.hasWritingPermission = false;
            this.institutionId = -1;
            this.institution = new Institution();
        }
        public void copy(AdminUserRequestDTO a, Institution i)
        {
            this.name = a.name;
            this.institutionId = i.institutionId;
            this.institution = i;
            this.email = a.email;
        }
        public void copy(AdminUser a)
        {
            this.name = a.name;
            this.institutionId = a.institutionId;
            this.institution = a.institution;
            this.email = a.email;
            this.password = a.password;
            this.hasReadingPermission = a.hasReadingPermission;
            this.hasWritingPermission = a.hasWritingPermission;
        }
        public AdminUser(AdminUser a, Institution i)
        {
            this.name = a.name;
            this.email = a.email;
            this.password = a.password;
            this.hasReadingPermission = a.hasReadingPermission;
            this.hasWritingPermission = a.hasWritingPermission;
            this.institutionId = a.institutionId;
            this.institution = i;
        }
        public bool comparePasswords(string unencodedPasswordToChekc)
        {
            return string.Equals(password, System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(unencodedPasswordToChekc)));

        }
    }
}