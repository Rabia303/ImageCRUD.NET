using System.Diagnostics;
using imageCrud.Models;
using Microsoft.AspNetCore.Mvc;

namespace imageCrud.Controllers
{
    public class HomeController : Controller
    {
        private readonly employeeDbContext db;

        public HomeController(employeeDbContext db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult fetch()
        {
            return View(db.Employees.ToList());
        }

        public IActionResult edit(int EmpId)
        {
            var id = db.Employees.Find(EmpId);
            return View(id);
        }
        [HttpPost]

        public IActionResult edit(Employee abcd, IFormFile img)
        {
            if (img != null && img.Length > 0)
            {
                var filename = Path.GetFileName(img.FileName);
                string imgFolder = Path.Combine(HttpContext.Request.PathBase.Value, "wwwroot/EmpImages");
                if (!Directory.Exists(imgFolder))
                {
                    Directory.CreateDirectory(imgFolder);
                }
                string filePath = Path.Combine(imgFolder, filename);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    img.CopyTo(stream);
                }
                var dbAddress = Path.Combine("empimages", filename);
                abcd.EmpImg = dbAddress;
                db.Employees.Update(abcd);
                db.SaveChanges();
                return RedirectToAction("fetch");
            }
            return View();
        }

        public IActionResult delete(int EmpId)
        {
            var id = db.Employees.Find(EmpId);
            db.Employees.Remove(id); 
            db.SaveChanges();
            return RedirectToAction("fetch");
        }

        public IActionResult imgInsert()
        {
            return View();
        }
        [HttpPost]
        public IActionResult imgInsert(Employee abc,IFormFile img)
        {
            var ext = new[] { "png", "jpg", "jpeg" };

            if (img != null && img.Length > 0)
            {
                var fe = System.IO.Path.GetExtension(img.FileName).Substring(1); 
                if (!ext.Contains(fe))
                {
                    ViewBag.e = "Choose png jpg jpeg Type only";
                    return View();
                }
                var rn = Path.GetFileName(img.FileName);
                //Random rand = new Random();
                //var filename = rand.Next(1, 50) + rn;
                var filename = Guid.NewGuid() + rn;
                string imgFolder = Path.Combine(HttpContext.Request.PathBase.Value, "wwwroot/EmpImages");
                if(!Directory.Exists(imgFolder))
                {
                    Directory.CreateDirectory(imgFolder);
                }
                string filePath = Path.Combine(imgFolder, filename);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    img.CopyTo(stream);
                }
                var dbAddress = Path.Combine("empimages", filename);
                abc.EmpImg = dbAddress;
                db.Employees.Add(abc);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}