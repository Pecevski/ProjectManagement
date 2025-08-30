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
    public class WorkLogsController : ControllerBase
    {
        private readonly ILogService _workLogService;
        private readonly ITaskService _taskService;
        private readonly IUserService _userService;

        public WorkLogsController(ILogService workLogService, ITaskService taskService, IUserService userService)
        {
            _workLogService = workLogService;
            _taskService = taskService;
            _userService = userService;
        }


        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<WorkLogResponse>> Get(string id)
        {
            WorkLog logsFromDB = await _workLogService.GetById(id);

            if (logsFromDB == null)
            {
                return NotFound();
            }

            return WorkLogMapper.MapLog(logsFromDB);
        }

        [HttpPost]
        [ActionName(nameof(Get))]
        public async Task<IActionResult> Create(string taskId, WorkLogRequest createLog)
        {
            ProjectTask logsFromTask = await _taskService.GetById(taskId);
            User currentUser = await _userService.GetUserByUserName(User.Identity.Name);

            if (logsFromTask == null)
            {
                return NotFound();
            }

            WorkLog log = WorkLogMapper.MapWorkLogRequest(createLog);
            log.User = currentUser;
            log.ProjectTask = logsFromTask;

            bool isCreated = await _workLogService.Create(log);

            if (isCreated && ModelState.IsValid)
            {

                return CreatedAtAction("Get", "WorkLogs", new { id = log.Id }, null);
            }

            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> Update(WorkLogRequest logRequest, string logId)
        {
            WorkLog logFromDB =await  _workLogService.GetById(logId);
            User currentUser = await _userService.GetUserByUserName(User.Identity.Name);

            if (logFromDB == null)
            {
                return NotFound();
            }

            if (logFromDB.UserId != currentUser.Id)
            {
                return Unauthorized();
            }

            if (ModelState.IsValid)
            {
                WorkLog updateLog = WorkLogMapper.MapWorkLogRequest(logRequest);
                await _workLogService.Update(logId, updateLog);
                return Ok("WorLog updated.");
            }

            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            WorkLog logFromDB = await _workLogService.GetById(id);
            User currentUser = await _userService.GetUserByUserName(User.Identity.Name);

            if (logFromDB == null)
            {
                return NotFound();
            }

            if (logFromDB.UserId != currentUser.Id)
            {
                return Unauthorized();
            }

            if (await _workLogService.Delete(id))
            {
                return Ok();
            }

            return NoContent();
        }
    }
}
