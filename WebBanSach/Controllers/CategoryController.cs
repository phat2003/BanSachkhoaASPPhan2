using Microsoft.AspNetCore.Mvc;
using WebBanSach.Data;
using WebBanSach.Models;

namespace WebBanSach.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;//biến này chỉ được đọc(không được ghi hay làm gì khác).
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        //action đưa index lên html
        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _db.Categories.ToList();
            return View(objCategoryList);
        }
        //action view create
        public IActionResult Create()
        {
            return View();
        }

        //post
        [HttpPost]
        [ValidateAntiForgeryToken]//lệnh này dùng để chống giả mạo về method này
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("DisplayOrder", "The Name must not same displayorder");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["Sucess"] = "Category Create sucessfully";
                return RedirectToAction("index");
            }
            return View(obj);
        }

        public IActionResult Edit(int? id)
        {
            if (id==null || id==0)
            {
                return NotFound();
            }
            var categoryfromDb = _db.Categories.Find(id);
            //var categoryfromDbFirst = _db.Categories.FirstOrDefault(u=>u.Id==id);
            //var categoryfromDbsingle = _db.Categories.SingleOrDefault(u => u.Id == id);
            if (categoryfromDb == null)
            {
                return NotFound();
            }
            return View(categoryfromDb);
        }

        //post
        [HttpPost]
        [ValidateAntiForgeryToken]//lệnh này dùng để chống giả mạo về method này
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The Name must not same displayorder");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["Sucess"] = "Category Edit sucessfully";
                return RedirectToAction("index");
            }
            return View(obj);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryfromDb = _db.Categories.Find(id);
            //var categoryfromDbFirst = _db.Categories.FirstOrDefault(u=>u.Id==id);
            //var categoryfromDbsingle = _db.Categories.SingleOrDefault(u => u.Id == id);
            if (categoryfromDb == null)
            {
                return NotFound();
            }
            return View(categoryfromDb);
        }

        //post
        [HttpPost]
        [ValidateAntiForgeryToken]//lệnh này dùng để chống giả mạo về method này
        public IActionResult DeletePost(int? id)
        {
            var obj = _db.Categories.Find(id);
            //var categoryfromDbFirst = _db.Categories.FirstOrDefault(u=>u.Id==id);
            //var categoryfromDbsingle = _db.Categories.SingleOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            else
            {
                _db.Categories.Remove(obj);
                _db.SaveChanges();
                TempData["Sucess"] = "Category Delete sucessfully";
                return RedirectToAction("index");
            }
            return View(obj);
        }
    }
}
