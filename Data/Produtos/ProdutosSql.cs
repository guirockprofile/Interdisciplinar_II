using Microsoft.Data.SqlClient;

public class ProdutosSql : Database, IProdutosData
{
    public List<Produtos> Read()
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "SELECT * FROM VProdutos";

        SqlDataReader reader = cmd.ExecuteReader();

        List<Produtos> lista = new List<Produtos>();

        while(reader.Read())
        {
           Produtos produto = new Produtos();
            produto.IdProduto = reader.GetInt32(0);
            produto.NomeProd = reader.GetString(1);
            produto.Descricao = reader.GetString(2);
            produto.Preco = reader.GetDecimal(3);
            produto.Quantidade = reader.GetInt32(4);
            produto.Status = reader.GetString(5);
            produto.Categorias.NomeCategoria = reader.GetString(6);
            produto.Imagem = reader.GetString(7);

            lista.Add(produto);
        }
        
        return lista;
    }

    public List<Produtos> Read(string search)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "SELECT * FROM VProdutos WHERE NomeProd LIKE @nomeProd";

        // EVITA SQL INJECTION!
        cmd.Parameters.AddWithValue("@nomeProd", "%" + search + "%");

        SqlDataReader reader = cmd.ExecuteReader();

         List<Produtos> lista = new List<Produtos>();

        while(reader.Read())
        {
            Produtos produto = new Produtos();
            produto.IdProduto = reader.GetInt32(0);
            produto.NomeProd = reader.GetString(1);
            produto.Descricao = reader.GetString(2);
            produto.Preco = reader.GetDecimal(3);
            produto.Quantidade = reader.GetInt32(4);
            produto.Status = reader.GetString(5);
            produto.Categorias.NomeCategoria = reader.GetString(6); 
            produto.Imagem = reader.GetString(7); 
          
            lista.Add(produto);
        }
        
        return lista;
    }

    public Produtos Read(int id)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "SELECT * FROM VProdutos WHERE IdProduto = @id";

        // EVITA SQL INJECTION!
        cmd.Parameters.AddWithValue("@id", id);

        SqlDataReader reader = cmd.ExecuteReader();

        while(reader.Read())
        {
            Produtos produto = new Produtos();
            produto.IdProduto = reader.GetInt32(0);
            produto.NomeProd = reader.GetString(1);
            produto.Descricao = reader.GetString(2);
            produto.Preco = reader.GetDecimal(3);
            produto.Quantidade = reader.GetInt32(4);
            produto.Status = reader.GetString(5);
            produto.Categorias.NomeCategoria = reader.GetString(6);  
            produto.Imagem = reader.GetString(7);

            return produto;
        }
        
        return null;
    }
    public void Create(Produtos produto)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "INSERT INTO Produtos VALUES (@NomeProd, @descricao, @valor, @Qtd, @status, @IdCategoria, @imagem)";

        cmd.Parameters.AddWithValue("@NomeProd", produto.NomeProd);
        cmd.Parameters.AddWithValue("@descricao", produto.Descricao);
        cmd.Parameters.AddWithValue("@valor", produto.Preco);
        cmd.Parameters.AddWithValue("@Qtd", produto.Quantidade);
        cmd.Parameters.AddWithValue("@status", produto.Status);       
        cmd.Parameters.AddWithValue("@IdCategoria", produto.CategoriaId);
        cmd.Parameters.AddWithValue("@Imagem", produto.Imagem);

        cmd.ExecuteNonQuery();
    
    }

    public void Update(int id, Produtos produto)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "UPDATE Produtos SET NomeProd = @NomeProd, Descricao = @descricao, Preco = @valor, Quantidade = @Qtd, status = @status, CategoriaId = @IdCategoria WHERE IdProduto = @id";

        cmd.Parameters.AddWithValue("@NomeProd", produto.NomeProd);
        cmd.Parameters.AddWithValue("@descricao", produto.Descricao);
        cmd.Parameters.AddWithValue("@valor", produto.Preco);
        cmd.Parameters.AddWithValue("@Qtd", produto.Quantidade);
        cmd.Parameters.AddWithValue("@status", produto.Status);
        cmd.Parameters.AddWithValue("@IdCategoria", produto.CategoriaId);
        cmd.Parameters.AddWithValue("@id", id);

        cmd.ExecuteNonQuery();
    }
    public void Delete(int id)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "DELETE FROM Produtos WHERE IdProduto = @id";

        cmd.Parameters.AddWithValue("@id", id);

        cmd.ExecuteNonQuery();
    }


}