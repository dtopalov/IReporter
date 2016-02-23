using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IReporter.Web.Areas.Editors.Controllers
{
    using IReporter.Web.Controllers;

    public class EditorsController : BaseController
    {
        [HttpGet]
        public ActionResult AddArticle()
        {
            return null;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddArticle(object input)
        {
            return null;
        }
    }
}