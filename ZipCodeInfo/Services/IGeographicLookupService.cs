using System.Threading.Tasks;
using ZipCodeInfo.Models;

namespace ZipCodeInfo.Services
{
    public interface IGeographicLookupService
    {
        Task<GeographicDataModel> Lookup(string postalCode);
    }
}
