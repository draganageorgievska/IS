using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domashna1.Domain.DomainModels
{
    public class TicketInTicketCart : BaseEntity
    {
        public Guid TicketId { get; set; }
        public Guid CartId { get; set; }
        public Ticket Ticket { get; set; }
        public TicketCart TicketCart { get; set; }
        public int Quantity { get; set; }
    }
}
