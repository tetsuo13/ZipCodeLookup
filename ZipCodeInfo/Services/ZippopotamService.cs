using Newtonsoft.Json;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ZipCodeInfo.Models;
using ZipCodeInfo.Models.Zippopotam;

namespace ZipCodeInfo.Services
{
    /// <summary>
    /// Using the free public service Zippopotam.us.
    /// </summary>
    /// <seealso href="https://zippopotam.us/">https://zippopotam.us/</seealso>
    public class ZippopotamService : IGeographicLookupService
    {
        private const string BaseUrl = "http://api.zippopotam.us/us/{0}";

        private static readonly HttpClient _httpClient = new HttpClient();

        public async Task<GeographicDataModel> Lookup(string postalCode)
        {
            var response = await _httpClient.GetAsync(string.Format(BaseUrl, postalCode));
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var deserialized = JsonConvert.DeserializeObject<ZipCodeResultModel>(jsonResponse);

            if (deserialized == null)
            {
                return null;
            }

            var place = deserialized.Places.FirstOrDefault();

            if (place == null)
            {
                return null;
            }

            return new GeographicDataModel
            {
                ZipCode = postalCode,
                Latitude = place.Latitude,
                Longitude = place.Longitude,
                City = place.PlaceName,
                StateName = place.State
            };
        }
    }
}
