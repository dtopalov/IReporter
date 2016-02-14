namespace IReporter.Web.Routes.Tests
{
    using System.Web.Routing;

    using IReporter.Web.Controllers;

    using MvcRouteTester;

    using NUnit.Framework;

    [TestFixture]
    public class JokesRouteTests
    {
        [Test]
        public void TestRouteById()
        {
            const string Url = "/Joke/123";
            var routeCollection = new RouteCollection();
            RouteConfig.RegisterRoutes(routeCollection);
            routeCollection.ShouldMap(Url).To<ArticlesController>(c => c.ById(123));
        }
    }
}
