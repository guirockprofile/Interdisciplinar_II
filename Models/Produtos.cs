public class Produtos
{
    public int IdProduto { get; set; }
    public string NomeProd { get; set; }
    public string Descricao { get; set; }
    public decimal Preco { get; set; }
    public int Quantidade { get; set; }
    public string Status { get; set; }
    public int CategoriaId { get; set; }
    public Categorias Categorias { get; set; }
    public string Imagem { get; set; }    

    public Produtos()
{
    Categorias = new Categorias();
}
}

