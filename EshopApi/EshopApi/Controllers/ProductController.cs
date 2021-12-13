using System.IO;
using System.Net;
using System.Net.Http;
using Core.Contracts;
using Core.DTO.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EshopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [Route("GetProducts", Name = "GetProducts")]
        public IActionResult GetProductsByFilter([FromQuery]FilterProductsDTO filter)
        {
            filter.TakeEntity = 8;

            return new ObjectResult(_productService.GetProductsByFilter(filter));
        }

        [HttpGet]
        [Route("ShowProduct/{productId}", Name = "ShowProduct")]
        public IActionResult ShowProduct(int productId)
        {
            var product = _productService.GetProduct(productId);

            if (product == null) return NotFound();

            return Ok(product);
        }

        [HttpGet]
        [Route("LatestProducts")]
        public IActionResult GetLatestProducts()
        {
            return new ObjectResult(_productService.GetLatestProducts());
        }

        [HttpGet]
        [Route("MostSellProducts")]
        public IActionResult GetMostSellProducts()
        {
            return new ObjectResult(_productService.GetMostSellProducts());
        }

        [HttpGet]
        [Route("SuggestedProducts")]
        public IActionResult GetSuggestedProducts()
        {
            return new ObjectResult(_productService.GetSuggestedProducts());
        }


        [HttpGet]
        [Route("Image/{imageName}")]
        [AllowAnonymous]
        public IActionResult GetImage(string imageName)
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);

            if (System.IO.File.Exists(Directory.GetCurrentDirectory() + "/wwwroot/Images/Products/" + imageName))
            {
                var image = System.IO.File.OpenRead(Directory.GetCurrentDirectory() + "/wwwroot/Images/Products/" + imageName);
                response.Content = new StreamContent(image);

                return File(image, "image/jpg");
            }

            return NotFound();
        }
    }
}