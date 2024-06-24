namespace Caber.Repositories.Interfaces
{
    public interface IRepository<K, T> where T : class
    {
        Task<T> GetByKey(K key);

        Task<IEnumerable<T>> GetAll();

        Task<T> Add(T entity);

        Task<T> Update(T entity);

        Task<T> Delete(K key);
    }
}
