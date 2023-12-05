public interface IPedidosData
{
    public List<Pedidos> Read();
    public Pedidos Read(int id);
    public void AddPedido(int id, int user);
    public void AddPedidoIn(int id, int user);
    public void Update(int id, PedidosProdutos pedidosProdutos);
    public void Delete(int id);
}