namespace IReporter.Web.ViewModels.Home
{
    using System.Collections.Generic;

    public class IndexViewModel
    {
        public IEnumerable<ArticleViewModel> Articles { get; set; }

        public IEnumerable<CategoryViewModel> Categories { get; set; }
    }
}
