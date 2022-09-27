//using Domashna1.Domain.DTO;
using Domashna1.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domashna1.Services.Interface
{
    public interface ITicketCartService
    {
        TicketCartDto getTicketCartInfo(string userId);
        bool deleteTicketFromTicketCart(string userId, Guid id);
        bool orderNow(string userId);
    }
}
