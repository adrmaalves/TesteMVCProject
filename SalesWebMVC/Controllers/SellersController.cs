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

		public async Task<IActionResult> Index()
		{
			var list = await _sellerServive.FindAllAsync();

			return View(list);
		}

		public async Task<IActionResult> Create()   //Get
		{
			var departments = await _departmentService.FindAllAsync();
			var viewModel = new SellerFormViewModel { Departments = departments };
			return View(viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(Seller seller)  //Post
		{
			if (!ModelState.IsValid)
			{
				var departments = await _departmentService.FindAllAsync();
				var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
				return View(viewModel);
			}

			await _sellerServive.InsertAsync(seller);
			return RedirectToAction(nameof(Index));
		}

		public async Task<IActionResult> Delete(int? id)    //Get
		{
			if (id == null)
			{
				return RedirectToAction(nameof(Error), new { Message = "Id not provided!" });
			}

			var obj = await _sellerServive.FindByIdAsync(id.Value);

			if (obj == null)
			{
				return RedirectToAction(nameof(Error), new { Message = "Id not found!" });
			}

			return View(obj);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Delete(int id) //Post
		{
			try
			{
				await _sellerServive.RemoveAsync(id);
				return RedirectToAction(nameof(Index));
			}
			catch(IntegrityException i)
			{
				return RedirectToAction(nameof(Error), new { i.Message });
			}
		}

		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return RedirectToAction(nameof(Error), new { Message = "Id not provided!" });
			}

			var obj = await _sellerServive.FindByIdAsync(id.Value);

			if (obj == null)
			{
				return RedirectToAction(nameof(Error), new { Message = "Id not found!" });
			}

			return View(obj);
		}

		public async Task<IActionResult> Edit(int? id)  //Get
		{
			if (id == null)
			{
				return RedirectToAction(nameof(Error), new { Message = "Id not provided!" });
			}

			var obj = await _sellerServive.FindByIdAsync(id.Value);

			if (obj == null)
			{
				return RedirectToAction(nameof(Error), new { Message = "Id not found!" });
			}

			List<Department> departments = await _departmentService.FindAllAsync();
			SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departments = departments };

			return View(viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, Seller seller) //Post
		{
			if (!ModelState.IsValid)
			{
				var departments = await _departmentService.FindAllAsync();
				var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
				return View(viewModel);
			}

			if (id != seller.Id)
			{
				return RedirectToAction(nameof(Error), new { Message = "Id mismatch!" });
			}
			try
			{
				await _sellerServive.UpdateAsync(seller);

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
			catch (IntegrityException e)
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
