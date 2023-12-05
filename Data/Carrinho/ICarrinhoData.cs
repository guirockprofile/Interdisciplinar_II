public interface ICarrinhoData 
{
    public List<Pedidos> Read();

    public List<Pedidos> ReadCarrin(int id);
    public Pedidos Read(int id);
    public void FinalizarPedido(int id);
    public void Update(int id, PedidosProdutos pedidosProdutos);
    public void Delete(int id);
}
