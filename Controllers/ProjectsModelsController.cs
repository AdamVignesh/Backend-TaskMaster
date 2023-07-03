using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using capstone.Data;
using capstone.Models;
using Microsoft.AspNetCore.Identity;
using System.Data;

namespace capstone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsModelsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<UserModel> _userManager;

        public ProjectsModelsController(ApplicationDbContext context,UserManager<UserModel> userManager)
        {
            _context = context;
            _userManager = userManager;

        }

        // GET: api/ProjectsModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectsModel>>> GetProjects()
        {
            if (_context.Projects == null)
            {
              return NotFound();
            }
            return await _context.Projects.ToListAsync();
        }

        // GET: api/ProjectsModels/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProjectsModel(string id)
        {
            var projects = await _context.Projects.Where(p => p.User.Id == id).ToListAsync();

            //var projectsModel = await _context.Projects.FindAsync().Where(User=>User.created_By=id);

            if (projects == null)
            {
                return NotFound();
            }

            return Ok(new {projects});
        }

        // PUT: api/ProjectsModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProjectsModel(int id, int progress)
        {
            try
            {
                var projectModel = _context.Projects.FirstOrDefault(t => t.Project_id == id);

                if (projectModel != null)
                {
                    projectModel.Project_Progress = progress;
                    _context.SaveChanges();
                }
                return Ok("Updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        // POST: api/ProjectsModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostProjectsModel(ProjectInputModel projectsInputModel)
        {
          if (_context.Projects == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Projects'  is null.");
          } 
            var user = await _userManager.FindByIdAsync(projectsInputModel.User_Id);

            var projectsModel = new ProjectsModel
             { 

                Project_Title = projectsInputModel.Project_Title,
                Project_Description = projectsInputModel.Project_Description,
                Deadline = projectsInputModel.Deadline,
                Project_Status = "started",
                Project_Progress = 0,
                Start_Date=DateTime.Now,
                End_Date = DateTime.Now,
                User = user,
            };

            _context.Projects.Add(projectsModel);
            await _context.SaveChangesAsync();

            return Ok(new {createdProject=projectsModel});
        }

        // DELETE: api/ProjectsModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProjectsModel(int id)
        {
            if (_context.Projects == null)
            {
                return NotFound();
            }
            var projectsModel = await _context.Projects.FindAsync(id);
            if (projectsModel == null)
            {
                return NotFound();
            }

            _context.Projects.Remove(projectsModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProjectsModelExists(int id)
        {
            return (_context.Projects?.Any(e => e.Project_id == id)).GetValueOrDefault();
        }
    }
}
