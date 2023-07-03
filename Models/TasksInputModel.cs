namespace capstone.Models
{
    public class TasksInputModel
    {
        public string title { get; set; }
        public string description { get; set; }
        public DateTime deadline { get; set; }
        
        public int weightage { get; set; }

        public string user_id { get; set; }

        public int project_id { get; set; }
    }
}
