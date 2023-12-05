using Microsoft.AspNetCore.Mvc;

public class EnderecoController : Controller
{
    private IEnderecoData data;

    public EnderecoController(IEnderecoData data){
        this.data = data;
    }

    public ActionResult Index(){
        List<Endereco> lista = data.Read();
        return View(lista);
    }

    public ActionResult Search(IFormCollection form)
    {
        string search = form["search"]; // <input name="search" />
        
        List<Endereco> lista = data.Read(search);
        return View("Index", lista);
    }
    public ActionResult Endereco(int id){
        Endereco Endereco = data.Read(id);
        return View(Endereco);
    }
    [HttpGet]
    public ActionResult Create() 
    {
        ViewBag.Endereco = data.Read();
        return View();
    }

    [HttpPost]
    public ActionResult Create(int id, Endereco Endereco)
    {
        data.Create(id,Endereco);
        return RedirectToAction("Endereco");
    }

     public ActionResult Delete(int id) 
    {
        data.Delete(id);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public ActionResult Update(int id)
    {
      Endereco Endereco = data.Read(id);

        if(Endereco == null)
            return RedirectToAction("Index");
            
        ViewBag.Endereco = data.Read();

        return View(Endereco);
    }

    [HttpPost]
    public ActionResult Update(int id, Endereco Endereco)
    {
        data.Update(id, Endereco);
        return RedirectToAction("Endereco");
    }
}
    

