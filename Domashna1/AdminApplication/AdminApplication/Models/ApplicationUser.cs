using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminApplication.Models
{
    public class ApplicationUser
    {
        public string Email { get; set; }

        public string NormalizedUserName { get; set; }

        public string UserName { get; set; }
    }
}
