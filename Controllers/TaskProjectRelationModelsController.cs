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
    public class TaskProjectRelationModelsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TaskProjectRelationModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/TaskProjectRelationModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskProjectRelationModel>>> GetTaskProjectRelationTable()
        {
          if (_context.TaskProjectRelationTable == null)
          {
              return NotFound();
          }
            return await _context.TaskProjectRelationTable.ToListAsync();
        }

        // GET: api/TaskProjectRelationModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskProjectRelationModel>> GetTaskProjectRelationModel(int id)
        {
          if (_context.TaskProjectRelationTable == null)
          {
              return NotFound();
          }
            var tasksOfprojectId =  await _context.TaskProjectRelationTable.Include(p => p.Projects).Include(p => p.Tasks).Where(p => p.Projects.Project_id == id).Include(p=>p.Tasks.User).Select(p => p.Tasks).ToListAsync();

            if (tasksOfprojectId == null)
            {
                return NotFound();
            }

            return Ok(tasksOfprojectId);
        }

        // PUT: api/TaskProjectRelationModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTaskProjectRelationModel(int id, TaskProjectRelationModel taskProjectRelationModel)
        {
            if (id != taskProjectRelationModel.Task_Project_Relation_id)
            {
                return BadRequest();
            }

            _context.Entry(taskProjectRelationModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskProjectRelationModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TaskProjectRelationModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TaskProjectRelationModel>> PostTaskProjectRelationModel(TaskProjectRelationModel taskProjectRelationModel)
        {
          if (_context.TaskProjectRelationTable == null)
          {
              return Problem("Entity set 'ApplicationDbContext.TaskProjectRelationTable'  is null.");
          }
            _context.TaskProjectRelationTable.Add(taskProjectRelationModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTaskProjectRelationModel", new { id = taskProjectRelationModel.Task_Project_Relation_id }, taskProjectRelationModel);
        }

        // DELETE: api/TaskProjectRelationModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskProjectRelationModel(int id)
        {
            if (_context.TaskProjectRelationTable == null)
            {
                return NotFound();
            }
            var taskProjectRelationModel = await _context.TaskProjectRelationTable.FindAsync(id);
            if (taskProjectRelationModel == null)
            {
                return NotFound();
            }

            _context.TaskProjectRelationTable.Remove(taskProjectRelationModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TaskProjectRelationModelExists(int id)
        {
            return (_context.TaskProjectRelationTable?.Any(e => e.Task_Project_Relation_id == id)).GetValueOrDefault();
        }
    }
}
