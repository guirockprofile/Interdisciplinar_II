public interface IUsersRepository
{
    public Users? Login(string name, string senha);
    public void Create(Users users, Clientes clientes);
}