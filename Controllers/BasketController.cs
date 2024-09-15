﻿using e_commece_API_React.Data;
using e_commece_API_React.DTOs;
using e_commece_API_React.Entities;
using e_commece_API_React.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace e_commece_API_React.Controllers
{
    public class BasketController : BaseApiController
    {
        private readonly StoreContext _context;

        public BasketController(StoreContext context) 
        {
            _context = context;
        }

        [HttpGet(Name ="GetBasket")]
        public async Task<ActionResult<BasketDto>> GetBasket()
        {
            var basket = await RetriveBasket(GetBuyerId());

            if (basket == null) return NotFound();
            return basket.MapBasketToDto();
        }

        

        [HttpPost]
        public async Task<ActionResult<BasketDto>> AddItemToBasket(int productId,  int quantity)
        {
            var basket = await RetriveBasket(GetBuyerId());

            if (basket == null) basket = CreateBasket();

            var product = await _context.Products.FindAsync(productId);

            if (product == null) return BadRequest(new ProblemDetails { Title= "Product Not Found" });

            basket.AddItem(product, quantity);

            var result = await _context.SaveChangesAsync() > 0;
         
            if (result)  return CreatedAtRoute("GetBasket", basket.MapBasketToDto());

            return BadRequest(new ProblemDetails { Title="Problem Saving item to basket"});
        }

        [HttpDelete]
        public async Task<ActionResult> RemoveBasketItem(int productId, int quantity)
        {
           
            var basket = await RetriveBasket(GetBuyerId());

            if (basket == null) return NotFound();

            basket.RemoveItem(productId, quantity);

            var result = await _context.SaveChangesAsync() > 0;

            if (result) return Ok();

            return BadRequest(new ProblemDetails { Title = "Problem Removing Item From Basket" });
            
            
        }

        private async Task<Basket> RetriveBasket(string buyerId)
        {
            if (string.IsNullOrWhiteSpace(buyerId))
            {
                Response.Cookies.Delete("buyerId");
                return null;
            }
            return await _context.Baskets
                .Include(i => i.Items)
                .ThenInclude(p => p.Product)
                .FirstOrDefaultAsync(x => x.BuyerId == buyerId);
        }

        private string GetBuyerId()
        {
            return User.Identity?.Name ?? Request.Cookies["buyerId"];
        }

        private Basket CreateBasket()
        {
            var buyerId = User.Identity?.Name;

            if (string.IsNullOrEmpty(buyerId))
            {
                buyerId = Guid.NewGuid().ToString();
                var cookieOptions = new CookieOptions { IsEssential = true, Expires = DateTime.Now.AddDays(30), SameSite = SameSiteMode.None, Secure=true };
                Response.Cookies.Append("buyerId", buyerId, cookieOptions);
            }
            
            var basket = new Basket { BuyerId = buyerId };
            _context.Baskets.Add(basket);
            return basket;
        }
    }
}
