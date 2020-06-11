using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SalesWebMVC.Models;
using SalesWebMVC.Models.ViewModels;
using SalesWebMVC.Services;
using SalesWebMVC.Services.Exceptions;

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

		public IActionResult Create()   //Get
		{
			var departments = _departmentService.FindAll();
			var viewModel = new SellerFormViewModel { Departments = departments };
			return View(viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(Seller seller)  //Post
		{
			if (!ModelState.IsValid)
			{
				var departments = _departmentService.FindAll();
				var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
				return View(viewModel);
			}

			_sellerServive.Insert(seller);
			return RedirectToAction(nameof(Index));
		}

		public IActionResult Delete(int? id)    //Get
		{
			if (id == null)
			{
				return RedirectToAction(nameof(Error), new { Message = "Id not provided!" });
			}

			var obj = _sellerServive.FindById(id.Value);

			if (obj == null)
			{
				return RedirectToAction(nameof(Error), new { Message = "Id not found!" });
			}

			return View(obj);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Delete(int id) //Post
		{
			_sellerServive.Remove(id);
			return RedirectToAction(nameof(Index));
		}

		public IActionResult Details(int? id)
		{
			if (id == null)
			{
				return RedirectToAction(nameof(Error), new { Message = "Id not provided!" });
			}

			var obj = _sellerServive.FindById(id.Value);

			if (obj == null)
			{
				return RedirectToAction(nameof(Error), new { Message = "Id not found!" });
			}

			return View(obj);
		}

		public IActionResult Edit(int? id)  //Get
		{
			if (id == null)
			{
				return RedirectToAction(nameof(Error), new { Message = "Id not provided!" });
			}

			var obj = _sellerServive.FindById(id.Value);

			if (obj == null)
			{
				return RedirectToAction(nameof(Error), new { Message = "Id not found!" });
			}

			List<Department> departments = _departmentService.FindAll();
			SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departments = departments };

			return View(viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(int id, Seller seller) //Post
		{
			if (!ModelState.IsValid)
			{
				var departments = _departmentService.FindAll();
				var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
				return View(viewModel);
			}

			if (id != seller.Id)
			{
				return RedirectToAction(nameof(Error), new { Message = "Id mismatch!" });
			}
			try
			{
				_sellerServive.Update(seller);

				return RedirectToAction(nameof(Index));
			}
			catch (NotFoundException e)
			{
				return RedirectToAction(nameof(Error), new { e.Message });
			}
			catch (DbConcurrencyException e)
			{
				return RedirectToAction(nameof(Error), new { e.Message });
			}
			catch (ApplicationException e)
			{
				return RedirectToAction(nameof(Error), new { e.Message });
			}
		}

		public IActionResult Error(string message)
		{
			var viewModel = new ErrorViewModel { Message = message, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier };

			return View(viewModel);
		}
	}
}
