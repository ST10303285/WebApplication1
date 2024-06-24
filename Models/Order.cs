using Microsoft.AspNetCore.Identity;
using WebApplication1.Data;
namespace WebApplication1.Models
  
{
    public class Order
    {
        public int OrderID { get; set; }
        public int UserID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }

        public Product Product { get; set; }

        public virtual Transaction Transaction { get; set; }

        public virtual IdentityUser User { get; set; }

        public virtual ICollection<Order> OrderItems { get; set; }


    }
}
