using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication.ExtendedProtection;
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
        public async Task<ActionResult> Index()
        {
            var user = await GetCurrentUserAsync();

            var trade = await _context.Trade
                .Where(t => t.SenderId == user.Id)
                .Include(t => t.Receiver)
                .Include(t => t.BarterTrades)
                .ThenInclude(bt => bt.BarterItem)
                .ToListAsync();

            return View(trade);


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

                return RedirectToAction(nameof(Trade), new { receiverId = trade.ReceiverId, tradeId = trade.TradeId });
            }
            catch
            {
                return View();
            }
        }
        // GET: Trades/Edit/5
        public async Task<ActionResult> Trade(int tradeId, string receiverId, List<int> SelectedItemInput)
        {
            var receiverBarterItems = await _context.BarterItem
                .Where(bi => bi.AppUserId == receiverId)
              .ToListAsync();

            var checkboxItems = receiverBarterItems.Select(cbi => new BarterItemSelectViewModel()
            {
                Title = cbi.Title,
                Description = cbi.Description,
                ImagePath = cbi.ImagePath,
                Value = cbi.Value,
                BarterItemId = cbi.BarterItemId,
                IsSelected = false
               
            }
            );

            var viewModel = new TradeWithItemsViewModel
            {
                TradeId = tradeId,
                SelectedItems = checkboxItems.ToList()
            };

            return View(viewModel);
        }

        // POST: Trades/Trade/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Trade([Bind("SelectedItems,TradeId")] TradeWithItemsViewModel viewModelItem)
        {
            try
            {
                var tradeRequestExists = _context.Trade.FirstOrDefault(o => o.TradeId == viewModelItem.TradeId);
                var barterTradeItems = viewModelItem.SelectedItems.Where(vmi => vmi.IsSelected == true)
                    .Select(si => new BarterTrade 
                    { 
                        BarterItemId = si.BarterItemId,
                        TradeId = tradeRequestExists.TradeId
                    });

                tradeRequestExists.BarterTrades = barterTradeItems.ToList();
                    
                    _context.Trade.Update(tradeRequestExists);
                    await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
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