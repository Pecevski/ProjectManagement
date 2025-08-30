using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PMApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMApp.Data
{
    public class PMDbContext : IdentityDbContext<User>
    {
        public PMDbContext(DbContextOptions<PMDbContext> options)
            :base(options)
        {

        }

        public DbSet<Team> Teams { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectTask> ProjectTasks { get; set; }
        public DbSet<WorkLog> WorkLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // User Model
            modelBuilder
               .Entity<User>()
               .ToTable("Users")
               .HasKey(u => u.Id);

            modelBuilder
               .Entity<User>()
               .Property(u => u.FirstName)
               .HasMaxLength(20)
               .IsRequired();

            modelBuilder
               .Entity<User>()
               .Property(u => u.LastName)
               .HasMaxLength(20)
               .IsRequired();

            modelBuilder
                .Entity<User>()
                .HasMany(u => u.Teams)
                .WithMany(u => u.TeamMembers)
                .UsingEntity(join => join.ToTable("UsersTeams"));


            //Team Model
            modelBuilder
                .Entity<Team>()
                .ToTable("Teams")
                .HasKey(t => t.Id);

            modelBuilder
                .Entity<Team>()
                .Property(t => t.TeamName)
                .HasMaxLength(20)
                .IsRequired();

            modelBuilder
              .Entity<Team>()
              .HasOne(t => t.TeamCreator)
              .WithMany(u => u.CreatedTeams)
              .HasForeignKey(t => t.TeamCreatorId)
              .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder
                .Entity<Team>()
                .HasOne(t => t.AssignedProject)
                .WithMany(p => p.Teams)
                .HasForeignKey(t => t.ProjectId)
                .OnDelete(DeleteBehavior.ClientCascade);


            // Project Model
            modelBuilder
                .Entity<Project>()
                .ToTable("Projects")
                .HasKey(p => p.Id);

            modelBuilder
                .Entity<Project>()
                .Property(p => p.Title)
                .HasMaxLength(20)
                .IsRequired();

            modelBuilder
               .Entity<Project>()
               .HasOne(p => p.Owner)
               .WithMany(u => u.CreatedProjects)
               .HasForeignKey(p => p.OwnerId)
               .OnDelete(DeleteBehavior.ClientCascade);


            // ProjectTask Model
            modelBuilder
                .Entity<ProjectTask>()
                .ToTable("ProjectTasks")
                .HasKey(pt => pt.Id);

            modelBuilder
                .Entity<ProjectTask>()
                .Property(pt => pt.Title)
                .HasMaxLength(20)
                .IsRequired();

            modelBuilder
                .Entity<ProjectTask>()
                .Property(pt => pt.Status)
                .IsRequired();

            modelBuilder
               .Entity<ProjectTask>()
               .HasOne(pt => pt.TaskCreator)
               .WithMany(u => u.CreatedTasks)
               .HasForeignKey(pt => pt.TaskCreatorId)
               .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder
               .Entity<ProjectTask>()
               .HasOne(tp => tp.AssignedUser)
               .WithMany(u => u.ProjectTasks)
               .HasForeignKey(tp => tp.AssignedUserId)
               .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder
              .Entity<ProjectTask>()
              .HasOne(pt => pt.Project)
              .WithMany(p => p.ProjectTasks)
              .HasForeignKey(pt => pt.ProjectId)
              .OnDelete(DeleteBehavior.ClientCascade);


            // WorkLog Model
            modelBuilder
                .Entity<WorkLog>()
                .ToTable("WorkLogs")
                .HasKey(w => w.Id);

            modelBuilder
                .Entity<WorkLog>()
                .Property(w => w.WorkingPeriod)
                .IsRequired();

            modelBuilder
               .Entity<WorkLog>()
               .Property(w => w.WorkingHours)
               .IsRequired();

            modelBuilder
              .Entity<WorkLog>()
              .HasOne(w => w.ProjectTask)
              .WithMany(pt => pt.WorkLogs)
              .HasForeignKey(w => w.TaskId)
              .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder
             .Entity<WorkLog>()
             .HasOne(w => w.User)
             .WithMany(u => u.WorkLogs)
             .HasForeignKey(w => w.UserId)
             .OnDelete(DeleteBehavior.ClientCascade);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}
