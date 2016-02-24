namespace IReporter.Web.Controllers
{
    using System;
    using System.Web.Http;
    using System.Web.Mvc;

    using IReporter.Common;

    public class ErrorController : BaseController
    {
        // GET: Error
        public ActionResult Index()
        {
            var url = this.Request.Url;
            if (url != null)
            {
                var invalidPath = url.Query.Substring(url.Query.IndexOf("/", StringComparison.Ordinal));

                this.TempData["ErrorMessage"] = GlobalConstants.GlobalErrorMessage + $" - {invalidPath} is not a valid path.";
            }

            return this.View("Error");
        }
    }
}
