using System.Web.Mvc;

namespace WebApplication9.Controllers
{
    public class SearchController : Controller
    {
        [HttpGet]
        public ActionResult Search(string category,string price,string searchtext)
        {
            return View();
        }
    }
}