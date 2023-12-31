﻿using capstone.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace capstone.Data
{
    public class ApplicationDbContext: IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<UserModel> Users { get; set; }

        public DbSet<ProjectsModel> Projects { get; set; }
        public DbSet<UserProjectRelation> UserProjectRelation { get; set; }

        public DbSet<TasksModel> UserTasks { get; set; }
       
        public DbSet<TaskProjectRelationModel> TaskProjectRelationTable { get; set; }



    }
}
