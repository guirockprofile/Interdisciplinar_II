using System.Data;
using Microsoft.Data.SqlClient;
public class CarrinhoSql : Database, ICarrinhoData
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

     public List<Pedidos> ReadCarrin(int id){
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "SELECT * FROM VPedidosProdutos WHERE UserId = @id";

        cmd.Parameters.AddWithValue("@id", id);

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
        cmd.CommandText = "SELECT * FROM VPedidosProdutos WHERE UserId = @id";

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

    public void FinalizarPedido(int id)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "EXEC ExcluirPedidoEProdutos @IdPedido = @Id";

        cmd.Parameters.AddWithValue("@Id", id);    
   
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
        cmd.CommandText = "Delete FROM Pedidos WHERE @IdPedido = @id";

        cmd.Parameters.AddWithValue("@id", id);

        cmd.ExecuteNonQuery();
    }

}