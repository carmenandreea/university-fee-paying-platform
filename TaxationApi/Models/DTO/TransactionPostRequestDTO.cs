using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaxationApi.Models
{
    public class TransactionPostRequestDTO
    {
        public long transactionId { get; set; }
        public string institutionName { get; set; }
        public string payerEmail { get; set; }
        public string payerFirstName { get; set; }
        public string payerLastName { get; set; }
        public string payerCNP { get; set; }
        public string payerAddress { get; set; }
        public string payerPhone { get; set; }
        public long status { get; set; }
        public long value { get; set; }
        public long type { get; set; }
        public TransactionPostRequestDTO()
        {
            this.institutionName = "";
            this.payerEmail = "";
            this.status = -1;
            this.value = -1;
            this.type = -1;
            this.payerAddress = "";
            this.payerCNP = "";
            this.payerFirstName = "";
            this.payerLastName = "";
            this.payerPhone = "";
        }
    }
}