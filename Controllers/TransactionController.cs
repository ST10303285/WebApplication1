using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebApplication1.Data;
using WebApplication1.Models;

[Authorize]
public class TransactionController : Controller
{
    private readonly ApplicationDbContext _context;

    public TransactionController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var transactions = _context.Transactions.ToList();
        return View(transactions);
    }

    public IActionResult Create(int orderId)
    {
        var order = _context.Orders.FirstOrDefault(o => o.OrderID == orderId);
        if (order == null)
        {
            return NotFound();
        }

        var transaction = new Transaction
        {
            OrderID = orderId,
            TransactionDate = DateTime.Now,
            TotalAmount = order.OrderItems.Sum(item => item.Product.Price * item.Quantity)
        };

        return View(transaction);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Transaction transaction)
    {
        if (ModelState.IsValid)
        {
            _context.Transactions.Add(transaction);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        return View(transaction);
    }
}

