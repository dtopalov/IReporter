namespace IReporter.Web.Controllers
{
    using System.Web.Mvc;

    using AutoMapper;

    using IReporter.Services.Web;
    using IReporter.Web.Infrastructure.Mapping;

    public abstract class BaseController : Controller
    {
        public ICacheService Cache { get; set; }

        protected IMapper Mapper
        {
            get
            {
                return AutoMapperConfig.Configuration.CreateMapper();
            }
        }
    }
}
