using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductAPICapstone.Context;
using ProductAPICapstone.Data;

namespace ProductAPICapstone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductAPIContext _context;

        public ProductsController(ProductAPIContext context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        //GET: api/Products/Category/2
        [HttpGet("Category/{id}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductByCategoryId(int id)
        {
            var products =  await _context.Products.Where(c => c.CategoryId == id).ToListAsync();

            bool isEmpty = !products.Any();

            if(isEmpty)
            {
                return NotFound();
            }

            return products;
        }

        //GET: api/Products/Supplier/4
        [HttpGet("Supplier/{id}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductBySupplierId(int id)
        {
            var products = await _context.Products.Where(c => c.SupplierId == id).ToListAsync();

            bool isEmpty = !products.Any();

            if (isEmpty)
            {
                return NotFound();
            }

            return products;
        }

        //GET: api/Products/Price/24.50
        [HttpGet("Price/{maxPrice}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductByMaxPrice(decimal maxPrice )
        {
            var products = await _context.Products.Where(c => c.UnitPrice <=  maxPrice).ToListAsync();

            bool isEmpty = !products.Any();

            if (isEmpty)
            {
                return NotFound();
            }

            return products;
        }

        //GET: api/Products/Discontinue/true
        [HttpGet("Discontinue/{discontinueFlag}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductByDiscontinueStatus(bool discontinueFlag)
        {
            var products = await _context.Products.Where(c => c.Discontinued == discontinueFlag).ToListAsync();

            bool isEmpty = !products.Any();

            if (isEmpty)
            {
                return NotFound();
            }

            return products;
        }


        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}
