using MessagePack;
using System.ComponentModel.DataAnnotations.Schema;

namespace capstone.Models
{
    public class ProjectInputModel
    {
        public string Project_Title { get; set; }
        public string Project_Description { get; set; }
        public DateTime Deadline { get; set; }
        public string User_Id { get; set; }
    }
}
