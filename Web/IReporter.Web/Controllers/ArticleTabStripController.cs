namespace IReporter.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using IReporter.Services.Data;
    using IReporter.Web.Infrastructure.Mapping;
    using ViewModels.Home;

    public class ArticleTabStripController : BaseController
    {
        private readonly IArticlesService articles;

        public ArticleTabStripController(IArticlesService articles)
        {
            this.articles = articles;
        }

        public ActionResult GetMostRead(string id)
        {
            var mostReadArticles =
                this.articles.GetAll()
                    .Where(a => a.Category.Name == id)
                    .OrderByDescending(a => a.NumberOfViews)
                    .To<ArticleViewModel>()
                    .Take(5)
                    .ToList();

             return this.PartialView("_MostReadArticles", mostReadArticles);
        }

        public ActionResult GetMostLiked(string id)
        {
            var mostLikedArticles =
                 this.articles.GetAll()
                     .Where(a => a.Category.Name == id)
                     .OrderByDescending(a => a.Votes.Sum(x => x.Value))
                     .To<ArticleViewModel>()
                     .Take(5)
                     .ToList();

            return this.PartialView("_MostLikedArticles", mostLikedArticles);
        }

        public ActionResult GetMostCommented(string id)
        {
            var mostCommentedArticles =
                 this.articles.GetAll()
                     .Where(a => a.Category.Name == id)
                     .OrderByDescending(a => a.Comments.Count)
                     .To<ArticleViewModel>()
                     .Take(5)
                     .ToList();

            return this.PartialView("_MostCommentedArticles", mostCommentedArticles);
        }

        public ActionResult GetNewest(string id)
        {
            var newestArticles =
                 this.articles.GetAll()
                     .Where(a => a.Category.Name == id)
                     .OrderByDescending(a => a.CreatedOn)
                     .To<ArticleViewModel>()
                     .Take(5)
                     .ToList();

            return this.PartialView("_NewestArticles", newestArticles);
        }
    }
}
