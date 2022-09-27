using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Domashna1.Domain.DomainModels
{
    public class Ticket : BaseEntity
    {
        public string MovieName { get; set; }
        public string Image { get; set; }
        public string Genre { get; set; }
        public float Price { get; set; }
        public string Date { get; set; }
        public virtual ICollection<TicketInTicketCart> TicketsInTicketCart { get; set; }
        public IEnumerable<TicketInOrder> TicketInOrders { get; set; }
    }
}
