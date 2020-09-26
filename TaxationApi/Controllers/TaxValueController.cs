using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TaxationApi.DAL;
using System.Collections.Generic;
namespace TaxationApi.Models
{
    [ApiController]
    [Route("[controller]")]
    public class TaxValueController : ControllerBase
    {
        private GenericRepository<TaxValue> taxValueRepository;
        private InstitutionRepository institutionRepository;
        private AdminUserRepository adminUserRepository;
        private readonly ILogger<TaxValueController> _logger;
        public TaxValueController(ILogger<TaxValueController> logger, TaxationDbContext dbContext)
        {
            _logger = logger;
            taxValueRepository = new GenericRepository<TaxValue>(dbContext);
            institutionRepository = new InstitutionRepository(dbContext);
            adminUserRepository = new AdminUserRepository(dbContext);

        }
        [HttpGet("{id}")]

        public ActionResult<TaxValueResponseDTO> GetTaxValue(long id)
        {
            TaxValue taxValue = taxValueRepository.GetEntityById(id);
            if (taxValue == null) return NotFound();

            taxValueRepository.Save();
            return new TaxValueResponseDTO(taxValue, institutionRepository.GetEntityById(taxValue.institutionId).name);
        }
        [HttpPost()]
        public ActionResult<TaxValueResponseDTO> PostTaxValue(TaxValueRequestDTO taxValue)
        {
            if (!adminUserRepository.Allowed(taxValue.requesterEmail))
                return ValidationProblem();

            Institution institution = institutionRepository.FindByName(taxValue.institutionName);
            if (institution == null) return NotFound();

            TaxValue t = new TaxValue(taxValue, institution);
            taxValueRepository.InsertEntity(t);

            taxValueRepository.Save();
            institutionRepository.Save();
            return new TaxValueResponseDTO(t, institution.name);
        }
        [HttpPut]
        public ActionResult<TaxValueResponseDTO> PutTaxValue(TaxValueRequestDTO taxValue)
        {
            if (!adminUserRepository.Allowed(taxValue.requesterEmail))
                return ValidationProblem();

            Institution institution = institutionRepository.FindByName(taxValue.institutionName);
            TaxValue t = taxValueRepository.GetEntityById(taxValue.taxValueId);

            t.copy(taxValue, institution.institutionId);
            t.institution = institution;

            taxValueRepository.UpdateEntity(t);
            taxValueRepository.Save();

            institutionRepository.Save();

            return new TaxValueResponseDTO(t, institution.name);
        }
        [HttpDelete("{id}")]
        public ActionResult<TaxValueResponseDTO> DeleteTaxValue(long id, string requesterEmail)
        {
            if (!adminUserRepository.Allowed(requesterEmail))
                return ValidationProblem();

            TaxValue taxValue = taxValueRepository.GetEntityById(id);
            if (taxValue == null) return NotFound();

            taxValueRepository.DeleteEntity(id);
            taxValueRepository.Save();

            return new TaxValueResponseDTO(taxValue, "");
        }
        [HttpGet("byI")]
        public ActionResult<TaxValuesByInstitutionDTO> GetTaxValueByInstitution(string name)
        {
            ICollection<TaxValue> taxValues = new List<TaxValue>();
            IEnumerable<TaxValue> all = taxValueRepository.Get();
            Institution i = institutionRepository.FindByName(name);
            System.Console.WriteLine(name);
            if (i == null) return NotFound();

            foreach (TaxValue taxValue in all)
            {
                if (taxValue.institutionId == i.institutionId)
                    taxValues.Add(taxValue);
            }

            return new TaxValuesByInstitutionDTO(taxValues, i.name);

        }
        [HttpGet("byT/{type}")]
        public ActionResult<TaxValuesByInstitutionDTO> GetTaxValueByType(long type)
        {
            ICollection<TaxValue> taxValues = new List<TaxValue>();
            IEnumerable<TaxValue> all = taxValueRepository.Get();

            foreach (TaxValue taxValue in all)
            {
                if (taxValue.type == type)
                    taxValues.Add(taxValue);
            }

            return new TaxValuesByInstitutionDTO(taxValues, "");

        }
        [HttpGet("byT/{type}/byI/{institutionName}")]
        public ActionResult<TaxValuesByInstitutionDTO> GetValuesByTypeByInstitution(long type, string institutionName)
        {
            ICollection<TaxValue> taxValues = new List<TaxValue>();
            IEnumerable<TaxValue> all = taxValueRepository.Get();

            Institution institution = institutionRepository.FindByName(institutionName);

            if (institution == null) return NotFound();
            foreach (TaxValue taxValue in all)
            {
                if (taxValue.type == type && taxValue.institutionId == institution.institutionId)
                    taxValues.Add(taxValue);
            }
            institutionRepository.Save();
            return new TaxValuesByInstitutionDTO(taxValues, institution.name);
        }
    }
}