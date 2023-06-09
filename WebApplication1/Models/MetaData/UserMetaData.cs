using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApplication1.Models.MetaData;

namespace WebApplication1.Models.EFModels
{
	[MetadataType(typeof(UserMetaData))]

	public partial class User
	{

	}
}
	namespace WebApplication1.Models.MetaData
{
	public class UserMetaData
	{
		public int Id { get; set; }

		[Display(Name = "姓名")]
		//[Required]
		//[StringLength(50)]
		public string Name { get; set; }

		[Display(Name = "帳號")]
		//[Required]
		//[StringLength(50)]
		public string Account { get; set; }

		[Display(Name = "密碼")]
		//[Required]
		//[StringLength(50)]
		public string Password { get; set; }

		[Display(Name = "生日")]
		[DisplayFormat(DataFormatString ="{0:yyyy/MM/dd}",ApplyFormatInEditMode =true)]
		public DateTime? DateOfBirth { get; set; }

		[Display(Name = "身高")]
		public int? Height { get; set; }

		[Display(Name = "電子信箱")]
		//[StringLength(256)]
		public string Email { get; set; }
	}
}