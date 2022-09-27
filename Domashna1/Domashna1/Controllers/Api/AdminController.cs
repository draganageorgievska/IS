
using Domashna1.Domain.DomainModels;
using Domashna1.Domain.Identity;
using Domashna1.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domashna.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly ITicketService _ticketService;
        private readonly UserManager<ApplicationUser> userManager;

        public AdminController(IOrderService orderService, ITicketService ticketService,UserManager<ApplicationUser> userManager)
        {
            this._orderService = orderService;
            this._ticketService = ticketService;
            this.userManager = userManager;
        }

        [HttpGet("[action]")]
        public List<Order> GetOrders()
        {
            return this._orderService.getAllOrders();
        }
        [HttpGet("[action]")]
        public List<Ticket> GetTickets()
        {
            return this._ticketService.GetAllTickets();
        }
        [HttpPost("[action]")]
        public Order GetDetailsForTickets(BaseEntity model)
        {
            return this._orderService.getOrderDetails(model);
        }

        [HttpPost("[action]")]
        public bool ImportAllUsers(List<UserRegistrationDto> model)
        {
            bool status = true;

            foreach (var item in model)
            {
                var userCheck = userManager.FindByEmailAsync(item.Email).Result;

                if (userCheck == null)
                {
                    var user = new ApplicationUser
                    {
                        UserName = item.Email,
                        NormalizedUserName = item.Email,
                        Email = item.Email,
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = true,
                        TicketCart = new TicketCart()
                    };
                    var result = userManager.CreateAsync(user, item.Password).Result;

                    status = status && result.Succeeded;
                }
                else
                {
                    continue;
                }
            }

            return status;
        }
        }
}
