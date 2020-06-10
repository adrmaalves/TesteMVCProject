using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMVC.Models;
using SalesWebMVC.Services;

namespace SalesWebMVC.Controllers
{
	public class SellersController : Controller
	{
		private readonly SellerServive _sellerServive;

		public SellersController(SellerServive sellerServive)
		{
			_sellerServive = sellerServive;
		}

		public IActionResult Index()
		{
			var list = _sellerServive.FindAll();

			return View(list);
		}

		public IActionResult Create()	//Get
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public  IActionResult Create(Seller seller)	//Post
		{
			_sellerServive.Insert(seller);
			return RedirectToAction(nameof(Index));
		}
	}
}
