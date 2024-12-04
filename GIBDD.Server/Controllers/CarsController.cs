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


        [HttpPost("seed")]
        public IActionResult SeedData()
        {
            if (!_context.Cars.Any())
            {
                var cars = new List<Car>
            {
                new Car { LicensePlate = "A123BC", EngineNumber = "EN123456789", Color = "Red", Brand = "Toyota", TechnicalPassportNumber = "TP123456789", ManufactureDate = new DateTime(2020, 5, 15) },
                new Car { LicensePlate = "B234CD", EngineNumber = "EN234567890", Color = "Blue", Brand = "Honda", TechnicalPassportNumber = "TP234567890", ManufactureDate = new DateTime(2021, 6, 20) },
                new Car { LicensePlate = "C345DE", EngineNumber = "EN345678901", Color = "Black", Brand = "BMW", TechnicalPassportNumber = "TP345678901", ManufactureDate = new DateTime(2019, 4, 10) },
                new Car { LicensePlate = "D456EF", EngineNumber = "EN456789012", Color = "White", Brand = "Mercedes", TechnicalPassportNumber = "TP456789012", ManufactureDate = new DateTime(2022, 7, 5) },
                new Car { LicensePlate = "E567FG", EngineNumber = "EN567890123", Color = "Gray", Brand = "Audi", TechnicalPassportNumber = "TP567890123", ManufactureDate = new DateTime(2018, 3, 30) },
                new Car { LicensePlate = "F678GH", EngineNumber = "EN678901234", Color = "Green", Brand = "Volkswagen", TechnicalPassportNumber = "TP678901234", ManufactureDate = new DateTime(2020, 11, 15) },
                new Car { LicensePlate = "G789HI", EngineNumber = "EN789012345", Color = "Yellow", Brand = "Ford", TechnicalPassportNumber = "TP789012345", ManufactureDate = new DateTime(2017, 2, 25) },
                new Car { LicensePlate = "H890IJ", EngineNumber = "EN890123456", Color = "Pink", Brand = "Chevrolet", TechnicalPassportNumber = "TP890123456", ManufactureDate = new DateTime(2021, 8, 10) },
                new Car { LicensePlate = "I901JK", EngineNumber = "EN901234567", Color = "Purple", Brand = "Nissan", TechnicalPassportNumber = "TP901234567", ManufactureDate = new DateTime(2016, 1, 20) },
                new Car { LicensePlate = "J012KL", EngineNumber = "EN012345678", Color = "Orange", Brand = "Hyundai", TechnicalPassportNumber = "TP012345678", ManufactureDate = new DateTime(2023, 9, 5) }
            };

                _context.Cars.AddRange(cars);
                _context.SaveChangesAsync();
            }

            return Ok("Data seeded successfully");
        }


        // Добавить несколько автомобилей
        [HttpPost("mult")]
        public async Task<ActionResult<IEnumerable<CarDto>>> PostCars(List<CarDto> carDtos)
        {
            if (carDtos == null || carDtos.Count == 0)
            {
                return BadRequest("No cars provided.");
            }

            var cars = carDtos.Select(carDto => new Car
            {
                LicensePlate = carDto.LicensePlate,
                EngineNumber = carDto.EngineNumber,
                Color = carDto.Color,
                Brand = carDto.Brand,
                TechnicalPassportNumber = carDto.TechnicalPassportNumber,
                ManufactureDate = DateTime.SpecifyKind(carDto.ManufactureDate, DateTimeKind.Utc),
            }).ToList();

            _context.Cars.AddRange(cars);
            await _context.SaveChangesAsync();

            // Создаем список DTO для возвращаемых автомобилей
            var createdCarDtos = cars.Select(car => new CarDto
            {
                CarId = car.CarId,
                LicensePlate = car.LicensePlate,
                EngineNumber = car.EngineNumber,
                Color = car.Color,
                Brand = car.Brand,
                TechnicalPassportNumber = car.TechnicalPassportNumber,
                ManufactureDate = car.ManufactureDate
            }).ToList();

            return CreatedAtAction(nameof(GetCar), new { id = createdCarDtos.First().CarId }, createdCarDtos);
        }

    }
}
