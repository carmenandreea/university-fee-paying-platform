using System.Collections.Generic;
using TaxationApi.DAL;
namespace TaxationApi.Models
{
    public class TransactionsByEntityDTO
    {
        public static readonly int BY_INSTITUTION = 1;
        public static readonly int BY_PAYER = 2;
        public ICollection<TransactionResponseDTO> transactions { get; set; }
        public TransactionsByEntityDTO(ICollection<Transaction> newTransactions)
        {
            transactions = new List<TransactionResponseDTO>();
            foreach (Transaction transaction in newTransactions)
            {
                transactions.Add(new TransactionResponseDTO(transaction));
            }
        }
        public TransactionsByEntityDTO()
        {
            transactions = new List<TransactionResponseDTO>();
        }
        public void setByEntity(long id, int type, IEnumerable<Transaction> allTransactions,
                                 InstitutionRepository institutionSet,
                                 PayerRepository payerSet)
        {
            if (type == 1) setByInstitution(id, allTransactions, institutionSet, payerSet);
            else if (type == 2) setByPayer(id, allTransactions, institutionSet, payerSet);
        }
        private void setByInstitution(long id, IEnumerable<Transaction> allTransactions, InstitutionRepository institutionSet,
                                 PayerRepository payerSet)
        {
            Institution institution = institutionSet.GetEntityById(id);
            foreach (Transaction transaction in allTransactions)
            {

                if (transaction.institutionId == id)
                    transactions.Add(new TransactionResponseDTO(transaction, institution.name, payerSet.GetEntityById(transaction.payerId).email));
            }
        }
        private void setByPayer(long id, IEnumerable<Transaction> allTransactions, InstitutionRepository institutionSet,
                                 PayerRepository payerSet)
        {
            Payer p = payerSet.GetEntityById(id);
            foreach (Transaction transaction in allTransactions)
            {
                if (transaction.payerId == id)
                    transactions.Add(new TransactionResponseDTO(transaction, institutionSet.GetEntityById(transaction.institutionId).name, p.email));
            }
        }

    }
}