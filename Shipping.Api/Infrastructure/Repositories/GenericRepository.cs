using Microsoft.EntityFrameworkCore;
using Shipping.Api.Core.Abstraction;
using Shipping.Api.Infrastructure.Data;
namespace Shipping.Api.Infrastructure.Repositories;

    public class GenericRepository<T, Tkey>:IGenericRepository<T,Tkey> where T : class where Tkey : IEquatable<Tkey>
    {


        private readonly ShippingContext _context;

        public GenericRepository(ShippingContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }
        public async Task<T?> GetByIdAsync(Tkey id)
        {
            return await _context.Set<T>().FindAsync(id);
        }
        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }
        public void UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
        }
        public void DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        // This Is A Generic Repository  That Contains The Basic CRUD Operations 
        // That Can Be Performed On Any Entity Without ---- User Entity ----  


        //public async Task<ApplicationUser?> GetUserByIdAsync(string id)
        //{
        //    return await _context.Set<ApplicationUser>().FirstOrDefaultAsync(user => user.Id == id);
        //}
    }

