public class Pedidos
{
    public int idPedido { get; set; }
    public int UserId { get; set; }
    public DateTime DataPedido { get; set; }
    public decimal valorPedido { get; set; }
    public string StatusPedido { get; set; }
    public PedidosProdutos pedidosProdutos { get; set; }
    public Produtos produtos { get; set; }
    public Categorias categoria { get; set; }

    public Pedidos()
    {
        pedidosProdutos = new PedidosProdutos();
        produtos = new Produtos();
        categoria = new Categorias();
    }
} 