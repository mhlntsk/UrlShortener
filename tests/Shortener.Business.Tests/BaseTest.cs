using AutoMapper;
using NSubstitute;
using Shortener.Data.Interfaces;

namespace Shortener.Business.Tests
{
    public class BaseTest
    {
        protected IUnitOfWork unitOfWork;
        protected IMapper mapper;

        [OneTimeSetUp]
        public void RunOnce()
        {
            unitOfWork = Substitute.For<IUnitOfWork>();
            mapper = Substitute.For<IMapper>();
        }
    }
}
