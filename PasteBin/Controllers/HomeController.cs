using StackExchange.Redis;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace PasteBin.Controllers
{
    public class HomeController : Controller
    {
        pastebinEntities db = new pastebinEntities();

        // [HttpPost, ValidateInput(false,")]
        [ValidateInput(false)]
        public ActionResult Index(string name = "", string message = "")
        {
            Pastebin ps = new Pastebin();
            ps.Code = "";
            ps.Name = "";

            name = Regex.Replace(name, "[^a-zA-Z0-9_.]+", "", RegexOptions.Compiled);
            name = name.ToLower();
            if (name.Length == 0)
                return View(ps);

            var ps2 = db.Pastebin.Find(name);
            string k = name;
            int i = 0;
            while(ps2 != null)
            {
                k = name;
                k += i.ToString();
                ps2 = db.Pastebin.Find(k);
                i++;
            }


            ps.Code = message;
            ps.Name = k;
            db.Pastebin.Add(ps);
            db.SaveChanges();
            return View("~/Views/Home/Code.cshtml", ps);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Code(string id)
        {
            var ps = db.Pastebin.Find(id);

            return View(ps);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}