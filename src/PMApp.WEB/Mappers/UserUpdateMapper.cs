using PMApp.Data.Entities;
using PMApp.Models.DTO.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMApp.WEB.Mappers
{
    public class UserUpdateMapper
    {
        public static User MapUserUpdateRequest(UserUpdateRequest userRequest)
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
