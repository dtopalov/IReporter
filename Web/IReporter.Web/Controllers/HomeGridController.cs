namespace IReporter.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using IReporter.Services.Data;
    using IReporter.Web.Infrastructure.Mapping;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using ViewModels.Home;

    public class HomeGridController : BaseController
    {
        private readonly IArticlesService articles;

        public HomeGridController(
            IArticlesService articles,
            ICategoriesService categories)
        {
            this.articles = articles;
        }

        public ActionResult GetAll([DataSourceRequest] DataSourceRequest request)
        {
            var allArticles = this.articles.GetAll().OrderByDescending(x => x.CreatedOn).To<ArticleViewModel>();

            return this.Json(allArticles.ToDataSourceResult(request));
        }
    }
}
