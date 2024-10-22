using Microsoft.Identity.Client;
using Mvc.DataAccess.Data;
using Mvc.DataAccess.Respository.IRepository;
using Mvc.Model;
using NuGet.Protocol.Core.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Mvc.DataAccess.Respository
{
    public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository
    {
        private readonly ApplicationDbContext _db;
        public ShoppingCartRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }
        
        public void Update(ShoppingCart shop)
        {
            _db.ShoppingCarts.Update(shop); 
        }
    }
}
