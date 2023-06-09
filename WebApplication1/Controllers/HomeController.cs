using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models.EFModels;

namespace WebApplication1.Controllers
{
	public class HomeController : Controller
	{
		private AppDbContext db = new AppDbContext();

		public ActionResult Index()
		{
			return View();
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();	
		}
		
		public ActionResult CreateGuestBooks()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult CreateGuestBooks([Bind(Include = "Id,Name,Email,CellPhone,Message")] GuestBook guestBook)
		{
			if (ModelState.IsValid)
			{
				guestBook.CreatedTime = DateTime.Now;
				db.GuestBooks.Add(guestBook);
				db.SaveChanges();
				return RedirectToAction("Confirm");
			}

			return View(guestBook);
		}
		public ActionResult Confirm()
		{
			return View();
		}
	}
}