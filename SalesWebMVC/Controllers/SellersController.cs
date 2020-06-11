using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMVC.Models;
using SalesWebMVC.Models.ViewModels;
using SalesWebMVC.Services;

namespace SalesWebMVC.Controllers
{
	public class SellersController : Controller
	{
		private readonly SellerServive _sellerServive;
		private readonly DepartmentService _departmentService;

		public SellersController(SellerServive sellerServive, DepartmentService departmentService)
		{
			_sellerServive = sellerServive;
			_departmentService = departmentService;
		}

		public IActionResult Index()
		{
			var list = _sellerServive.FindAll();

			return View(list);
		}

		public IActionResult Create()	//Get
		{
			var departments = _departmentService.FindAll();
			var viewModel = new SellerFormViewModel { Departments = departments };
			return View(viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public  IActionResult Create(Seller seller)	//Post
		{
			_sellerServive.Insert(seller);
			return RedirectToAction(nameof(Index));
		}

		public IActionResult Delete(int? id)	//Get
		{
			if(id == null)
			{
				return NotFound();
			}

			var obj = _sellerServive.FindById(id.Value);

			if(obj == null)
			{
				return NotFound();
			}

			return View(obj);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Delete(int id)
		{
			_sellerServive.Remove(id);
			return RedirectToAction(nameof(Index));
		}
	}
}
