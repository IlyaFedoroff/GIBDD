using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GIBDD.Server.Data;
using GIBDD.Server.Models;
using Microsoft.Extensions.Logging;


namespace GIBDD.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<OwnersController> _logger;

        public OwnersController(ApplicationDbContext context, ILogger<OwnersController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OwnerDto>>> GetOwners()
        {
            var owners = await _context.Owners
                .Select(o => new OwnerDto
                {
                    OwnerId=o.OwnerId,
                    DriverLicenseNumber=o.DriverLicenseNumber,
                    FirstName = o.FirstName,
                    MiddleName = o.MiddleName,
                    LastName = o.LastName,
                    Address=o.Address,
                    BirthYear=o.BirthYear,
                    Gender=o.Gender,
                })
                .ToListAsync();
            _logger.LogInformation("Received owners");
            return owners;
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<OwnerDto>> GetOwner(int id)
        {
            var owner = await _context.Owners
                .Where(o => o.OwnerId == id)
                .Select(o => new OwnerDto
                {
                    OwnerId = o.OwnerId,
                    DriverLicenseNumber = o.DriverLicenseNumber,
                    FirstName = o.FirstName,
                    LastName = o.LastName,
                    MiddleName = o.MiddleName,
                    Address = o.Address,
                    BirthYear = o.BirthYear,
                    Gender = o.Gender
                })
                .FirstOrDefaultAsync();

            if (owner == null)
            {
                _logger.LogError("Not found owner");
                return NotFound();
            }

            _logger.LogInformation($"Received owner {owner.FirstName}");
            return owner;
        }

        [HttpPost]
        public async Task<ActionResult<OwnerDto>> PostOwner(OwnerDto ownerDto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Error with PostOwner. Owner: {ownerDto.FirstName}");
                return BadRequest(ModelState);
            }

            var owner = new Owner
            {
                DriverLicenseNumber = ownerDto.DriverLicenseNumber,
                FirstName = ownerDto.FirstName,
                LastName = ownerDto.LastName,
                MiddleName = ownerDto.MiddleName,
                Address = ownerDto.Address,
                BirthYear = ownerDto.BirthYear,
                Gender = ownerDto.Gender
            };

            _context.Owners.Add(owner);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Post query success. Owner: {owner.FirstName}");
            // Возвращаем DTO с созданным владельцем
            return CreatedAtAction(nameof(GetOwner), new { id = owner.OwnerId }, new OwnerDto
            {
                OwnerId = owner.OwnerId,
                DriverLicenseNumber = owner.DriverLicenseNumber,
                FirstName = owner.FirstName,
                LastName = owner.LastName,
                MiddleName = owner.LastName,
                Address = owner.Address,
                BirthYear = owner.BirthYear,
                Gender = owner.Gender
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutOwner(int id, OwnerDto ownerDto)
        {


            if (id != ownerDto.OwnerId)
            {
                _logger.LogError($"Bad request with Put query. Id: {id}, owner Id: {ownerDto.OwnerId}");
                return BadRequest();
            }

            var owner = await _context.Owners.FindAsync(id);
            if (owner == null)
            {
                _logger.LogError($"Not found owner with this id: {id}");
                return NotFound();
            }

            // Обновляем владельца с новыми значениями из DTO
            owner.DriverLicenseNumber = ownerDto.DriverLicenseNumber;
            owner.FirstName = ownerDto.FirstName;
            owner.LastName = ownerDto.LastName;
            owner.Address = ownerDto.Address;
            owner.Address = ownerDto.Address;
            owner.BirthYear = ownerDto.BirthYear;
            owner.Gender = ownerDto.Gender;


            _context.Entry(owner).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Owners.Any(e => e.OwnerId == id))
                {
                    _logger.LogError($"Owner with this id {id} does not exist in database.");
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            _logger.LogInformation($"Success put query. Owner: {owner.OwnerId}");
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOwner(int id)
        {
            var owner = await _context.Owners.FindAsync(id);
            if (owner == null)
            {
                _logger.LogError($"Owner with this id {id} does not exist");
                return NotFound();
            }

            _context.Owners.Remove(owner);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Deleted owner with this id {owner.OwnerId}");
            return NoContent();
        }
    }
}
