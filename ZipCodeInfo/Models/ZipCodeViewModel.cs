using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ZipCodeInfo.Models
{
    public class ZipCodeViewModel
    {
        [Required]
        [DataType(DataType.PostalCode)]
        public string ZipCode { get; set; }

        public GeographicDataModel GeographicData { get; set; }

        public IEnumerable<GeographicDataModel> PreviousZipCodeEntries { get; set; }
    }
}
