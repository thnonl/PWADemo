using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using React_Redux.Models;
using React_Redux.Repositories;

namespace React_Redux.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
         private readonly IDocumentDBRepository<Product> _respository;
        public ProductsController (IDocumentDBRepository<Product> respository)
        {
             this._respository = respository;
        }

        [HttpPost("create")]
        public async Task<ActionResult> Create(Product prod)
        {
            prod.CreatedOn = DateTime.Now;
            await _respository.CreateItemAsync(prod);

            return NoContent();
        }

        [HttpPut("{productId}")]
        public async Task<ActionResult> Update(string productId, Product prod)
        {
            var product = await _respository.GetItemAsync(productId);
            if (product == null)
            {
                return NotFound();
            }

            product.Title = prod.Title;
            product.Description = prod.Description;
            product.Price = prod.Price;
            product.UpdatedOn = DateTime.Now;

            await _respository.UpdateItemAsync(productId, product);
            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetAll()
        {
            var products = await _respository.GetItemsAsync(p => p.CreatedOn <= DateTime.Now);
            return products.ToList();
        }

        [HttpGet("{productId}")]
        public async Task<ActionResult<Product>> GetById(string productId)
        {
            var product = await _respository.GetItemAsync(productId);
            if (product != null) {
                return product;
            }
            return NotFound();
        }

        [HttpDelete("{productId}")]
        public IActionResult Delete (string productId)
        {
            _respository.DeleteItemAsync(productId);
            return NoContent();
        }
    }
}