 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BackendCapstone.Data;
using BackendCapstone.Models;
using BackendCapstone.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.IO;
using BackendCapstone.Models.ViewModels.BarterItems;

namespace BackendCapstone.Controllers
{
  
        [Authorize]
        public class BarterItemsController : Controller
        {

            private readonly ApplicationDbContext _context;

            private readonly UserManager<ApplicationUser> _userManager;

            public BarterItemsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
            {

                _context = context;
                _userManager = userManager;
            }
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
        // GET: BarterItems
        public async Task<IActionResult> Index()
        {
            var user = await GetCurrentUserAsync();
            var barterItems = await _context.BarterItem
                .Where(bi => bi.AppUserId != user.Id)
                .Include(bi => bi.AppUser)
                .Include(bi => bi.BarterType)
                .ToListAsync();

            return View(barterItems);
        }

        // GET: BarterItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var barterItem = await _context.BarterItem
                .Include(b => b.AppUser)
                .Include(b => b.BarterType)
                .FirstOrDefaultAsync(m => m.BarterItemId == id);
            if (barterItem == null)
            {
                return NotFound();
            }

            return View(barterItem);
        }

        public async Task<ActionResult> Create()
        {
            var BarterTypeOptions = await _context.BarterType
              .Select(bt => new SelectListItem() { Text = bt.Title, Value = bt.BarterTypeId.ToString() })
              .ToListAsync();

            var viewModel = new BarterItemFormViewModel();

            viewModel.BarterTypeOptions = BarterTypeOptions;


            return View(viewModel);
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("BarterItemId,Title,Description,BarterTypeId,AppUserId,ImagePath,ImageFile,Value,Quantity,IsAvailable")]BarterItemFormViewModel viewModelItem)
        {
            try
            {
                var user = await GetCurrentUserAsync();

                var barterItem = new BarterItem
                {

                    Title = viewModelItem.Title,
                    Description = viewModelItem.Description,
                    BarterTypeId = viewModelItem.BarterTypeId, 
                    IsAvailable = viewModelItem.IsAvailable,
                    Value = viewModelItem.Value,
                    Quantity = viewModelItem.Quantity,
                    AppUserId = user.Id,

                };
                
                if (viewModelItem.ImageFile != null && viewModelItem.ImageFile.Length > 0)
                {
                
                    var fileName = Guid.NewGuid().ToString() + Path.GetFileName(viewModelItem.ImageFile.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", fileName);

                    barterItem.ImagePath = fileName;

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await viewModelItem.ImageFile.CopyToAsync(stream);
                    }

                }

                _context.BarterItem.Add(barterItem);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // GET: BarterItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var barterItem = await _context.BarterItem.FindAsync(id);
            if (barterItem == null)
            {
                return NotFound();
            }
            ViewData["AppUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", barterItem.AppUserId);
            ViewData["BarterTypeId"] = new SelectList(_context.BarterType, "BarterTypeId", "Title", barterItem.BarterTypeId);
            return View(barterItem);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BarterItemId,Title,Description,BarterTypeId,AppUserId,ImagePath,Value,Quantity,IsAvailable")] BarterItem barterItem)
        {
            if (id != barterItem.BarterItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(barterItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BarterItemExists(barterItem.BarterItemId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", barterItem.AppUserId);
            ViewData["BarterTypeId"] = new SelectList(_context.BarterType, "BarterTypeId", "Title", barterItem.BarterTypeId);
            return View(barterItem);
        }

        // GET: BarterItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var barterItem = await _context.BarterItem
                .Include(b => b.AppUser)
                .Include(b => b.BarterType)
                .FirstOrDefaultAsync(m => m.BarterItemId == id);
            if (barterItem == null)
            {
                return NotFound();
            }

            return View(barterItem);
        }

        // POST: BarterItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var barterItem = await _context.BarterItem.FindAsync(id);
            _context.BarterItem.Remove(barterItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BarterItemExists(int id)
        {
            return _context.BarterItem.Any(e => e.BarterItemId == id);
        }
    }
}
