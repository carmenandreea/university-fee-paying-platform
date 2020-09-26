using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

using TaxationApi.Models;
namespace TaxationApi.DAL
{
    public class PayerRepository : GenericRepository<Payer>
    {
        public PayerRepository(TaxationDbContext theContext) : base(theContext)
        {

        }
        public Payer FindByEmail(string email)
        {
            foreach (Payer payer in this.dbSet)
            {
                if (string.Equals(payer.email, email)) return payer;
            }
            return null;
        }
        public Payer FindByCNP(string CNP)
        {
            foreach (Payer payer in this.dbSet)
            {
                if (string.Equals(payer.cnp, CNP)) return payer;
            }
            return null;
        }
        public new void InsertEntity(Payer payer)
        {
            IEnumerable<Payer> all = base.Get();
            foreach (Payer payer2 in all)
            {
                if (string.Equals(payer2.email, payer.email) || string.Equals(payer.cnp, payer2.cnp))
                {
                    return;
                }
            }
            payer.password = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(payer.password));
            base.InsertEntity(payer);
        }
    }
}
