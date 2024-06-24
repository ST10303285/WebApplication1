using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;


        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public ActionResult MyWork()
        {
            var products = _context.Products.ToList();
            return View(products);
        }
        public IActionResult AddProduct(Product product)
        {
            if (string.IsNullOrEmpty(product.Name))
            {
                ModelState.AddModelError("Name", "Product name cannot be null or empty");
                return View(product);
            }

            _context.Products.Add(product);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult AddTransaction(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            _context.SaveChanges();
            return RedirectToAction("OrderDetails", new { id = transaction.OrderID });
        }

        public IActionResult OrderDetails(int id)
        {
            var order = _context.Orders
                .Include(o => o.Product)
                //.Include(o => o.User)
                .FirstOrDefault(o => o.OrderID == id);
            var transactions = _context.Transactions
                .Where(t => t.OrderID == id)
                .ToList();
            var viewModel = new OrderDetailsViewModel
            {
                Order = order,
                Transactions = transactions
            };
            return View(viewModel);
        }

        public IActionResult OrderView(int id)
        {

            var order = _context.Orders
                .Include(o => o.Product)
                .FirstOrDefault(o => o.OrderID == id);
            var transactions = _context.Transactions
                .Where(t => t.OrderID == id)
                .ToList();
            var viewModel = new OrderDetailsViewModel
            {
                Order = order,
                Transactions = transactions
            };
            return View(viewModel);
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}