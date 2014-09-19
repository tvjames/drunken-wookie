using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using ServiceStack;
using ServiceStack.Logging.NLogger;
using ServiceStack.Razor;
using ServiceStack.Validation;

namespace DrunkenWookie.Evictus
{
    public class AppHost : AppHostBase
    {
        public AppHost()
            : base("Evictus - Drunken Wookie", typeof(AppHost).Assembly)
        {
        }

        public static void Start()
        {
            NLog.LogManager.GetCurrentClassLogger().Info("Start");

            new AppHost().Init();
        }

        public override void Configure(Funq.Container container)
        {
            ServiceStack.Text.JsConfig.EmitCamelCaseNames = true;
            ServiceStack.Text.JsConfig.IncludeNullValues = true;
            ServiceStack.Logging.LogManager.LogFactory = new NLogFactory();

            Plugins.Add(new RazorFormat { LoadFromAssemblies = new List<Assembly> { typeof(AppHost).Assembly } });
            Plugins.Add(new ValidationFeature());

            container.RegisterValidators(typeof(AppHost).Assembly);

            RewriteReturnVoidToNoContent();
        }

        private void RewriteReturnVoidToNoContent()
        {
            this.GlobalResponseFilters.Add((req, res, dto) =>
            {
                var returnVoid = req.Dto as IReturnVoid;
                if (returnVoid == null)
                    return;

                if (dto == null && res.StatusCode == 200)
                {
                    res.StatusCode = (int)HttpStatusCode.NoContent;
                    res.StatusDescription = "No Content";
                }
            });
        }
    }
}