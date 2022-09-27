using Domashna1.Domain.DomainModels;
using Domashna1.Domain.DTO;
using Domashna1.Repository.Interface;
using Domashna1.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domashna1.Services.Implementation
{
    public class TicketCartService : ITicketCartService
    {
        private readonly IRepository<TicketCart> _ticketCartRepositorty;
        private readonly IRepository<Order> _orderRepositorty;
        private readonly IRepository<TicketInOrder> _ticketInOrderRepositorty;
        private readonly IUserRepository _userRepository;
        private readonly IRepository<EmailMessage> _mailRepository;

        public TicketCartService(IRepository<EmailMessage> mailRepository, IRepository<TicketCart> ticketCartRepositorty, IRepository<TicketInOrder> ticketInOrderRepositorty, IRepository<Order> orderRepositorty, IUserRepository userRepository)
        {
            _ticketCartRepositorty = ticketCartRepositorty;
            _userRepository = userRepository;
            _orderRepositorty = orderRepositorty;
            _ticketInOrderRepositorty = ticketInOrderRepositorty;
            _mailRepository = mailRepository;
        }

        public bool deleteTicketFromTicketCart(string userId, Guid id)
        {
            if (!string.IsNullOrEmpty(userId) && id!=null)
            {
                var loggedInUser = this._userRepository.Get(userId);

                var userTicketCart = loggedInUser.TicketCart;

                var itemToDelete = userTicketCart.TicketsInTicketCart.Where(z => z.TicketId.Equals(id)).FirstOrDefault();

                userTicketCart.TicketsInTicketCart.Remove(itemToDelete);

                this._ticketCartRepositorty.Update(userTicketCart);

                return true;
            }

            return false;
        }

        public TicketCartDto getTicketCartInfo(string userId)
        {
            var loggedInUser = this._userRepository.Get(userId);

            var userTicketCart = loggedInUser.TicketCart;

            var AllTickets = userTicketCart.TicketsInTicketCart.ToList();

            var allTicketsPrice = AllTickets.Select(z => new
            {
                ProductPrice = z.Ticket.Price,
                Quanitity = z.Quantity
            }).ToList();

            float totalPrice = 0;


            foreach (var item in allTicketsPrice)
            {
                totalPrice += item.Quanitity * item.ProductPrice;
            }


            TicketCartDto tcDto = new TicketCartDto
            {
                TicketsInTicketCart = AllTickets,
                TotalPrice = totalPrice
            };


            return tcDto;

        }

        public bool orderNow(string userId)
        {
            if (!string.IsNullOrEmpty(userId))
            {

                var loggedInUser = this._userRepository.Get(userId);

                var userTicketCart = loggedInUser.TicketCart;

                EmailMessage mail = new EmailMessage();
                mail.MailTo = loggedInUser.Email;
                mail.Subject = "Successfully created order";
                mail.Status = false;

                Order order = new Order
                {
                    Id = Guid.NewGuid(),
                    OrderedBy = loggedInUser,
                    UserId = userId
                };

                this._orderRepositorty.Insert(order);

                List<TicketInOrder> ticketInOrders = new List<TicketInOrder>();

                var result = userTicketCart.TicketsInTicketCart.Select(z => new TicketInOrder
                {
                    Id = Guid.NewGuid(),
                    TicketId = z.Ticket.Id,
                    Ticket = z.Ticket,
                    OrderId = order.Id,
                    Order = order,
                    Quantity=z.Quantity
                }).ToList();
                StringBuilder sb = new StringBuilder();

                float totalPrice = 0;

                sb.AppendLine("Your order is completed. The order contains: ");

                for (int i = 1; i <= result.Count(); i++)
                {
                    var item = result[i - 1];

                    totalPrice += item.Quantity * item.Ticket.Price;

                    sb.AppendLine(i.ToString() + ". " + item.Ticket.MovieName + " with price of: " + item.Ticket.Price + " and quantity of: " + item.Quantity);
                }

                sb.AppendLine("Total price: " + totalPrice.ToString());


                mail.Content = sb.ToString();


                ticketInOrders.AddRange(result);

                foreach (var item in ticketInOrders)
                {
                    this._ticketInOrderRepositorty.Insert(item);
                }

                loggedInUser.TicketCart.TicketsInTicketCart.Clear();

                this._userRepository.Update(loggedInUser);
                this._mailRepository.Insert(mail);

                return true;
            }
            return false;
        }
    }
}
