using Mvc.DataAccess.Data;
using Mvc.DataAccess.Respository.IRepository;
using Mvc.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvc.DataAccess.Respository
{
	public class OrderDetailRepository:Repository<OrderDetail> , IOrderDetailRepository 
	{
		private readonly ApplicationDbContext _db;
		public OrderDetailRepository( ApplicationDbContext db):base(db)
		{
			_db = db;	
		}
		public void Update(OrderDetail obj)
		{
			_db.OrderDetails.Update(obj);
		}
	}
}
