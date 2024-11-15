using l4_web_api.Data;
using l4_web_api.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace l4_web_api.Controllers
{
    public class PrescriptionsController(MedicinalProductsContext context) : Controller
    {
        private readonly MedicinalProductsContext _context = context;
        [ResponseCache(Duration = 2 * 3 + 240, Location = ResponseCacheLocation.Any, NoStore = false)]
        public IActionResult Index()
    {
            var prescriptions = _context.Prescriptions
                    .Include(p => p.FamilyMember)  // Подгрузка FamilyMember
                    .Include(p => p.Diseases)      // Подгрузка Diseases
                    .ToList();

            var viewModel = new PrescriptionsViewModel
            {
                Prescriptions = prescriptions
            };

            return View(viewModel);
        }
}
}
