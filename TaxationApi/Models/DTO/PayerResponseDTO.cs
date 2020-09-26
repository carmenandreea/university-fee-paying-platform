using System.Collections.Generic;
using TaxationApi.DAL;
namespace TaxationApi.Models
{
    public class PayerResponseDTO
    {
        private long payerId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string cnp { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public ICollection<TransactionResponseDTO> transactions { get; set; }
        public PayerResponseDTO()
        {
            this.payerId = -1;
            this.firstName = null;
            this.lastName = null;
            this.cnp = null;
            this.email = null;
            this.address = null;
            this.phone = null;

        }
        public PayerResponseDTO(Payer p, InstitutionRepository institutionRepository)
        {
            this.payerId = p.payerId;
            this.firstName = p.firstName;
            this.lastName = p.lastName;
            this.cnp = p.cnp;
            this.email = p.email;
            this.address = p.address;
            this.phone = p.phone;
            this.transactions = new List<TransactionResponseDTO>();
            IDictionary<long, string> institutions = new Dictionary<long, string>();
            foreach (Transaction transaction in p.transactions)
            {
                string institutionName = "";
                if (institutions.ContainsKey(transaction.institutionId))
                {
                    institutionName = institutions[transaction.institutionId];
                }
                else
                {
                    foreach (Institution i in institutionRepository.dbSet)
                    {
                        if (i.institutionId == transaction.institutionId)
                        {
                            institutionName = i.name;
                            institutions.Add(i.institutionId, i.name);
                            break;
                        }
                    }
                }
                if (transaction.payerId == this.payerId)
                {
                    this.transactions.Add(new TransactionResponseDTO(transaction, institutionName, this.email));
                }
            }

        }
    }
}