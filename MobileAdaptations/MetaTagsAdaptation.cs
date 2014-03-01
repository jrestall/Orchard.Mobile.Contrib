//using System.Web;
//using Orchard;
//using Orchard.DisplayManagement.Implementation;
//using Orchard.Mvc;
//using Orchard.UI.Resources;

//namespace Contrib.Mobile.MobileAdaptations
//{
//    /// <summary>
//    /// Adds mobile meta tags to the head tag for mobile devices
//    /// </summary>
//    public class MetaTagsAdaptation : IShapeDisplayEvents
//    {
//        private readonly IWorkContextAccessor _workContextAccessor;
//        private readonly IHttpContextAccessor _httpContextAccessor;

//        public MetaTagsAdaptation(IWorkContextAccessor workContextAccessor, IHttpContextAccessor httpContextAccessor)
//        {
//            _workContextAccessor = workContextAccessor;
//            _httpContextAccessor = httpContextAccessor;
//        }

//        public void Displaying(ShapeDisplayingContext context)
//        {
//            if (!IsMobileDevice() || context.ShapeMetadata.Type != "Metas") return;

//            var workContext = _workContextAccessor.GetContext(_httpContextAccessor.Current());
//            var resourceManager = workContext.Resolve<IResourceManager>();

//            AddViewportMetaTag(resourceManager, workContext.HttpContext.Request.Browser);

//            // Mobile IE
//            // <meta name="MobileOptimized" content="width" />

//            resourceManager.SetMeta(new MetaEntry
//            {
//                Name = "MobileOptimized",
//                Content = "width"
//            });

//            // Blackberry -->
//            // <meta name="HandheldFriendly" content="true" />

//            resourceManager.SetMeta(new MetaEntry
//            {
//                Name = "HandheldFriendly",
//                Content = "true"
//            });
//        }

//        private bool IsMobileDevice()
//        {
//            return _httpContextAccessor.Current().Request.Browser.IsMobileDevice;
//        }

//        public void Displayed(ShapeDisplayedContext context)
//        {
            
//        }

//        private void AddViewportMetaTag(IResourceManager resourceManager, HttpBrowserCapabilitiesBase browser)
//        {
//            // <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />
//            string content;

//            bool isViewportTagSupported = GetViewportTagSupported(browser);
//            if (isViewportTagSupported)
//            {
//                // Calculate the viewport tag based on wurfl browser capabilities
//                string initialScale;
//                TryGetCapability(browser, "viewport_initial_scale", out initialScale);

//                string maximumScale;
//                TryGetCapability(browser, "viewport_maximum_scale", out maximumScale);

//                string minimumScale;
//                TryGetCapability(browser, "viewport_minimum_scale", out minimumScale);

//                content = string.Format("width={0}, initial-scale={1}, maximum-scale={2}, minimum-scale={3}, user-scalable=no",
//                    GetViewportWidth(browser), initialScale, maximumScale, minimumScale);
//            }
//            else
//            {
//                // Set a sensible default - device should just ignore this tag if not supported so send it anyway in case wurfl is wrong.
//                content = "width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no";
//            }

//            resourceManager.SetMeta(new MetaEntry
//            {
//                Name = "viewport",
//                Content = content
//            });
//        }

//        private bool GetViewportTagSupported(HttpBrowserCapabilitiesBase browser)
//        {
//            string supportedText = browser["viewport_supported"];
//            if (string.IsNullOrEmpty(supportedText))
//            {
//                return false;
//            }

//            bool supported;
//            if (bool.TryParse(supportedText, out supported))
//            {
//                return supported;
//            }

//            return false;
//        }

//        private string GetViewportWidth(HttpBrowserCapabilitiesBase browser)
//        {
//            string viewportWidthKey;
//            if (!TryGetCapability(browser, "viewport_width", out viewportWidthKey))
//            {
//                return string.Empty;
//            }

//            string width;
//            if (string.Equals(viewportWidthKey, "device_width_token"))
//            {
//                width = "device-width";
//            }
//            else if (string.Equals(viewportWidthKey, "width_equals_resolution_width"))
//            {
//                TryGetCapability(browser, "resolution_width", out width);
//            }
//            else if (string.Equals(viewportWidthKey, "width_equals_max_image_width"))
//            {
//                TryGetCapability(browser, "max_image_width", out width);
//            }
//            else
//            {
//                width = string.Empty;
//            }

//            return width;
//        }

//        private static bool TryGetCapability(HttpBrowserCapabilitiesBase browser, string key, out string value)
//        {
//            value = browser[key];
//            if (value == null)
//            {
//                value = string.Empty;
//                return false;
//            }

//            return true;
//        }
//    }
//}