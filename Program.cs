var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddTransient<IProdutosData, ProdutosSql>();
builder.Services.AddTransient<ICategoriaData, CategoriasSql>();
builder.Services.AddTransient<IClientesData, ClientesSql>();
builder.Services.AddTransient<IEnderecoData, EnderecoSql>();
builder.Services.AddTransient<IPedidosData, PedidosSql>();
builder.Services.AddTransient<IUsersRepository, UsersSql>();
builder.Services.AddTransient<ICarrinhoData, CarrinhoSql>();
builder.Services.AddSession();

var app = builder.Build();

app.UseStaticFiles();

app.UseSession();

app.MapControllerRoute("default","/{controller=Home}/{action=TelaInicial}/{id?}");

app.Run();
