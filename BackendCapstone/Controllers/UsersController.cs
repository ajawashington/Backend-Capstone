using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BackendCapstone.Data;
using BackendCapstone.Models;
using BackendCapstone.Models.ViewModels.Profile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.IO;

namespace BackendCapstone.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {

        private readonly ApplicationDbContext _context;

        private readonly UserManager<ApplicationUser> _userManager;

        public UsersController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {

            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var user = await GetCurrentUserAsync();
            var users = await _context.ApplicationUsers
                .Where(bi => bi.Id != user.Id)
                .ToListAsync();

            return View(users);
        }

        // GET: Profile/Details/5
        public async Task<IActionResult> Details(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            var userInfo  = await _context.ApplicationUsers
                .Where(app => app.Id == user.Id)
                    .Include(bi => bi.MyBarterItems)
                    .Include(bi => bi.ReceivedTrades)
                        .ThenInclude(t => t.Sender)
                    .Include(bi => bi.SentTrades)
                        .ThenInclude(t => t.Receiver)
                    .ToListAsync();

            var userId = await _context.ApplicationUsers.FirstOrDefaultAsync(app => app.Id == id);

            var viewModel = new UserProfileViewModel()
            {
                User = user,
                AppUserId = userId.Id,
                BarterItems = userId.MyBarterItems.ToList(),
                ReceivedTrades = userId.ReceivedTrades.ToList(),
                SentTrades = userId.SentTrades.ToList()

            };

            return View(viewModel);
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public async Task<ActionResult> Edit(int id)
        {
            var user = await GetCurrentUserAsync();
            var viewModel = new ProfileFormViewModel
            {
                AppUserId = user.Id,
                TagName = user.TagName,
                Location = user.Location,
                ImagePath = user.ImagePath
            };
            
            return View(viewModel);
        }

        // POST: Profiles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind("AppUserId,TagName,Location,ImagePath,ImageFile")]ProfileFormViewModel viewModelItem, int id)
        {
            try
            {
                var profileData = await GetCurrentUserAsync();

                profileData.TagName = viewModelItem.TagName;
                profileData.Location = viewModelItem.Location;


                if (viewModelItem.ImageFile != null && viewModelItem.ImageFile.Length > 0)
                {

                    var fileName = Guid.NewGuid().ToString() + Path.GetFileName(viewModelItem.ImageFile.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", fileName);

                    profileData.ImagePath = fileName;

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await viewModelItem.ImageFile.CopyToAsync(stream);
                    }

                }

                _context.ApplicationUsers.Update(profileData);
                await _context.SaveChangesAsync();

                return RedirectToAction("Details", new { id = viewModelItem.AppUserId });
            }

            catch
            {
                return View();
            }
        }
    }
}