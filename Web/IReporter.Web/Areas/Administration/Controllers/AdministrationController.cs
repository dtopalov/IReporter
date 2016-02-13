namespace IReporter.Web.Areas.Administration.Controllers
{
    using System.Web.Mvc;

    using IReporter.Common;
    using IReporter.Web.Controllers;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    public class AdministrationController : BaseController
    {
    }
}
