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
		[Display(Name="商品分類")]
		public string CategoryName { get; set; }

		[Display(Name = "商品名稱")]
		public string Name { get; set; }

		public string Description { get; set; }

		[Display(Name = "描述")]
		public string DescriptionText { get
			{
				return Description.Length >= 15 ? Description.Substring(0,15)+"...": Description;
			}
		}

		[Display(Name = "售價")]
		[DisplayFormat(DataFormatString ="{0:#,#}")]
		public int Price { get; set; }

		public bool Status { get; set; }

		[Display(Name = "是否上架")]
		public string StatusText { get {
				return Status? "上架中" : "已下架";
			} }

		[Display(Name = "庫存量")]
		[DisplayFormat(DataFormatString = "{0:#,#}")]
		public int Stock { get; set; }

	}

}