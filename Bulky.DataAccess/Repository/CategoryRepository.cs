using BulkyApp.DataAccess.Repository.IRepository;
using BulkyApp.Models;
using BulkyApp.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BulkyApp.DataAccess.Repository
{
    public class CategoryRepository : Repository<Category> ,ICategoryRepository
    {

        private ApplicationDbContext _db;

        // we are getting dbContext and passing it to base class
        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public void Update(Category obj)
        {
            _db.Categories.Update(obj);
        }
    }
}
