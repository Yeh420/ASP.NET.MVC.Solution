using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Products.Models.ViewModels
{
	public class ProductImageEditVM
	{
		public int Id { get; set; }

		[Required]
		[StringLength(70)]
		public string ProductImage { get; set; }
	}
}