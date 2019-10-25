using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using ZipCodeInfo.Models;
using ZipCodeInfo.Services;

namespace ZipCodeInfo.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// Gets or sets the geographical data for the last successful zip code.
        /// </summary>
        [TempData]
        public string LastZipCode { get; set; }

        private readonly ILogger<HomeController> _logger;
        private readonly IGeographicLookupService _geographicLookupService;
        private readonly IZipCodeService _zipCodeService;

        public HomeController(ILogger<HomeController> logger,
            IGeographicLookupService geographicLookupService,
            IZipCodeService zipCodeService)
        {
            _logger = logger;
            _geographicLookupService = geographicLookupService;
            _zipCodeService = zipCodeService;
        }

        public IActionResult Index()
        {
            var model = new ZipCodeViewModel
            {
                PreviousZipCodeEntries = _zipCodeService.GetAll()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(ZipCodeViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.GeographicData = await _geographicLookupService.Lookup(model.ZipCode);
                }
                catch (Exception e)
                {
                    _logger.LogError(e, $"Error looking up postal code {model.ZipCode}: {e.Message}");
                }

                if (model.GeographicData == null)
                {
                    ModelState.AddModelError("", "Couldn't geolocate the postal code entered. Please check again.");
                }
                else
                {
                    LastZipCode = JsonConvert.SerializeObject(model.GeographicData);
                }
            }

            model.PreviousZipCodeEntries = _zipCodeService.GetAll();

            return View(model);
        }

        public IActionResult AddZipCode()
        {
            _zipCodeService.Add(JsonConvert.DeserializeObject<GeographicDataModel>(LastZipCode));
            return RedirectToAction(nameof(HomeController.Index));
        }
    }
}
