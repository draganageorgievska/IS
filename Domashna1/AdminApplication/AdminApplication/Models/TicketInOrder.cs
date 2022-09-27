using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminApplication.Models
{
    public class TicketInOrder
    {
        public Guid TicketId { get; set; }
        public Ticket Ticket { get; set; }
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
        public int Quantity { get; set; }
    }
}
