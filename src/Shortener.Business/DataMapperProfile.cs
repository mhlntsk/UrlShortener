using AutoMapper;
using Shortener.Business.Models;
using Shortener.Data.Entities;
using System.IO;

namespace Shortener.Business
{
    public class DataMapperProfile : Profile
    {
        public DataMapperProfile()
        {
            CreateMap<UrlShortenerModel, URL>()
                .ReverseMap();
        }
    }
}
