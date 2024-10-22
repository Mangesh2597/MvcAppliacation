using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Mvc.DataAccess.Respository.IRepository;
using Mvc.Model;
using System.Diagnostics;
using System.Security.Claims;
// 
namespace MvcApplicationWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IUnitOfWork _un;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork un)
        {
            _logger = logger;
            _un=un;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> products = _un.product.GetAll(includeProperties: "Category");
            return View(products);
        }
        public IActionResult Detail(int productId)
        {
            ShoppingCart cart = new ShoppingCart()
            {
                Count=1,
                Product= _un.product.Get(u => u.Id==productId, includeProperties: "Category"),
                ProductId=productId
            };

            return View(cart);
        }
        [HttpPost]
        [Authorize]
        public IActionResult Detail(ShoppingCart shoppingCart)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            shoppingCart.ApplicationUserId= userId;

            ShoppingCart getCart = _un.shoppingcart.Get(u => u.ProductId==shoppingCart.ProductId && 
                u.ApplicationUserId==userId);
            if(getCart!=null)
            {
                //shopping cart exists
                getCart.Count+=shoppingCart.Count;
                _un.shoppingcart.Update(getCart);
            }
            else
            {
                //shopping cart not exist
                _un.shoppingcart.Add(shoppingCart);
            }
            TempData["success"]="Cart Udpated successfully";         

            _un.Save();
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
