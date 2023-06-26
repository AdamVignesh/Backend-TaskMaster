using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace capstone.Models
{
    public class UserProjectRelation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int User_Project_Relation_id { get; set; }
        
        public ProjectsModel Projects { get; set; }
        public UserModel User { get; set; }
    }
}
