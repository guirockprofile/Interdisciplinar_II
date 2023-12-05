public interface IEnderecoData
{
    public List<Endereco> Read();
    public List<Endereco> Read(string search);
    public Endereco Read(int id);
    public void Create(int id,Endereco Endereco);
    public void Update(int id, Endereco Endereco);
    public void Delete(int id);
}
   
