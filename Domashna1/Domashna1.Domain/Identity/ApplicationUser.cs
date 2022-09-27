using Domashna1.Domain.DomainModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domashna1.Domain.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public virtual TicketCart TicketCart { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
