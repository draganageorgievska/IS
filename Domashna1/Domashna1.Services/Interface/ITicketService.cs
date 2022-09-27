using Domashna1.Domain.DomainModels;
using Domashna1.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domashna1.Services.Interface
{
    public interface ITicketService
    {
        List<Ticket> GetAllTickets();
        Ticket GetDetailsForTicket(Guid? id);
        void CreateNewTicket(Ticket t);
        void UpdateExistingTicket(Ticket t);
        AddToCartDto GetCartInfo(Guid? id);
        void DeleteTicket(Guid id);
        bool AddToCart(AddToCartDto item, string userID);
    }
}
