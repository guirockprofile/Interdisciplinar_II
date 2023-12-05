using Microsoft.Data.SqlClient;
public class UsersSql:Database,IUsersRepository
{
    public Users? Login(string name, string senha){
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "Select * from Users where name = @name And senha = @senha";

        cmd.Parameters.AddWithValue("@name", name);
        cmd.Parameters.AddWithValue("@senha", senha);

        SqlDataReader reader = cmd.ExecuteReader();
        while(reader.Read())
        {
            Users user = new Users();    
            user.IdUser = reader.GetInt32(0);
            user.Name = reader.GetString(1);
            user.Senha = reader.GetString(2);
            return user;
        }
        return null;
    }
 
    public void Create(Users users, Clientes clientes)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "EXEC CadastrarUsuarioCliente  @UserName = @Name,  @Senha = @senha, @NomeCliente = @nome, @SobrenomeCliente = @SobNome, @CPFCliente =@cpf, @TelefoneCliente = @Telefone";

        cmd.Parameters.AddWithValue("@Name", users.Name);
        cmd.Parameters.AddWithValue("@senha", users.Senha);
        cmd.Parameters.AddWithValue("@nome", clientes.Nome);
        cmd.Parameters.AddWithValue("@SobNome", clientes.Sobrenome);
        cmd.Parameters.AddWithValue("@cpf", clientes.CPF);
        cmd.Parameters.AddWithValue("@Telefone", clientes.Telefone);

        cmd.ExecuteNonQuery();
    
    }

}