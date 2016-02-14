namespace Crawler
{
    using System;
    using AngleSharp;
    using IReporter.Data;
    using IReporter.Data.Common;
    using IReporter.Data.Models;
    using IReporter.Services.Data;

    public static class Program
    {
        public static void Main()
        {
            var db = new ApplicationDbContext();
            var repo = new DbRepository<Category>(db);
            var categoriesService = new CategoriesService(repo);

            var configuration = Configuration.Default.WithDefaultLoader();
            var browsingContext = BrowsingContext.New(configuration);

            for (int i = 1; i <= 10000; i++)
            {
                var url = $"http://vicove.com/vic-{i}";
                var document = browsingContext.OpenAsync(url).Result;
                var articleContent = document.QuerySelector("#content_box .post-content").TextContent.Trim();
                if (!string.IsNullOrWhiteSpace(articleContent))
                {
                    var categoryName = document.QuerySelector("#content_box .thecategory a").TextContent.Trim();
                    var category = categoriesService.EnsureCategory(categoryName);
                    var article = new Article { Category = category, Content = articleContent };
                    db.Articles.Add(article);
                    db.SaveChanges();
                    Console.WriteLine(i);
                }
            }
        }
    }
}
