using PMApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMApp.Services.Contracts
{
    public interface ILogService : IServices<WorkLog>
    {
        public Task<IEnumerable<WorkLog>> GetAll(string taskId);
    }
}
