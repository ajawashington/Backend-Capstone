using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SwapShop.Models;
using Microsoft.AspNetCore.Authorization;
using SwapShop.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace SwapShop.Controllers
{
    // add all the comments girl
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Search(string searchBar)
        {

            if (searchBar != null)
            {
                var barterItems = await _context.BarterItem
                .Where(b =>
                 b.Title.Contains(searchBar) && b.IsAvailable == true ||
                 b.Description.Contains(searchBar) && b.IsAvailable == true ||
                 b.AppUser.Location.Contains(searchBar) && b.IsAvailable == true ||
                 b.AppUser.TagName.Contains(searchBar) && b.IsAvailable == true ||
                 b.BarterType.Title.Contains(searchBar) && b.IsAvailable == true)
                 .Include(b => b.BarterType)
                 .Include(b => b.AppUser)
                 .ToListAsync();

                return View(barterItems);
            }

            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
