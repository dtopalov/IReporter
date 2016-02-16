namespace IReporter.Web.Controllers
{
    using System.Web.Mvc;

    using IReporter.Services.Data;
    using IReporter.Web.ViewModels.Home;

    public class ArticlesController : BaseController
    {
        private readonly IArticlesService articles;

        public ArticlesController(
            IArticlesService articles)
        {
            this.articles = articles;
        }

        public ActionResult ById(string id)
        {
            var article = this.articles.GetById(id);
            var viewModel = this.Mapper.Map<ArticleViewModel>(article);
            return this.View(viewModel);
        }
    }
}
