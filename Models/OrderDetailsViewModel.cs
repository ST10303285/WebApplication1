using WebApplication1.Data;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public class OrderDetailsViewModel
    {
        public Order? Order { get; set; }
        public List<Transaction>? Transactions { get; set; }
    }
}
