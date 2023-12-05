using Microsoft.AspNetCore.Mvc;

public class CarrinhoController : Controller
{
    private ICarrinhoData data;

    public CarrinhoController(ICarrinhoData data){
        this.data = data;
    }
    public ActionResult Index(int id){
        List<Pedidos> pedido = data.ReadCarrin(id);
        int? idPedido = pedido.Count > 0 ? pedido[0].idPedido : (int?)null;
        if(idPedido == null){
            return RedirectToAction("NoBitches");
        }
        return View(pedido);
    }
    public ActionResult NoBitches(){
        return View();
    }
        
     public ActionResult FinalizarPedido(int id) 
    {
        data.FinalizarPedido(id);
        return RedirectToAction("CompraFinalizada");
    }

        public ActionResult Delete(int id) 
    {
        data.Delete(id);
        return RedirectToAction("Home","Home");
    }

        public ActionResult CompraFinalizada(){
        return View();
    }

    [HttpGet]
    public ActionResult Update(int id)
    {
       Pedidos Pedido = data.Read(id);

        if(Pedido == null)
            return RedirectToAction("Home","Home");
            
        ViewBag.PedidosProdutos = data.Read();

        return View(Pedido);
    }

    [HttpPost]
    public ActionResult Update(int id   , PedidosProdutos pedidosProdutos)
    {
        data.Update(id, pedidosProdutos);
        return RedirectToAction("Home","Home");
    }

}

    

