using BulkyBook.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(db);
            CoverType = new CoverTypeRepository(db);
            Product = new ProductRepository(db);
        }
        public ICategoryRepository Category { get; set; }
        public ICoverTypeRepository CoverType { get; set; }
        public IProductRepository Product { get; set; }
       public void Save()
        {
            _db.SaveChanges();
        }
    }
}
