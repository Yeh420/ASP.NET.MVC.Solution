using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Products.Models.ViewModels
{
	public class ProdcutIndexVM
	{
		public int Id { get; set; }

		//public int CategoryId { get; set; }
		public int CategoryName { get; set; }

		[Required]
		[StringLength(50)]
		public string Name { get; set; }

		[Required]
		[StringLength(3000)]
		public string Description { get; set; }

		public int Price { get; set; }

		public bool Status { get; set; }

		//[Required]
		//[StringLength(70)]
		//public string ProductImage { get; set; }

		public int Stock { get; set; }

	}
}