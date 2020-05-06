using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendCapstone.Data;
using BackendCapstone.Models;
using BackendCapstone.Models.ViewModels.Trade;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BackendCapstone.Controllers
{
    [Authorize]
    public class TradesController : Controller
    {

        private readonly ApplicationDbContext _context;

        private readonly UserManager<ApplicationUser> _userManager;

        public TradesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {

            _context = context;
            _userManager = userManager;
        }
        // GET: Trades
        public ActionResult Index()
        {
            return View();
        }

        // GET: Trades/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
        // GET: Trades/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Trade/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(TradeRequestFormViewModel modelItem)
        {
            try
            {
                var user = await GetCurrentUserAsync();

                var barterItem = new Trade
                {

                    Message = modelItem.Message,
                    DateCreated = DateTime
                    BarterTypeId = modelItem.BarterTypeId,
                    IsAvailable = modelItem.IsAvailable,
                    Value = modelItem.Value,
                    Quantity = modelItem.Quantity,
                    AppUserId = user.Id,

                };


                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        // GET: Trades/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Trades/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Trades/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Trades/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}