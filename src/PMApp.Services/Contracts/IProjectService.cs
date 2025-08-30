using PMApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMApp.Services.Contracts
{
    public interface IProjectService : IServices<Project>
    {
        public Task<IEnumerable<Project>> GetAll();
        public Task<bool> AssignTeamToProject(Team team, string projectId, User owner);
    }
}
