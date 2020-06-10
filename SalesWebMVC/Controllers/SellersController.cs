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
	}
}
