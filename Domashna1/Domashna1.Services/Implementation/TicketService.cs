using Domashna1.Domain.DomainModels;
using Domashna1.Domain.DTO;
using Domashna1.Repository.Interface;
using Domashna1.Services.Interface;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domashna1.Services.Implementation
{
    public class TicketService : ITicketService
    {
        private readonly IRepository<Ticket> _ticketRepository;
        private readonly IRepository<TicketInTicketCart> _ticketInCartRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILogger<TicketService> _logger;
        public TicketService(IRepository<Ticket> ticketRepository, ILogger<TicketService> logger, IRepository<TicketInTicketCart> ticketInCartRepository, IUserRepository userRepository)
        {
            _ticketRepository = ticketRepository;
            _userRepository = userRepository;
            _ticketInCartRepository = ticketInCartRepository;
            _logger = logger;
        }

        public bool AddToCart(AddToCartDto item, string userID)
        {

            var user = this._userRepository.Get(userID);

            var userCart = user.TicketCart;

            if (item.TicketId!=null && userCart != null)
            {
                var ticket = this.GetDetailsForTicket(item.TicketId);

                if (ticket != null)
                {
                    TicketInTicketCart itemToAdd = new TicketInTicketCart
                    {
                        Id = Guid.NewGuid(),
                        Ticket = ticket,
                        TicketId = ticket.Id,
                        TicketCart = userCart,
                        CartId = userCart.Id,
                        Quantity = item.Quantity
                    };

                    this._ticketInCartRepository.Insert(itemToAdd);
                    _logger.LogInformation("Ticket was successfully added into TicketCart");
                    return true;
                }
                return false;
            }
            _logger.LogInformation("Something was wrong. TicketId or UserCard may be unavailable!");
            return false;
        }

        public void CreateNewTicket(Ticket t)
        {
            this._ticketRepository.Insert(t);
        }

        public void DeleteTicket(Guid id)
        {
            var ticket = this.GetDetailsForTicket(id);
            this._ticketRepository.Delete(ticket);
        }

        public List<Ticket> GetAllTickets()
        {
            _logger.LogInformation("GetAllTickets was called!");
            return this._ticketRepository.GetAll().ToList();
        }

        public Ticket GetDetailsForTicket(Guid? id)
        {
            return this._ticketRepository.Get(id);
        }

        public AddToCartDto GetCartInfo(Guid? id)
        {
            var ticket = this.GetDetailsForTicket(id);
            AddToCartDto model = new AddToCartDto
            {
                SelectedTicket = ticket,
                TicketId = ticket.Id,
                Quantity = 1
            };

            return model;
        }

        public void UpdateExistingTicket(Ticket t)
        {
            this._ticketRepository.Update(t);
        }


    }
}
