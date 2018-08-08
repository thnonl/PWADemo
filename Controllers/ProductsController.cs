using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using React_Redux.Models;
using React_Redux.Repositories;

namespace React_Redux.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly IProductRepository<Product> _respository;
        public ProductsController (IProductRepository<Product> respository)
        {
             this._respository = respository;
        }

        [HttpPost("create")]
        public ActionResult Create(Product prod)
        {
            prod.CreatedOn = DateTime.Now;
            _respository.Create(prod);

            return NoContent();
        }

        [HttpPut("{productId:length(24)}")]
        public ActionResult Update(string productId, Product prod)
        {
            prod.Id = new ObjectId(productId);

            _respository.Update(prod.Id, prod);
            return NoContent();
        }

        [HttpGet]
        public ActionResult<List<Product>> GetAll()
        {
            var products = _respository.Gets();
            return products.ToList();
        }

        [HttpGet("{productId:length(24)}")]
        public ActionResult<Product> GetById(string productId)
        {
            var product = _respository.GetProduct(new ObjectId(productId));
            if (product != null) {
                return product;
            }
            return NotFound();
        }

        [HttpDelete("{productId:length(24)}")]
        public IActionResult Delete (string productId)
        {
            _respository.Remove(new ObjectId(productId));
            return NoContent();
        }
    }
}