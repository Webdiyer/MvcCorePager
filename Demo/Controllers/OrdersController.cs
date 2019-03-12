using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Webdiyer.MvcCorePagerDemo.Models;
using Webdiyer.AspNetCore;

namespace Demo.Controllers
{
    public class OrdersController : Controller
    {
        private readonly DemoDbContext _context;
        int pageSize = 5; //default page size

        public OrdersController(DemoDbContext context)
        {
            _context = context;    
        }
        
        public IActionResult Default(int pageindex=1)
        {
            var model = _context.Orders.OrderByDescending(o => o.OrderDate).ToPagedList(pageindex, pageSize);
            return View(model);
        }

        public IActionResult Ajax(int pageindex = 1)
        {
            var model = _context.Orders.OrderByDescending(o => o.OrderDate).ToPagedList(pageindex, pageSize);
            string xrh = Request.Headers["X-Requested-With"];
            if (!string.IsNullOrEmpty(xrh) && xrh.Equals("XMLHttpRequest", System.StringComparison.OrdinalIgnoreCase))
            {
                return PartialView("_OrderList", model);
            }
            return View(model);
        }

        public IActionResult Bootstrap(int id = 1)
        {
            var model = _context.Orders.OrderByDescending(o => o.OrderDate).ToPagedList(id, pageSize);
            return View(model);
        }
        
    }
}
