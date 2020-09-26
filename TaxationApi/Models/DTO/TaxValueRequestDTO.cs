using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaxationApi.Models
{
    public class TaxValueRequestDTO
    {
        public long taxValueId { get; set; }
        public string institutionName { get; set; }
        public long value { get; set; }
        public long type { get; set; }
        public string requesterEmail { get; set; }
        public TaxValueRequestDTO()
        {
            this.institutionName = "";
            this.value = -1;
            this.type = -1;
            this.requesterEmail = "";

        }
    }
}