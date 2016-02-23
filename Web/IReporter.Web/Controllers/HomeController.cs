namespace IReporter.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using IReporter.Services.Data;
    using IReporter.Web.Infrastructure.Mapping;
    using ViewModels.Home;

    public class HomeController : BaseController
    {
        private readonly IArticlesService articles;
        private readonly ICategoriesService categories;

        public HomeController(
            IArticlesService articles,
            ICategoriesService categories)
        {
            this.articles = articles;
            this.categories = categories;
        }

        public ActionResult Index()
        {
            var allArticles = this.articles.GetAll().To<ArticleViewModel>().ToList();
            var allCategories =
                this.Cache.Get(
                    "categories",
                    () => this.categories.GetAll().To<CategoryViewModel>().ToList(),
                    30 * 60);

            var viewModel = new IndexViewModel
            {
                Articles = allArticles,
                Categories = allCategories
            };

            return this.View(viewModel);
        }
    }
}
