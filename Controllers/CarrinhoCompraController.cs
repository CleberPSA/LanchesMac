using LanchesMac.Models;
using LanchesMac.Repositories.Interfaces;
using LanchesMac.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LanchesMac.Controllers
{
    public class CarrinhoCompraController : Controller
    {
        private readonly ILanchesRepository _lancheRespository;
        private readonly CarrinhoCompra _carrinhoCompra;

        public CarrinhoCompraController(ILanchesRepository lancheRespository, 
            CarrinhoCompra carrinhoCompra)
        {
            _lancheRespository = lancheRespository;
            _carrinhoCompra = carrinhoCompra;
        }

        public IActionResult Index()
        {
            var itens = _carrinhoCompra.GetCarrinhoCompraItens();
            _carrinhoCompra.CarrinhoCompraItens = itens;

            var carrinhoCompraVM = new CarrinhoCompraViewModel
            {
                CarrinhoCompra = _carrinhoCompra,
                CarrinhoCompraTotal = _carrinhoCompra.GetCarrinhoCompraTotal()
            };

            return View(carrinhoCompraVM);
        }

        [Authorize]
        public IActionResult AdicionarItemNoCarrinhoCompra(int lancheID)
        {
            var lancheSelecionado = _lancheRespository.Lanches
                .FirstOrDefault(p=> p.LancheID == lancheID);

            if(lancheSelecionado != null)
            {
                _carrinhoCompra.AdicionarCarrinho(lancheSelecionado);
            }
            return RedirectToAction("Index");
        }
        [Authorize]
        public IActionResult RemoverItemNoCarrinho (int lancheId)
        {
            var lancheSelecionado = _lancheRespository.Lanches
                .FirstOrDefault(p => p.LancheID == lancheId);

            if(lancheSelecionado != null)
            {
                _carrinhoCompra.RemoverDoCarrinho(lancheSelecionado);
            }
            return RedirectToAction("Index");
        }


    }
}
