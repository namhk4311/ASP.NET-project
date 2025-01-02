using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
	public class User
	{
		[Key]
		public int Id { get; set; }

		[MaxLength(100)]
		public string firstname { get; set; } = "";

		[MaxLength(100)]
		public string lastname { get; set; } = "";

		[MaxLength(100)]
		public string email { get; set; } = "";

		[MaxLength(100)]
		public string username { get; set; } = "";

		[MaxLength(100)]
		public string password { get; set; } = "";

		[MaxLength(100)]
		public string role { get; set; } = "";

		[MaxLength(100)]
		public string phone { get; set; } = "";

		public DateTime createdAccountDate { get; set; }

		public virtual ICollection<Class> Classes { get; set; }
	}
}
