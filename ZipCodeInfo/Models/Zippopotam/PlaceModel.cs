using Newtonsoft.Json;

namespace ZipCodeInfo.Models.Zippopotam
{
    public class PlaceModel
    {
        [JsonProperty(PropertyName = "place name")]
        public string PlaceName { get; set; }

        [JsonProperty(PropertyName = "longitude")]
        public double Longitude { get; set; }

        [JsonProperty(PropertyName = "state")]
        public string State { get; set; }

        [JsonProperty(PropertyName = "state abbreviation")]
        public string StateAbbreviation { get; set; }

        [JsonProperty(PropertyName = "latitude")]
        public double Latitude { get; set; }
    }
}
