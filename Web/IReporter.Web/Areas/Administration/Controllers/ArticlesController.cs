namespace IReporter.Web.Areas.Administration.Controllers
{
    using System.Data.Entity;
    using System.Linq;
    using System.Web.Mvc;

    using IReporter.Common;
    using IReporter.Data;
    using IReporter.Data.Models;
    using IReporter.Services.Data;
    using IReporter.Web.Areas.Administration.Models;
    using IReporter.Web.Controllers;
    using IReporter.Web.Infrastructure.Mapping;
    using IReporter.Web.ViewModels.Home;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;

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
            var allArticles = this.articles.GetAll().OrderByDescending(x => x.CreatedOn).To<ArticleEditModel>();

            return this.Json(allArticles.ToDataSourceResult(request));
        }

        //[HttpPost]
        //public ActionResult ArticleViewModels_Create([DataSourceRequest]DataSourceRequest request, Article articleViewModel)
        //{
        //    if (this.ModelState.IsValid)
        //    {
        //        var entity = new Article
        //        {
        //            Title = articleViewModel.Title,
        //            PrimaryImageUrl = articleViewModel.PrimaryImageUrl,
        //            Excerpt = articleViewModel.Excerpt,
        //            Content = articleViewModel.Content,
        //            Category = articleViewModel.Category,
        //            NumberOfViews = articleViewModel.NumberOfViews,
        //            CreatedOn = articleViewModel.CreatedOn
        //        };

        //        this.articles.Create(entity);

        //        articleViewModel.Id = entity.Id;
        //    }

        //    return Json(new[] { articleViewModel }.ToDataSourceResult(request, ModelState));
        //}

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

        //[AcceptVerbs(HttpVerbs.Post)]
        //public ActionResult ArticleViewModels_Destroy([DataSourceRequest]DataSourceRequest request, ArticleViewModel articleViewModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var entity = new ArticleViewModel
        //        {
        //            Id = articleViewModel.Id,
        //            Title = articleViewModel.Title,
        //            PrimaryImageUrl = articleViewModel.PrimaryImageUrl,
        //            Excerpt = articleViewModel.Excerpt,
        //            Content = articleViewModel.Content,
        //            Category = articleViewModel.Category,
        //            Rating = articleViewModel.Rating,
        //            NumberOfViews = articleViewModel.NumberOfViews,
        //            CurrentUserHasVoted = articleViewModel.CurrentUserHasVoted,
        //            CreatedOn = articleViewModel.CreatedOn
        //        };

        //        db.ArticleViewModels.Attach(entity);
        //        db.ArticleViewModels.Remove(entity);
        //        db.SaveChanges();
        //    }

        //    return Json(new[] { articleViewModel }.ToDataSourceResult(request, ModelState));
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    articles.Dispose();
        //    base.Dispose(disposing);
        //}
    }
}
