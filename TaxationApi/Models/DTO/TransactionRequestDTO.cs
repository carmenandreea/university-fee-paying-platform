using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaxationApi.Models
{
    public class TransactionRequestDTO
    {
        public long transactionId { get; set; }
        public string institutionName { get; set; }
        public string payerEmail { get; set; }
        public long status { get; set; }
        public long value { get; set; }
        public long type { get; set; }
        public TransactionRequestDTO()
        {
            this.institutionName = "";
            this.payerEmail = "";
            this.status = -1;
            this.value = -1;
            this.type = -1;
        }
    }
}