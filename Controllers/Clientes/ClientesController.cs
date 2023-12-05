using Microsoft.AspNetCore.Mvc;

public class ClientesController : Controller
{
    private IClientesData data;

    public ClientesController(IClientesData data){
        this.data = data;
    }

    public ActionResult Index(){
        List<Clientes> lista = data.Read();
        return View(lista);
    }

    public ActionResult Search(IFormCollection form)
    {
        string search = form["search"]; // <input name="search" />
        
        List<Clientes> lista = data.Read(search);
        return View("Index", lista);
    }
    public ActionResult Perfil(int id){
        Clientes cliente = data.Read(id);
        return View(cliente);
    }
    
    [HttpGet]
    public ActionResult Create() 
    {
        ViewBag.Clientes = data.Read();
        return View();
    }

    [HttpPost]
    public ActionResult Create(Clientes clientes)
    {
        data.Create(clientes);
        return RedirectToAction("Index");
    }

     public ActionResult Delete(int id) 
    {
        data.Delete(id);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public ActionResult Update(int id)
    {
      Clientes clientes = data.Read(id);

        if(clientes == null)
            return RedirectToAction("Home", "Home");
            
        ViewBag.clientes = data.Read();

        return View(clientes);
    }

    [HttpPost]
    public ActionResult Update(int id, Clientes clientes)
    {
        data.Update(id, clientes);
        return RedirectToAction("Home", "Home");
    }

    
}
    

