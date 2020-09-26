using System.Collections.Generic;
using TaxationApi.DAL;
namespace TaxationApi.Models
{
    public class InstitutionResponseDTO
    {
        public long institutionId { get; set; }
        public string name { get; set; }
        public ICollection<TaxValueResponseDTO> taxValues { get; set; }
        public ICollection<AdminUserResponseDTO> adminUsers { get; set; }
        public ICollection<TransactionResponseDTO> transactions { get; set; }
        public InstitutionResponseDTO(Institution i, PayerRepository payerReposiory)
        {
            this.taxValues = new List<TaxValueResponseDTO>();
            this.adminUsers = new List<AdminUserResponseDTO>();
            this.transactions = new List<TransactionResponseDTO>();
            this.institutionId = i.institutionId;
            this.name = i.name;
            foreach (TaxValue taxValue in i.taxValues)
            {
                this.taxValues.Add(new TaxValueResponseDTO(taxValue, name));
            }
            foreach (AdminUser adminUser in i.adminUsers)
            {
                this.adminUsers.Add(new AdminUserResponseDTO(adminUser, name));
            }
            IDictionary<long, string> payers = new Dictionary<long, string>();
            foreach (Transaction transaction in i.transactions)
            {
                string payerEmail = "";
                if (payers.ContainsKey(transaction.payerId))
                    payerEmail = payers[transaction.payerId];
                else
                {
                    foreach (Payer payer in payerReposiory.dbSet)
                    {
                        if (payer.payerId == transaction.payerId)
                        {
                            payers.Add(payer.payerId, payer.email);
                            payerEmail = payer.email;
                            break;
                        }
                    }

                }
                this.transactions.Add(new TransactionResponseDTO(transaction, i.name, payerEmail));
            }
        }
    }
}