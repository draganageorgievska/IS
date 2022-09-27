//using Domashna.Domain.DomainModels;
using Domashna1.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domashna1.Services.Interface
{
    public interface IOrderService
    {
        List<Order> getAllOrders();

        Order getOrderDetails(BaseEntity model);
    }
}
