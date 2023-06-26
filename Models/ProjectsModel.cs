using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace capstone.Models
{
    public class ProjectsModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Project_id { get; set; }
        public string Project_Title { get; set; }
        public string Project_Description { get; set;}
        public DateTime Deadline { get; set; }
        public string Project_Status { get; set;}
        public int Project_Progress { get; set;}
        public DateTime Start_Date { get; set; }
        public DateTime End_Date { get; set;}
        public UserModel User { get; set; }

    }
}
