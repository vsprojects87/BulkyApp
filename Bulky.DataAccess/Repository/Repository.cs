using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BulkyApp.DataAccess.Repository.IRepository;
using BulkyApp.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.Formula.Functions;

namespace BulkyApp.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;

        public Repository(ApplicationDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
            _db.Products.Include(u => u.Category).Include(u=>u.CategoryId);
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        public T Get(Expression<Func<T, bool>> filter,string? includeProperties=null)
        {
            IQueryable<T> query = dbSet;
            query = query.Where(filter);

			// 7.0
			if (!string.IsNullOrEmpty(includeProperties))
			{
				foreach (var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
				{
					query = query.Include(property);
				}
			}

			return query.FirstOrDefault();
        }

        public IEnumerable<T> GetAll(string? includeProperties=null)
        {
            IQueryable<T> query = dbSet;
            return query.ToList();
            // 7.0
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach(var property in includeProperties.Split(new char[] {','},StringSplitOptions.RemoveEmptyEntries)) {
                    query = query.Include(property);                    
                }
            }
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
           dbSet.RemoveRange(entity);
        }
    }
}
