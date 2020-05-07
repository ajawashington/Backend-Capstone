using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendCapstone.Data;
using BackendCapstone.Models;
using BackendCapstone.Models.ViewModels.Trades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

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
        public async Task<ActionResult> Create(string id)
        {

            var receiverId = await _userManager.FindByIdAsync(id);
            var user = await GetCurrentUserAsync();

            var viewModel = new TradeRequestFormViewModel
            {
                ReceiverId = receiverId.ToString(),
                SenderId = user.Id,
                DateCreated = DateTime.Now
            };

            return View(viewModel);
        }
    

        // POST: Trade/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(TradeRequestFormViewModel modelItem, string id)
        {
            try
            {
                var user = await GetCurrentUserAsync();

                var trade = new Trade
                {
                    Message = modelItem.Message,
                    ReceiverId = id,
                    SenderId = user.Id,
                    DateCreated = DateTime.Now
                };

                _context.Trade.Add(trade);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Trade));
            }
            catch
            {
                return View();
            }
        }
        // GET: Trades/Edit/5
        public async Task<ActionResult> Trade(Trade trade)
        {
            var receiverBarterItems = await _context.BarterItem
                .Where(bi => bi.AppUserId == trade.ReceiverId)
              .Select(bt => new SelectListItem() { Text = bt.Title, Value = bt.BarterItemId.ToString() })
              .ToListAsync();

            var viewModel = new TradeWithItemsViewModel
            {
                TradeId = trade.TradeId
                
            };

            viewModel.ReceieverBarterItems = receiverBarterItems;

            return View(viewModel);
        }

        // POST: Trades/Trade/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Trade(int TradeId, TradeWithItemsViewModel viewModelItem)
        {
            try
            {
                //need to pass through receiverId to get the users items, pass through the tradeRequest
                var user = await GetCurrentUserAsync();


                var tradeRequestExists = _context.Trade.FirstOrDefault(t => t.TradeId == TradeId && t.BarterTrades == null);

                while (tradeRequestExists == null)
                {

                    var newBarterItems = new BarterTrade
                    {
                        BarterItemId = viewModelItem.BarterItemId
                    };

                    _context.BarterTrade.Add(newBarterItems);
                    await _context.SaveChangesAsync();

                }


                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        //// GET: Orders/Edit/5
        //public async Task<ActionResult> Edit(int id)
        //{
        //    var paymentOptions = await _context.PaymentType
        //      .Select(pt => new SelectListItem() { Text = pt.Description, Value = pt.PaymentTypeId.ToString() })
        //      .ToListAsync();

        //    var viewModel = new OrderPaymentFormViewModel();

        //    viewModel.PaymentTypeOptions = paymentOptions;
        //    viewModel.OrderId = id;

        //    return View(viewModel);
        //}

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