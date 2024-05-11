using Shortener.Business.Models;

namespace Shortener.Business.Tests
{
    public class DataInitializer
    {
        public static IEnumerable<UrlShortenerModel> GetValidUrlShortenerModels()
        {
            return new List<UrlShortenerModel>
            {
                new UrlShortenerModel
                {
                    FullUrl = "https://inforce.digital/about-us",
                    NumberOfAppeals = 0,
                    CreatedDate = DateTime.Now.AddSeconds(-1),
                    LastAppeal = DateTime.MinValue,
                    UserId = 12,
                },
                new UrlShortenerModel
                {
                    FullUrl = "https://drive.google.com/file/d/1yLTwR1_5aYbpiB_p-dsQMtkie_UJ0cwK/view",
                    NumberOfAppeals = 8,
                    CreatedDate = DateTime.Now.AddMonths(-2),
                    LastAppeal = DateTime.Now.AddDays(-5),
                    UserId = 1,
                },
            };
        }

        public static IEnumerable<UrlShortenerModel> GetUrlShortenerModelsWithInvalidFullUrl()
        {
            return new List<UrlShortenerModel>
            {
                new UrlShortenerModel
                {
                    FullUrl = "",
                    NumberOfAppeals = 0,
                    CreatedDate = DateTime.Now.AddSeconds(-1),
                    LastAppeal = DateTime.MinValue,
                    UserId = 12,
                },
                new UrlShortenerModel
                {
                    FullUrl = null,
                    NumberOfAppeals = 8,
                    CreatedDate = DateTime.Now.AddMonths(-2),
                    LastAppeal = DateTime.Now.AddDays(-5),
                    UserId = 1,
                },
                new UrlShortenerModel
                {
                    FullUrl = "    ",
                    NumberOfAppeals = 13,
                    CreatedDate = DateTime.Now.AddMonths(-3),
                    LastAppeal = DateTime.Now.AddDays(-15),
                    UserId = 76,
                },
            };
        }

        public static IEnumerable<UrlShortenerModel> GetUrlShortenerModelsWithInvalidNumberOfAppeals()
        {
            return new List<UrlShortenerModel>
            {
                new UrlShortenerModel
                {
                    FullUrl = "https://inforce.digital/about-us",
                    NumberOfAppeals = -1,
                    CreatedDate = DateTime.Now.AddSeconds(-1),
                    LastAppeal = DateTime.MinValue,
                    UserId = 12,
                },
                new UrlShortenerModel
                {
                    FullUrl = "https://drive.google.com/file/d/1yLTwR1_5aYbpiB_p-dsQMtkie_UJ0cwK/view",
                    NumberOfAppeals = -3245,
                    CreatedDate = DateTime.Now.AddMonths(-2),
                    LastAppeal = DateTime.Now.AddDays(-5),
                    UserId = 1,
                },
            };
        }

        public static IEnumerable<UrlShortenerModel> GetUrlShortenerModelsWithInvalidCreatedDate()
        {
            return new List<UrlShortenerModel>
            {
                new UrlShortenerModel
                {
                    FullUrl = "https://inforce.digital/about-us",
                    NumberOfAppeals = 0,
                    CreatedDate = DateTime.Now.AddYears(-13),
                    LastAppeal = DateTime.MinValue,
                    UserId = 12,
                },
                new UrlShortenerModel
                {
                    FullUrl = "https://drive.google.com/file/d/1yLTwR1_5aYbpiB_p-dsQMtkie_UJ0cwK/view",
                    NumberOfAppeals = 8,
                    CreatedDate = DateTime.Now.AddHours(1),
                    LastAppeal = DateTime.Now.AddDays(-5),
                    UserId = 1,
                },
            };
        }

        public static IEnumerable<UrlShortenerModel> GetUrlShortenerModelsWithInvalidLastAppeal()
        {
            return new List<UrlShortenerModel>
            {
                new UrlShortenerModel
                {
                    FullUrl = "https://inforce.digital/about-us",
                    NumberOfAppeals = 0,
                    CreatedDate = DateTime.Now.AddYears(-13),
                    LastAppeal = DateTime.MinValue,
                    UserId = 12,
                },
                new UrlShortenerModel
                {
                    FullUrl = "https://drive.google.com/file/d/1yLTwR1_5aYbpiB_p-dsQMtkie_UJ0cwK/view",
                    NumberOfAppeals = 8,
                    CreatedDate = DateTime.Now.AddMonths(-2),
                    LastAppeal = DateTime.Now.AddHours(1),
                    UserId = 1,
                },
            };
        }

        public static IEnumerable<UrlShortenerModel> GetUrlShortenerModelsWithInvalidUserId()
        {
            return new List<UrlShortenerModel>
            {
                new UrlShortenerModel
                {
                    FullUrl = "https://inforce.digital/about-us",
                    NumberOfAppeals = 0,
                    CreatedDate = DateTime.Now.AddSeconds(-1),
                    LastAppeal = DateTime.MinValue,
                    UserId = -243,
                },
                new UrlShortenerModel
                {
                    FullUrl = "https://drive.google.com/file/d/1yLTwR1_5aYbpiB_p-dsQMtkie_UJ0cwK/view",
                    NumberOfAppeals = 8,
                    CreatedDate = DateTime.Now.AddMonths(-2),
                    LastAppeal = DateTime.Now.AddDays(-5),
                    UserId = 0,
                },
            };
        }
    }
}
