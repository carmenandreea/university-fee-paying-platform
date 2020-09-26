using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

using TaxationApi.Models;
namespace TaxationApi.DAL
{
    public class AdminUserRepository : GenericRepository<AdminUser>
    {
        public AdminUserRepository(TaxationDbContext theContext) : base(theContext)
        {

        }
        public AdminUser FindByEmail(string email)
        {
            foreach (AdminUser adminUser in this.dbSet)
            {
                if (string.Equals(adminUser.email, email)) return adminUser;
            }
            return null;
        }
        public bool Allowed(string email)
        {
            AdminUser adminUser = this.FindByEmail(email);
            if (adminUser != null && adminUser.hasWritingPermission == true) return true;
            return false;
        }
        public new void InsertEntity(AdminUser adminUser)
        {
            IEnumerable<AdminUser> all = base.Get();
            foreach (AdminUser adminUser1 in all)
            {
                if (string.Equals(adminUser1.email, adminUser.email))
                {
                    return;
                }
            }
            adminUser.password = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(adminUser.password));
            base.InsertEntity(adminUser);
        }
    }
}
