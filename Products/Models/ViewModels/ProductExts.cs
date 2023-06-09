using Products.Models.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Products.Models.ViewModels
{
	public static class ProductExts
	{
		public static ProdcutIndexVM ToIndexVM(this Product entity)
		{
			return new ProdcutIndexVM
			{
				Id = entity.Id,
				CategoryName = entity.Category.Name,
				Name = entity.Name,
				Description = entity.Description,
				Price = entity.Price,
				Status = entity.Status,
				Stock = entity.Stock
			};
		}
		public static ProductCreateVM ToCreateVM(this Product entity)
		{
			return new ProductCreateVM
			{
				Id = entity.Id,
				CategoryId = entity.Category.Id,
				Name = entity.Name,
				Description = entity.Description,
				Price = entity.Price,
				Status = entity.Status,
				ProductImage= entity.ProductImage,
				Stock = entity.Stock
			};
		}
		public static ProductEditVM ToEditVM(this Product entity)
		{
			return new ProductEditVM
			{
				Id = entity.Id,
				CategoryId = entity.Category.Id,
				Name = entity.Name,
				Description = entity.Description,
				Price = entity.Price,
				Status = entity.Status,
				Stock = entity.Stock
			};
		}

	}
}