using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model.DBModels;
using Model.RequestModels;
using Model.ResponseModels;

namespace WebApi.Controllers
{
    /// <summary>
    /// Users related controller.
    /// </summary>
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        /// <summary>
        /// Instance of <see cref="ApiAssignmentContext"/>.
        /// </summary>
        private readonly ApiAssignmentContext context;

        /// <summary>
        /// Intialization <see cref="UserController"/>.
        /// </summary>
        /// <param name="context">Instance</param>
        public UserController(ApiAssignmentContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// To get All Users
        /// </summary>
        /// <returns>Returns list of users.</returns>
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
        /// To get single user by id
        /// </summary>
        /// <param name="id">Provide userid here</param>
        /// <returns>Returns user details</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserDetailsByUserId(int id)
        {
            var existingUser = await context.Users.FirstOrDefaultAsync(u => u.Userid == id);

            if(existingUser != null)
            {
                UserDTO userDTO = new UserDTO()
                {
                    Id = existingUser.Userid,
                    Name = existingUser.Name,
                    City = existingUser.City,

                };

                return Ok(userDTO);
            }  

            return BadRequest();
        }

        /// <summary>
        /// To Create New User
        /// </summary>
        /// <param name="newUser">Provide User details here</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<UserDTO> CreateUser([FromBody] UserCreateRequest newUser)
        {
            User user = new User()
            {
                Name = newUser.Name,
                MobileNumber = newUser.MobileNumber,
                City = newUser.City,
            };

            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();

            UserDTO userDTO = new UserDTO()
            {
                Id = user.Userid,
                Name = user.Name,
                City = user.City
            };

            return userDTO;
        }

       /// <summary>
       /// To update existing user details
       /// </summary>
       /// <param name="id">Provide userid here</param>
       /// <param name="userUpdateRequest">Provide user detils here</param>
       /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserUpdateRequest userUpdateRequest)
        {
            if(userUpdateRequest == null || id != userUpdateRequest.Id)
            {
                return BadRequest();
            }

            var existingUser = await context.Users.FirstOrDefaultAsync(u => u.Userid == userUpdateRequest.Id);
            if (existingUser != null)
            {
                if (userUpdateRequest.Name != null)
                {
                    existingUser.Name = userUpdateRequest.Name;
                }

                if (userUpdateRequest.City != null) existingUser.City = userUpdateRequest.City;
                if (userUpdateRequest.MobileNumber != null) existingUser.MobileNumber = userUpdateRequest.MobileNumber;

                await context.SaveChangesAsync();
            }
            else
            {
                return NotFound();
            }

            return NoContent();
        }

        /// <summary>
        /// To delete existing user
        /// </summary>
        /// <param name="id">Provide UserId here</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Not a valid userid");
            }

            User user = await context.Users.FirstOrDefaultAsync(u => u.Userid == id);
            if (user != null)
            {
                context.Users.Remove(user);
                await context.SaveChangesAsync();
            }     

            return Ok();
        }
    }
}
