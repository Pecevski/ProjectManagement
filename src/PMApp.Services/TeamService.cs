using Microsoft.EntityFrameworkCore;
using PMApp.Data;
using PMApp.Data.Entities;
using PMApp.Services.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMApp.Services
{
    public class TeamService : ITeamService
    {
        private readonly PMDbContext _context;
        public TeamService(PMDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Team>> GetAll()
        {
            return await _context.Teams.ToListAsync();
        }
        public async Task<Team> GetById(string id)
        {
            return await _context.Teams.SingleOrDefaultAsync(t => t.Id == id);
        }
        public async Task<bool> Create(Team team)
        {
            if(await _context.Teams.SingleOrDefaultAsync(t => t.TeamName == team.TeamName) != null)
            {
                return false;
            }
            await _context.Teams.AddAsync(team);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Update(string teamId, Team team)
        {
            Team updateTeam = await _context.Teams.FindAsync(teamId);

            if (updateTeam == null)
            {
                return false;
            }

            updateTeam.TeamName = team.TeamName;

            _context.Teams.Update(updateTeam);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(string id)
        {

            Team removeTeam = await _context.Teams.FindAsync(id);

            if (removeTeam == null)
            {
                return false;
            }

            _context.Teams.Remove(removeTeam);
            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<bool> AssignUserToTeam(User user, string teamId)
        {
            Team team= await _context.Teams.SingleOrDefaultAsync(t => t.Id == teamId);

            if (team == null || !team.TeamMembers.Any(u => u.Id == user.Id))
            {
                return false;
            }
            team.TeamMembers.Add(user);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> RemoveUserFromTeam(User user, string teamId)
        {

            Team team = await _context.Teams.SingleOrDefaultAsync(t => t.Id == teamId);

            if (team == null || !team.TeamMembers.Any(u => u.Id == user.Id))
            {
                return false;
            }
            team.TeamMembers.Remove(user);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
