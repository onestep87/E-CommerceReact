using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceReact.Entities.OrderAggregate
{
    public class OrderItems
    {
        public int Id { get; set; }
        public ProductItemOrdered ItemOrdered { get; set; }
        public long Price { get; set; }
        public int Quantity { get; set; }
        
    }
}
