using Microsoft.Data.SqlClient;

public class CategoriasSql : Database, ICategoriaData
{
    public List<Categorias> Read()
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "SELECT * FROM Categorias";

        SqlDataReader reader = cmd.ExecuteReader();

        List<Categorias> lista = new List<Categorias>();

        while(reader.Read())
        {
           Categorias categorias = new Categorias();
            categorias.IdCategoria = reader.GetInt32(0);
            categorias.NomeCategoria = reader.GetString(1);

            lista.Add(categorias);
        }
        
        return lista;
    }

    public List<Categorias> Read(string search)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "SELECT * FROM Categorias WHERE NomeCategoria LIKE @NomeCategoria";

        // EVITA SQL INJECTION!
        cmd.Parameters.AddWithValue("@NomeCategoria", "%" + search + "%");

        SqlDataReader reader = cmd.ExecuteReader();

         List<Categorias> lista = new List<Categorias>();

        while(reader.Read())
        {
            Categorias categorias = new Categorias();
            categorias.IdCategoria = reader.GetInt32(0);
            categorias.NomeCategoria = reader.GetString(1);
          
            lista.Add(categorias);
        }
        
        return lista;
    }

    public Categorias Read(int id)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "SELECT * FROM Categorias WHERE IdCategoria = @id";

        // EVITA SQL INJECTION!
        cmd.Parameters.AddWithValue("@id", id);

        SqlDataReader reader = cmd.ExecuteReader();

        while(reader.Read())
        {
            Categorias categorias = new Categorias();
            categorias.IdCategoria = reader.GetInt32(0);
            categorias.NomeCategoria = reader.GetString(1);

            return categorias;
        }
        
        return null;
    }
    public void Create(Categorias categorias)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "INSERT INTO Categorias VALUES (@NomeCategoria)";

        cmd.Parameters.AddWithValue("@NomeCategoria", categorias.NomeCategoria);

        cmd.ExecuteNonQuery();
    
    }

    public void Update(int id, Categorias categorias)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "UPDATE Categorias SET NomeCategoria = @NomeCategoria WHERE IdCategoria = @id";

        cmd.Parameters.AddWithValue("@NomeCategoria", categorias.NomeCategoria);
        cmd.Parameters.AddWithValue("@id", id);

        cmd.ExecuteNonQuery();
    }
    public void Delete(int id)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "DELETE FROM Categorias WHERE IdCategoria = @id";

        cmd.Parameters.AddWithValue("@id", id);

        cmd.ExecuteNonQuery();
    }

}