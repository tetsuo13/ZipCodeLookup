using System.Collections.Generic;
using ZipCodeInfo.Models;

namespace ZipCodeInfo.Services
{
    public interface IZipCodeService
    {
        void Add(GeographicDataModel zipCode);
        IEnumerable<GeographicDataModel> GetAll();
    }
}
