using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using MongoDB.Bson;
using React_Redux.Models;

namespace React_Redux.Repositories
{
    public interface IProductRepository<T>
    {
        IQueryable<Product> Gets();
 
        Product GetProduct(ObjectId id);
 
        Product Create(Product p);
 
        void Update(ObjectId id, Product p);
        void Remove(ObjectId id);
    }
}