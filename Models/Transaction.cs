namespace WebApplication1.Models
{
    public class Transaction
    {
        public int TransactionID { get; set; }
        public int OrderID { get; set; }

        public virtual Order Order { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal TotalAmount { get; set; }

       
    }
}
