using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace capstone.Models
{
    public class TaskProjectRelationModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Task_Project_Relation_id { get; set; }

        public ProjectsModel Projects { get; set; }
        public TasksModel Tasks { get; set; }

    }
}
