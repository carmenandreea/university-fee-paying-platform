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
    public class InstitutionController : ControllerBase
    {
        private InstitutionRepository institutionRepository;
        private GenericRepository<TaxValue> taxValueRepository;
        private GenericRepository<Transaction> transactionRepository;
        private AdminUserRepository adminUserRepository;
        private PayerRepository payerRepository;

        private readonly ILogger<InstitutionController> _logger;
        public InstitutionController(ILogger<InstitutionController> logger, TaxationDbContext dbContext)
        {
            _logger = logger;
            institutionRepository = new InstitutionRepository(dbContext);
            taxValueRepository = new GenericRepository<TaxValue>(dbContext);
            transactionRepository = new GenericRepository<Transaction>(dbContext);
            adminUserRepository = new AdminUserRepository(dbContext);
            payerRepository = new PayerRepository(dbContext);
        }
        [HttpGet("{id}")]
        public ActionResult<InstitutionResponseDTO> GetInstitution(long id)
        {
            Institution institution = institutionRepository.GetEntityById(id);
            if (institution == null) return NotFound();
            institution.updateDbDependencies(taxValueRepository.Get(), adminUserRepository.Get(), transactionRepository.Get());
            institutionRepository.Save();
            return new InstitutionResponseDTO(institution, payerRepository);
        }
        [HttpPost()]
        public ActionResult<InstitutionResponseDTO> PostInstitution(Institution institution)
        {
            institutionRepository.InsertEntity(institution);
            institutionRepository.Save();

            return new InstitutionResponseDTO(institution, payerRepository);
        }
        [HttpPut()]
        public ActionResult<InstitutionResponseDTO> PutInstitution(Institution institution)
        {
            Institution i = institutionRepository.FindByName(institution.name);
            if (i == null) return NotFound();

            i.copy(institution);
            institution.updateDbDependencies(taxValueRepository.Get(), adminUserRepository.Get(), transactionRepository.Get());
            institutionRepository.UpdateEntity(i);
            institutionRepository.Save();
            return new InstitutionResponseDTO(i, payerRepository);
        }
        [HttpDelete("{id}")]
        public ActionResult<InstitutionResponseDTO> DeleteInstitution(long id)
        {
            Institution i = institutionRepository.GetEntityById(id);
            if (i == null) return NotFound();
            institutionRepository.DeleteEntity(id);
            institutionRepository.Save();
            return new InstitutionResponseDTO(i, payerRepository);
        }

    }
}