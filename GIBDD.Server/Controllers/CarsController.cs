using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GIBDD.Server.Data;
using GIBDD.Server.Models;

namespace GIBDD.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CarsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Получить список всех автомобилей
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarDto>>> GetCars()
        {

            var cars = await _context.Cars
                .Select(car => new CarDto
                {
                    CarId = car.CarId,
                    LicensePlate = car.LicensePlate,
                    EngineNumber = car.EngineNumber,
                    Color = car.Color,
                    Brand = car.Brand,
                    TechnicalPassportNumber = car.TechnicalPassportNumber,
                    ManufactureDate = car.ManufactureDate
                })
                .ToListAsync();

            return cars;
        }

        // Получить автомобиль по ID
        [HttpGet("{id}")]
        public async Task<ActionResult<CarDto>> GetCar(int id)
        {
            var car = await _context.Cars
                .Where(c => c.CarId == id)
                .Select(c => new CarDto
                {
                    CarId = c.CarId,
                    LicensePlate = c.LicensePlate,
                    EngineNumber = c.EngineNumber,
                    Color = c.Color,
                    Brand = c.Brand,
                    TechnicalPassportNumber = c.TechnicalPassportNumber,
                    ManufactureDate = c.ManufactureDate
                })
                .FirstOrDefaultAsync();

            if (car == null)
            {
                return NotFound();
            }

            return car;
        }

        // Добавить новый автомобиль
        [HttpPost]
        public async Task<ActionResult<CarDto>> PostCar(CarDto carDto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var car = new Car
            {
                LicensePlate = carDto.LicensePlate,
                EngineNumber = carDto.EngineNumber,
                Color = carDto.Color,
                Brand = carDto.Brand,
                TechnicalPassportNumber = carDto.TechnicalPassportNumber,
                ManufactureDate = DateTime.SpecifyKind(carDto.ManufactureDate, DateTimeKind.Utc),
                //ManufactureDate = carDto.ManufactureDate,
            };

            _context.Cars.Add(car);
            await _context.SaveChangesAsync();

            var createdCarDto = new CarDto
            {
                CarId = car.CarId,
                LicensePlate = car.LicensePlate,
                EngineNumber = car.EngineNumber,
                Color = car.Color,
                Brand = car.Brand,
                TechnicalPassportNumber = car.TechnicalPassportNumber,
                ManufactureDate = car.ManufactureDate
            };

            return CreatedAtAction(nameof(GetCar), new { id = car.CarId }, createdCarDto);
        }

        // Обновить данные автомобиля
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCar(int id, CarDto carDto)
        {
            if (id != carDto.CarId)
            {
                return BadRequest();
            }

            var car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }

            car.LicensePlate = carDto.LicensePlate;
            car.EngineNumber = carDto.EngineNumber;
            car.Color = carDto.Color;
            car.Brand = carDto.Brand;
            car.TechnicalPassportNumber = carDto.TechnicalPassportNumber;
            car.ManufactureDate = carDto.ManufactureDate;

            _context.Entry(car).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarExists(id))
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

        // Удалить автомобиль
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                Console.WriteLine("не нашли авто:(");
                return NotFound();
            }
            
            _context.Cars.Remove(car);
            Console.WriteLine("нашли и пытаемся удалить");
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CarExists(int id)
        {
            return _context.Cars.Any(e => e.CarId == id);
        }



        // История осмотров авто
        [HttpGet("{id}/history")]
        public async Task<ActionResult<IEnumerable<InspectionHistoryDto>>> GetCarInspectionHistory(int id)
        {
            var inspections = await _context.Inspections
                .Where(i => i.CarId == id)
                .OrderByDescending(i => i.InspectionDate)
                .Select(i => new InspectionHistoryDto
                {
                    InspectionDate = i.InspectionDate,
                    Result = i.Result
                })
                .ToListAsync();

            if (!inspections.Any())
            {
                return NotFound("Осмотры для данного автомобиля не найдены.");
            }

            return Ok(inspections);
        }





        //// История осмотров авто
        //[HttpGet("{id}/history")]
        //public async Task<ActionResult<IEnumerable<InspectionDto>>> GetCarInspectionHistory(int id)
        //{
        //    // Проверяем, существует ли автомобиль с данным ID
        //    var carExists = await _context.Cars.AnyAsync(c => c.CarId == id);
        //    if (!carExists)
        //    {
        //        return NotFound($"Автомобиль с ID {id} не найден.");
        //    }

        //    // Получаем осмотры, связанные с автомобилем
        //    var inspections = await _context.Inspections
        //        .Include(i => i.Officer) // Подгружаем данные о сотрудниках
        //        .Include(i => i.Owner)  // Подгружаем данные о владельце
        //        .Where(i => i.CarId == id)
        //        .OrderByDescending(i => i.InspectionDate) // Сортировка по дате (от последнего)
        //        .ToListAsync();

        //    // Конвертируем осмотры в DTO
        //    var inspectionDtos = inspections.Select(i => new InspectionDto
        //    {
        //        InspectionId = i.InspectionId,
        //        InspectionDate = i.InspectionDate,
        //        Result = i.Result,
        //        CarId = i.CarId,
        //        OfficerId = i.Officer.OfficerId,
        //        OwnerId = i.Owner.OwnerId
        //    }).ToList();

        //    return Ok(inspectionDtos);
        //}
    }
}
