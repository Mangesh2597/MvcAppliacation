using Microsoft.AspNetCore.Mvc;
using Mvc.DataAccess.Respository.IRepository;
using Mvc.Model;

namespace MvcApplicationWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CompanyController : Controller
    {
        private IUnitOfWork _un;

        public CompanyController(IUnitOfWork un)
        {
            _un = un;
        }
        public IActionResult Index()
        {
            List<Company> compaies = _un.company.GetAll(includeProperties:null).ToList();
            return View(compaies);  
        }
        
        public IActionResult Upsert(int? id)
        {
            Company company = new Company();

            if (id==null || id==0)
            {
                return View(company);
            }
            else
            {
                company= _un.company.Get(u => u.Id==id, includeProperties: null);
                return View(company);
            }

        }
        [HttpPost]
        public IActionResult Upsert(Company company)
        {
            if (ModelState.IsValid)
            {



                if (company.Id==0)
                {
                    _un.company.Add(company);

                }
                else
                {
                    _un.company.Update(company);
                }
                _un.Save();
                TempData["Success"]="Company Created SuccessFully";
                return RedirectToAction("Index");
            }
            else
            {

                ;
                return View(company);

            }
        }









        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Company> companies = _un.company.GetAll(includeProperties: null).ToList();
            return Json(new { data = companies });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var selectedCompany = _un.company.Get(u => u.Id==id, includeProperties: null);
            if (selectedCompany==null)
            {
                return Json(new { success = false, message = "error while deleting" });
            }

            _un.company.Remove(selectedCompany);
            _un.Save();
            return Json(new { sucess = true, message = "deleted successfully" });
        }
        #endregion
    }
}
