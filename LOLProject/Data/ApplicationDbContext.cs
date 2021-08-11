using LOLProject.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LOLProject.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        //declare all models so the db can work with them and so can the rest of the app
        public DbSet<Post> Posts { get; set; }
        public DbSet<Champion> Champions { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Role> Roles { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<LOLProject.Models.Champion> Champion { get; set; }
    }
}
