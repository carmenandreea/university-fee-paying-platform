using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

using TaxationApi.Models;
namespace TaxationApi.DAL
{
    public class InstitutionRepository : GenericRepository<Institution>
    {
        public InstitutionRepository(TaxationDbContext theContext) : base(theContext)
        {
        }
        public Institution FindByName(string institutionName)
        {
            foreach (Institution institution in this.dbSet)
            {
                if (string.Equals(institution.name, institutionName)) return institution;
            }
            return null;
        }
    }
}
