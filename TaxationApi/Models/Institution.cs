using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace TaxationApi.Models
{
    public class Institution
    {
        // private long _institutionId { get; set; }
        [Key]
        public long institutionId { get; set; }
        // private string _name { get; set; }
        public string name { get; set; }
        public ICollection<TaxValue> taxValues { get; set; }
        public ICollection<AdminUser> adminUsers { get; set; }
        public ICollection<Transaction> transactions { get; set; }
        public Institution()
        {
            this.taxValues = new List<TaxValue>();
            this.adminUsers = new List<AdminUser>();
            this.transactions = new List<Transaction>();
        }
        public void copy(Institution institution)
        {
            this.name = institution.name;
        }
        public void updateDbDependencies(IEnumerable<TaxValue> TaxValues,
        IEnumerable<AdminUser> AdminUsers,
        IEnumerable<Transaction> Transactions)
        {
            foreach (TaxValue taxVal in TaxValues)
            {
                if (taxVal.institutionId == this.institutionId)
                    this.taxValues.Add(taxVal);
            }
            foreach (AdminUser adminUser in AdminUsers)
            {
                if (adminUser.institutionId == this.institutionId)
                    this.adminUsers.Add(adminUser);
            }
            foreach (Transaction transaction in Transactions)
            {
                if (transaction.institutionId == this.institutionId)
                    this.transactions.Add(transaction);
            }
        }
    }
}