using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;

public class ProdutosController : Controller
{
    private IProdutosData data;

    public ProdutosController(IProdutosData data){
        this.data = data;
    }

    public ActionResult Index(){
        List<Produtos> lista = data.Read();
        return View(lista);
    }

    public ActionResult Search(IFormCollection form)
    {
        string search = form["search"]; // <input name="search" />
        
        List<Produtos> lista = data.Read(search);
        return View("Index", lista);
    }
        
    [HttpGet]
    public ActionResult Create() 
    {
        ViewBag.Categorias = data.Read();
        return View();
    }

    [HttpPost]
    public ActionResult Create(Produtos produto,IFormFile imagem){
     try{
        // Verifica se foi enviado um arquivo de imagem
        if (imagem != null && imagem.Length > 0){
            // Lê a imagem como um array de bytes
            using (MemoryStream ms = new MemoryStream()){
                imagem.CopyTo(ms);
                byte[] imagemBytes = ms.ToArray();

                // Converte os bytes para base64
                string imagemBase64 = Convert.ToBase64String(imagemBytes);
                // imagemBase64 = "data:image/png;charset=utf-8;base64," + imagemBase64;

                // Adiciona o base64 ao seu objeto produto
                produto.Imagem = imagemBase64;
            }
        }
        data.Create(produto);
        return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            // Lide com exceções conforme necessário
            return View("Error");
        }
    }
     public ActionResult Delete(int id) 
    {
        data.Delete(id);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public ActionResult Update(int id)
    {
       Produtos produto = data.Read(id);

        if(produto == null){
            return RedirectToAction("Index");
        }            
        ViewBag.Categorias = data.Read();

        return View(produto);
    }

    [HttpPost]
    public ActionResult Update(int id, Produtos produto)
    {
        data.Update(id, produto);
        return RedirectToAction("Index");
    }
    [HttpGet]
    public ActionResult Produto(int id){
        Produtos produto = data.Read(id);
        return View(produto);
    }

}
    

