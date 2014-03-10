using Orchard;
using Orchard.DisplayManagement.Implementation;
using Orchard.Mvc;
using Orchard.UI.Resources;

namespace Contrib.Mobile.MobileAdaptations
{
    /// <summary>
    /// Measures to avoid transcoding proxies from doing any content reformatting
    /// </summary>
    public class TranscodingProxyAdaptation : IShapeDisplayEvents
    {
        private readonly IWorkContextAccessor _workContextAccessor;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TranscodingProxyAdaptation(IWorkContextAccessor workContextAccessor, IHttpContextAccessor httpContextAccessor)
        {
            _workContextAccessor = workContextAccessor;
            _httpContextAccessor = httpContextAccessor;
        }

        public void Displaying(ShapeDisplayingContext context)
        {
            if(!IsMobileDevice())
            {
                return;
            }

            var shapeMetadata = context.ShapeMetadata;
            if (shapeMetadata.Type != "HeadLinks" && shapeMetadata.Type != "Metas") return;

            var workContext = _workContextAccessor.GetContext(_httpContextAccessor.Current());
            var resourceManager = workContext.Resolve<IResourceManager>();

            if (shapeMetadata.Type == "HeadLinks")
            {
                // <link rel="alternate" type="text/html" media="handheld" href="" />
                var handheldLink = new LinkEntry
                                        {
                                            Type = "text/html",
                                            Rel = "alternate",
                                            Href = ""
                                        };

                handheldLink.AddAttribute("media", "handheld");

                resourceManager.RegisterLink(handheldLink);

                // Set the transcoding protection http response headers
                workContext.HttpContext.Response.Cache.SetNoTransforms();
                workContext.HttpContext.Response.AppendHeader("Vary", "User-Agent");
            }
            else if (shapeMetadata.Type == "Metas")
            {
                resourceManager.SetMeta(new MetaEntry
                                            {
                                                // HACK: Orchard doesn't allow metas without a name.
                                                Name = "no-transform", 
                                                HttpEquiv = "Cache-control",
                                                Content = "no-transform"
                                            });
            }
        }

        private bool IsMobileDevice()
        {
            return _httpContextAccessor.Current().Request.Browser.IsMobileDevice;
        }

        public void Displayed(ShapeDisplayedContext context)
        {
            
        }
    }
}