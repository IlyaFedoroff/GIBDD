using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GIBDD.Server.Data;
using GIBDD.Server.Models;

namespace GIBDD.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InspectionsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public InspectionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Получить список всех осмотров с необходимыми данными
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InspectionDto>>> GetInspections()
        {
            var inspections = await _context.Inspections
        .Include(i => i.Car)
        .Include(i => i.Officer)
        .Include(i => i.Owner)
        .Select(i => new InspectionDto
        {
            InspectionId = i.InspectionId,
            InspectionDate = i.InspectionDate,
            Result = i.Result,
            CarBrand = i.Car.Brand,
            LicensePlate = i.Car.LicensePlate,
            OfficerName = $"{i.Officer.FirstName} {i.Officer.LastName}",
            OwnerFullName = $"{i.Owner.FirstName} {i.Owner.MiddleName} {i.Owner.LastName}"
        })
        .ToListAsync();

            return Ok(inspections);
        }


        // Удалить осмотр
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInspection(int id)
        {
            var inspection = await _context.Inspections.FindAsync(id);
            if (inspection == null)
            {
                return NotFound();
            }

            _context.Inspections.Remove(inspection);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult> AddInspection(AddInspectionDto addInspectionDto)
        {
            Console.WriteLine($"Получен запрос на добавление осмотра: CarId={addInspectionDto.CarId}, OfficerId={addInspectionDto.OfficerId}, OwnerId={addInspectionDto.OwnerId}");

            // Проверяем, существует ли указанный автомобиль, инспектор и владелец
            var carExists = await _context.Cars.AnyAsync(c => c.CarId == addInspectionDto.CarId);
            var officerExists = await _context.Officers.AnyAsync(o => o.OfficerId == addInspectionDto.OfficerId);
            var ownerExists = await _context.Owners.AnyAsync(o => o.OwnerId == addInspectionDto.OwnerId);

            if (!carExists || !officerExists || !ownerExists)
            {

                if (!carExists)
                {
                    Console.WriteLine("Ошибка: Указанный автомобиль не существует.");
                    return NotFound("Указанный автомобиль не существует.");
                }
                if (!officerExists)
                {
                    Console.WriteLine("Ошибка: Указанный инспектор не существует.");
                    return NotFound("Указанный инспектор не существует");
                }
                if (!ownerExists)
                {
                    Console.WriteLine("Ошибка: Указанный владелец не существует.");
                    return NotFound("Указанный владелец не существует");
                }
            }
            var inspection = new Inspection
            {
                InspectionDate = DateTime.SpecifyKind(addInspectionDto.InspectionDate, DateTimeKind.Utc),
                Result = addInspectionDto.Result,
                CarId = addInspectionDto.CarId,
                OfficerId = addInspectionDto.OfficerId,
                OwnerId = addInspectionDto.OwnerId
            };

            _context.Inspections.Add(inspection);
            await _context.SaveChangesAsync();
            Console.WriteLine("Отправляем ОК клиенту");
            return Ok(new { message = "Осмотр успешно добавлен" });
        }

        //// Получить осмотр по ID
        //[HttpGet("{id}")]
        //public async Task<ActionResult<InspectionDto>> GetInspection(int id)
        //{
        //    var inspection = await _context.Inspections
        //        .Include(i => i.Car)
        //        .Include(i =>i.Owner)
        //        .Include(i => i.Officer)
        //        .FirstOrDefaultAsync(i => i.InspectionId == id);

        //    if (inspection == null)
        //    {
        //        return NotFound();
        //    }

        //    var inspectionDto = new InspectionDto
        //    {
        //        InspectionId = inspection.InspectionId,
        //        InspectionDate = inspection.InspectionDate,
        //        Result = inspection.Result,
        //        CarId = inspection.Car.CarId,
        //        OwnerId = inspection.Owner.OwnerId,
        //        OfficerId = inspection.Officer.OfficerId
        //    };

        //    return Ok(inspectionDto);
        //}

        //// Добавить новый осмотр
        //[HttpPost]
        //public async Task<ActionResult<InspectionDto>> PostInspection(InspectionDto inspectionDto)
        //{

        //    // Извлекаем Car, Owner и Officer по их Id
        //    var car = await _context.Cars.FindAsync(inspectionDto.CarId);
        //    var owner = await _context.Owners.FindAsync(inspectionDto.OwnerId);
        //    var officer = await _context.Officers.FindAsync(inspectionDto.OfficerId);

        //    if (car == null) return BadRequest("Invalid car");
        //    else if (owner == null) return BadRequest("Invalid owner");
        //    else if (officer == null) return BadRequest("Invalid officer");



        //    var inspection = new Inspection
        //    {
        //        Car = car,
        //        Owner = owner,
        //        Officer = officer,
        //        InspectionDate = DateTime.SpecifyKind(inspectionDto.InspectionDate, DateTimeKind.Utc),
        //        Result = inspectionDto.Result
        //    };

        //    _context.Inspections.Add(inspection);
        //    await _context.SaveChangesAsync();

        //    // Возвращаем DTO осмотра
        //    var inspectionResponse = new InspectionDto
        //    {
        //        InspectionId = inspection.InspectionId,
        //        InspectionDate = inspection.InspectionDate,
        //        Result = inspection.Result,
        //        CarId = inspection.Car.CarId,
        //        OwnerId = inspection.Owner.OwnerId,
        //        OfficerId = inspection.Officer.OfficerId
        //    };

        //    return CreatedAtAction(nameof(GetInspection), new { id = inspection.InspectionId }, inspectionResponse);
        //}


        //// Осмотры по инспектору
        //[HttpGet("officers/history")]
        //public async Task<ActionResult<IEnumerable<InspectionHistoryDto>>> GetInspectionsByOfficers()
        //{
        //    // Загружаем все осмотры с информацией об инспекторе, автомобиле и владельце
        //    var inspections = await _context.Inspections
        //        .Include(i => i.Officer) // Подгружаем данные о инспекторе
        //        .Include(i => i.Car)    // Подгружаем данные о машине
        //        .Include(i => i.Owner)  // Подгружаем данные о владельце
        //        .OrderByDescending(i => i.InspectionDate) // Сортируем по дате осмотра
        //        .ToListAsync();

        //    // Преобразуем данные в DTO
        //    var inspectionHistories = inspections.Select(i => new InspectionHistoryDto
        //    {
        //        InspectionDate = i.InspectionDate,
        //        OfficerName = $"{i.Officer.FirstName} {i.Officer.LastName}",
        //        CarInfo = $"{i.Car.Brand} ({i.Car.LicensePlate})",
        //        OwnerName = $"{i.Owner.FirstName} {i.Owner.LastName}",
        //        Result = i.Result
        //    }).ToList();

        //    return Ok(inspectionHistories);
        //}


    }
}
