using GIBDD.Server.Data;
using GIBDD.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GIBDD.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfficersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OfficersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Officers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OfficerDto>>> GetOfficers()
        {
            var officers = await _context.Officers
                .Select(o => new OfficerDto
                {
                    OfficerId = o.OfficerId,
                    FirstName = o.FirstName,
                    LastName = o.LastName,
                    MiddleName = o.MiddleName,
                    Position = o.Position 
                })
                .ToListAsync();

            return officers;
        }

        // GET: api/Officers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OfficerDto>> GetOfficer(int id)
        {
            var officer = await _context.Officers
                .Where(o => o.OfficerId == id)
                .Select(o => new OfficerDto
                {
                    OfficerId = o.OfficerId,
                    FirstName = o.FirstName,
                    LastName = o.LastName,
                    MiddleName = o.MiddleName,
                    Position = o.Position
                })
                .FirstOrDefaultAsync();

            if (officer == null)
            {
                return NotFound();
            }

            return officer;
        }

        // POST: api/Officers
        [HttpPost]
        public async Task<ActionResult<OfficerDto>> PostOfficer(OfficerDto officerDto)
        {
            var officer = new Officer
            {
                FirstName = officerDto.FirstName,
                LastName = officerDto.LastName,
                MiddleName = officerDto.MiddleName,
                Position = officerDto.Position
            };


            _context.Officers.Add(officer);
            await _context.SaveChangesAsync();

            var createdOfficerDto = new OfficerDto
            {
                OfficerId = officer.OfficerId,
                FirstName = officer.FirstName,
                LastName = officer.LastName,
                MiddleName = officer.MiddleName,
                Position = officer.Position
            };


            return CreatedAtAction(nameof(GetOfficer), new { id = officer.OfficerId }, createdOfficerDto);
        }

        // PUT: api/Officers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOfficer(int id, Officer officerDto)
        {
            if (id != officerDto.OfficerId)
            {
                return BadRequest();
            }

            var officer = await _context.Officers.FindAsync(id);
            if (officer == null)
            {
                return NotFound();
            }

            officer.FirstName = officerDto.FirstName;
            officer.LastName = officerDto.LastName;
            officer.MiddleName = officerDto.MiddleName;
            officer.Position = officerDto.Position; // Обновляем Position

            _context.Entry(officer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OfficerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Officers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOfficer(int id)
        {
            var officer = await _context.Officers.FindAsync(id);
            if (officer == null)
            {
                return NotFound();
            }

            _context.Officers.Remove(officer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OfficerExists(int id)
        {
            return _context.Officers.Any(e => e.OfficerId == id);
        }
    }

}
