using Microsoft.Data.SqlClient;

public class ClientesSql : Database, IClientesData
{
    public List<Clientes> Read()
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "SELECT * FROM Clientes";

        SqlDataReader reader = cmd.ExecuteReader();

        List<Clientes> lista = new List<Clientes>();

        while(reader.Read())
        {
           Clientes clientes = new Clientes();
            clientes.IdCliente = reader.GetInt32(0);
            clientes.Nome = reader.GetString(1);
            clientes.Sobrenome = reader.GetString(2);
            clientes.CPF = reader.GetString(3);
            clientes.Telefone = reader.GetString(4);    

            lista.Add(clientes);
        }
        
        return lista;
    }

    public List<Clientes> Read(string search)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "SELECT * FROM Clientes WHERE Nome LIKE @Nome";

        // EVITA SQL INJECTION!
        cmd.Parameters.AddWithValue("@Nome", "%" + search + "%");

        SqlDataReader reader = cmd.ExecuteReader();

         List<Clientes> lista = new List<Clientes>();

        while(reader.Read())
        {
            Clientes clientes = new Clientes();
            clientes.IdCliente = reader.GetInt32(0);
            clientes.Nome = reader.GetString(1);
            clientes.Sobrenome = reader.GetString(2);
            clientes.CPF = reader.GetString(3);
            clientes.Telefone = reader.GetString(4);   
          
            lista.Add(clientes);
        }
        
        return lista;
    }

    public Clientes Read(int id)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "SELECT * FROM Clientes WHERE IdCliente = @id";

        // EVITA SQL INJECTION!
        cmd.Parameters.AddWithValue("@id", id);

        SqlDataReader reader = cmd.ExecuteReader();

        while(reader.Read())
        {
            Clientes clientes = new Clientes();
            clientes.IdCliente = reader.GetInt32(0);
            clientes.Nome = reader.GetString(1);
            clientes.Sobrenome = reader.GetString(2);
            clientes.CPF = reader.GetString(3);
            clientes.Telefone = reader.GetString(4);     

            return clientes;
        }
        
        return null;
    }
    public void Create(Clientes clientes)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "INSERT INTO Clientes VALUES (@Nome, @Sobrenome, @CPF, @Telefone)";

        cmd.Parameters.AddWithValue("@Nome", clientes.Nome);
        cmd.Parameters.AddWithValue("@Sobrenome", clientes.Sobrenome);
        cmd.Parameters.AddWithValue("@CPF", clientes.CPF);
        cmd.Parameters.AddWithValue("@Telefone", clientes.Telefone);

        cmd.ExecuteNonQuery();
    
    }

    public void Update(int id, Clientes clientes)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "UPDATE Clientes SET Nome = @Nome, Sobrenome = @Sobrenome, CPF = @CPF, Telefone = @Telefone WHERE IdCliente = @id";

        cmd.Parameters.AddWithValue("@Nome", clientes.Nome);
        cmd.Parameters.AddWithValue("@Sobrenome", clientes.Sobrenome);
        cmd.Parameters.AddWithValue("@CPF", clientes.CPF);
        cmd.Parameters.AddWithValue("@Telefone", clientes.Telefone);
        cmd.Parameters.AddWithValue("@id", id);

        cmd.ExecuteNonQuery();
    }
    public void Delete(int id)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "DELETE FROM Clientes WHERE IdCliente = @id";

        cmd.Parameters.AddWithValue("@id", id);

        cmd.ExecuteNonQuery();
    }

}