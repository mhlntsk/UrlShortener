using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MVC.Services.Validation;
using Shortener.Business.Interfaces;
using Shortener.Business.Models;
using Shortener.Data.Validation;
using Shortener.Presentation.Services;

namespace Shortener.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UrlController : ControllerBase
    {
        private readonly IUrlService urlService;
        public UrlController(IUrlService urlService)
        {
            this.urlService = urlService;
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<IEnumerable<UrlShortenerModel>>> GetUrls()
        {
            try
            {
                var urls = await urlService.GetAllAsync();

                foreach (var url in urls)
                    url.ShortUrl.CastUrl();

                return Ok(urls);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("{id}")]
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
        [Route("")]
        public async Task<ActionResult> AddUrl(UrlShortenerModel url, [FromServices] IValidator<UrlShortenerModel> validator)
        {
            try
            {
                var result = await validator.ValidateAsync(url);

                if (!result.IsValid)
                {
                    result.AddToModelState(ModelState);

                    return BadRequest(ModelState);
                }

                await urlService.AddAsync(url);

                return Ok();
            }
            catch (AlreadyExistException ex)
            {
                return Conflict(new ProblemDetails
                {
                    Status = 409,
                    Title = "Such link already exists!",
                    Detail = $"{ex.Message}"
                });
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpDelete]
        [Route("{id}")]
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
