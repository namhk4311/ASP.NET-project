using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
	public class Class
	{
		[Key]
		public int Id { get; set; }

		public string startTime { get; set; } = "";

		[MaxLength(50)]
		public string Room { get; set; } = "";

		[MaxLength(50)]
		public string DayInWeek { get; set; } = "";


		[ForeignKey("Course")]
		public int CourseId { get; set; }
		public virtual Course Course { get; set; }


		//teacher

		[ForeignKey("User")]
		public int UserId { get; set; }
		public virtual User User { get; set; }

		public virtual ICollection<Registration> Registrations { get; set; }

	}
}
