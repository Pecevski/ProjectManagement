using PMApp.Data.Entities;
using PMApp.Models.DTO.Requests;
using PMApp.Models.DTO.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMApp.WEB.Mappers
{
    public static class UserMapper
    {
        public static UserResponse MapUser(User user)
        {
            var userResponse = new UserResponse()
            {
                Id = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
            };
            return userResponse;
        }

        public static User MapUserRequest(UserRequest userRequest)
        {
            var user = new User()
            {
                UserName = userRequest.UserName,
                FirstName = userRequest.FirstName,
                LastName = userRequest.LastName,
            };
            return user;
        }
    }
}
