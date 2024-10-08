﻿using Mvc.DataAccess.Data;
using Mvc.DataAccess.Respository.IRepository;
using Mvc.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvc.DataAccess.Respository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db):base(db)
        {
            _db=db;
        }
        public void Update(Product obj)
        {
            _db.Products.Update(obj);
            var objFromDb = _db.Products.FirstOrDefault(u=>u.Id==obj.Id);
            if(objFromDb!=null)
            {
                objFromDb.Title= obj.Title;
                objFromDb.ISBN=obj.ISBN;
                objFromDb.Price=obj.Price;
                objFromDb.LastPrice=obj.LastPrice;
                objFromDb.Price50=obj.Price;
                objFromDb.Price100=obj.Price100;
                objFromDb.Description=obj.Description;
                objFromDb.CategoryId=obj.CategoryId;
                objFromDb.Author=obj.Author;
                if(obj.imageUrl!=null)
                {
                    objFromDb.imageUrl=obj.imageUrl;
                }

            }
        }
    }
}
