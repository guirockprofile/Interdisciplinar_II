public interface ICategoriaData
{
    public List<Categorias> Read();
    public List<Categorias> Read(string search);
    public Categorias Read(int id);
    public void Create(Categorias categorias);
    public void Update(int id, Categorias categorias);
    public void Delete(int id);
}
   
