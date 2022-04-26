﻿using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Meets.WebApi.User;

[ApiController]
[Route("/users")]
[Consumes(MediaTypeNames.Application.Json)]
[Produces(MediaTypeNames.Application.Json)]
public class UserController : ControllerBase
{
    private readonly DatabaseContext _context;

    public UserController(DatabaseContext context) =>
        _context = context;

    
    /// <summary>Register a new user.</summary>
    /// <param name="registerDto">User registration information.</param>
    /// <response code="200">Newly registered user.</response>
    /// <response code="409">Failed to register a user: username already taken.</response>
    [HttpPost]
    [ProducesResponseType(typeof(ReadUserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> RegisterNewUser([FromBody] RegisterUserDto registerDto)
    {
        var usernameToken = await _context.Users.AnyAsync(user => user.Username == registerDto.Username);
        if (usernameToken)
        {
            return Conflict("Username already taken.");
        }
        var newUser = new UserEntity
        {
            Id = Guid.NewGuid(),
            DisplayName = registerDto.DisplayName,
            Username = registerDto.Username,
            Password = BCrypt.Net.BCrypt.HashPassword(registerDto.Password)
        };
        _context.Users.Add(newUser);
        await _context.SaveChangesAsync();

        var readDto = new ReadUserDto
        {
            Id = newUser.Id,
            DisplayName = newUser.DisplayName,
            Username = newUser.Username
        };
        return Ok(readDto);
    }
}