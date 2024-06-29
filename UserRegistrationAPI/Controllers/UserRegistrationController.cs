using Microsoft.AspNetCore.Mvc;
using UserRegistrationAPI.Interfaces;
using UserRegistrationAPI.Models.DTO;

namespace UserRegistrationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRegistrationController : ControllerBase
    {
        private readonly IService<UserRegisterDTO, UserDTO> _userService;

        public UserRegistrationController(IService<UserRegisterDTO, UserDTO> userService)
        {
            _userService = userService;
        }

        [HttpPost("Register")]
        [ProducesResponseType(typeof(UserDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<UserDTO> Register([FromBody] UserRegisterDTO userDTO)
        {
            var user = _userService.Register(userDTO);
            if (user != null)
                return Ok(user);
            return BadRequest("Impossible de s'enregistrer !");
        }

        [HttpPost("Login")]
        [ProducesResponseType(typeof(UserDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<UserDTO> Login([FromBody] UserDTO userDTO)
        {
            var user = _userService.Login(userDTO);
            if (user != null)
                return Ok(user);
            return BadRequest("Login ou mot de passe incorrect.");
        }
    }
}
