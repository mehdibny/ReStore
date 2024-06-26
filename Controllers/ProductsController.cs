﻿using e_commece_API_React.Data;
using e_commece_API_React.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;

namespace e_commece_API_React.Controllers
{
    public class ProductsController : BaseApiController
    {
        
        private readonly StoreContext _context;

        public ProductsController(StoreContext context)
        {
            _context = context;
            
        }
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts() 
        {
            return await _context.Products.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>>  GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if(product==null) return NotFound();

            return product;
        }

    }
}
