using BrazilGeographicalData.src.Application.Common.Exceptions;
using BrazilGeographicalData.src.Application.Interfaces;
using BrazilGeographicalData.src.Domain.Entities;
using BrazilGeographicalData.src.Infra.Repositories;
using BrazilGeographicalData.src.Persistence.Context;
using BrazilGeographicalData.src.Services.Token;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BrazilGeographicalData.src.Services.Controllers
{

    [Route("v1/[controller]")]
    public class UserController : ControllerBase
    {

        UserRepository _userRepository = 
            new UserRepository(new DataContext(new DbContextOptionsBuilder<DataContext>()
                    .UseSqlite("Data Source=mydatabase.db").Options));

        [HttpGet]
        [Authorize(Roles = "admin")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ListDataPagination<User>), 200)]
        public async Task<IActionResult> GetAll(
            [FromQuery] int page = 0, [FromQuery] int size = 15,
            [FromQuery] string? searchString = null,
            [FromQuery] string? email = null,
            [FromQuery] bool isDeleted = false,
            [FromQuery] string? orderBy = null)
        {
            try
            {
                var users = await _userRepository.GetAll(page, size, searchString, email, isDeleted, orderBy);
                return Ok(users);
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ex.Message);
            }
        }


        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var user = await _userRepository.GetById(id);
                return Ok(user);
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ex.Message);
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Create([FromBody] User model)
        {
            try
            {
                var user = await _userRepository.Create(model);

                if (user == null)
                {
                    throw new NotFoundException("Username or password is incorrect");
                }

                var token = TokenService.GenerateToken(user);
                user.Password = "";

                return Ok(new { user = user, token = token });
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ex.Message);
            }
        }
    }
}
