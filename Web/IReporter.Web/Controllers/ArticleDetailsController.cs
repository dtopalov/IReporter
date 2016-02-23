namespace IReporter.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using IReporter.Services.Data;
    using IReporter.Services.Web;

    using Microsoft.AspNet.Identity;

    using ViewModels.ArticleDetails;
    using ViewModels.Comment;
    using ViewModels.Home;

    public class ArticleDetailsController : BaseController
    {
        private readonly IArticlesService articles;
        private readonly IIdentifierProvider identifierProvider;
        private int currentArticleId;

        public ArticleDetailsController(IArticlesService articles, IIdentifierProvider identifierProvider)
        {
            this.articles = articles;
            this.identifierProvider = identifierProvider;
        }

        // GET: ArticleDetails
        public ActionResult Index(string route)
        {
            if (string.IsNullOrEmpty(route))
            {
                this.TempData["ErrorMessage"] = "No such article!";
                return this.View("Error");
            }

            this.currentArticleId = int.Parse(route.Substring(0, route.IndexOf("-", StringComparison.Ordinal)));
            var currentStringId = this.identifierProvider.EncodeId(this.currentArticleId);
            var currentArticle = this.articles.GetById(currentStringId);
            if (currentArticle == null)
            {
                this.TempData["ErrorMessage"] = "No such article!";
                return this.View("Error");
            }

            currentArticle.NumberOfViews++;

            this.articles.Update(currentArticle);

            ArticleViewModel currentArticleViewModel = this.Mapper.Map<ArticleViewModel>(currentArticle);
            // currentArticleViewModel.Id = this.currentArticleId;
            currentArticleViewModel.CurrentUserHasVoted =
                currentArticle.Votes.Any(v => v.AuthorId == this.User.Identity.GetUserId());

            var vm = new ArticleWithCommentsViewModel
                         {
                             Article = currentArticleViewModel,
                             Comment = new CommentPostModel()
                         };

            return this.View(vm);
        }
    }
}
