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
	public class OrderHeaderRepository:Repository<OrderHeader> , IOrderHeaderRepository
	{
		private readonly ApplicationDbContext _db;
		public OrderHeaderRepository(ApplicationDbContext db):base(db)
		{
			_db=db;
		}
		public void Update(OrderHeader obj)
		{
			_db.OrderHeaders.Update(obj);
		}
	}
}
