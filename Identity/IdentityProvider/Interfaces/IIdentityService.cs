using IdentityProvider.Models;
using System.Linq.Expressions;

namespace IdentityProvider.Interfaces
{
    public interface IIdentityService
    {
        Task<AppUser> GetByIdAsynsc(int id);
        Task<IEnumerable<AppUser>> GetAll();
        Task<AppUser> AddASync(AppUser entity);
        void Delete(AppUser entity);
        void Update(AppUser entity);
        IQueryable<AppUser> Where(Expression<Func<AppUser, bool>> predicate);
    }
}
