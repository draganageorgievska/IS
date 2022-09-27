using Domashna1.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domashna1.Domain.DTO
{
    public class TicketCartDto
    {
        public List<TicketInTicketCart> TicketsInTicketCart { get; set; }
        public float TotalPrice { get; set; }
    }
}
