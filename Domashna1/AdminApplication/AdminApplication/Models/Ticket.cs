using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminApplication.Models
{
    public class Ticket
    {
        public Guid Id { get; set; }
        public string MovieName { get; set; }
        public string Image { get; set; }
        public string Genre { get; set; }
        public float Price { get; set; }
    }
}
