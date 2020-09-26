using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaxationApi.Models
{
    public class Transaction
    {
        [Key]
        public long transactionId { get; set; }
        [ForeignKey("Institution")]
        public long institutionId { get; set; }
        public Institution institution { get; set; }
        [ForeignKey("Payer")]
        public long payerId { get; set; }
        public Payer payer { get; set; }
        public long status { get; set; }
        public long value { get; set; }
        public long type { get; set; }
        public Transaction()
        {
            this.institutionId = -1;
            this.payerId = -1;
            this.status = -1;
            this.value = -1;
            this.type = -1;
            this.institution = new Institution();
            this.payer = new Payer();
        }
        public Transaction(TransactionRequestDTO transaction, Institution i, Payer p)
        {
            this.institutionId = i.institutionId;
            this.payerId = p.payerId;
            this.status = transaction.status;
            this.value = transaction.value;
            this.type = transaction.type;
            this.institution = i;
            this.payer = p;
        }
        public Transaction(TransactionPostRequestDTO transaction, Institution i, Payer p)
        {
            this.institutionId = i.institutionId;
            this.payerId = p.payerId;
            this.status = transaction.status;
            this.value = transaction.value;
            this.type = transaction.type;
            this.institution = i;
            this.payer = p;
        }

        public void copy(TransactionRequestDTO transaction)
        {
            // this.institutionId = transaction.institutionId;
            this.status = transaction.status;
        }

    }
}