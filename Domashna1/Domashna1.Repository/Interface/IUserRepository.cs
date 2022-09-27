using Domashna1.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domashna1.Repository.Interface
{
    public interface IUserRepository
    {
        IEnumerable<ApplicationUser> GetAll();
        ApplicationUser Get(string id);
        void Insert(ApplicationUser entity);
        void Update(ApplicationUser entity);
        void Delete(ApplicationUser entity);
    }
}
