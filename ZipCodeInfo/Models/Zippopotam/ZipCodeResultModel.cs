using Newtonsoft.Json;
using System.Collections.Generic;

namespace ZipCodeInfo.Models.Zippopotam
{
    public class ZipCodeResultModel
    {
        [JsonProperty(PropertyName = "post code")]
        public string PostCode { get; set; }

        [JsonProperty(PropertyName = "country")]
        public string Country { get; set; }

        [JsonProperty(PropertyName = "country abbreviation")]
        public string CountryAbbreviation { get; set; }

        [JsonProperty(PropertyName = "places")]
        public IEnumerable<PlaceModel> Places { get; set; }
    }
}
