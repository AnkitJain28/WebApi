using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model.DBModels;
using Model.RequestModels;
using Model.ResponseModels;
using System.Runtime.CompilerServices;


namespace WebApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("users")]
    public class UserController : ControllerBase
    {
        private readonly ApiAssignmentContext context;

        public UserController(ApiAssignmentContext context)
        {
            this.context = context;
        }
        /// <summary>
        /// To get All Users
        /// </summary>
        /// <returns></returns>
        [HttpGet("getallusers")]
        public async Task<List<UserDTO>> GetAllUsers()
        {
            List<UserDTO> users = await context.Users.Select(u => new UserDTO()
            {
                Id = u.Userid,
                Name = u.Name,
                City = u.City,

            }).ToListAsync();

            return users;

        }
        /// <summary>
        /// To get Single User 
        /// </summary>
        /// <param name="id">Provide UserId here</param>
        /// <returns></returns>
        [HttpGet("getuserdetails")]
        public async Task<UserDTO> GetUserDetails(int id)
        {
            var existingUser =  context.Users.FirstOrDefault(u => u.Userid == id);
            
            UserDTO userDTO = new UserDTO()
            {
                Id = existingUser.Userid,
                Name = existingUser.Name,
                City = existingUser.City,

            };

            return userDTO;

        }



        /// <summary>
        /// To Create New User
        /// </summary>
        /// <param name="newUser">Provide User detils here</param>
        /// <returns></returns>
        [HttpPost("createusers")]
        public async Task<UserDTO> CreateUser(UserCreateRequest newUser)
        {
            User user = new User()
            {
                Name = newUser.Name,
                MoblieNumber = newUser.MoblieNumber,
                City = newUser.City,
            };

            context.Users.Add(user);
            await context.SaveChangesAsync();

            UserDTO userDTO = new UserDTO()
            {
                Id = user.Userid,
                Name = user.Name,
                City = user.City,

            };

            return userDTO;


        }
        /// <summary>
        /// To update existing user details
        /// </summary>
        /// <param name="userUpdateRequest">Provide Update Parameters Here</param>
        /// <returns></returns>
        [HttpPut("updateusers")]
        public  IActionResult UpdateUser(UserUpdateRequest userUpdateRequest)
        {
            // User existingUser = (User)context.Users.Where(u => u.Userid == userUpdateRequest.Id);
            var existingUser = context.Users.FirstOrDefault(u => u.Userid == userUpdateRequest.Id);
            if (existingUser != null)
            {
                if (userUpdateRequest.Name != null) existingUser.Name = userUpdateRequest.Name;
                if (userUpdateRequest.City != null) existingUser.City = userUpdateRequest.City;
                if (userUpdateRequest.MoblieNumber != null) existingUser.MoblieNumber = userUpdateRequest.MoblieNumber;

                context.SaveChanges();
            }
            else
            {
                return NotFound();
            }

            return Ok();
        }

        /// <summary>
        /// To delete existing user
        /// </summary>
        /// <param name="id">Provide UserId here</param>
        /// <returns></returns>
        [HttpDelete("deleteusers")]
        public IActionResult DeleteUser(int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid userid");

            context.Users.Remove(context.Users.FirstOrDefault(u => u.Userid == id));    
            context.SaveChanges();
            

            return Ok();

        }


       
    }
}
