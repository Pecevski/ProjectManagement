using PMApp.Data.Entities;
using PMApp.Models.DTO.Requests;
using PMApp.Models.DTO.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMApp.WEB.Mappers
{
    public static class TeamMapper
    {
        public static TeamResponse MapTeam(Team team)
        {
            var teamResponse = new TeamResponse()
            {
                Id = team.Id,
                TeamName = team.TeamName,
            };
            return teamResponse;
        }

        public static Team MapTeamRequest(TeamRequest teamRequest)
        {
            var team = new Team()
            {
                TeamName = teamRequest.TeamName,
            };
            return team;
        }
    }
}
