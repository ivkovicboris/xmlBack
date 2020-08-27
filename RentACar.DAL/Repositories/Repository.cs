using Microsoft.EntityFrameworkCore;
using RentACar.DAL.Context;
using RentACar.DAL.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.DAL.Repositories
{
    public class Repository<E> : IRepository<E> where E : class
    {

        private readonly DbSet<E> _dbSet;
        private readonly RentContext _rentContext;

        public Repository(RentContext rentContext)
        {
            _rentContext = rentContext;
            _dbSet = rentContext.Set<E>();

        }

        public async Task<bool> Create(E entity)
        {
            try
            {
                await _dbSet.AddAsync(entity);
                await _rentContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return false;
                throw e;
            }

            return true;
        }

        public async Task<bool> Delete(E entity)
        {
            try
            {
                _dbSet.Remove(entity);
                await _rentContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return false;
                throw e;
            }

            return true;
        }

        public IEnumerable<E> Find(Func<E, bool> predicate)
        {
            return _dbSet.Where(predicate);

        }

        public IEnumerable<E> GetAll()
        {
            return _dbSet;
        }

        public async Task<bool> Update(E entity)
        {
            try
            {
                //await _centerContext.SaveChangesAsync();
                await _rentContext.SaveChangesAsync();

                return true;
            }
            catch (Exception e)
            {
                return false;
                throw e;
            }



        }


    }
}
