using Microsoft.EntityFrameworkCore;
using PMApp.Data;
using PMApp.Data.Entities;
using PMApp.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMApp.Services
{
    public class WorkLogService : ILogService
    {
        private readonly PMDbContext _context;

        public WorkLogService(PMDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<WorkLog>> GetAll(string taskId)
        {
            return await _context.WorkLogs.Where(w => w.TaskId == taskId).ToListAsync();
        }

        public async Task<WorkLog> GetById(string id)
        {
            return await _context.WorkLogs.SingleOrDefaultAsync(w => w.Id == id);
        }

        public async Task<bool> Create(WorkLog workLog)
        {
            await _context.WorkLogs.AddAsync(workLog);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(string id, WorkLog workLog)
        {
            WorkLog updateLog = await _context.WorkLogs.FindAsync(id);

            if (updateLog == null)
            {
                return false;
            }

            updateLog.WorkingPeriod = workLog.WorkingPeriod;
            updateLog.WorkingHours = workLog.WorkingHours;

            _context.WorkLogs.Update(updateLog);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(string id)
        {
            WorkLog removeLog = await _context.WorkLogs.FindAsync(id);
            if (removeLog == null)
            {
                return false;
            }

            _context.WorkLogs.Remove(removeLog);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
