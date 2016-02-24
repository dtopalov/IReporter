namespace IReporter.Web.Areas.Editors.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using IReporter.Common;
    using IReporter.Data.Models;
    using IReporter.Services.Data;
    using IReporter.Web.Areas.Administration.Models;
    using IReporter.Web.Controllers;
    using IReporter.Web.Infrastructure.Mapping;
    using IReporter.Web.ViewModels.Home;

    using Microsoft.AspNet.Identity;

    public class EditorsController : BaseController
    {
        private readonly ICategoriesService categories;

        private readonly IArticlesService articles;

        public EditorsController(ICategoriesService categories, IArticlesService articles)
        {
            this.categories = categories;
            this.articles = articles;
        }

        [HttpGet]
        public ActionResult AddArticle()
        {
            var allCategories = this.categories.GetAll().To<CategoryViewModel>().ToList();
            this.ViewData["categories"] = allCategories;

            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddArticle(ArticleEditModel input)
        {
            if (this.ModelState.IsValid)
            {
                var newArticle = new Article
                                     {
                                         Title = input.Title,
                                         PrimaryImageUrl = input.PrimaryImageUrl,
                                         Excerpt = input.Excerpt,
                                         Content = input.Content,
                                         CategoryId = input.CategoryId,
                                         AuthorId = this.User.Identity.GetUserId()
                                     };

                this.articles.Create(newArticle);
                this.TempData["Success"] = GlobalConstants.ThankYouMessage;

                return this.Redirect("/");
            }

            return this.View(input);
        }
    }
}
