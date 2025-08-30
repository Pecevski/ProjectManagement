using Microsoft.EntityFrameworkCore;
using PMApp.Data;
using PMApp.Data.Entities;
using PMApp.Data.Enums;
using PMApp.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMApp.Services
{
    public class TaskService : ITaskService
    {
        private readonly PMDbContext _context; 

        public TaskService(PMDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProjectTask>> GetAll(string projectId)
        {
            return await _context.ProjectTasks.Where(t => t.ProjectId == projectId).ToListAsync();
        }

        public async Task<ProjectTask> GetById(string id)
        {
            return await _context.ProjectTasks.SingleOrDefaultAsync(t => t.Id == id);
        }

        public async Task<bool> Create(ProjectTask task)
        {

            if (await _context.ProjectTasks.SingleOrDefaultAsync(t => t.Title == task.Title) != null)
            {
                return false;
            }

            await _context.ProjectTasks.AddAsync(task);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Update(string id, ProjectTask task)
        {
            ProjectTask updateTask = await _context.ProjectTasks.FindAsync(id);

            if (updateTask == null)
            {
                return false;
            }

            updateTask.Title = task.Title;

            _context.ProjectTasks.Update(updateTask);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(string id)
        {
            ProjectTask removeTask = await _context.ProjectTasks.FindAsync(id);

            if (removeTask == null)
            {
                return false;
            }

            _context.ProjectTasks.Remove(removeTask);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> AssignTaskToUser(string taskId, string assignUserId, string creatorId)
        {
            var assignTask = await _context.ProjectTasks.SingleOrDefaultAsync(t => t.Id == taskId);

            if (assignTask == null)
            {
                return false;
            }
            if (assignTask.TaskCreatorId != creatorId)
            {
                return false;
            }
            if (assignTask.AssignedUserId != assignUserId)
            {
                assignTask.AssignedUserId = assignUserId;
                await _context.SaveChangesAsync();
            }
            return true;
        }

        public async Task<bool> CompleteTask(string taskId, string creatorId, string assignUserId)
        {
            var task =  await _context.ProjectTasks.SingleOrDefaultAsync(t => t.Id == taskId);
            if (task == null)
            {
                return false;
            }
            if (task.TaskCreatorId != creatorId && task.AssignedUserId != assignUserId)
            {
                return false;
            }

            task.Status = Status.Complete;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
