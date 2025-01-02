using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class ClassInput
    {

        public string startTime { get; set; } = "";

        [MaxLength(50)]
        public string Room { get; set; } = "";

        [MaxLength(50)]
        public string DayInWeek { get; set; } = "";

        [MaxLength(50)]
        public string CourseCode { get; set; } = "";

        public int LecturerID { get; set; }
    }
}
