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

        [HttpPut("{productId}")]
        public ActionResult Update(ObjectId productId, Product prod)
        {
            prod.Id = productId;

            _respository.Update(productId, prod);
            return NoContent();
        }

        [HttpGet]
        public ActionResult<List<Product>> GetAll()
        {
            var products = _respository.Gets();
            return products.ToList();
        }

        [HttpGet("{productId}")]
        public ActionResult<Product> GetById(ObjectId productId)
        {
            var product = _respository.GetProduct(productId);
            if (product != null) {
                return product;
            }
            return NotFound();
        }

        [HttpDelete("{productId}")]
        public IActionResult Delete (ObjectId productId)
        {
            _respository.Remove(productId);
            return NoContent();
        }
    }
}