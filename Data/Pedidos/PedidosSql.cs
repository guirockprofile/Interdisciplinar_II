using System.Data;
using Microsoft.Data.SqlClient;
public class PedidosSql : Database, IPedidosData
{
    public List<Pedidos> Read(){
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "SELECT * FROM VPedidosProdutos";

        SqlDataReader reader = cmd.ExecuteReader();

        List<Pedidos> lista = new List<Pedidos>();

        while(reader.Read())
        {
            Pedidos pedidos = new Pedidos();
            pedidos.idPedido = reader.GetInt32(0);
            pedidos.pedidosProdutos.IdProduto = reader.GetInt32(1);
            pedidos.UserId = reader.GetInt32(2);            
            pedidos.produtos.NomeProd = reader.GetString(3);
            pedidos.produtos.Descricao = reader.GetString(4);
            pedidos.categoria.NomeCategoria = reader.GetString(5);
            pedidos.pedidosProdutos.Preco = reader.GetDecimal(6);
            pedidos.pedidosProdutos.Qtd = reader.GetInt32(7);
            pedidos.pedidosProdutos.TotalPedido = reader.GetDecimal(8);
            pedidos.DataPedido = reader.GetDateTime(9);            
            pedidos.StatusPedido = reader.GetString(10);
            
            lista.Add(pedidos);
        }
        
        return lista;
    }

    //     public List<Pedidos> Read(string search)
    // {
    //     SqlCommand cmd = new SqlCommand();
    //     cmd.Connection = connection;
    //     cmd.CommandText = "SELECT * FROM VPedidos WHERE NomeProd LIKE @nomeProd";

    //     // EVITA SQL INJECTION!
    //     cmd.Parameters.AddWithValue("@nomeProd", "%" + search + "%");

    //     SqlDataReader reader = cmd.ExecuteReader();

    //      List<Pedidos> lista = new List<Pedidos>();

    //     while(reader.Read())
    //     {
    //         Pedidos pedidos = new Pedidos();
    //         pedidos.IdPedido = reader.GetInt32(0);
    //         pedidos.IdProduto = reader.GetInt32(1);
    //         pedidos.Produto.NomeProd = reader.GetString(2);
    //         pedidos.Produto.Descricao = reader.GetString(3);
    //         pedidos.Categoria.NomeCategoria = reader.GetString(4);
    //         pedidos.Preco = reader.GetDecimal(5);
    //         pedidos.Qtd = reader.GetInt32(6);
    //         pedidos.TotalPedido = reader.GetDecimal(7);
    //         pedidos.Pedido.DataPedido = reader.GetDateTime(8);            
    //         pedidos.Pedido.StatusPedido = reader.GetString(9);

    //         lista.Add(pedidos);
    //     }
        
    //     return lista;
    // }

    public Pedidos Read(int id){
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "SELECT * FROM VPedidosProdutos WHERE IdPedido = @id";

        // EVITA SQL INJECTION!
        cmd.Parameters.AddWithValue("@id", id);

        SqlDataReader reader = cmd.ExecuteReader();
        // List<Pedidos> lista = new List<Pedidos>();
        while(reader.Read())
        {
            Pedidos pedidos = new Pedidos();
            pedidos.idPedido = reader.GetInt32(0);
            pedidos.pedidosProdutos.IdProduto = reader.GetInt32(1);
            pedidos.UserId = reader.GetInt32(2);            
            pedidos.produtos.NomeProd = reader.GetString(3);
            pedidos.produtos.Descricao = reader.GetString(4);
            pedidos.categoria.NomeCategoria = reader.GetString(5);
            pedidos.pedidosProdutos.Preco = reader.GetDecimal(6);
            pedidos.pedidosProdutos.Qtd = reader.GetInt32(7);
            pedidos.pedidosProdutos.TotalPedido = reader.GetDecimal(8);
            pedidos.DataPedido = reader.GetDateTime(9);            
            pedidos.StatusPedido = reader.GetString(10);

            return pedidos;
        }
        
        return null;
    }

    public void AddPedido(int id, int user)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "EXEC AdicionarProdutoAoPedido @IdUsuario = @UserId,  @IdProduto = @IdProduto, @Qtd = @Quantidade;";

        cmd.Parameters.AddWithValue("@UserId", user);    
        cmd.Parameters.AddWithValue("@IdProduto", id);
        // cmd.Parameters.AddWithValue("@Quantidade", pedido.pedidosProdutos.Qtd);    
        
        cmd.ExecuteNonQuery();
    
    }

    public void AddPedidoIn(int id, int user)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "EXEC AdicionarProdutoAoPedido @IdUsuario = @UserId,  @IdProduto = @IdProduto, @Qtd = 1;";

        cmd.Parameters.AddWithValue("@UserId", user);    
        cmd.Parameters.AddWithValue("@IdProduto", id);
        
        cmd.ExecuteNonQuery();
    
    }


    public void Update(int id, PedidosProdutos pedidosProdutos){
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "EXEC AtualizarQtdPedido @IdPedido = @id, @IdProduto = @IdProd, @Quantidade = @Qtd ;";

        cmd.Parameters.AddWithValue("@id",id);
        cmd.Parameters.AddWithValue("@IdProd", pedidosProdutos.IdProduto);   
        cmd.Parameters.AddWithValue("@Qtd", pedidosProdutos.Qtd);      

        cmd.ExecuteNonQuery();
    }
    public void Delete(int id){
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "ExcluirPedidoEProdutos @IdPedido = @id";

        cmd.Parameters.AddWithValue("@id", id);

        cmd.ExecuteNonQuery();
    }

    // public void AddPedido(int id, Pedidos pedidos, Pedidos pedido)
    // {
    //     SqlCommand cmd = new SqlCommand();
    //     cmd.Connection = connection;
    //     cmd.CommandText = "EXEC InserirPedido @StatusPedido;";

    //     cmd.Parameters.AddWithValue("@StatusPedido", pedido.StatusPedido);

    //     cmd.CommandText = "EXEC InserirItemPedido @ProdutoId = @Id, @Qtd = @Quantidade;";

    //     cmd.Parameters.AddWithValue("@Id", id);
    //     cmd.Parameters.AddWithValue("@Quantidade", pedidos.Quantidade);    

    //     cmd.ExecuteNonQuery();
    // }

}