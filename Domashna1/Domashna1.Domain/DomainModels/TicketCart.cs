using Domashna1.Domain.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Domashna1.Domain.DomainModels
{
    public class TicketCart : BaseEntity
    {
        public string ApplicationUserId { get; set; }
        public ApplicationUser AppUser { get; set; }
        public virtual ICollection<TicketInTicketCart> TicketsInTicketCart { get; set; }
    }
}
