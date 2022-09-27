using Domashna1.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domashna1.Domain.DomainModels
{
    public class Order : BaseEntity
    {
        public string UserId { get; set; }
        public ApplicationUser OrderedBy { get; set; }
        public IEnumerable<TicketInOrder> TicketsInOrder { get; set; }
    }
}
