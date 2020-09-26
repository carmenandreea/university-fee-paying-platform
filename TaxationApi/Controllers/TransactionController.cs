using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TaxationApi.Models;
using System.Collections.Generic;
using TaxationApi.DAL;
using System;
namespace TaxationApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionController : ControllerBase
    {
        private InstitutionRepository institutionRepository;
        private PayerRepository payerRepository;
        private GenericRepository<Transaction> transactionRepository;
        private GenericRepository<TaxValue> taxValueRepository;
        private readonly ILogger<TransactionController> _logger;
        public TransactionController(ILogger<TransactionController> logger, TaxationDbContext dbContext)
        {
            _logger = logger;
            institutionRepository = new InstitutionRepository(dbContext);
            payerRepository = new PayerRepository(dbContext);
            transactionRepository = new GenericRepository<Transaction>(dbContext);
            taxValueRepository = new GenericRepository<TaxValue>(dbContext);
        }
        [HttpGet("{id}")]
        public ActionResult<TransactionResponseDTO> GetTransaction(long id)
        {
            Transaction transaction = transactionRepository.GetEntityById(id);
            if (transaction == null) return NotFound();
            transactionRepository.Save();
            return new TransactionResponseDTO(transaction);
        }
        [HttpPost()]
        public ActionResult<TransactionResponseDTO> PostTransaction(TransactionPostRequestDTO transaction)
        {
            System.Console.WriteLine(transaction.institutionName);
            Institution institution = institutionRepository.FindByName(transaction.institutionName);
            Payer payer = payerRepository.FindByEmail(transaction.payerEmail);
            System.Console.WriteLine(transaction.payerCNP);
            if (institution == null)
                return NotFound();
            if (payer == null)
            {
                Payer newPayer = new Payer();
                newPayer.email = transaction.payerEmail;
                newPayer.copy(transaction);
                System.Console.WriteLine(newPayer.email);
                payer = newPayer;
                payerRepository.Save();
            }
            bool valid = false;
            foreach (TaxValue taxValue in taxValueRepository.dbSet)
            {
                if (taxValue.type == transaction.type && taxValue.value == transaction.value && institution.institutionId == taxValue.institutionId)
                {
                    valid = true;
                    break;
                }
            }
            if (!valid) return NotFound();
            Transaction t = new Transaction(transaction, institution, payer);
            System.Console.WriteLine(t.institutionId);

            transactionRepository.InsertEntity(t);

            transactionRepository.Save();
            institutionRepository.Save();
            payerRepository.Save();

            return new TransactionResponseDTO(t, institution.name, payer.email);
        }
        [HttpPut()]
        public ActionResult<TransactionResponseDTO> PutTransaction(TransactionRequestDTO transaction)
        {
            Transaction t = transactionRepository.GetEntityById(transaction.transactionId);
            if (t == null) return NotFound();

            Institution institution = institutionRepository.GetEntityById(t.institutionId);
            Payer payer = payerRepository.GetEntityById(t.payerId);

            t.status = transaction.status;
            t.payer = payer;
            t.institution = institution;

            transactionRepository.UpdateEntity(t);

            transactionRepository.Save();
            institutionRepository.Save();
            payerRepository.Save();

            return new TransactionResponseDTO(t, institution.name, payer.email);
        }
        [HttpDelete("{id}")]
        public ActionResult<TransactionResponseDTO> DeleteTransaction(long id)
        {
            Transaction transaction = transactionRepository.GetEntityById(id);
            if (transaction == null) return NotFound();

            transactionRepository.DeleteEntity(id);
            transactionRepository.Save();
            return new TransactionResponseDTO(transaction);
        }
        [HttpGet("byE/{entity}/{identifier}")]
        public ActionResult<TransactionsByEntityDTO> GetTransactionByEntity(int entity, string identifier)
        {
            System.Console.WriteLine(identifier);
            TransactionsByEntityDTO transactionsByEntity = new TransactionsByEntityDTO();

            if (TransactionsByEntityDTO.BY_INSTITUTION == entity)
            {
                Institution i = institutionRepository.FindByName(identifier);
                if (i == null) return NotFound();
                transactionsByEntity.setByEntity(i.institutionId, TransactionsByEntityDTO.BY_INSTITUTION, transactionRepository.Get(),
                                                institutionRepository, payerRepository);
            }

            if (TransactionsByEntityDTO.BY_PAYER == entity)
            {
                Payer p = payerRepository.FindByEmail(identifier);
                System.Console.WriteLine(p);
                if (p == null) return NotFound();
                transactionsByEntity.setByEntity(p.payerId, TransactionsByEntityDTO.BY_PAYER, transactionRepository.Get(), institutionRepository, payerRepository);
            }
            return transactionsByEntity;

        }
    }
}