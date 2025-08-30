using PMApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMApp.Services.Contracts
{
    public interface ITaskService : IServices<ProjectTask>
    {
        public Task<IEnumerable<ProjectTask>> GetAll(string projectId);
        public Task<bool> AssignTaskToUser(string taskId, string userId, string creatorId);
        public Task<bool> CompleteTask(string taskId, string creatorId, string assignUserId);
    }
}
