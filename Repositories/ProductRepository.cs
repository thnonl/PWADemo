namespace React_Redux.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Microsoft.Azure.Documents;
    using Microsoft.Azure.Documents.Client;
    using Microsoft.Azure.Documents.Linq;
    using MongoDB.Bson;
    using MongoDB.Driver;
    using MongoDB.Driver.Builders;
    using React_Redux.Helper;
    using React_Redux.Models;

    public class ProductRepository<T> : IProductRepository<T>
    {
        MongoClient _client;
        IMongoDatabase _db;

        public ProductRepository()
        {
            _client = new MongoClient($"mongodb://{Constants.Server.Username}:{Constants.Server.Password}@{Constants.Server.Url}");
            _db = _client.GetDatabase("pwasimpleapp");
        }

        public IQueryable<Product> Gets()
        {
            return _db.GetCollection<Product>(nameof(Product)).AsQueryable();
        }
 
        public Product GetProduct(ObjectId id)
        {
            return _db.GetCollection<Product>(nameof(Product)).AsQueryable().FirstOrDefault(p => p.Id == id);
        }
 
        public Product Create(Product p)
        {
            _db.GetCollection<Product>(nameof(Product)).InsertOne(p);
            return p;
        }
 
        public void Update(ObjectId id, Product p)
        {
            p.Id = id;
            var res = Query<Product>.EQ(pd => pd.Id, id).ToBsonDocument();
            _db.GetCollection<Product>(nameof(Product)).ReplaceOne(res, p);
        }
        public void Remove(ObjectId id)
        {
            var res = Query<Product>.EQ(e => e.Id, id).ToBsonDocument();
            var operation = _db.GetCollection<Product>(nameof(Product)).DeleteOne(res);
        }
    }
}