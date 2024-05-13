using FluentValidation;
using Microsoft.AspNetCore.Authorization;
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
        private readonly JwtTokenService jwtTokenService;
        public UrlController(IUrlService urlService, JwtTokenService jwtTokenService)
        {
            this.urlService = urlService;
            this.jwtTokenService = jwtTokenService;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<UrlShortenerModel>>> GetUrls()
        {
            try
            {
                var urls = await urlService.GetAllAsync();

                foreach (var url in urls)
                    url.ShortUrl = url.ShortUrl.CastUrl();

                return Ok(urls);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UrlShortenerModel>> GetUrl(int id)
        {
            try
            {
                var url = await urlService.GetByIdAsync(id);

                if (url == null)
                    return NoContent();

                url.ShortUrl = url.ShortUrl.CastUrl();

                return Ok(url);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPost("")]
        public async Task<ActionResult> AddUrl(UrlShortenerModel url, [FromServices] IValidator<UrlShortenerModel> validator)
        {
            try
            {
                var token = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];
                var principal = jwtTokenService.ValidateToken(token);

                if (principal == null)
                {
                    return Unauthorized("Invalid token");
                }

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

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveUrl(int id)
        {
            try
            {
                var token = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];
                var principal = jwtTokenService.ValidateToken(token);

                if (principal == null)
                {
                    return Unauthorized("Invalid token");
                }

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
