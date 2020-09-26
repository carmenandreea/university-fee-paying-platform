namespace TaxationApi.Models
{
    public class TransactionResponseDTO
    {
        public long transactionId { get; set; }
        private long institutionId { get; set; }
        public string institutionName { get; set; }
        private long payerId { get; set; }
        public string payerEmail { get; set; }
        public long value { get; set; }
        public long status { get; set; }
        public long type { get; set; }

        // public TransactionResponseDTO(Transaction t)
        // {
        //     this.transactionId = t.transactionId;
        //     this.institutionId = t.institutionId;
        //     this.payerId = t.payerId;
        //     this.value = t.value;
        //     this.status = t.status;
        //     this.type = t.type;
        // }
        public TransactionResponseDTO(Transaction t, string name, string email)
        {
            this.transactionId = t.transactionId;
            this.institutionName = name;
            this.payerEmail = email;
            this.value = t.value;
            this.status = t.status;
            this.type = t.type;
        }
        public TransactionResponseDTO(Transaction t)
        {
            this.transactionId = t.transactionId;
            this.institutionId = t.institutionId;
            this.payerId = t.payerId;
            this.value = t.value;
            this.status = t.status;
            this.type = t.type;
        }
        public TransactionResponseDTO()
        {
            this.institutionName = "";
            this.payerEmail = "";
            this.value = -1;
            this.status = -1;
            this.type = -1;
        }
    }
}