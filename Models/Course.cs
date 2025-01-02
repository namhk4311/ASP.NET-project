using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
	public class Course
	{
		[Key]
		public int Id { get; set; }

		[MaxLength(100)]
		public string nameCourse { get; set; } = "";

		[MaxLength(100)]
		public string codeCourse { get; set; } = "";

		public int duration { get; set; }

		public virtual ICollection<Class> Classes { get; set; }
	}
}
