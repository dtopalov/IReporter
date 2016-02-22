namespace IReporter.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using IReporter.Services.Data;
    using IReporter.Web.Infrastructure.Mapping;
    using IReporter.Web.ViewModels.Home;

    public class HomeTabStripController : BaseController
    {
        private readonly IArticlesService articles;

        public HomeTabStripController(IArticlesService articles)
        {
            this.articles = articles;
        }

        public ActionResult GetMostRead()
        {
            var mostReadArticles = this.articles.GetAll()
                .OrderByDescending(a => a.NumberOfViews)
                .To<ArticleViewModel>()
                .Take(5)
                .ToList();

            return this.PartialView("_MostReadArticles", mostReadArticles);
        }

        public ActionResult GetMostLiked()
        {
            var mostLikedArticles = this.articles.GetAll()
                .OrderByDescending(a => a.Rating)
                .To<ArticleViewModel>()
                .Take(5)
                .ToList();

            return this.PartialView("_MostLikedArticles", mostLikedArticles);
        }

        public ActionResult GetMostCommented()
        {
            var mostCommentedArticles = this.articles.GetAll()
                .OrderByDescending(a => a.Comments.Count)
                .To<ArticleViewModel>()
                .Take(5)
                .ToList();

            return this.PartialView("_MostCommentedArticles", mostCommentedArticles);
        }

        public ActionResult GetNewest()
        {
            var newestArticles = this.articles.GetAll()
                .OrderByDescending(a => a.CreatedOn)
                .To<ArticleViewModel>()
                .Take(5)
                .ToList();

            return this.PartialView("_NewestArticles", newestArticles);
        }

        public ActionResult GetWeather()
        {
            return this.PartialView("_Weather");
        }
    }
}
