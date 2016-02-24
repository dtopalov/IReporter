namespace IReporter.Web.Areas.Editors
{
    using System.Web.Mvc;

    public class EditorsAreaRegistration : AreaRegistration
    {
        public override string AreaName => "Editors";

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Editors_default",
                "Editors/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional });
        }
    }
}
