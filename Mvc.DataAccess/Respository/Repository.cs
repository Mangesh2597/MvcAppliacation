﻿using Microsoft.EntityFrameworkCore;
using Mvc.DataAccess.Data;
using Mvc.DataAccess.Respository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Mvc.DataAccess.Respository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        private DbSet<T> dbSet;
        public Repository(ApplicationDbContext db) 
        {
            _db = db;
            this.dbSet= _db.Set<T>();
            _db.Products.Include(u=>u.Category).Include(u=>u.CategoryId);
        }

        public void Add(T entity)
        {
           dbSet.Add(entity);
        }

        public T Get(Expression<Func<T, bool>> filter,string? includeProperties=null, bool tracked =false)
        {
            IQueryable<T> query;
            if (tracked)
            {
                 query = dbSet;
             
            }
            else
            {
                 query = dbSet.AsNoTracking();
               
            }
            query = query.Where(filter);
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query=query.Include(includeProp);
                }
            }
            return query.FirstOrDefault();

        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter=null,string? includeProperties=null)
        {
            IQueryable<T> query = dbSet;
            if(filter!=null)
            {
                query = query.Where(filter);

            }

            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query=query.Include(includeProp);
                }

            }
            return query.ToList();
        }

        public void Remove(T entity)
        {
           dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
            dbSet.RemoveRange(entity);
        }
    }
}
