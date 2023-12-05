using Microsoft.Data.SqlClient;

public abstract class Database : IDisposable
{
    protected SqlConnection connection;
    public Database()
    {
        connection = new SqlConnection("Data Source=localhost; Initial Catalog=VendasGeek; Integrated Security=True; TrustServerCertificate=True; Multiple Active Result Sets=True;");
        connection.Open();
    }

    public void Dispose()
    {
        connection.Close();
    }
}