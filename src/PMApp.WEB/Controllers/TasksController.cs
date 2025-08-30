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
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;
        private readonly IUserService _userService;
        private readonly IProjectService _projectService;

        public TasksController(ITaskService taskService, IUserService userService, IProjectService projectService)
        {
            _taskService = taskService;
            _userService = userService;
            _projectService = projectService;
        }

        [HttpGet]
        [Authorize]
        [Route("All")]
        public async Task<ActionResult<List<ProjectTaskResponse>>> GetAll(string projectId)
        {
            Project tasksFromProject = await _projectService.GetById(projectId);

            if (tasksFromProject == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                List<ProjectTaskResponse> projectTask = new List<ProjectTaskResponse>();

                List<ProjectTask> allTasks = (List<ProjectTask>)await _taskService.GetAll(projectId);

                foreach (var task in allTasks)
                {
                    ProjectTaskResponse projectTaskResponse = TaskMapper.MapTask(task);
                    projectTask.Add(projectTaskResponse);
                }

                return projectTask;
            }

            return NoContent();
        }

        [HttpGet]
        [Authorize]
        [Route("{id}")]
        public async Task<ActionResult<ProjectTaskResponse>> Get(string id)
        {
            ProjectTask tasksFromDB = await _taskService.GetById(id);

            if (tasksFromDB == null)
            {
                return NotFound();
            }

            return TaskMapper.MapTask(tasksFromDB);
        }

        [HttpPost]
        [ActionName(nameof(Get))]
        public async Task<IActionResult> Create(string projectId, ProjectTaskRequest createTask)
        {
            Project tasksFromProject = await _projectService.GetById(projectId);
            ProjectTask task = TaskMapper.MapTaskRequest(createTask);
            User currentUser = await _userService.GetUserByUserName(User.Identity.Name);

            task.TaskCreator = currentUser;
            task.Project = tasksFromProject;

            if (tasksFromProject == null)
            {
                return NotFound();
            }

            bool isCreated = await _taskService.Create(task);

            if (isCreated && ModelState.IsValid)
            {

                return CreatedAtAction("Get", "ProjectTasks", new { id = task.Id }, createTask);
            }

            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> Update(ProjectTaskRequest taskRequest, string taskId)
        {
            ProjectTask tasksFromDB = await _taskService.GetById(taskId);
            User currentUser = await _userService.GetUserByUserName(User.Identity.Name);

            if (tasksFromDB == null)
            {
                return NotFound();
            }

            if (tasksFromDB.TaskCreatorId != currentUser.Id)
            {
                return Unauthorized();
            }

            if (ModelState.IsValid)
            {
                ProjectTask updateTask = TaskMapper.MapTaskRequest(taskRequest);
                await _taskService.Update(taskId, updateTask);
                return Ok("Task updated.");
            }

            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            ProjectTask tasksFromDB = await _taskService.GetById(id);
            User currentUser = await _userService.GetUserByUserName(User.Identity.Name);

            if (tasksFromDB == null)
            {
                return NotFound();
            }

            if (tasksFromDB.TaskCreatorId != currentUser.Id)
            {
                return Unauthorized();
            }

            if (await _taskService.Delete(id))
            {
                return Ok();
            }

            return NoContent();
        }

        [HttpPut]
        [Route("Assign")]
        public async Task<IActionResult> Assign(string taskId, string assignedUserId)
        {
            ProjectTask tasksFromDB = await _taskService.GetById(taskId);
            User currentUser = await _userService.GetUserByUserName(User.Identity.Name);

            if (tasksFromDB == null)
            {
                return NotFound();
            }

            if (tasksFromDB.TaskCreatorId != currentUser.Id)
            {
                return Unauthorized();
            }

            bool isAssigned = false;

            if (ModelState.IsValid)
            {
                isAssigned = await _taskService.AssignTaskToUser(taskId, assignedUserId, currentUser.Id);
            }

            if (isAssigned)
            {
                return Ok("User assigned to Task.");
            }

            return BadRequest();
        }

        [HttpPut]
        [Route("CompleteTask/{taskId}")]
        public async Task<IActionResult> CompleteTask(string taskId, string assignedUserId)
        {
            ProjectTask tasksFromDB = await _taskService.GetById(taskId);
            User currentUser = await _userService.GetUserByUserName(User.Identity.Name);

            if (tasksFromDB == null)
            {
                return NotFound();
            }

            if (tasksFromDB.TaskCreatorId != currentUser.Id && tasksFromDB.AssignedUserId != assignedUserId)
            {
                return Unauthorized();
            }

            bool isComplete = false;

            if (ModelState.IsValid)
            {
                isComplete = await _taskService.CompleteTask(taskId, currentUser.Id, assignedUserId);
            }

            if (isComplete)
            {
                return Ok("Task completed.");
            }

            return BadRequest();
        }
    }
}
