using Mvc.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvc.DataAccess.Respository.IRepository
{
    public interface IShoppingCartRepository:IRepository<ShoppingCart>
    {
        void Update(ShoppingCart shop);
    }
}
