using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
namespace TaxationApi.Models
{
    public class PayerPutRequestDTO
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string address { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string password { get; set; }
        public PayerPutRequestDTO()
        {
            this.firstName = null;
            this.lastName = null;

            this.address = null;
            this.email = null;
            this.phone = null;
            this.password = "123456";
        }


    }
}