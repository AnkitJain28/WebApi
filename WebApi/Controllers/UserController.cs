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
        [HttpGet]
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
        /// To Create New User
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns></returns>
        [HttpPost]
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
                City= user.City,

            };

            return userDTO;


        }
        /// <summary>
        /// To update existing user details
        /// </summary>
        /// <param name="userUpdateRequest"></param>
        /// <returns></returns>
        [HttpPut]
        public  IActionResult UpdateUser(UserUpdateRequest userUpdateRequest)
        {
            User existingUser = (User)context.Users.Where(u => u.Userid == userUpdateRequest.Id);

            if (existingUser != null)
            {
                existingUser.Name = userUpdateRequest.Name;
                existingUser.City = userUpdateRequest.City;
                existingUser.MoblieNumber = userUpdateRequest.MoblieNumber;

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
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
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
