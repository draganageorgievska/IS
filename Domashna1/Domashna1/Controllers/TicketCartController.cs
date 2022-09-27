using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Domashna1.Domain.DomainModels;
using Domashna1.Domain.DTO;
using Domashna1.Services.Interface;
using Stripe;

namespace Domashna1.Web.Controllers
{
    public class TicketCartController : Controller
    {
        private readonly ITicketCartService _ticketCartService;

        public TicketCartController(ITicketCartService ticketCartService)
        {
            _ticketCartService = ticketCartService;
        }
        public IActionResult Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return View(this._ticketCartService.getTicketCartInfo(userId));
        }
        public IActionResult DeleteFromTicketCart(Guid id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = this._ticketCartService.deleteTicketFromTicketCart(userId, id);
            if (result)
            {
                return RedirectToAction("Index", "TicketCart");
            }
            else
            {
                return RedirectToAction("Index", "TicketCart");
            }
        }
        public IActionResult PayOrder(string stripeEmail, string stripeToken)
        {
            var customerService = new CustomerService();
            var chargeService = new ChargeService();
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var order = this._ticketCartService.getTicketCartInfo(userId);

            var customer = customerService.Create(new CustomerCreateOptions
            {
                Email = stripeEmail,
                Source = stripeToken
            });

            var charge = chargeService.Create(new ChargeCreateOptions
            {
                Amount = (Convert.ToInt32(order.TotalPrice) * 100),
                Description = "Ticket Application Payment",
                Currency = "usd",
                Customer = customer.Id
            });

            if (charge.Status == "succeeded")
            {
                var result = this.Order();

                if (result)
                {
                    return RedirectToAction("Index", "TicketCart");
                }
                else
                {
                    return RedirectToAction("Index", "TicketCart");
                }
            }

            return RedirectToAction("Index", "TicketCart");
        }
        public Boolean Order()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = this._ticketCartService.orderNow(userId);

            return result;
        }
    }
}
