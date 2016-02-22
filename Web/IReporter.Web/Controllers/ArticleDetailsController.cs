namespace IReporter.Web.Controllers
{
    using System;
    using System.Web.Mvc;

    using IReporter.Services.Data;
    using IReporter.Services.Web;
    using IReporter.Web.ViewModels.Home;

    public class ArticleDetailsController : BaseController
    {
        private readonly IArticlesService articles;
        private readonly IIdentifierProvider identifierProvider;

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

            var currentIntId = int.Parse(route.Substring(0, route.IndexOf("-", StringComparison.Ordinal)));
            var currentStringId = this.identifierProvider.EncodeId(currentIntId);
            var currentArticle = this.articles.GetById(currentStringId);
            if (currentArticle == null)
            {
                this.TempData["ErrorMessage"] = "No such article!";
                return this.View("Error");
            }

            currentArticle.NumberOfViews++;

            this.articles.Update(currentArticle);

            ArticleViewModel currentArticleViewModel = this.Mapper.Map<ArticleViewModel>(currentArticle);

            return this.View(currentArticleViewModel);
        }
    }
}
