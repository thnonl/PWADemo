using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using React_Redux.Models;

namespace React_Redux.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
         private readonly AppDBContext _context;
        public ProductsController (AppDBContext context)
        {
            _context = context;

            if (_context.Products.Count() == 0)
            {
                _context.Products.Add(new Product{
                    Title = "Tested product",
                    Description = "Tested Description",
                    Price = 100,
                    CreatedOn = DateTime.Now
                });
                _context.SaveChanges();
            }
        }

        [HttpPost]
        public IActionResult Create(Product prod)
        {
            prod.CreatedOn = DateTime.Now;
            _context.Products.Add(prod);
            _context.SaveChanges();

            return CreatedAtAction("GetById", new Product{ProductId = prod.ProductId});
        }

        [HttpPut("{id}")]
        public IActionResult Update(int productId, Product prod)
        {
            var product = _context.Products.Find(productId);
            if (product == null)
            {
                return NotFound();
            }

            product.Title = prod.Title;
            product.Description = prod.Description;
            product.Price = prod.Price;
            product.UpdatedOn = DateTime.Now;

            _context.Products.Update(product);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpGet]
        public ActionResult<List<Product>> GetAll()
        {
            return _context.Products.ToList();
        }

        [HttpGet("{productId}")]
        public ActionResult<Product> GetById(int productId)
        {
            var product = _context.Products.Find(productId);
            if (product != null) {
                return product;
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete (int productId)
        {
            var prod = _context.Products.Find(productId);
            if (prod == null)
            {
                return NotFound();
            }
            _context.Products.Remove(prod);
            _context.SaveChanges();
            return NoContent();
        }
    }
}