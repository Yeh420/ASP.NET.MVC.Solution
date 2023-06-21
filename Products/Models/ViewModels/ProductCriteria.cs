using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Products.Models.ViewModels
{
	public class ProductCriteria
    {
        public string Name { get; set; }
        public int? CategoryId { get; set; }
        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }
    }
}