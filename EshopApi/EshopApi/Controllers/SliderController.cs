using System.IO;
using System.Net;
using System.Net.Http;
using Core.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EshopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SliderController : ControllerBase
    {
        private ISliderService _sliderService;

        public SliderController(ISliderService sliderService)
        {
            _sliderService = sliderService;
        }
        
        [HttpGet]
        [Route("GetSliders")]
        public IActionResult GetSliders()
        {
            return new ObjectResult(_sliderService.GetAllSliders());
        }

        [HttpGet]
        [Route("Image/{imageName}")]
        [AllowAnonymous]
        public IActionResult GetImage(string imageName)
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);

            if (System.IO.File.Exists(Directory.GetCurrentDirectory() + "/wwwroot/Images/Slider/" + imageName))
            {
                var image = System.IO.File.OpenRead(Directory.GetCurrentDirectory() + "/wwwroot/Images/Slider/" + imageName);
                response.Content = new StreamContent(image);

                return File(image, "image/jpg");
            }

            return NotFound();
        }
    }

}