public class PedidosProdutos
{
    public int IdPedido { get; set; }
    public int IdProduto { get; set; }
    public int Qtd { get; set; }
    public decimal Preco { get; set; }
    public decimal TotalPedido { get; set; }
    public DateTime DataPedido { get; set; }
    public string StatusPedido { get; set; }
}