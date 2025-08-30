using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PMApp.Data.Entities;
using PMApp.Models.DTO.Requests;
using PMApp.Models.DTO.Responses;
using PMApp.Services.Contracts;
using PMApp.WEB.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMApp.WEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;
        private readonly ITeamService _teamService;
        private readonly IUserService _userService;

        public ProjectsController(IProjectService projectServic, ITeamService teamService, IUserService userService)
        {
            _projectService = projectServic;
            _teamService = teamService;
            _userService = userService;
        }


        [HttpGet]
        [Authorize]
        [Route("All")]
        public async Task<ActionResult<IEnumerable<ProjectResponse>>> GetAll()
        {
            if (ModelState.IsValid)
            {
                List<ProjectResponse> projects = new List<ProjectResponse>();

                List<Project> allProjects = (List<Project>)await  _projectService.GetAll();

                foreach (var project in allProjects)
                {
                    ProjectResponse projectResponse = ProjectMapper.MapProject(project);
                    projects.Add(projectResponse);
                }

                return projects;
            }

            return NoContent();
        }

        [HttpGet]
        [Authorize]
        [Route("{id}")]
        public async Task<ActionResult<ProjectResponse>> Get(string id)
        {
            Project project = await _projectService.GetById(id);

            if (project == null)
            {
                return NotFound();
            }

            return ProjectMapper.MapProject(project);
        }

        [HttpPost]
        [ActionName(nameof(Get))]
        public async Task<IActionResult> Post(ProjectRequest projectRequest)
        {
            Project project = ProjectMapper.MapProjectRequest(projectRequest);
            User currentUser = await _userService.GetUserByUserName(User.Identity.Name);
            project.Owner = currentUser;

            if (project == null)
            {
                return NotFound();
            }

            bool isCreated = await _projectService.Create(project);

            if (isCreated && ModelState.IsValid)
            {
                Project projectFromDB = await _projectService.GetById(project.Id);
                return CreatedAtAction("Get", "Projects", new { id = projectFromDB.Id }, projectRequest);
            }

            return BadRequest("Project already exist.");
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Put(string id, ProjectRequest projectRequest)
        {
            Project projectFromDB = await _projectService.GetById(id);
            User currentUser = await _userService.GetUserByUserName(User.Identity.Name);

            if (projectFromDB == null)
            {
                return NotFound();
            }

            if (projectFromDB.Owner != currentUser)
            {
                return Unauthorized();
            }

            if (ModelState.IsValid)
            {
                Project updateProject = ProjectMapper.MapProjectRequest(projectRequest);
                await _projectService.Update(id, updateProject);
                return Ok("Project updated.");
            }

            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(string id)
        {

            Project project = await _projectService.GetById(id);

            if (project == null)
            {
                return NotFound();
            }

            if (await _projectService.Delete(project.Id))
            {
                return Ok("Project deleted.");
            }

            return NoContent();
        }

        [HttpPut]
        [Route("Assign/{teamId}")]
        public async Task<IActionResult> AssigningTeamToProject(string teamId, string projectId)
        {
            Team team = await _teamService.GetById(teamId);
            Project project = await _projectService.GetById(projectId);
            User currentUser = await _userService.GetUserByUserName(User.Identity.Name);  

            if (team == null || project == null)
            {
                return BadRequest();
            }
            if (project.Owner == currentUser)
            {
                await _projectService.AssignTeamToProject(team, projectId, currentUser);
                return Ok("Team assigned to Project.");
            }

            return NoContent();
        }

    }
}
