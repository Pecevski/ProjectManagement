using PMApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMApp.Services.Contracts
{
    public interface ITeamService : IServices<Team>
    {
        public Task<IEnumerable<Team>> GetAll();
        public Task<bool> AssignUserToTeam(User user, string teamId);
        public Task<bool> RemoveUserFromTeam(User user, string teamId);
    }
}
