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
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        private ApplicationDbContext _db;
        public CompanyRepository(ApplicationDbContext db):base(db)
        {
            _db=db;
        }
        public void Update(Company obj)
        {
            _db.Companies.Update(obj);
        }
    }
}
