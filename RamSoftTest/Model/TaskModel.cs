using System.ComponentModel.DataAnnotations;

namespace RamSoftTest.Model
{
    public class TaskModel
    {
        [Required(ErrorMessage ="Title is requried")]
        public string Title { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }
        public string? Priority { get; set; }
        public DateTime? DueDate { get; set; }
        public string? AssignTo { get; set; }
        public string? ReportTo { get; set; }

    }
}
