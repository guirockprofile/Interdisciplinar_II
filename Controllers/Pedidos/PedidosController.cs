using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;

public class PedidosController : Controller
{
    private IPedidosData data;

    public PedidosController(IPedidosData data){
        this.data = data;
    }

    public ActionResult Index(){
        List<Pedidos> lista = data.Read();
        return View(lista);
    }

    // public ActionResult Search(IFormCollection form)
    // {
    //     string search = form["search"]; // <input name="search" />
        
    //     List<PedidosProdutos> lista = data.Read(search);
    //     return View("Index", lista);
    // }
        
    // [HttpGet]
    // public ActionResult AddPedido() 
    // {
    //     ViewBag.Pedidos = data.Read();
    //     return View();
    // }

    // [HttpPost]
    public ActionResult AddPedido(int id, int user )
    {
        data.AddPedidoIn(id, user);
        return RedirectToAction("Home","home");
    }

     public ActionResult Delete(int id) 
    {
        data.Delete(id);
        return RedirectToAction("Home","home");
    }

    [HttpGet]
    public ActionResult Update(int id)
    {
       Pedidos Pedido = data.Read(id);

        if(Pedido == null)
            return RedirectToAction("Index");
            
        ViewBag.PedidosProdutos = data.Read();

        return View(Pedido);
    }

    [HttpPost]
    public ActionResult Update(int id   , PedidosProdutos pedidosProdutos)
    {
        data.Update(id, pedidosProdutos);
        return RedirectToAction("Index","carrinho");
    }

}
    

