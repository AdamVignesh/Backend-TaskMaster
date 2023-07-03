using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace capstone.Models
{
    public class TasksModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int task_id { get; set; }
        [Required]
        public string task_title { get; set; }
        [Required]
        public string task_description { get; set;}
        
        public string file_url { get; set;}

        [Required]
        public DateTime deadline { get; set;}

        [Required]
        public DateTime start_date { get; set;}

        [Required]
        public DateTime end_date { get; set;}

        [Required]
        public string status { get; set;}
        [Required]
        public int weightage { get; set;}

        [Required]
        public UserModel User { get; set; }



    }
}
