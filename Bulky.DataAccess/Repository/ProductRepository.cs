using BulkyApp.DataAccess.Repository.IRepository;
using BulkyApp.Models;
using BulkyApp.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Bulky.Models;

namespace BulkyApp.DataAccess.Repository
{
    public class ProductRepository : Repository<Product> ,IProductRepository
    {

        private ApplicationDbContext _db;

        // we are getting dbContext and passing it to base class
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public void Update(Product obj)
        {
            _db.Products.Update(obj);
        }
    }
}
