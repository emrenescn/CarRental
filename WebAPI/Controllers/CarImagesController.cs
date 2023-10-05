using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarImagesController : ControllerBase
    {
        ICarImageService _carImageService;
        public CarImagesController(ICarImageService carImageService)
        {
                _carImageService = carImageService;
        }

        [HttpPost("add")]
        public IActionResult Add([FromForm]IFormFile file,[FromForm]CarImage carImage)
        {
           var result= _carImageService.Add(file, carImage);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);  
        }
        [HttpPost("delete")]
        public IActionResult Delete([FromForm]CarImage carImage)
        {
            var deleteCar= _carImageService.GetByImageId(carImage.Id).Data;
            var result=_carImageService.Delete(deleteCar);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);  
        }
       [HttpPost("update")]
       public IActionResult Update([FromForm]IFormFile file,[FromForm]CarImage carImage)
        {
            var result = _carImageService.Update(file,carImage);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var result = _carImageService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("getbyimageid")]
        public IActionResult GetByImageId(int id)
        {
            var result = _carImageService.GetByImageId(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("getbycarid")]
        public IActionResult GetByCarId(int carId)
        {
            var result = _carImageService.GetAllByCarId(carId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
