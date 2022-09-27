using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminApplication.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUser OrderedBy { get; set; }
        public List<TicketInOrder> TicketsInOrder { get; set; }
    }
}
