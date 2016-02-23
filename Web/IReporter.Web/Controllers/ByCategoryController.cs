namespace IReporter.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using IReporter.Common;
    using IReporter.Services.Data;
    using IReporter.Web.Infrastructure.Mapping;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using ViewModels.Home;

    public class ByCategoryController : BaseController
    {
        private readonly IArticlesService articles;

        public ByCategoryController(ICategoriesService categories, IArticlesService articles)
        {
            this.articles = articles;
        }

        public ActionResult Sport()
        {
            return this.View();
        }

        public ActionResult Music()
        {
            return this.View();
        }

        public ActionResult Politics()
        {
            return this.View();
        }

        public ActionResult Entertainment()
        {
            return this.View();
        }

        public ActionResult Tech()
        {
            return this.View();
        }

        public ActionResult Gossip()
        {
            return this.View();
        }

        public ActionResult Business()
        {
            return this.View();
        }

        public ActionResult Cinema()
        {
            return this.View();
        }

        public ActionResult GetAllSport([DataSourceRequest] DataSourceRequest request)
        {
            var allArticles = this.articles.GetAll()
                .Where(a => a.Category.Name == GlobalConstants.SportCategoryName)
                .OrderByDescending(x => x.CreatedOn)
                .To<ArticleViewModel>()
                .ToList();

            return this.Json(allArticles.ToDataSourceResult(request));
        }

        public ActionResult GetAllMusic([DataSourceRequest] DataSourceRequest request)
        {
            var allArticles = this.articles.GetAll()
                .Where(a => a.Category.Name == GlobalConstants.MusicCategoryName)
                .OrderByDescending(x => x.CreatedOn)
                .To<ArticleViewModel>()
                .ToList();

            return this.Json(allArticles.ToDataSourceResult(request));
        }

        public ActionResult GetAllPolitics([DataSourceRequest] DataSourceRequest request)
        {
            var allArticles = this.articles.GetAll()
                .Where(a => a.Category.Name == GlobalConstants.PoliticsCategoryName)
                .OrderByDescending(x => x.CreatedOn)
                .To<ArticleViewModel>()
                .ToList();

            return this.Json(allArticles.ToDataSourceResult(request));
        }

        public ActionResult GetAllEntertainment([DataSourceRequest] DataSourceRequest request)
        {
            var allArticles = this.articles.GetAll()
                .Where(a => a.Category.Name == GlobalConstants.EntertainmentCategoryName)
                .OrderByDescending(x => x.CreatedOn)
                .To<ArticleViewModel>()
                .ToList();

            return this.Json(allArticles.ToDataSourceResult(request));
        }

        public ActionResult GetAllGossip([DataSourceRequest] DataSourceRequest request)
        {
            var allArticles = this.articles.GetAll()
                .Where(a => a.Category.Name == GlobalConstants.GossipCategoryName)
                .OrderByDescending(x => x.CreatedOn)
                .To<ArticleViewModel>()
                .ToList();

            return this.Json(allArticles.ToDataSourceResult(request));
        }

        public ActionResult GetAllTech([DataSourceRequest] DataSourceRequest request)
        {
            var allArticles = this.articles.GetAll()
                .Where(a => a.Category.Name == GlobalConstants.TechCategoryName)
                .OrderByDescending(x => x.CreatedOn)
                .To<ArticleViewModel>()
                .ToList();

            return this.Json(allArticles.ToDataSourceResult(request));
        }

        public ActionResult GetAllBusiness([DataSourceRequest] DataSourceRequest request)
        {
            var allArticles = this.articles.GetAll()
                .Where(a => a.Category.Name == GlobalConstants.BusinessCategoryName)
                .OrderByDescending(x => x.CreatedOn)
                .To<ArticleViewModel>()
                .ToList();

            return this.Json(allArticles.ToDataSourceResult(request));
        }

        public ActionResult GetAllCinema([DataSourceRequest] DataSourceRequest request)
        {
            var allArticles = this.articles.GetAll()
                .Where(a => a.Category.Name == GlobalConstants.CinemaCategoryName)
                .OrderByDescending(x => x.CreatedOn)
                .To<ArticleViewModel>()
                .ToList();

            return this.Json(allArticles.ToDataSourceResult(request));
        }
    }
}
