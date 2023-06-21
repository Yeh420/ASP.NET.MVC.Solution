using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Products.Models.ViewModels
{
	public class ProductEditVM
	{
		public int Id { get; set; }

		[Display(Name = "商品分類")]
		public int CategoryId { get; set; }

		[Display(Name = "商品名稱")]
		[Required]
		[StringLength(50)]
		public string Name { get; set; }

		[Display(Name = "描述")]
		[Required]
		[StringLength(3000)]
		public string Description { get; set; }

		[Display(Name = "售價")]
		public int Price { get; set; }

		[Display(Name = "是否上架")]
		public bool Status { get; set; }

		//[Required]
		//[StringLength(70)]
		//public string ProductImage { get; set; }

		[Display(Name = "庫存量")]
		public int Stock { get; set; }

	}
}