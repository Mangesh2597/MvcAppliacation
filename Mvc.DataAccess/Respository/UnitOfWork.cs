using Mvc.DataAccess.Data;
using Mvc.DataAccess.Respository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvc.DataAccess.Respository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        public ICategoryRepository category { get; private set; }
        public IProductRepository product { get; private set; }
        public ICompanyRepository company { get; private set; }
        public IShoppingCartRepository shoppingcart { get; private set;}
        public IApplicationUserRepository applicationUser { get; private set; } 
        public IOrderHeaderRepository orderheader { get; private set; }
        public IOrderDetailRepository orderdetail { get; private set; }
        public UnitOfWork(ApplicationDbContext db)
        {
            _db =db;
            category = new CategoryRepository(_db);
            product = new ProductRepository(_db);
            company = new CompanyRepository(_db);
            applicationUser= new ApplicationUserRepository(_db);
            shoppingcart = new ShoppingCartRepository(_db);
            orderheader= new OrderHeaderRepository(_db);
            orderdetail = new OrderDetailRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
