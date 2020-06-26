using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Security.Authentication.ExtendedProtection;
using System.Threading.Tasks;
using SwapShop.Data;
using SwapShop.Models;
using SwapShop.Models.ViewModels.Trades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.View;

namespace SwapShop.Controllers
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

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
            //this is a universal method to get the current user logged in 

        public async Task<ActionResult> Details(int id)
        {
            // this is the view for the full trade details 
            // this can viewed by both sender and reciever
            var trade = await _context.Trade
                .Include(t => t.Receiver)
                .Include(t => t.Sender)
                .Include(t => t.BarterTrades)
                .ThenInclude(bt => bt.BarterItem)
                .ThenInclude(bt => bt.AppUser)
                .FirstOrDefaultAsync(tad => tad.TradeId == id);

            //this gets all the barterTrades related to both sender and receiver and current trade
            var senderSelectedItems = trade.BarterTrades.Where(bt => bt.BarterItem.AppUserId == trade.ReceiverId);
            var receiverSelectedItems = trade.BarterTrades.Where(bt => bt.BarterItem.AppUserId == trade.SenderId);

            //this converts those selected barterTrades to the BarterItemSelectList view 
                var sender = senderSelectedItems.Select(list => new BarterItemSelectViewModel 
                {
                    Title = list.BarterItem.Title,
                    Description = list.BarterItem.Description,
                    ImagePath = list.BarterItem.ImagePath,
                    Value = list.BarterItem.Value,
                    BarterItemId = list.BarterItem.BarterItemId,
                    RequestedAmount = list.RequestedAmount
                });

           //this converts those selected barterTrades to the BarterItemSelectList view 
                var receiver = receiverSelectedItems.Select(list => new BarterItemSelectViewModel
                {
                    Title = list.BarterItem.Title,
                    Description = list.BarterItem.Description,
                    ImagePath = list.BarterItem.ImagePath,
                    Value = list.BarterItem.Value,
                    BarterItemId = list.BarterItem.BarterItemId,
                    RequestedAmount = list.RequestedAmount
                });

            //this converts the list of selected items and pass them to the  TradeWithItems view 
            var itemsView = new TradeWithItemsViewModel
            {
                TradeId = id,
                Trade = trade,
                ReceiverSelectedItems = receiver.ToList(),
                SenderSelectedItems = sender.ToList()
            };

            //this takes that now defined TradeWithItems views and passes it to the value view model 
            var valueView = new TradeValueViewModel
            {
               Trade = trade,
               TradeId = id,
               Items = itemsView
            };

            return View(valueView);
        }

        public async Task<ActionResult> Create(string id)
        {
        // this gets both the current user and the user(id) being passed through in the url 
        // this defines which is receiver and sender 
            var sender = await GetCurrentUserAsync();
            var receiver = await _userManager.FindByIdAsync(id);
            
            //this will return a view to initiate a trade
              var viewModel = new TradeRequestFormViewModel
            {
                ReceiverId = receiver.ToString(),
                SenderId = sender.Id,
                DateCreated = DateTime.Now
            };
           
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(TradeRequestFormViewModel modelItem, string id)
        {
            try
            {
                // this gets both the current user and the user(id) being passed through in the url 
                // this defines which is receiver and sender 
                var user = await GetCurrentUserAsync();
                var receiverId = await _userManager.FindByIdAsync(id);

                //This creates the initial trade 
                var trade = new Trade
                {
                    Message = modelItem.Message,
                    ReceiverId = receiverId.Id,
                    SenderId = user.Id,
                    DateCreated = DateTime.Now
                };

                _context.Trade.Add(trade);
                await _context.SaveChangesAsync();

                //passes through the senderId, receieverId, tradeId to the Trade method so the user can select their items 
                return RedirectToAction(nameof(Trade), new { receiverId = trade.ReceiverId, tradeId = trade.TradeId, senderId = user.Id});
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> Trade(int tradeId, string receiverId, string senderId)
        {
            //find the current user logged in 
            var user = await GetCurrentUserAsync();

            //this allows for either use (sender or receiver) to access the others items. 
            List<BarterItem> barterItems = null;

            //this is getting the trade that we are passing through  
            var trade = await _context.Trade
                        .Include(b => b.BarterTrades)
                        .FirstOrDefaultAsync(o => o.TradeId == tradeId);

            //that empty list of barter items is populated with the users items here 
            if (user.Id.ToString() == senderId)
            {
                barterItems = await _context.BarterItem
                                    .Where(bi => bi.AppUserId == receiverId && bi.IsAvailable == true)
                                    .Include(bi => bi.AppUser)
                                    .ToListAsync();
            }else
            {
                barterItems = await _context.BarterItem
                                    .Where(bi => bi.AppUserId == senderId && bi.IsAvailable == true)
                                    .Include(bi => bi.AppUser)
                                    .ToListAsync();
            }

            //this takes the now populated list of barter items and gets all the data relating to it 
            var checkboxItems = barterItems.Select(cbi => new BarterItemSelectViewModel()
            {
                Title = cbi.Title,
                Description = cbi.Description,
                ImagePath = cbi.ImagePath,
                Value = cbi.Value,
                BarterItemId = cbi.BarterItemId,
                IsSelected = false
            });

            //this will take those items and populate either selectedItems list for the user to choose 
            var viewModel = new TradeWithItemsViewModel
            {
                TradeId = tradeId,
                Trade = trade,
                ReceiverSelectedItems = checkboxItems.ToList(),
                SenderSelectedItems = checkboxItems.ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Trade(TradeWithItemsViewModel viewModelItem)
        {
            try
            {
                //find current user logged in 
                var user = await GetCurrentUserAsync();

                //find the existing trade the user is working with 
                var tradeRequestExists = await _context.Trade.FirstOrDefaultAsync(o => o.TradeId == viewModelItem.TradeId);
                
                //this says the trade on the viewModel is the current trade we got 
                viewModelItem.Trade = tradeRequestExists;

                //this is renders either checkbox items for whatever user is logged in 
                if (viewModelItem.Trade.SenderId == user.Id)
                {
                    //this grabs all selected items and creates new barterTrades 
                    var senderSelectedItems = viewModelItem.SenderSelectedItems.Where(vmi => vmi.IsSelected == true)
                    .Select(si => new BarterTrade
                    {
                        BarterItemId = si.BarterItemId,
                        TradeId = tradeRequestExists.TradeId,
                        RequestedAmount = si.RequestedAmount
                    });
                        tradeRequestExists.BarterTrades = senderSelectedItems.ToList();
                }
                else
                {
                    //this grabs all selected items and creates new barterTrades 
                    var receiverSelectedItems = viewModelItem.ReceiverSelectedItems.Where(vmi => vmi.IsSelected == true)
                    .Select(si => new BarterTrade
                    {
                        BarterItemId = si.BarterItemId,
                        TradeId = tradeRequestExists.TradeId,
                        RequestedAmount = si.RequestedAmount
                    });
                        tradeRequestExists.BarterTrades = receiverSelectedItems.ToList();
                }
               
                //this updates the trade with barterTrades added 
                _context.Trade.Update(tradeRequestExists);
                await _context.SaveChangesAsync();

                //redirects to current trades details view
                return RedirectToAction("Details", new { id = viewModelItem.TradeId });
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        public async Task<ActionResult> Deny(int tradeId, string receiverId, string senderId)
        {
            var trade = await _context.Trade
                    .Include(t => t.Receiver)
                    .Where(r => r.ReceiverId == receiverId)
                    .Include(t => t.Sender)
                    .Where(r => r.SenderId == senderId)
                    .Include(t => t.BarterTrades)
                        .ThenInclude(bt => bt.BarterItem)
                            .ThenInclude(bt => bt.AppUser)
                            .FirstOrDefaultAsync(tad => tad.TradeId == tradeId);

            return View(trade);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteItem(int id, Trade trade)
        {
            try
            {


                var requestedItem = await _context.BarterTrade.Where(o => o.BarterItemId == id && o.TradeId == trade.TradeId).FirstOrDefaultAsync();

                _context.BarterTrade.Remove(requestedItem);
                await _context.SaveChangesAsync();

                return RedirectToAction("Details", new { id = trade.TradeId });
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CancelTrade(Trade trade)
        {
            try
            {
                var user = await GetCurrentUserAsync();

                var tradeToCancel = await _context.Trade.FirstOrDefaultAsync(o => o.TradeId == trade.TradeId);

                _context.Trade.Remove(tradeToCancel);
                await _context.SaveChangesAsync();

                return RedirectToAction("Details" ,"Users", new { id = user.Id  });
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Complete(int id)
        {
            try
            {
                var trade = await _context.Trade.Where(b => b.TradeId == id)
                        .Include(bt => bt.Sender)
                        .Include(bt => bt.Receiver)
                        .Include(bt => bt.BarterTrades)
                            .ThenInclude(bi => bi.BarterItem)
                            .FirstOrDefaultAsync();
                
                trade.Accepted = true;
                trade.IsCompleted = true;
                trade.DateCompleted = DateTime.Now;

                _context.Trade.Attach(trade);
                await _context.SaveChangesAsync();


                //this is for when you are complete and to update user barterItem stock 
                foreach (var item in trade.BarterTrades)
                {
                    var quantityChange = _context.BarterItem.Where(t => t.BarterItemId == item.BarterItem.BarterItemId).FirstOrDefault();
                    quantityChange.Quantity = item.BarterItem.Quantity - item.RequestedAmount;

                    _context.BarterItem.Update(quantityChange);
                    await _context.SaveChangesAsync();
              
                    if (quantityChange.Quantity == 0)
                {
                    quantityChange.IsAvailable = false;

                    _context.BarterItem.Update(quantityChange);
                    await _context.SaveChangesAsync();
                }

                };



                return RedirectToAction("Details", new { id = id});
            }
            catch(Exception ex)
            {
                return RedirectToAction("Details", new { id = id });
            }
        }
      
    }
}