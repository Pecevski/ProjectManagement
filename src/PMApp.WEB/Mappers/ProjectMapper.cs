using PMApp.Data.Entities;
using PMApp.Models.DTO.Requests;
using PMApp.Models.DTO.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMApp.WEB.Mappers
{
    public static class ProjectMapper
    {

        public static ProjectResponse MapProject(Project project)
        {
            var projectResponse = new ProjectResponse()
            {
                Id = project.Id,
                Title = project.Title,
            };
            return projectResponse;
        }

        public static Project MapProjectRequest(ProjectRequest projectRequest)
        {
            var project = new Project()
            {
                Title = projectRequest.Title,
            };
            return project;
        }
    }
}
