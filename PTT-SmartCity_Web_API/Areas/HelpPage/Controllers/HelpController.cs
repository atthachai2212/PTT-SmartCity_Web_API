using System;
using System.Web.Http;
using System.Web.Mvc;
using PTT_SmartCity_Web_API.Areas.HelpPage.ModelDescriptions;
using PTT_SmartCity_Web_API.Areas.HelpPage.Models;

namespace PTT_SmartCity_Web_API.Areas.HelpPage.Controllers
{
    /// <summary>
    /// The controller that will handle requests for the help page.
    /// </summary>
    public class HelpController : Controller
    {
        private const string ErrorViewName = "Error";

        public HelpController()
            : this(GlobalConfiguration.Configuration)
        {
        }

        public HelpController(HttpConfiguration config)
        {
            ViewBag.AppTitle = "REST API";
            ViewBag.AppTitleIcon = "fa fa-file-code-o";
            ViewBag.AppSubtitle = "SmartCity API Help Page";
            ViewBag.AppBreadcrumbMemu = "REST API";
            ViewBag.AppBreadcrumbItem = "API Help Page";
            ViewBag.AppBreadcrumbItemIcon = "fa fa-file-code-o";
            Configuration = config;
        }

        public HttpConfiguration Configuration { get; private set; }

        public ActionResult Index(string ctrId)
        {

            ViewBag.ctrId = ctrId;
            ViewBag.DocumentationProvider = Configuration.Services.GetDocumentationProvider();
            var ApiDescriptions = Configuration.Services.GetApiExplorer().ApiDescriptions;
            return View(Configuration.Services.GetApiExplorer().ApiDescriptions);
        }
        public ActionResult BusTracking()
        {
            ViewBag.DocumentationProvider = Configuration.Services.GetDocumentationProvider();
            return View(Configuration.Services.GetApiExplorer().ApiDescriptions);
        }
        public ActionResult Api(string apiId)
        {
            if (!String.IsNullOrEmpty(apiId))
            {
                HelpPageApiModel apiModel = Configuration.GetHelpPageApiModel(apiId);
                if (apiModel != null)
                {
                    return View(apiModel);
                }
            }

            return View(ErrorViewName);
        }

        public ActionResult ResourceModel(string modelName)
        {
            if (!String.IsNullOrEmpty(modelName))
            {
                ModelDescriptionGenerator modelDescriptionGenerator = Configuration.GetModelDescriptionGenerator();
                ModelDescription modelDescription;
                if (modelDescriptionGenerator.GeneratedModels.TryGetValue(modelName, out modelDescription))
                {
                    return View(modelDescription);
                }
            }

            return View(ErrorViewName);
        }
    }
}