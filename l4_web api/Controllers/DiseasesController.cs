using l4_web_api.Data;
using l4_web_api.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace l4_web_api.Controllers
{
    public class DiseasesController(MedicinalProductsContext context) : Controller
    {
        private readonly MedicinalProductsContext _context = context;
        [ResponseCache(Duration = 2 * 3 + 240, Location = ResponseCacheLocation.Any, NoStore = false)]
        public IActionResult Index()
        {
            var diseases = _context.Diseases.ToList();
            var viewModel = new DiseasesViewModel
            {
                Diseases = diseases
            };
            return View(viewModel);
        }
    }
}
