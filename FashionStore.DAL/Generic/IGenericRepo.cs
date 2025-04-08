namespace FashionStore.DAL.Generic;

public interface IGenericRepo<T> where T : class
{
    Task<List<T>> GetAllAsync();
    Task<T?> GetByIdAsync(Guid id);
    void Add(T entity);
    void Update(T entity);
    public void Delete(T entity);
}
