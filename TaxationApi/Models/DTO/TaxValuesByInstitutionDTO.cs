using System.Collections.Generic;
namespace TaxationApi.Models
{
    public class TaxValuesByInstitutionDTO
    {
        public ICollection<TaxValueResponseDTO> taxValues { get; set; }
        public TaxValuesByInstitutionDTO(ICollection<TaxValue> newTaxValues, string institutionName)
        {
            taxValues = new List<TaxValueResponseDTO>();
            foreach (TaxValue taxValue in newTaxValues)
            {
                taxValues.Add(new TaxValueResponseDTO(taxValue, institutionName));
            }
        }
    }
}