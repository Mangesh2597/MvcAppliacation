﻿using Mvc.DataAccess.Data;
using Mvc.DataAccess.Respository.IRepository;
using Mvc.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvc.DataAccess.Respository
{
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        private readonly ApplicationDbContext _db;
        public ApplicationUserRepository(ApplicationDbContext db):base(db) 
        {
            _db = db;
        }

        public void Update(ApplicationUser obj)
        {
            _db.ApplicationUsers.Update(obj);
        }
    }
}
