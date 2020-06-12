using System;
using System.ComponentModel.DataAnnotations;

namespace SalesWebMVC.Models
{
	public class SalesRecord
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "{0} required")]
		[DataType(DataType.Date)]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
		public DateTime Date { get; set; }

		[Required(ErrorMessage ="Ammount has to be provided.")]
		[Range(5.00, 999000000.00, ErrorMessage = "{0} must be from {1} to {2}")]
		public double Amount { get; set; }
		public SaleStatus Status { get; set; }
		public Seller Seller { get; set; }

		public SalesRecord()
		{
		}

		public SalesRecord(int id, DateTime date, double amount, SaleStatus status, Seller seller)
		{
			Id = id;
			Date = date;
			Amount = amount;
			Status = status;
			Seller = seller;
		}
	}
}
