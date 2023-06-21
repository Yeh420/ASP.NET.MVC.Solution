using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Products.Models.EFModels;
using Products.Models.ViewModels;

namespace Products.Controllers
{
    public class ProductsController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Products
        public ActionResult Index(ProductCriteria criteria)
        {
			PrepareCategoryDataSource(criteria.CategoryId);

			ViewBag.Criteria = criteria;

            // 查詢紀錄, 由於第一次進到這網頁時, criteria是沒有值的

            var query = db.Products.Include(p => p.Category);
            if (string.IsNullOrEmpty(criteria.Name) == false)
            {
                query = query.Where(p => p.Name.Contains(criteria.Name));
            }
            if (criteria.CategoryId != null && criteria.CategoryId.Value > 0) 
            {
				query = query.Where(p => p.CategoryId == criteria.CategoryId.Value);
            }
            if(criteria.MinPrice.HasValue)
            {
				query = query.Where(p => p.Price >= criteria.MinPrice.Value);
            }
            if(criteria.MaxPrice.HasValue)
            {
				query = query.Where(p => p.Price <= criteria.MaxPrice.Value);
            }
            
            var products = query.ToList().Select(p => p.ToIndexVM());
            return View(products);
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
			//ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name");
			PrepareCategoryDataSource(null);

			return View();
        }

        // POST: Products/Create
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CategoryId,Name,Description,Price,Status,ProductImage,Stock")] ProductCreateVM vm, HttpPostedFileBase file1)
        {
            var files = Request.Files;


			if (ModelState.IsValid) //如果通過驗證欄位
			foreach (HttpPostedFileBase file in files)
            {
				string path = Server.MapPath("/Uploads"); // 檔案要存放的資料夾位置
				string fileName = SaveUploadedFile(path, file);

				// 將檔名存入 vm裡
				vm.ProductImage = fileName;

				// 將 view model轉型為 Product
				Product product = vm.ToEntity();

				// 新增一筆紀錄
				db.Products.Add(product);
				db.SaveChanges();

				// 轉向到 Products/Index/
				return RedirectToAction("Index");
			}

    //        if (ModelState.IsValid) //如果通過驗證欄位
    //        {
    //            // 將 file1存檔, 並取得最後存檔的檔案名稱
    //            string path = Server.MapPath("/Uploads"); // 檔案要存放的資料夾位置
    //            string fileName = SaveUploadedFile(path,file);

    //            // 將檔名存入 vm裡
    //            vm.ProductImage = fileName;

    //            // 將 view model轉型為 Product
    //            Product product = vm.ToEntity();

				//// 新增一筆紀錄
				//db.Products.Add(product);
    //            db.SaveChanges();

    //            // 轉向到 Products/Index/
    //            return RedirectToAction("Index");
    //        }
			// 如果驗證失敗, 就仍停留在本網頁

			// 取得 CategoryId需要的下拉選項內容
			PrepareCategoryDataSource(vm.CategoryId);

			//顯示網頁
			return View(vm);
        }

		private string SaveUploadedFile(string path, HttpPostedFileBase file1)
		{
            // 如果沒有上傳檔案或檔案是空的, 就不處理, 傳回 string.empty
            if(file1 == null || file1.ContentLength == 0) return string.Empty;

            // 取得上傳檔案的副檔名
            string ext = Path.GetExtension(file1.FileName); // ".jpg" 而不是"jpg"

            // 如果副檔名不在允許的範圍裡, 表示上傳不合理的檔案類型, 就不處理, 傳回 string.empty
            string[] allowedExts = new string[] { ".jpg", ".jpeg", ".png", ".tif" };
            if (allowedExts.Contains(ext.ToLower()) == false) return string.Empty;

            // 生成一個不會重複的檔名
            string newFileName = Guid.NewGuid().ToString("N") + ext; // "N"格式不會產生 dash字串縮短
            string fullName = Path.Combine(path, newFileName);

            //將上傳檔案存放到指定位置
            file1.SaveAs(fullName);

            //傳回存放的檔名
            return newFileName;
		}

		// GET: Products/Edit/5
		public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var vm = db.Products.Find(id);
            if (vm == null)
            {
                return HttpNotFound();
            }
			PrepareCategoryDataSource(vm.CategoryId);

			return View(vm.ToEditVM());
        }

        // POST: Products/Edit/5
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CategoryId,Name,Description,Price,Status,ProductImage,Stock")] ProductEditVM vm)
        {
            if (ModelState.IsValid)
            {
				var product = db.Products.FirstOrDefault(p => p.Id == vm.Id);
				if (product == null) return null;

                db.Entry(product).CurrentValues.SetValues(vm);

				//product.CategoryId = vm.CategoryId;
				//product.Name = vm.Name;
				//product.Description = vm.Description;
				//product.Price = vm.Price;
				// product.ProductImage = vm.ProductImage;
				//product.Status = vm.Status;
				//product.Stock = vm.Stock;
				//db.Entry(vm.ToEntity()).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            PrepareCategoryDataSource(vm.CategoryId);

			return View(vm);
        }

		public ActionResult ImageEdit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			var vm = db.Products.Find(id).ToImageEditVM();
			if (vm == null)
			{
				return HttpNotFound();
			}
			PrepareCategoryDataSource(null);

			return View(vm);
		}

		// POST: Products/Edit/5
		// 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
		// 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult ImageEdit([Bind(Include = "Id,CategoryId,Name,Description,Price,Status,ProductImage,Stock")] ProductImageEditVM vm, HttpPostedFileBase file1)
		{
			if (ModelState.IsValid)
			{
				// 將 file1存檔, 並取得最後存檔的檔案名稱
				string path = Server.MapPath("/Uploads"); // 檔案要存放的資料夾位置
				string fileName = SaveUploadedFile(path, file1);

				// 將檔名存入 vm裡
				vm.ProductImage = fileName;

                var product = db.Products.FirstOrDefault(p => p.Id == vm.Id);
				if (product == null) return null;

                product.ProductImage = vm.ProductImage;

                //db.Entry(vm.ToEntity()).State = EntityState.Modified;
                db.SaveChanges();
				return RedirectToAction("Index");
			}
			PrepareCategoryDataSource(null);

			return View(vm);
		}
		private void PrepareCategoryDataSource(int? categoryId)
        {
            var categories = db.Categories.ToList().Prepend(new Category());
            ViewBag.CategoryId = new SelectList(categories, "Id", "Name", categoryId);
		}
		// GET: Products/Delete/5
		public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
