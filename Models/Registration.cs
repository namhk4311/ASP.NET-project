using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
	public class Registration
	{
		[Key]
		public int Id {  get; set; }

		[MaxLength(100)]
		public string FirstName { get; set; } = "";

		[MaxLength(100)]
		public string LastName { get; set; } = "";

		[MaxLength(100)]
		public string Email { get; set; } = "";

		[MaxLength(100)]
		public string StudentId { get; set; } = "";

		[ForeignKey("Class")]
		public int ClassId { get; set; }
		public Class Classes { get; set; }

	}
}
