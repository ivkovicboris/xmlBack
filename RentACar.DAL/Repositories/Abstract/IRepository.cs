using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.DAL.Repositories.Abstract
{
    public interface IRepository<E> where E : class

    {
        IEnumerable<E> GetAll();
        Task<bool> Create(E entity);
        Task<bool> Update(E entity);
        Task<bool> Delete(E entity);
        Task<IEnumerable<E>> Find(Expression<Func<E, bool>> predicate);
    }
}
