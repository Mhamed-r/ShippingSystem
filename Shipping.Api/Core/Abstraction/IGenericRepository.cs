namespace Shipping.Api.Core.Abstraction;

public interface IGenericRepository<T, Tkey> where T : class where Tkey : IEquatable<Tkey>
{
    // This Is A Generic Repository Interface That Contains The Basic CRUD Operations 
    // That Can Be Performed On Any Entity Without ---- User Entity ----     
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(Tkey id);

    //Task <ApplicationUser?> GetUserByIdAsync(string id);
    Task AddAsync(T entity);
    void UpdateAsync(T entity);
    void DeleteAsync(T entity);
}