using Microsoft.EntityFrameworkCore;
using PMApp.Data;
using PMApp.Data.Entities;
using PMApp.Services.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMApp.Services
{
    public class ProjectService : IProjectService
    {
        private readonly PMDbContext _context;
        public ProjectService(PMDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Project>> GetAll()
        {
            return await _context.Projects.ToListAsync();
        }

        public async Task<Project> GetById(string id)
        {
            return await _context.Projects.SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task<bool> Create(Project project)
        {
            if (await _context.Projects.SingleOrDefaultAsync(t => t.Title == project.Title) != null)
            {
                return false;
            }

            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Update(string id, Project project)
        {
            Project updateProject = await _context.Projects.FindAsync(id);

            if (updateProject == null)
            {
                return false;
            }

            updateProject.Title = project.Title;

            _context.Projects.Update(updateProject);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(string id)
        {
            Project removeProject = await _context.Projects.FindAsync(id);

            if (removeProject == null)
            {
                return false;
            }

            _context.Projects.Remove(removeProject);
            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<bool> AssignTeamToProject(Team team, string projectId, User owner)
        {
            Project project = await _context.Projects.SingleOrDefaultAsync(p => p.Id == projectId);
            Team assignTeam = await _context.Teams.SingleOrDefaultAsync(t => t.Id == team.Id);

            if (project == null || assignTeam == null || project.Owner != owner)
            {
                return false;
            }

            project.Teams.Add(assignTeam);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
