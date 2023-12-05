using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

public class UsersController : Controller
{
    IUsersRepository repository;
    public ActionResult Index()
    {
        string? session = HttpContext.Session.GetString("users");
        Users? users = JsonSerializer.Deserialize<Users>(session);
        if(users.IdUser == 1){
            return View();
        }
        return RedirectToAction("Home","Home");
    }
    public UsersController(IUsersRepository repository) 
    {
        this.repository = repository;
    }

    [HttpGet]
    public ActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Login(IFormCollection form)
    {
        string? name = form["Name"];
        string? senha = form["Senha"];

        var users = repository.Login(name!, senha!);

        if(users == null)
        {
            ViewBag.Error = "Usuário/Senha inválidos";
            return View();
        }

        HttpContext.Session.SetString("users", JsonSerializer.Serialize(users));

        return RedirectToAction("Index");
    }

    [HttpGet]
    public ActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }

    [HttpGet]
    public ActionResult Create() 
    {
        return View();
    }

    [HttpPost]
    public ActionResult Create(Users users, Clientes clientes)
    {
        repository.Create(users,clientes);
        return RedirectToAction("Login");
    }

}