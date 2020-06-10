﻿using SalesWebMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMVC.Services
{
	public class SellerServive
	{
		private readonly SalesWebMVCContext _context;

		public SellerServive(SalesWebMVCContext context)
		{
			_context = context;
		}

		public List<Seller> FindAll()
		{
			return _context.Seller.ToList();
		}

		public void Insert(Seller obj)
		{
			_context.Add(obj);
			_context.SaveChanges();
		}
	}
}
