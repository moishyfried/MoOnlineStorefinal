using MoOnlineStore.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Text;
using MoOnlineStore.Core.DTO;
using MoOnlineStore.Core.EntityClasses;

namespace MoOnlineStore.Infrastructure.DataBase
{
     public class SpecificationEvaluator<TEntity>  where TEntity:BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> InputQuery,ISpecification<TEntity> spec)
        {
            var query = InputQuery;

            if (spec.Criteria != null)
            {
                query = query.Where(spec.Criteria);
            }
            if (spec.OrderBy != null)
            {
             query =   query.OrderBy(spec.OrderBy);
            }
            if (spec.OrderByDescending != null)
            {
                query = query.OrderByDescending(spec.OrderByDescending);
            }
            if(spec.IsPagingEnabled == true)
            {
                query = query.Skip(spec.Skip).Take(spec.Take);
            }
            query = spec.Includes.Aggregate(query,(current,include) => current.Include(include));

            return query;
        }
    }
} 

         
     
