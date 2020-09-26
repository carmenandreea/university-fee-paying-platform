using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaxationApi.Models
{
    public class TaxValue
    {
        [Key]
        public long taxValueId { get; set; }
        [ForeignKey("Institution")]
        public long institutionId { get; set; }
        public Institution institution { get; set; }
        public long value { get; set; }
        public long type { get; set; }
        public TaxValue(TaxValueRequestDTO t, Institution i)
        {
            this.institutionId = i.institutionId;
            this.value = t.value;
            this.type = t.type;
            this.institution = institution;
        }
        public TaxValue()
        {
            this.institutionId = -1;
            this.value = -1;
            this.type = -1;
            this.institution = new Institution();
        }
        public void copy(TaxValueRequestDTO t, long institutionId)
        {
            this.institutionId = institutionId;
            this.value = t.value;
            this.type = t.type;
        }
    }
}