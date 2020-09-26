namespace TaxationApi.Models
{
    public class TaxValueResponseDTO
    {
        public long taxValueId { get; set; }
        private long institutionId { get; set; }
        public string institutionName { get; set; }
        public long value { get; set; }
        public long type { get; set; }
        public TaxValueResponseDTO(TaxValue t, string name)
        {
            this.taxValueId = t.taxValueId;
            this.institutionId = t.institutionId;
            this.value = t.value;
            this.type = t.type;
            this.institutionName = name;
        }
        public TaxValueResponseDTO()
        {
            this.institutionId = -1;
            this.value = -1;
            this.type = -1;
            this.institutionName = "";
        }
    }
}