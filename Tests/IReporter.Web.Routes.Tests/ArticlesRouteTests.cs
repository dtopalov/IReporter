namespace IReporter.Web.Routes.Tests
{
    using System.Web.Routing;

    using IReporter.Web.Controllers;

    using MvcRouteTester;

    using NUnit.Framework;

    [TestFixture]
    public class ArticlesRouteTests
    {
        [Test]
        public void TestRouteById()
        {
            const string Url = "/Joke/Mjc2NS4xMjMxMjMxMzEyMw==";
            var routeCollection = new RouteCollection();
            RouteConfig.RegisterRoutes(routeCollection);
            routeCollection.ShouldMap(Url).To<ArticlesController>(c => c.ById("Mjc2NS4xMjMxMjMxMzEyMw=="));
        }
    }
}
