using LanchesMac.Areas.Admin.Services;
using Microsoft.AspNetCore.Mvc;

namespace LanchesMac.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminRelatorioVendaController : Controller
    {
        private readonly RelatorioVendaService relatorioVendaService;

        public AdminRelatorioVendaController(RelatorioVendaService _relatorioVendaService)
        {
            relatorioVendaService = _relatorioVendaService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> RelatorioVendasSimples(DateTime? minDate, 
            DateTime? maxDate)
        {
            if (!minDate.HasValue)
            {
                minDate = new DateTime(DateTime.Now.Year, 1, 1);
            }

            if (!maxDate.HasValue)
            {
                maxDate = DateTime.Now;
            }

            ViewData["minDate"] = minDate.Value.ToString("yyyy-mm-dd");
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy-mm-dd");

            var result = await relatorioVendaService.FindByDateAsync(minDate, maxDate);
            return View(result);
        }
    }
}
