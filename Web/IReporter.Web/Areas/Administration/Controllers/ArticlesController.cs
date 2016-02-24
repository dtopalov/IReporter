namespace IReporter.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using IReporter.Common;
    using IReporter.Data.Models;
    using IReporter.Services.Data;
    using IReporter.Web.Controllers;
    using IReporter.Web.Infrastructure.Mapping;
    using IReporter.Web.ViewModels.Home;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using Models;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    public class ArticlesController : BaseController
    {
        private readonly IArticlesService articles;
        private readonly ICategoriesService categories;

        public ArticlesController(IArticlesService articles, ICategoriesService categories)
        {
            this.articles = articles;
            this.categories = categories;
        }

        public ActionResult AllArticles()
        {
            var allCategories =
                this.Cache.Get(
                    "categories",
                    () => this.categories.GetAll().To<CategoryViewModel>().ToList(),
                    30 * 60);
            this.ViewData["categories"] = allCategories;
            return this.PartialView("_AllArticles");
        }

        public ActionResult ArticlesRead([DataSourceRequest]DataSourceRequest request)
        {
            var allArticles = this.articles.GetAllWithDeleted()
                .OrderByDescending(x => x.CreatedOn)
                .To<ArticleEditModel>()
                .ToList();

            return this.Json(allArticles.ToDataSourceResult(request));
        }

        [HttpPost]
        public ActionResult ArticlesUpdate([DataSourceRequest]DataSourceRequest request, ArticleEditModel article)
        {
            if (this.ModelState.IsValid)
            {
                var originalArticle = this.articles.GetAll().FirstOrDefault(a => a.Id == article.Id);
                if (originalArticle != null)
                {
                    originalArticle.CategoryId = article.CategoryId;
                    originalArticle.Content = article.Content;
                    originalArticle.Excerpt = article.Excerpt;
                    originalArticle.PrimaryImageUrl = article.PrimaryImageUrl;
                    originalArticle.Title = article.Title;
                }

                this.articles.Update(originalArticle);
            }

            return this.Json(new[] { article }.ToDataSourceResult(request, this.ModelState));
        }

        [HttpPost]
        public ActionResult ArticlesCreate([DataSourceRequest]DataSourceRequest request, ArticleEditModel article)
        {
            if (this.ModelState.IsValid)
            {
                var newArticle = new Article
                                     {
                                         CategoryId = int.Parse(article.Category),
                                         Content = article.Content,
                                         Excerpt = article.Excerpt,
                                         PrimaryImageUrl = article.PrimaryImageUrl,
                                         Title = article.Title
                                     };

                this.articles.Create(newArticle);
            }

            return this.Json(new[] { article }.ToDataSourceResult(request, this.ModelState));
        }

        [HttpPost]
        public ActionResult ArticlesDelete([DataSourceRequest]DataSourceRequest request, ArticleEditModel article)
        {
            if (this.ModelState.IsValid)
            {
                var originalArticle = this.articles.GetAll().FirstOrDefault(a => a.Id == article.Id);
                if (originalArticle != null)
                {
                    originalArticle.IsDeleted = true;
                }

                this.articles.Update(originalArticle);
            }

            return this.Json(new[] { article }.ToDataSourceResult(request, this.ModelState));
        }
    }
}
