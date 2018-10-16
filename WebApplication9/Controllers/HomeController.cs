using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ImageMagick;

namespace WebApplication9.Controllers
{
   
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            System.IO.DirectoryInfo di = new DirectoryInfo(Server.MapPath("~/Upload/"));
            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
            return View();
        }

        [HttpPost]

        public ActionResult Index(HttpPostedFileBase postedFile)
        {
            string fileName =Path.GetFileName(postedFile.FileName);
            string filePath = "~/Upload/" + fileName;
            postedFile.SaveAs(Server.MapPath(filePath));
            using (MagickImage image = new MagickImage(filePath))
            {
                image.Clone();
                image.Format = MagickFormat.Jpeg;
                //image.Quality = 50;
                image.Resize(350, 0);

                //MagickGeometry size = new MagickGeometry(100);
                //size.IgnoreAspectRatio = false;
                //image.Resize(size);
                image.Write("~/Upload/" + "100x100.png");
            }
            return RedirectToAction("Show");
        }

        [HttpGet]
        public ActionResult Show()
        {
            ViewBag.Images = Directory.EnumerateFiles(Server.MapPath("~/Upload"))
                             .Select(fn => "~/Upload/" + Path.GetFileName(fn));
            return View();
        }
    }
}