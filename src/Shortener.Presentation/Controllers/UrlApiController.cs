using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MVC.Services.Validation;
using Shortener.Business.Interfaces;
using Shortener.Business.Models;
using Shortener.Presentation.Services;

namespace Shortener.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UrlApiController : ControllerBase
    {
        private readonly IUrlService urlService;
        public UrlApiController(IUrlService urlService)
        {
            this.urlService = urlService;
        }

        [HttpGet]
        [Route("urls")]
        public async Task<ActionResult<IEnumerable<UrlShortenerModel>>> GetUrls()
        {
            try
            {
                var urls = await urlService.GetAllAsync();

                return Ok(urls);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("urls/{id}")]
        public async Task<ActionResult<UrlShortenerModel>> GetUrl(int id)
        {
            try
            {
                var url = await urlService.GetByIdAsync(id);

                if (url == null)
                    return NoContent();

                url.ShortUrl.CastUrl();

                return Ok(url);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        [Route("urls/add")]
        public async Task<ActionResult> AddUrl(UrlShortenerModel model, [FromServices] IValidator<UrlShortenerModel> validator)
        {
            try
            {
                var result = await validator.ValidateAsync(model);

                if (!result.IsValid)
                {
                    result.AddToModelState(ModelState);

                    return BadRequest(ModelState);
                }

                await urlService.AddAsync(model);

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpDelete]
        [Route("urls/delete/{id}")]
        public async Task<ActionResult> RemoveUrl(int id)
        {
            try
            {
                await urlService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
