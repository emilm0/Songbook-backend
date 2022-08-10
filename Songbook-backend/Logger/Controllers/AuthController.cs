using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TestSongbook.Models;
using TestSongbook.Models.Requests;
using TestSongbook.Models.Responses;
using TestSongbook.Services;
using TestSongbook.Services.Tokens;

namespace Songbook_backend.Logger.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IUserService _userService;
    private readonly IRefreshTokenValidator _refreshTokenValidator;
    private readonly SongbookContext _context;

    public AuthController(IAuthService authService,
        IUserService userService,
        IRefreshTokenValidator refreshTokenValidator,
        SongbookContext context)
    {
        _authService = authService;
        _userService = userService;
        _refreshTokenValidator = refreshTokenValidator;
        _context = context;
    }

    [HttpPost("register")
        //, Authorize(Roles = "Admin")
        ]
    public async Task<ActionResult<User>> Register([FromBody] RegisterRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        if (_userService.FindUserInDatabaseByEmail(request.Email))
        {
            return BadRequest("Email is already taken");
        }

        _authService.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

        var user = _userService.CreateUser(request, passwordHash, passwordSalt);

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return Ok(user);
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthenticatedUserResponse>> Login([FromBody] LoginRequest loginRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        if (!_context.Users.Any())
        {
            return NotFound("Not found any users in database.");
        }
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginRequest.Email);

        if (user == null)
        {
            return NotFound("User not found.");
        }

        if (!_authService.VerifyPasswordHash(loginRequest.Password, user.PasswordHash, user.PasswordSalt))
        {
            return BadRequest("Wrong password");
        }



        _authService.DeleteAllRefreshTokensByUserId(user.Id);

        AuthenticatedUserResponse response = _authService.Authenticate(user);

        return Ok(response);

    }


    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] RefreshRequest refreshRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        bool isValidRefreshToken = _refreshTokenValidator.Validate(refreshRequest.RefreshToken);
        if (!isValidRefreshToken)
        {
            return BadRequest("Invalid refresh token.");
        }

        var refreshTokenDto = await _context.RefreshTokens
                                    .FirstOrDefaultAsync(r => r.Token == refreshRequest.RefreshToken);
        if (refreshTokenDto == null)
        {
            return NotFound("Invalid refresh token.");
        }

        User user = await _context.Users.FirstOrDefaultAsync(u => u.Id == refreshTokenDto.UserId);
        if (user == null)
        {
            return NotFound("User not found.");
        }

        AuthenticatedUserResponse response = _authService.Authenticate(user);

        return Ok(response);
    }


    [HttpDelete("logout"), Authorize]
    public async Task<IActionResult> Logout()
    {
        string rawUserId = HttpContext.User.FindFirstValue("id");

        if (!Guid.TryParse(rawUserId, out Guid userId))
        {
            return Unauthorized();
        }

        _authService.DeleteAllRefreshTokensByUserId(userId);

        return NoContent();
    }

    // GET: api/Users
    [HttpGet("Users"), Authorize(Roles = "User")]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
        if (_context.Users == null)
        {
            return NotFound();
        }
        return await _context.Users.ToListAsync();
    }

}
