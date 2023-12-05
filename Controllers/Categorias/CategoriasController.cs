using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;

public class CategoriasController : Controller
{
    private ICategoriaData data;

    public CategoriasController(ICategoriaData data){
        this.data = data;
    }

    public ActionResult Index(){
        List<Categorias> lista = data.Read();
        return View(lista);
    }

    public ActionResult Search(IFormCollection form)
    {
        string search = form["search"]; // <input name="search" />
        
        List<Categorias> lista = data.Read(search);
        return View("Index", lista);
    }
        
    [HttpGet]
    public ActionResult Create() 
    {
        ViewBag.Categorias = data.Read();
        return View();
    }

    [HttpPost]
    public ActionResult Create(Categorias categorias)
    {
        data.Create(categorias);
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
       Categorias categorias = data.Read(id);

        if(categorias == null)
            return RedirectToAction("Index");
            
        ViewBag.Categorias = data.Read();

        return View(categorias);
    }

    [HttpPost]
    public ActionResult Update(int id, Categorias categorias)
    {
        data.Update(id, categorias);
        return RedirectToAction("Index");
    }
}
    

