﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Thrift_Us.Data;

namespace Thrift_Us.ViewComponents
{
    public class CartViewComponent : ViewComponent
    {
        private readonly ThriftDbContext _context;

        public CartViewComponent(ThriftDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var claim = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier);
            /*
                        if (claim != null)
                        {
                            if (HttpContext.Session.GetInt32("SessionCart") != null)
                            {
                                return View(HttpContext.Session.GetInt32("SessionCart"));
                            }
                            else
                            {
                                var cartCount = _context.Carts.Count(x => x.ApplicationUserId == claim.Value);
                                HttpContext.Session.SetInt32("SessionCart", cartCount);
                                return View( cartCount);


                            }
                        }
                        else
                        {
                            HttpContext.Session.Clear();
                            return View(0);
                        }*/
            int cartCount = 0;
            if(claim!= null)
            {
                cartCount=await _context.Carts.CountAsync(x => x.ApplicationUserId==claim.Value);
            }
            return View(cartCount);

        }
    }
}