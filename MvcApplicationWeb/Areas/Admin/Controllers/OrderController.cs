using Microsoft.AspNetCore.Mvc;
using Mvc.DataAccess.Respository.IRepository;
using Mvc.Model;

namespace MvcApplicationWeb.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class OrderController : Controller
	{
		private readonly IUnitOfWork _un;
		public OrderController(IUnitOfWork un)
		{
			_un=un;
		}
		public IActionResult Index()
		{
			return View();
		}
		#region API CALLS

		[HttpGet]
		public IActionResult GetAll()
		{
			List<OrderHeader> companies = _un.orderheader.GetAll(includeProperties: "ApplicationUser").ToList();
			return Json(new { data = companies });
		}
		#endregion
	}

}
