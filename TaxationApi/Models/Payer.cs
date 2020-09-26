using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
namespace TaxationApi.Models
{
    public class Payer
    {
        [Key]
        public long payerId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string cnp { get; set; }
        public string address { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string password { get; set; }
        public ICollection<Transaction> transactions { get; set; }

        public Payer()
        {
            this.firstName = null;
            this.lastName = null;
            this.cnp = null;
            this.address = null;
            this.email = null;
            this.phone = null;
            this.password = "123456";
            this.transactions = new List<Transaction>();
        }
        public void copy(Payer p)
        {
            this.firstName = p.firstName;
            this.lastName = p.lastName;
            this.address = p.address;
            this.email = p.email;
            this.phone = p.phone;
            this.cnp = p.cnp;
            this.password = p.password;

        }
        public void copy(PayerPutRequestDTO p)
        {
            this.firstName = p.firstName;
            this.lastName = p.lastName;
            this.address = p.address;
            this.phone = p.phone;

        }
        public void copy(TransactionPostRequestDTO t)
        {
            this.firstName = t.payerFirstName;
            this.lastName = t.payerLastName;
            this.address = t.payerAddress;
            this.email = t.payerEmail;
            this.phone = t.payerPhone;
            this.cnp = t.payerCNP;
        }

        public void updateDbDependencies(IEnumerable<Transaction> Transactions)
        {
            foreach (Transaction transaction in Transactions)
            {
                if (transaction.payerId == this.payerId)
                    this.transactions.Add(transaction);
            }
        }
        public bool comparePasswords(string unencodedPasswordToChekc)
        {
            return string.Equals(password, System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(unencodedPasswordToChekc)));

        }

    }
}