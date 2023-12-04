using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RamSoftTest.Model
{
    public class TaskManager
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        //[Column(TypeName = "nvarchar(MAX)")]
        [Required]
        public string ? Title { get; set; }
       // [Column(TypeName = "nvarchar(MAX)")]
        public string? Description { get; set; }
        public string ? Status { get; set; }
        public string? Priority { get; set; }
        public DateTime? DueDate { get; set; }
        public string? AssignTo { get; set; }
        public string? ReportTo { get; set; }

    }
}
