using Ecommerce.Application.DTOs;
using Ecommerce.Application.Interface;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAPI.Controllers;
[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
  

    public UserController(IUserService userService)
    {
        _userService = userService;
      
    }

    [HttpPost]
    public async Task<ActionResult> CreateUser(UserCreateDto userCreateDto)
    {
        var createUser = await _userService.CreateUser(userCreateDto);
        return CreatedAtAction(nameof(GetByIdUser), new { id = createUser.Id }, createUser);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
    {
        var getUser = await _userService.GetAllUsers();
        return Ok(getUser);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetByIdUser(int id)
    {
        var getById = await _userService.GetUserById(id);
        return Ok(getById);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<UserDto>> UpdateUser(int id, UserUpdateDto userUpdateDto)
    {
        var updateUser = await _userService.UpdateUser(id, userUpdateDto);
        return Ok(updateUser);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteUser(int id)
    {
        var deleteUser = await _userService.DeleteUser(id);
        return NoContent();
    }
    
}