using Microsoft.AspNetCore.Mvc;
using Shortener.Business.Interfaces;
using Shortener.Business.Models;

namespace Shortener.Presentation.Controllers
{
    [Route("nav")]
    [ApiController]
    public class NavigationController : ControllerBase
    {
        private readonly IUrlService urlService;
        public NavigationController(IUrlService urlService)
        {
            this.urlService = urlService;
        }

        [HttpGet("{shortedUrl}")]
        public async Task<ActionResult<UrlShortenerModel>> GetUrl(string shortedUrl)
        {
            try
            {
                var fullUrl = await urlService.GetByShortedUrlAsync(shortedUrl);

                if (fullUrl == null)
                    return NoContent();

                return new RedirectResult(fullUrl);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
