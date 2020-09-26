using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TaxationApi.DAL;
namespace TaxationApi.Models
{
    [ApiController]
    [Route("[controller]")]
    public class AdminUserController : ControllerBase
    {
        private InstitutionRepository institutionRepository;
        private AdminUserRepository adminUserRepository;
        private readonly ILogger<AdminUserController> _logger;
        public AdminUserController(ILogger<AdminUserController> logger, TaxationDbContext dbContext)
        {
            _logger = logger;
            adminUserRepository = new AdminUserRepository(dbContext);
            institutionRepository = new InstitutionRepository(dbContext);


        }
        [HttpGet("{id}")]
        public ActionResult<AdminUserResponseDTO> GetAdminUser(long id)
        {
            AdminUser adminUser = adminUserRepository.GetEntityById(id);
            if (adminUser == null) return NotFound();
            adminUserRepository.Save();
            return new AdminUserResponseDTO(adminUser, institutionRepository.GetEntityById(adminUser.institutionId).name);
        }
        [HttpGet()]
        public ActionResult<AdminUserResponseDTO> GetAdminUserByEmail(string email)
        {
            AdminUser adminUser = adminUserRepository.FindByEmail(email);
            if (adminUser == null) return NotFound();
            adminUserRepository.Save();
            return new AdminUserResponseDTO(adminUser, institutionRepository.GetEntityById(adminUser.institutionId).name);
        }
        [HttpPost()]
        public ActionResult<AdminUserResponseDTO> PostAdminUser(AdminUser adminUser)
        {
            Institution institution = institutionRepository.GetEntityById(adminUser.institutionId);
            if (institution == null) return NotFound();

            adminUser.institution = institution;
            AdminUser a = new AdminUser();

            a.copy(adminUser);

            adminUserRepository.InsertEntity(a);
            adminUserRepository.Save();
            institutionRepository.Save();

            return new AdminUserResponseDTO(a, institution.name);
        }
        [HttpPut()]
        public ActionResult<AdminUserResponseDTO> PutAdminUser(AdminUserRequestDTO adminUser)
        {
            Institution institution = institutionRepository.FindByName(adminUser.institutionName);
            adminUser.institution = institution;
            AdminUser a = adminUserRepository.FindByEmail(adminUser.email);
            if (institution == null || a == null)
                return NotFound();
            if (!a.comparePasswords(adminUser.password))
                return ValidationProblem();

            a.copy(adminUser, institution);
            adminUserRepository.UpdateEntity(a);
            adminUserRepository.Save();
            institutionRepository.Save();
            return new AdminUserResponseDTO(a, institution.name);
        }
        [HttpDelete("{id}")]
        public ActionResult<AdminUserResponseDTO> DeleteTaxValue(long id)
        {
            AdminUser adminUser = adminUserRepository.GetEntityById(id);
            if (adminUser == null) return NotFound();
            adminUserRepository.DeleteEntity(id);
            adminUserRepository.Save();
            return new AdminUserResponseDTO(adminUser, "");
        }
        [HttpPost("login")]
        public ActionResult<LoginResponseDTO> LoginPayer(LoginRequestDTO loginRequest)
        {
            AdminUser adminUser = adminUserRepository.FindByEmail(loginRequest.email);
            if (adminUser == null) return NotFound();
            if (adminUser.comparePasswords(loginRequest.password))
                return new LoginResponseDTO(1, adminUser.email);
            return new LoginResponseDTO(0, "");
        }
    }
}