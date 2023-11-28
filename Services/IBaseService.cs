namespace UsersBlogs.Services
{
    public interface IBaseService<T> where T : class
    {
        List<T> GetAll();
        T GetById(int id);
        int Create(T data);
        int Update(int id, T data);
        int Delete(int id);
    }
}
