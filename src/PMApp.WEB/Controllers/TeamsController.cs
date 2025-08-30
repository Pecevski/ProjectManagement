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
    [Authorize(Roles = "Admin, Manager")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly ITeamService _teamService;
        private readonly IUserService _userService;

        public TeamsController(ITeamService teamService, IUserService userService)
        {
            _teamService = teamService;
            _userService = userService;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<TeamResponse>> Get(string id)
        {
            Team team = await _teamService.GetById(id);

            if (team == null)
            {
                return BadRequest();
            }

            return TeamMapper.MapTeam(team);
        }

        [HttpGet]
        [Route("All")]
        public async Task<ActionResult<IEnumerable<TeamResponse>>> GetAll()
        {
            if (ModelState.IsValid)
            {
                List<TeamResponse> teams = new List<TeamResponse>();

                List<Team> allTeams = (List<Team>)await _teamService.GetAll();

                foreach (var team in allTeams)
                {
                    TeamResponse TeamResponse = TeamMapper.MapTeam(team);
                    teams.Add(TeamResponse);
                }

                return teams;
            }

            return NoContent();
        }

        [HttpPost]
        [ActionName(nameof(Get))]
        public async Task<IActionResult> Post(TeamRequest teamRequest)
        {
            Team team = TeamMapper.MapTeamRequest(teamRequest);
            User currentUser = await _userService.GetUserByUserName(User.Identity.Name);
            team.TeamCreator = currentUser;

            if (team == null)
            {
                return NotFound();
            }

            bool isCreated = await _teamService.Create(team);

            if (isCreated && ModelState.IsValid)
            {
                Team teamFromDB = await _teamService.GetById(team.Id);
                return CreatedAtAction("Get", "Teams", new { id = teamFromDB.Id }, teamRequest);
            }

            return BadRequest("Team already exist.");
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Put(string id, TeamRequest teamRequest)
        {
            var team = await _teamService.GetById(id);
            if (team == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var updateTeam = TeamMapper.MapTeamRequest(teamRequest);
                await _teamService.Update(id, updateTeam);
                return Ok("Team updated.");
            }

            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            Team team = await _teamService.GetById(id);
            if (team == null)
            {
                return NotFound();
            }

            await _teamService.Delete(team.Id);
            return NoContent();
        }

        [HttpPut]
        [Route("Assign/User/{userId}")]
        public async Task<IActionResult> AssignUserToTeam(string userId, string teamId)
        {

            User user = await _userService.GetById(userId);
            Team team = await _teamService.GetById(teamId);

            if (user == null)
            {
                return NotFound();
            }

            if (team.TeamCreator != null)
            {
                await _teamService.AssignUserToTeam(user, teamId);
                return Ok("User is assigned to Team.");
            }

            return NoContent();
        }

        [HttpPut]
        [Route("Remove/User/{userId}")]
        public async Task<IActionResult> RemoveUserFromTeam(string userId, string teamId)
        {
            User user = await _userService.GetById(userId);
            if (user == null)
            {
                return BadRequest();
            }

            await _teamService.RemoveUserFromTeam(user, teamId);

            return NoContent();
        }
    }
}
