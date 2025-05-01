using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Entities;
using ServiceLayer.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private readonly AppDbContext _context;
		public UsersController(AppDbContext context) => _context = context;

		// GET: api/users
		[HttpGet]
		public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
		{
			var users = await _context.Users
				.Select(u => new UserDto
				{
					Id = u.Id,
					Username = u.Username,
					Email = u.Email
				})
				.ToListAsync();

			return Ok(users);
		}

		// GET: api/users/5
		[HttpGet("{id}")]
		public async Task<ActionResult<UserDto>> GetUser(int id)
		{
			var user = await _context.Users.FindAsync(id);
			if (user == null) return NotFound();

			return Ok(new UserDto
			{
				Id = user.Id,
				Username = user.Username,
				Email = user.Email
			});
		}

		// PUT: api/users/5
		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateUser(int id, [FromBody] UserDto dto)
		{
			

			var user = await _context.Users.FindAsync(id);
			if (user == null) return NotFound();

			// Update fields (never update password here)
			user.Username = dto.Username;
			user.Email = dto.Email;
			user.UpdatedAt = DateTime.UtcNow;

			await _context.SaveChangesAsync();
			return NoContent();
		}

		// DELETE: api/users/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteUser(int id)
		{
			var user = await _context.Users.FindAsync(id);
			if (user == null) return NotFound();

			_context.Users.Remove(user);
			await _context.SaveChangesAsync();
			return NoContent();
		}
	}
}
