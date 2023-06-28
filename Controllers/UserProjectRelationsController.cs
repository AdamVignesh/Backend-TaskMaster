﻿using System;
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
    public class UserProjectRelationsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UserProjectRelationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/UserProjectRelations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserProjectRelation>>> GetUserProjectRelation()
        {
          if (_context.UserProjectRelation == null)
          {
              return NotFound();
          }
            return await _context.UserProjectRelation.ToListAsync();
        }

        // GET: api/UserProjectRelations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserProjectRelation>> GetUserProjectRelation(int id)
        {
          if (_context.UserProjectRelation == null)
          {
              return NotFound();
          }
            var userProjectRelation = await _context.UserProjectRelation.FindAsync(id);

            if (userProjectRelation == null)
            {
                return NotFound();
            }

            return userProjectRelation;
        }

        // PUT: api/UserProjectRelations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserProjectRelation(int id, UserProjectRelation userProjectRelation)
        {
            if (id != userProjectRelation.User_Project_Relation_id)
            {
                return BadRequest();
            }

            _context.Entry(userProjectRelation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserProjectRelationExists(id))
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

        // POST: api/UserProjectRelations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserProjectRelation>> PostUserProjectRelation(UserProjectRelationInput UserProjectRelationInput)
        {
            int members_iterator =0;
            UserModel[] members = new UserModel[UserProjectRelationInput.members.Length];

            ProjectsModel project = await _context.Projects.FindAsync(UserProjectRelationInput.project_id);

            foreach (string user_id in UserProjectRelationInput.members)
            {
                UserModel User = await _context.Users.FindAsync(user_id);
                members[members_iterator++] = User;
            }

            

            foreach (UserModel user in members)
            {
                UserProjectRelation userProjectRelation = new UserProjectRelation();

                userProjectRelation.Projects = project;
                userProjectRelation.User = user;    

                _context.UserProjectRelation.Add(userProjectRelation);
            }
                await _context.SaveChangesAsync();

            return Ok("GetUserProjectRelation");

            //return CreatedAtAction("GetUserProjectRelation", new { id = userProjectRelation.User_Project_Relation_id }, userProjectRelation);
        }

        // DELETE: api/UserProjectRelations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserProjectRelation(int id)
        {
            if (_context.UserProjectRelation == null)
            {
                return NotFound();
            }
            var userProjectRelation = await _context.UserProjectRelation.FindAsync(id);
            if (userProjectRelation == null)
            {
                return NotFound();
            }

            _context.UserProjectRelation.Remove(userProjectRelation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserProjectRelationExists(int id)
        {
            return (_context.UserProjectRelation?.Any(e => e.User_Project_Relation_id == id)).GetValueOrDefault();
        }
    }
}