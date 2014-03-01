//using System;
//using System.Collections.Generic;
//using System.Web;
//using Contrib.Mobile.Models;
//using Contrib.Mobile.Services;
//using Orchard.DisplayManagement.Implementation;
//using Orchard.Mvc;
//using Orchard.ContentManagement;

//namespace Contrib.Mobile.MobileAdaptations
//{
//    /// <summary>
//    /// Changes or adds a doctype that the requesting device prefers.
//    /// </summary>
//    public class DocTypeAdaptation : IShapeDisplayEvents
//    {
//        private readonly Dictionary<string, string> _docTypes = new Dictionary<string, string>
//                                                                    {
//                                                                       {"none", ""},
//                                                                       {"xhtml_mp1", "<!DOCTYPE html PUBLIC  \"-//WAPFORUM//DTD XHTML Mobile 1.0//EN\" \"http://www.wapforum.org/DTD/xhtml-mobile10.dtd\">"},
//                                                                       {"xhtml_mp11", "<!DOCTYPE html PUBLIC  \"-//WAPFORUM//DTD XHTML Mobile 1.1//EN\" \"http://www.openmobilealliance.org/tech/DTD/xhtml-mobile11.dtd\">"},
//                                                                       {"xhtml_mp12", "<!DOCTYPE html PUBLIC  \"-//WAPFORUM//DTD XHTML Mobile 1.2//EN\" \"http://www.openmobilealliance.org/tech/DTD/xhtml-mobile12.dtd\">"},
//                                                                       {"html4", "<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.01 Transitional//EN\" \"http://www.w3.org/TR/html4/loose.dtd\">"},
//                                                                       {"xhtml_transitional", "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">"},
//                                                                       {"xhtml_basic", "<!DOCTYPE html PUBLIC  \"-//W3C//DTD XHTML Basic 1.0//EN\" \"http://www.w3.org/TR/xhtml-basic/xhtml-basic10.dtd\">"},
//                                                                       {"html5", "<!DOCTYPE HTML>"}
//                                                                    };

//        private readonly IHttpContextAccessor _httpContextAccessor;
//        private readonly IDeviceGroupService _deviceGroupService;

//        public DocTypeAdaptation(IHttpContextAccessor httpContextAccessor, IDeviceGroupService deviceGroupService)
//        {
//            _httpContextAccessor = httpContextAccessor;
//            _deviceGroupService = deviceGroupService;
//        }

//        public void Displaying(ShapeDisplayingContext context)
//        {

//        }

//        public void Displayed(ShapeDisplayedContext context)
//        {
//            if (!IsMobileDevice() || context.ShapeMetadata.Type != "Layout") return;

//            var group = _deviceGroupService.GetCurrentGroup();
//            if(group == null)
//            {
//                return;
//            }

//            var docTypePart = group.As<DocTypeAdaptationPart>();
//            if (docTypePart != null && docTypePart.Enabled)
//            {
//                context.ChildContent = TransformMobileDoctype(context.ChildContent);
//            }
//        }

//        private bool IsMobileDevice()
//        {
//            return _httpContextAccessor.Current().Request.Browser.IsMobileDevice;
//        }

//        private IHtmlString TransformMobileDoctype(IHtmlString htmlPageContent)
//        {
//            var pageContent = htmlPageContent.ToString();

//            string preferredDocType;
//            if (!GetBrowsersPreferredDocType(out preferredDocType))
//            {
//                return htmlPageContent;
//            }

//            int docTypeStartIndex = pageContent.IndexOf("<!DOCTYPE", StringComparison.InvariantCultureIgnoreCase);
//            if (docTypeStartIndex >= 0)
//            {
//                // Remove an existing doctype
//                int docTypeEndIndex = pageContent.IndexOf(">", docTypeStartIndex + "<!DOCTYPE".Length);
//                pageContent = pageContent.Remove(docTypeStartIndex, docTypeEndIndex - docTypeStartIndex + 1);
//            }

//            // Insert the devices preferred doctype
//            pageContent = pageContent.Insert(docTypeStartIndex, preferredDocType);

//            return new HtmlString(pageContent);
//        }

//        private bool GetBrowsersPreferredDocType(out string doctype)
//        {
//            var browser = _httpContextAccessor.Current().Request.Browser;

//            string doctypeToken = browser["html_preferred_dtd"];
//            if (!string.IsNullOrEmpty(doctypeToken))
//            {
//                doctype = _docTypes[doctypeToken];
//                return true;
//            }

//            doctype = string.Empty;
//            return false;
//        }
//    }
//}