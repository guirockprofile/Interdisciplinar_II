using Microsoft.Data.SqlClient;

public class EnderecoSql : Database, IEnderecoData
{
    public List<Endereco> Read()
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "SELECT * FROM VEndereco";

        SqlDataReader reader = cmd.ExecuteReader();

        List<Endereco> lista = new List<Endereco>();

        while(reader.Read())
        {
           Endereco Endereco = new Endereco();
            Endereco.Rua = reader.GetString(0);
            Endereco.Numero = reader.GetInt32(1);
            Endereco.Bairro = reader.GetString(2);
            Endereco.Cidade = reader.GetString(3);
            Endereco.Estado = reader.GetString(4);
            Endereco.CEP = reader.GetString(5);
            Endereco.UserId = reader.GetInt32(6);    
            
            
            lista.Add(Endereco);
        }
        
        return lista;
    }

    public List<Endereco> Read(string search)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "SELECT * FROM VEndereco WHERE UserId LIKE @UserId";

        // EVITA SQL INJECTION!
        cmd.Parameters.AddWithValue("@UserId", "%" + search + "%");

        SqlDataReader reader = cmd.ExecuteReader();

         List<Endereco> lista = new List<Endereco>();

        while(reader.Read())
        {
            Endereco Endereco = new Endereco();
            // Endereco.IdEndereco = reader.GetInt32(0);
            Endereco.Rua = reader.GetString(0);
            Endereco.Numero = reader.GetInt32(1);
            Endereco.Bairro = reader.GetString(2);
            Endereco.Cidade = reader.GetString(3);
            Endereco.Estado = reader.GetString(4);
            Endereco.CEP = reader.GetString(5);
            Endereco.UserId = reader.GetInt32(6);    
          
            lista.Add(Endereco);
        }
        
        return lista;
    }

    public Endereco Read(int id)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "SELECT * FROM VEndereco WHERE UserId = @id";

        // EVITA SQL INJECTION!
        cmd.Parameters.AddWithValue("@id", id);

        SqlDataReader reader = cmd.ExecuteReader();

        while(reader.Read())
        {
            Endereco Endereco = new Endereco();
            Endereco.Rua = reader.GetString(0);
            Endereco.Numero = reader.GetInt32(1);
            Endereco.Bairro = reader.GetString(2);
            Endereco.Cidade = reader.GetString(3);
            Endereco.Estado = reader.GetString(4);
            Endereco.CEP = reader.GetString(5);
            Endereco.UserId = reader.GetInt32(6);    
              

        }
        
        return null;
    }
    public void Create(int id,Endereco Endereco)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "Exec CadastrarEndereco @Rua = @Ruaa, @Numero = @Numeroo, @Bairro = @Bairroo, @Cidade = @Cidadee, @Estado = @Estadoo, @CEP = @CEPP, @UserId = @Id";

        cmd.Parameters.AddWithValue("@Ruaa", Endereco.Rua);
        cmd.Parameters.AddWithValue("@Numeroo", Endereco.Numero);
        cmd.Parameters.AddWithValue("@Bairroo", Endereco.Bairro);
        cmd.Parameters.AddWithValue("@Cidadee", Endereco.Cidade);
        cmd.Parameters.AddWithValue("@Estadoo", Endereco.Estado);
        cmd.Parameters.AddWithValue("@CEPP", Endereco.CEP);
        cmd.Parameters.AddWithValue("@Id", id);

        cmd.ExecuteNonQuery();
    
    }

public void Update(int id, Endereco Endereco)
{
    SqlCommand cmd = new SqlCommand();
    cmd.Connection = connection;
    cmd.CommandText = "UPDATE Endereco SET Rua = @Rua, Numero = @Numero, Bairro = @Bairro, Cidade = @Cidade, Estado = @Estado, CEP = @CEP WHERE UserId = @UserId";

    cmd.Parameters.AddWithValue("@Rua", Endereco.Rua);
    cmd.Parameters.AddWithValue("@Numero", Endereco.Numero);
    cmd.Parameters.AddWithValue("@Bairro", Endereco.Bairro);
    cmd.Parameters.AddWithValue("@Cidade", Endereco.Cidade);
    cmd.Parameters.AddWithValue("@Estado", Endereco.Estado);
    cmd.Parameters.AddWithValue("@CEP", Endereco.CEP);
    cmd.Parameters.AddWithValue("@UserId", id);

    cmd.ExecuteNonQuery();
}

    public void Delete(int id)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "DELETE FROM Endereco WHERE IdEndereco = @id";

        cmd.Parameters.AddWithValue("@id", id);

        cmd.ExecuteNonQuery();
    }

}