﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMVC.Services;

namespace SalesWebMVC.Controllers
{
	public class SalesRecordsController : Controller
	{
		private readonly SalesRecordService _salesrecordservice;

		public SalesRecordsController(SalesRecordService salesrecordservice)
		{
			_salesrecordservice = salesrecordservice;
		}

		public IActionResult Index()
		{
			return View();
		}

		public async Task<IActionResult> SimpleSearch(DateTime? minDate, DateTime? maxDate)
		{
			if (!minDate.HasValue)
				minDate = new DateTime(DateTime.Now.Year, 1, 1);
			if (!maxDate.HasValue)
				maxDate = DateTime.Now;

			ViewData["minDate"] = minDate.Value.ToString("dd/MM/yyyy");
			ViewData["maxDate"] = maxDate.Value.ToString("dd/MM/yyyy");
			var result = await _salesrecordservice.FindByDateAsync(minDate, maxDate);
			return View(result);
		}

		public async Task<IActionResult> GroupingSearch(DateTime? minDate, DateTime? maxDate)
		{
			if (!minDate.HasValue)
				minDate = new DateTime(DateTime.Now.Year, 1, 1);
			if (!maxDate.HasValue)
				maxDate = DateTime.Now;

			ViewData["minDate"] = minDate.Value.ToString("dd/MM/yyyy");
			ViewData["maxDate"] = maxDate.Value.ToString("dd/MM/yyyy");
			var result = await _salesrecordservice.FindByDateGroupingAsync(minDate, maxDate);
			return View(result);
		}
	}
}
