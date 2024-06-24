using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

public class OrderController : Controller
{
    private readonly ApplicationDbContext _context;

    public OrderController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Order/Create

    public IActionResult Index()
    {
        var orders = _context.Orders.ToList();
        return View(orders);
    }
    public IActionResult Create()
    {
        var products = _context.Products.ToList();
        return View(products);
    }

    // POST: Order/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create( Order order)
    {
        if (ModelState.IsValid)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();

            // Create a transaction for the order
            var transaction = new Transaction
            {
                OrderID = order.OrderID,
                TransactionDate = DateTime.Now,
                TotalAmount = order.OrderItems.Sum(item => item.Product.Price * item.Quantity)
            };
            _context.Transactions.Add(transaction);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        return View(order);
    }
}