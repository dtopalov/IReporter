namespace IReporter.Web.Controllers.Tests
{
    using IReporter.Data.Models;
    using IReporter.Services.Data;
    using IReporter.Web.Infrastructure.Mapping;
    using IReporter.Web.ViewModels.Home;

    using Moq;

    using NUnit.Framework;

    using TestStack.FluentMVCTesting;

    [TestFixture]
    public class ArticlesControllerTests
    {
        [Test]
        public void ByIdShouldWorkCorrectly()
        {
            var autoMapperConfig = new AutoMapperConfig();
            autoMapperConfig.Execute(typeof(ArticlesController).Assembly);
            const string ArticleContent = "SomeContent";
            var articlesServiceMock = new Mock<IArticlesService>();
            articlesServiceMock.Setup(x => x.GetById(It.IsAny<string>()))
                .Returns(new Article { Content = ArticleContent, Category = new Category { Name = "asda" } });
            var controller = new ArticlesController(articlesServiceMock.Object);
            controller.WithCallTo(x => x.ById("asdasasd"))
                .ShouldRenderView("ById")
                .WithModel<ArticleViewModel>(
                    viewModel =>
                        {
                            Assert.AreEqual(ArticleContent, viewModel.Content);
                        }).AndNoModelErrors();
        }
    }
}
