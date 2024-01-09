using FastReport.Data;
using FastReport.Export.PdfSimple;
using FastReport.Web;
using LanchesMac.Areas.Admin.FastReportUtils;
using LanchesMac.Areas.Admin.Services;
using Microsoft.AspNetCore.Mvc;

namespace LanchesMac.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminLanchesReportController : Controller
    {
        private readonly IWebHostEnvironment _webHost;
        private readonly RelatorioLanchesService _relatorioLanchesService;

        public AdminLanchesReportController(IWebHostEnvironment webHost, 
            RelatorioLanchesService relatorioLanchesService)
        {
            _webHost = webHost;
            _relatorioLanchesService = relatorioLanchesService;
        }

        public async Task<ActionResult> LanchesCategoriaReport()
        {
            var webReport = new WebReport();
            var mssqlDataConnection = new MsSqlDataConnection();

            webReport.Report.Dictionary.AddChild(mssqlDataConnection);
            webReport.Report.Load(Path.Combine(_webHost.ContentRootPath,"wwwroot/reports",
                "LanchesCategoria.frx"));

            var lanches = HelperFastReport.GetTable(await _relatorioLanchesService.
                GetLanchesReport(), "LanchesReport");
            var categorias = HelperFastReport.GetTable(await _relatorioLanchesService.
                GetCategoriaReport(), "CategoriasReport");

            webReport.Report.RegisterData(lanches, "LanchesReport");
            webReport.Report.RegisterData(categorias, "CategoriasReport");

            return View(webReport);

        }

        public async Task<ActionResult> LanchesCategoriaPDF()
        {
            var webReport = new WebReport();
            var mssqlDataConnection = new MsSqlDataConnection();

            webReport.Report.Dictionary.AddChild(mssqlDataConnection);
            webReport.Report.Load(Path.Combine(_webHost.ContentRootPath, "wwwroot/reports",
                "LanchesCategoria.frx"));

            var lanches = HelperFastReport.GetTable(await _relatorioLanchesService.
                GetLanchesReport(), "LanchesReport");
            var categorias = HelperFastReport.GetTable(await _relatorioLanchesService.
                GetCategoriaReport(), "CategoriasReport");

            webReport.Report.RegisterData(lanches, "LanchesReport");
            webReport.Report.RegisterData(categorias, "CategoriasReport");

            webReport.Report.Prepare();
            Stream stream = new MemoryStream();
            webReport.Report.Export(new PDFSimpleExport(), stream);
            stream.Position = 0;

            //return File(stream, "application/zip", "LanchesCategoria.pdf"); //Faz o download
            return new FileStreamResult(stream, "application/pdf"); //Abre o PDF


        }
    }
}
