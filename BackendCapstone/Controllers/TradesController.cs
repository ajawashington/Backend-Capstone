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
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.View;

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

        // this is the view for the trade as a whole...
        // this is viewed by both sender //and reciever, with 
        public async Task<ActionResult> Details(int id)
        {
            
            var trades = await _context.Trade
                .Where(t => t.TradeId == id)
                .Include(t => t.Receiver)
                .Include(t => t.Sender)
                .Include(t => t.BarterTrades)
                .ThenInclude(bt => bt.BarterItem)
                .ToListAsync();


            var tradeId = await _context.Trade.FirstOrDefaultAsync(app => app.TradeId == id);

            var viewModel = new TradeDetailsViewModel
            {
                Trade = tradeId,
                TradeId = id,
                AssociatedTrades = tradeId.BarterTrades.ToList()

            };

            return View(viewModel);


        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
        // GET: Trades/Create
        public async Task<ActionResult> Create(string id)
        {
            var sender = await GetCurrentUserAsync();
            var receiver = await _userManager.FindByIdAsync(id);


            
            if (id != sender.Id)
            {

            var viewModel = new TradeRequestFormViewModel
            {
                ReceiverId = receiver.ToString(),
                SenderId = sender.Id,
                DateCreated = DateTime.Now
            };

            return View(viewModel);

            }

            return View();
        }
    

        // POST: Trade/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(TradeRequestFormViewModel modelItem, string id)
        {
            try
            {
                var user = await GetCurrentUserAsync();
                var receiverId = await _userManager.FindByIdAsync(id);

                var trade = new Trade
                {
                    Message = modelItem.Message,
                    ReceiverId = receiverId.Id,
                    SenderId = user.Id,
                    DateCreated = DateTime.Now
                };

                _context.Trade.Add(trade);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Trade), new { receiverId = trade.ReceiverId, tradeId = trade.TradeId, senderId = user.Id});
            }
            catch
            {
                return View();
            }
        }
        // GET: Trades/Edit/5
        public async Task<ActionResult> Trade(int tradeId, string receiverId, string senderId)
        {
            var user = await GetCurrentUserAsync();

            List<BarterItem> barterItems = null;
 
            if (user.Id.ToString() == receiverId)
            {
                barterItems = await _context.BarterItem
                .Where(bi => bi.AppUserId == senderId)
                .Include(bi => bi.AppUser)
                .ToListAsync();

            }else
            {
                barterItems = await _context.BarterItem
                .Where(bi => bi.AppUserId == receiverId)
                .Include(bi => bi.AppUser)
                .ToListAsync();

            }
          var checkboxItems = barterItems.Select(cbi => new BarterItemSelectViewModel()
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
                SelectedItems = checkboxItems.ToList(),
            };

            //if (viewModel.SelectedItems != null)
            //{
            //    var senderRequestItems = await _context.BarterItem
            //    .Where(bi => bi.AppUserId == senderId)
            //    .Where(bi => bi.AssociatedTrades == barterItems).ToListAsync();
            //};

            return View(viewModel);
        }

        // POST: Trades/Trade/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Trade(TradeWithItemsViewModel viewModelItem)
        {
            try
            {
                var user = await GetCurrentUserAsync();

                var tradeRequestExists = _context.Trade.FirstOrDefault(o => o.TradeId == viewModelItem.TradeId);
                var barterTradeItems = viewModelItem.SelectedItems.Where(vmi => vmi.IsSelected == true)
                    .Select(si => new BarterTrade
                    {
                        BarterItemId = si.BarterItemId,
                        TradeId = tradeRequestExists.TradeId
                    });


                tradeRequestExists.BarterTrades = barterTradeItems.ToList();

                //ViewBag.Total = barterTradeItems.Cast<BarterItemSelectViewModel>().Where(c => c.IsSelected).Sum(c => c.Value);


                _context.Trade.Update(tradeRequestExists);
                await _context.SaveChangesAsync();

                return RedirectToAction("Details", new { id = viewModelItem.TradeId });
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Complete(TradeDetailsViewModel viewModelItem)
        {
            try
            {
                var user = await GetCurrentUserAsync();

                var sum = viewModelItem.SenderValue + viewModelItem.ReceiverValue;
  

                var tradeRequestExists = _context.Trade.FirstOrDefault(o => o.TradeId == viewModelItem.TradeId);
                var completeTrade = new Trade
                {
                    IsCompleted = true,
                    DateCompleted = DateTime.Now,
                    TradeValue = sum

                };


                _context.Trade.Update(tradeRequestExists);
                await _context.SaveChangesAsync();

                return RedirectToAction("Details", new { id = viewModelItem.TradeId });
            }
            catch (Exception ex)
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