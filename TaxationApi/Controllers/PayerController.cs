
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TaxationApi.Models;
using TaxationApi.DAL;
using Microsoft.EntityFrameworkCore;
namespace TaxationApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PayerController : ControllerBase
    {
        private PayerRepository payerRepository;
        private InstitutionRepository institutionRepository;
        private GenericRepository<Transaction> transactionRepository;
        private readonly ILogger<PayerController> _logger;
        public PayerController(ILogger<PayerController> logger, TaxationDbContext dbContext)
        {
            _logger = logger;
            payerRepository = new PayerRepository(dbContext);
            institutionRepository = new InstitutionRepository(dbContext);
            transactionRepository = new GenericRepository<Transaction>(dbContext);

        }
        [HttpGet()]
        public ActionResult<PayerResponseDTO> GetPayerByEmail(string email)
        {
            Payer payer = payerRepository.FindByEmail(email);
            if (payer == null) return NotFound();

            payer.updateDbDependencies(transactionRepository.Get());
            payerRepository.Save();
            return new PayerResponseDTO(payer, institutionRepository);
        }
        // [HttpGet()]
        // public ActionResult<PayerResponseDTO> GetPayerByCNP(string cnp)
        // {
        //     Payer payer = payerRepository.FindByCNP(cnp);
        //     if (payer == null) return NotFound();

        //     payer.updateDbDependencies(transactionRepository.Get());
        //     payerRepository.Save();
        //     return new PayerResponseDTO(payer);
        // }
        [HttpPost()]
        public ActionResult<PayerResponseDTO> PostPayer(Payer payer)
        {
            payerRepository.InsertEntity(payer);
            payerRepository.Save();
            return new PayerResponseDTO(payer, institutionRepository);
        }
        [HttpPut()]
        public ActionResult<PayerResponseDTO> PutPayer(PayerPutRequestDTO payer)
        {
            Payer p = payerRepository.FindByEmail(payer.email);
            if (p == null) return NotFound();
            if (p.comparePasswords(payer.password))
                return ValidationProblem();

            p.copy(payer);
            p.updateDbDependencies(transactionRepository.Get());

            payerRepository.UpdateEntity(p);
            payerRepository.Save();
            return new PayerResponseDTO(p, institutionRepository);
        }
        [HttpDelete("{email}")]
        public ActionResult<PayerResponseDTO> DeletePayer(string email)
        {
            Payer payer = payerRepository.FindByEmail(email);
            if (payer == null) return NotFound();

            payerRepository.DeleteEntity(payer.payerId);
            payerRepository.Save();
            return new PayerResponseDTO(payer, institutionRepository);
        }
        [HttpPost("login")]
        public ActionResult<LoginResponseDTO> LoginPayer(LoginRequestDTO loginRequest)
        {
            Payer payer = payerRepository.FindByEmail(loginRequest.email);
            if (payer == null) return NotFound();
            if (payer.comparePasswords(loginRequest.password))
                return new LoginResponseDTO(1, payer.email);
            return new LoginResponseDTO(0, "");
        }

    }
}
