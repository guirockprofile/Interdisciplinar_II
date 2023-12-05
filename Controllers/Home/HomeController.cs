using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

public class HomeController : Controller
{
    private IProdutosData ProdutoData;
    public HomeController(IProdutosData ProdutoData){
        this.ProdutoData = ProdutoData;
    }
    public ActionResult Home()
    {
        List<Produtos> lista = ProdutoData.Read();
        return View(lista);
    }
    public ActionResult TelaInicial()
    {
        return View();
    }
    public ActionResult Search(IFormCollection form)
    {
        string search = form["search"]; // <input name="search" />
        List<Produtos> lista = ProdutoData.Read(search);
        return View("Home", lista);
    }
    public ActionResult SearchLog(IFormCollection form)
    {
        string search = form["search"]; // <input name="search" />
        List<Produtos> lista = ProdutoData.Read(search);
        return View("HomeLogado", lista);
    }

    //    public ActionResult Search(IFormCollection form)
    //     {
    //         string search = form["search"]; // <input name="search" />
            
    //         List<Produtos> lista = data.Read(search);
    //         return View("Index", lista);
    //     }
}
//     public ActionResult Search(IFormCollection form)
//     {
//         string search = form["search"]; // <input name="search" />
        
//         List<Produtos> lista = data.Read(search);
//         return View("Index", lista);
//     }

//         [HttpGet]
//     public ActionResult AddPedido() 
//     {
//         ViewBag.Produto = data.Read();
//         return View();
//     }

//     [HttpPost]
//     public ActionResult AddPedido(ItensPedidos ItensPed)
//     {
//         data.AddPedido(ItensPed);
//         return RedirectToAction("Index");
//     }
    
// }