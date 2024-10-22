using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Mvc.DataAccess.Respository.IRepository;
using Mvc.Model;
using Mvc.Model.ViewModels;
using Mvc.Utility;
using System.Numerics;
using System.Security.Claims;

namespace MvcApplicationWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _un;
        [BindProperty]
        public ShoppingCartVM shoppingCartVM { get; set; }
        public CartController(IUnitOfWork un)
        {
            _un=un;
        }
        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            shoppingCartVM= new ShoppingCartVM()
            {
                Listcart = _un.shoppingcart.GetAll(u => u.ApplicationUserId==userId, includeProperties: "Product"),
                orderheader= new OrderHeader()
            };
            foreach (var cart in shoppingCartVM.Listcart) { 
                 cart.Price = getPriceBasedOnQuanity(cart);
                shoppingCartVM.orderheader.OrderTotal+= (cart.Price * cart.Count);
            }
            return View(shoppingCartVM);
        }
		[HttpPost]
		[ActionName("Summary")]
		public IActionResult SummaryPOST()
		{

			var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            shoppingCartVM.Listcart = _un.shoppingcart.GetAll(u => u.ApplicationUserId==userId, includeProperties: "Product");
            shoppingCartVM.orderheader.OrderDate= DateTime.Now;
            shoppingCartVM.orderheader.ApplicationUserId=userId;

     
            ApplicationUser applicationUser= _un.applicationUser.Get(u => u.Id==userId);
           

            foreach(var cart in shoppingCartVM.Listcart)
            {
                cart.Price = getPriceBasedOnQuanity(cart);
                shoppingCartVM.orderheader.OrderTotal+=(cart.Price*cart.Count);
            }
            if(applicationUser.CompanyId.GetValueOrDefault()==0)
            {
                // It is regular customer account and we need to capture payments directly
                shoppingCartVM.orderheader.PaymentStatus=SD.PaymentStatusPending;
                shoppingCartVM.orderheader.OrderStatus=SD.StatusPending;
            }
            else
            {
                //It is comapany account
                shoppingCartVM.orderheader.PaymentStatus=SD.PaymentStatusDelayedPayment;
                shoppingCartVM.orderheader.OrderStatus=SD.StatusApproved;
            }
            _un.orderheader.Add(shoppingCartVM.orderheader);
            _un.Save();
            foreach(var cart in shoppingCartVM.Listcart)
            {
                OrderDetail detail = new OrderDetail()
                {
                    ProductId= cart.ProductId,
                    OrderHeaderId=shoppingCartVM.orderheader.Id,
                    Price= cart.Price,
                    Count= cart.Count
                };
                _un.orderdetail.Add(detail);
                _un.Save();
            }
            if(applicationUser.CompanyId.GetValueOrDefault() == 0)
            {
             //regular customer account
             //stripe logic
            }

            return RedirectToAction(nameof(OrderConfirmation), new { id = shoppingCartVM.orderheader.Id });
        }
        public IActionResult OrderConfirmation(int id)
        {
            return View(id);
        }

      
	public IActionResult Summary()
	{
		var claimsIdentity = (ClaimsIdentity)User.Identity;
			var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
			shoppingCartVM= new ShoppingCartVM()
			{
				Listcart = _un.shoppingcart.GetAll(u => u.ApplicationUserId==userId, includeProperties: "Product"),
				orderheader=new OrderHeader()
			};
			shoppingCartVM.orderheader.ApplicationUser= _un.applicationUser.Get(u => u.Id==userId);
			shoppingCartVM.orderheader.Name= shoppingCartVM.orderheader.ApplicationUser.Name;
			shoppingCartVM.orderheader.PhoneNumber=shoppingCartVM.orderheader.ApplicationUser.PhoneNumber;
			shoppingCartVM.orderheader.StreetAddress =shoppingCartVM.orderheader.ApplicationUser.CurrentAddress;
			shoppingCartVM.orderheader.City=shoppingCartVM.orderheader.ApplicationUser.City;
			shoppingCartVM.orderheader.State=shoppingCartVM.orderheader.ApplicationUser.State;
			shoppingCartVM.orderheader.PostalCode =shoppingCartVM.orderheader.ApplicationUser.PostalCode;

			foreach (var cart in shoppingCartVM.Listcart)
			{
				cart.Price = getPriceBasedOnQuanity(cart);
				shoppingCartVM.orderheader.OrderTotal+=(cart.Price*cart.Count);
			}
			return View(shoppingCartVM);
		}
		public IActionResult Plus(int CartId)
        {
            ShoppingCart cartFromDb = _un.shoppingcart.Get(u => u.Id==CartId);
            cartFromDb.Count+=1;
            _un.shoppingcart.Update(cartFromDb);
            _un.Save();
            return RedirectToAction("Index");   
        }
        public IActionResult Minus(int CartId)
        {
            ShoppingCart cartFromDb = _un.shoppingcart.Get(U => U.Id==CartId);
            if(cartFromDb.Count<=1)
            {
                //remove cart from db
                _un.shoppingcart.Remove(cartFromDb);
            }
            else
            {
                cartFromDb.Count-=1;
                _un.shoppingcart.Update(cartFromDb);
            }
            _un.Save();

            return RedirectToAction("Index");
        }
        public IActionResult Remove(int CartId)
        {
            ShoppingCart cartFromDb = _un.shoppingcart.Get(u => u.Id==CartId);
            _un.shoppingcart.Remove(cartFromDb);
            _un.Save();
            return RedirectToAction("Index");
        }

        private double getPriceBasedOnQuanity(ShoppingCart shoppingCart)
        {
            if(shoppingCart.Count<=50)
            {
                return shoppingCart.Product.Price;
            }
            else
            {
                if(shoppingCart.Count<=100)
                {
                    return shoppingCart.Product.Price50;
                }
                else
                {
                    return shoppingCart.Product.Price100;
                }
            }
        }
    }
}
