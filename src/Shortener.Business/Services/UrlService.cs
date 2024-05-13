using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Shortener.Business.Interfaces;
using Shortener.Business.Models;
using Shortener.Business.Tools;
using Shortener.Business.Validation;
using Shortener.Data.Entities;
using Shortener.Data.Interfaces;
using Shortener.Data.Validation;

namespace Shortener.Business.Services
{
    public class UrlService : IUrlService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IUrlRepository urlRepository;
        public UrlService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.urlRepository = unitOfWork.UrlRepository;
        }
        public async Task AddAsync(UrlShortenerModel model)
        {
            if (model == null)
            {
                throw new ShortenerException("UrlShortenerModel cannot be null");
            }

            try
            {
                Validate(model);

                await urlRepository.CheckIfUniqueFullUrlAsync(model.FullUrl);
                
                do
                {
                    model.GenerateShortUrl();
                } while (!await urlRepository.IsUniqueShortUrlAsync(model.ShortUrl));

                var url = mapper.Map<URL>(model);

                await urlRepository.AddAsync(url);

                await unitOfWork.SaveAsync();
            }
            catch (AlreadyExistException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ShortenerException(ex.Message);
            }
        }

        public async Task DeleteAsync(int modelId)
        {
            try
            {
                await urlRepository.DeleteByIdAsync(modelId);

                await unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                throw new ShortenerException(ex.Message);
            }
        }

        public async Task<IEnumerable<UrlShortenerModel>> GetAllAsync()
        {
            try
            {
                var urls = await urlRepository.GetAllAsync();

                return mapper.Map<IEnumerable<UrlShortenerModel>>(urls);
            }
            catch (Exception ex)
            {
                throw new ShortenerException(ex.Message);
            }
        }

        public async Task<UrlShortenerModel> GetByIdAsync(int id)
        {
            try
            {
                var url = await urlRepository.GetByIdAsync(id);

                return mapper.Map<UrlShortenerModel>(url);
            }
            catch (Exception ex)
            {
                throw new ShortenerException(ex.Message);
            }
        }
        
        public async Task<string> GetByShortedUrlAsync(string shortedUrl)
        {
            try
            {
                var url = await urlRepository.GetAll().FirstOrDefaultAsync(url => url.ShortUrl == shortedUrl);

                if (url == null)
                    throw new DatabaseDoesntContainException("An entry with such shortenedUrl was not found in the database");

                url.NumberOfAppeals++;
                url.LastAppeal = DateTime.Now;

                await urlRepository.Update(url);
                await unitOfWork.SaveAsync();

                return url!.FullUrl;
            }
            catch (DatabaseDoesntContainException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ShortenerException(ex.Message);
            }
        }

        public async Task UpdateAsync(UrlShortenerModel model)
        {
            if (model == null)
            {
                throw new ShortenerException("UrlShortenerModel cannot be null");
            }

            try
            {
                Validate(model);

                var url = mapper.Map<URL>(model);

                await urlRepository.Update(url);

                await unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                throw new ShortenerException(ex.Message);
            }
        }

        public static void Validate(UrlShortenerModel model)
        {
            if (string.IsNullOrWhiteSpace(model.FullUrl) || model.FullUrl.Length <= 1)
            {
                throw new ShortenerException("Invalid UrlShortenerModel.FullUrl");
            }

            if (model.NumberOfAppeals < 0)
            {
                throw new ShortenerException("Invalid UrlShortenerModel.NumberOfAppeals");
            }

            if (model.CreatedDate > DateTime.Now || model.CreatedDate < Convert.ToDateTime("2024-01-01"))
            {
                throw new ShortenerException("Invalid UrlShortenerModel.CreatedDate");
            }

            if (model.LastAppeal > DateTime.Now || model.LastAppeal < Convert.ToDateTime("2024-01-01") && model.LastAppeal != DateTime.MinValue)
            {
                throw new ShortenerException("Invalid UrlShortenerModel.LastAppeal");
            }

            if (model.UserId < 1)
            {
                throw new ShortenerException("Invalid UrlShortenerModel.UserId");
            }
        }
    }
}
