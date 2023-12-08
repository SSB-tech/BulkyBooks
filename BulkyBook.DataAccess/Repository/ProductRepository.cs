using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
    public class ProductRepository: Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public void Update(Product model)
        {
            var objFromDb = _db.Product.FirstOrDefault(u => u.Id == model.Id);
            if (objFromDb != null)
            {
                objFromDb.Title = model.Title;
                objFromDb.ISBN = model.ISBN;
                objFromDb.Price = model.Price;
                objFromDb.ListPrice = model.ListPrice;
                objFromDb.Price100 = model.Price100;
                objFromDb.Price50 = model.Price50;
                objFromDb.Author = model.Author;
                objFromDb.Description = model.Description;
                objFromDb.CategoryId = model.CategoryId;
                objFromDb.CoverTypeId = model.CoverTypeId;
                if (model.ImageUrl != null)
                {
                    objFromDb.ImageUrl = model.ImageUrl;
                }
            }
        }
    }
}
