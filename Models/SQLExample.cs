using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace WebApplication1.Models
{
    public class SQLExample1 : Controller
    {
        public static string con_string = "Server=tcp:webserver-example1.database.windows.net,1433;Initial Catalog=database1;Persist Security Info=False;User ID=wadihaboat;Password=Malika2003.;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";
        public static SqlConnection con = new SqlConnection(con_string);
        public IActionResult Index()
        {
            return View();
        }
    }
}