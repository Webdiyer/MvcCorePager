using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Webdiyer.MvcCorePagerDemo.Models;
using Webdiyer.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace Demo.Controllers
{
    public class OrdersController : Controller
    {
        IHostingEnvironment Env;
        int pageSize = 5; //default page size

        public OrdersController(IHostingEnvironment env)
        {
            Env = env;
        }

        public IActionResult Default(int pageIndex = 1)
        {
            var model = GetPagedOrders(pageIndex, pageSize);
            return View(model);
        }

        public IActionResult Ajax(int pageIndex = 1)
        {
            var model = GetPagedOrders(pageIndex, pageSize);
            string xrh = Request.Headers["X-Requested-With"];
            if (!string.IsNullOrEmpty(xrh) && xrh.Equals("XMLHttpRequest", System.StringComparison.OrdinalIgnoreCase))
            {
                return PartialView("_OrderList", model);
            }
            return View(model);
        }

        public IActionResult Bootstrap(int id = 1)
        {
            var model = GetPagedOrders(id, pageSize);
            return View(model);
        }

        IPagedList<Order> GetPagedOrders(int pageIndex, int pageSize)
        {
            var path = Path.Combine(Env.WebRootPath, "orders.json");
            var ods = Newtonsoft.Json.JsonConvert.DeserializeObject<Order[]>(System.IO.File.ReadAllText(path));
            var model = ods.OrderBy(o => o.OrderId).ToPagedList(pageIndex, pageSize);
            return model;
        }
        
    }
}
