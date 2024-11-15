using l4_web_api.Data;
using l4_web_api.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace l4_web_api.Controllers
{
    public class MedicinesController(MedicinalProductsContext context) : Controller
    {
        private readonly MedicinalProductsContext _context = context;
        [ResponseCache(Duration = 2 * 3 + 240, Location = ResponseCacheLocation.Any, NoStore = false)]
        public IActionResult Index()
        {
            var medicines = _context.Medicines.ToList();
            var viewModel = new MedicinesViewModel
            {
                Medicines = medicines
            };
            return View(viewModel);
        }
    }
}
