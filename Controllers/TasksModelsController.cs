using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using capstone.Data;
using capstone.Models;

namespace capstone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksModelsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TasksModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/TasksModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TasksModel>>> GetUserTasks()
        {
          if (_context.UserTasks == null)
          {
              return NotFound();
          }
            return await _context.UserTasks.ToListAsync();
        }

        // GET: api/TasksModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TasksModel>> GetTasksModel(int id)
        {
          if (_context.UserTasks == null)
          {
              return NotFound();
          }
            var tasksModel = await _context.UserTasks.FindAsync(id);

            if (tasksModel == null)
            {
                return NotFound();
            }

            return tasksModel;
        }

        [HttpGet("TasksForTheDay{userId}")]
        public async Task<ActionResult<TasksModel>> GetTasksForTheDay(string userId)
        {
            if (_context.UserTasks == null)
            {
                return NotFound();
            }
            DateTime today = DateTime.Today;
            Console.WriteLine("----------------------" + today);
            var tasksForTheDay = await _context.UserTasks.Where(t => t.User.Id == userId && t.deadline.Date == today).ToListAsync() ;

            if (tasksForTheDay == null)
            {
                return NotFound();
            }

            return Ok(tasksForTheDay);
        }


        // PUT: api/TasksModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTasksModel(int id, string updatedStatus)
        {
            try
            {
                var tasksModel = _context.UserTasks.FirstOrDefault(t => t.task_id == id);

                if (tasksModel != null)
                {
                    tasksModel.status = updatedStatus; 
                    _context.SaveChanges();  
                }
                return Ok("Updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500); 
            }
        }
        // POST: api/TasksModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TasksModel>> PostTasksModel(TasksInputModel tasksInput)
        {
            UserModel User = await _context.Users.FindAsync(tasksInput.user_id);

            var taskModel = new TasksModel
            {
                task_title = tasksInput.title,
                task_description = tasksInput.description,
                deadline = tasksInput.deadline,
                weightage = tasksInput.weightage,
                start_date=DateTime.Now,
                end_date = DateTime.Now,
                file_url = "",
                status = "Assigned",
                User = User,
            };
            _context.UserTasks.Add(taskModel);
            int rowsAffected = await _context.SaveChangesAsync();

            if(rowsAffected > 0)
            {
                ProjectsModel project = await _context.Projects.FindAsync(tasksInput.project_id);
                TasksModel task = await _context.UserTasks.FindAsync(taskModel.task_id);
                var projectTaskRelationTable = new TaskProjectRelationModel
                {
                    Projects = project,
                    Tasks = task
                };
                _context.TaskProjectRelationTable.Add(projectTaskRelationTable);
                await _context.SaveChangesAsync();

            }
            return Ok( new { id = taskModel.task_id });
        }

        // DELETE: api/TasksModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTasksModel(int id)
        {
            if (_context.UserTasks == null)
            {
                return NotFound();
            }
            var tasksModel = await _context.UserTasks.FindAsync(id);
            if (tasksModel == null)
            {
                return NotFound();
            }

            _context.UserTasks.Remove(tasksModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TasksModelExists(int id)
        {
            return (_context.UserTasks?.Any(e => e.task_id == id)).GetValueOrDefault();
        }
    }
}
