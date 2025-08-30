using PMApp.Data.Entities;
using PMApp.Models.DTO.Requests;
using PMApp.Models.DTO.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMApp.WEB.Mappers
{
    public static class TaskMapper
    {
        public static ProjectTaskResponse MapTask(ProjectTask projectTask)
        {
            var taskResponse = new ProjectTaskResponse()
            {
                Id = projectTask.Id,
                Title = projectTask.Title,
                Status = projectTask.Status,
            };
            return taskResponse;
        }

        public static ProjectTask MapTaskRequest(ProjectTaskRequest taskRequest)
        {
            var projectTask = new ProjectTask()
            {
                Title = taskRequest.Title,
                Status = taskRequest.Status,
            };
            return projectTask;
        }
    }
}
