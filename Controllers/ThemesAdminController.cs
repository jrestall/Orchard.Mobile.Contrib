using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Orchard;
using Orchard.ContentManagement;
using Orchard.Data;
using Orchard.DisplayManagement;
using Orchard.Localization;
using Orchard.Logging;
using Contrib.Mobile.Models;
using Contrib.Mobile.Services;
using Contrib.Mobile.ViewModels;
using Orchard.Mvc;
using Orchard.UI.Admin;
using Orchard.UI.Notify;

namespace Contrib.Mobile.Controllers
{
    [Admin]
    public class ThemesAdminController : Controller, IUpdateModel
    {
        private readonly IDeviceGroupService _deviceGroupService;
        private readonly ITransactionManager _transactionManager;

        public ThemesAdminController(
            IOrchardServices orchardServices, 
            IDeviceGroupService deviceGroupService, 
            ITransactionManager transactionManager,
            IShapeFactory shapeFactory)
        {
            _deviceGroupService = deviceGroupService;
            _transactionManager = transactionManager;
            Services = orchardServices;
            T = NullLocalizer.Instance;
            Logger = NullLogger.Instance;
            Shape = shapeFactory;
        }

        dynamic Shape { get; set; }
        public IOrchardServices Services { get; set; }
        public Localizer T { get; set; }
        public ILogger Logger { get; set; }

        public ActionResult List()
        {
            if (!Services.Authorizer.Authorize(Permissions.ManageMobileThemes, T("Cannot manage mobile themes")))
                return new HttpUnauthorizedResult();

            IEnumerable<DeviceGroupPart> deviceGroups = _deviceGroupService.Get(VersionOptions.Latest);
                
            return View(new ThemesListViewModel{ DeviceGroups = deviceGroups });
        }

        public ActionResult Create()
        {
            if (!Services.Authorizer.Authorize(Permissions.ManageMobileThemes, T("Cannot manage mobile themes")))
                return new HttpUnauthorizedResult();

            DeviceGroupPart group = Services.ContentManager.New<DeviceGroupPart>("DeviceGroup");
            if (group == null)
                return HttpNotFound();

            dynamic model = Services.ContentManager.BuildEditor(group);
            // Casting to avoid invalid (under medium trust) reflection over the protected View method and force a static invocation.
            return View((object)model);
        }

        [HttpPost, ActionName("Create")]
        public ActionResult CreatePOST()
        {
            if (!Services.Authorizer.Authorize(Permissions.ManageMobileThemes, T("Cannot manage mobile themes")))
                return new HttpUnauthorizedResult();

            var group = Services.ContentManager.New<DeviceGroupPart>("DeviceGroup");
            group.Enabled = true;

            var groups = _deviceGroupService.Get(VersionOptions.Latest);
            group.Position = groups.Any() ? groups.Max(x => x.Position) + 1 : 0;

            Services.ContentManager.Create(group, VersionOptions.Draft);
            dynamic model = Services.ContentManager.UpdateEditor(group, this);

            if (!ModelState.IsValid)
            {
                _transactionManager.Cancel();
                // Casting to avoid invalid (under medium trust) reflection over the protected View method and force a static invocation.
                return View((object)model);
            }

            Services.ContentManager.Publish(group.ContentItem);

            return Redirect("List");
        }

        
        public ActionResult Edit(int id)
        {
            if (!Services.Authorizer.Authorize(Permissions.ManageMobileThemes, T("Cannot manage mobile themes")))
                return new HttpUnauthorizedResult();

            var group = _deviceGroupService.Get(id, VersionOptions.Latest);
            if (group == null)
                return HttpNotFound();

            dynamic model = Services.ContentManager.BuildEditor(group);
            // Casting to avoid invalid (under medium trust) reflection over the protected View method and force a static invocation.
            return View((object)model);
        }

        [HttpPost, ActionName("Edit")]
        [FormValueRequired("submit.Save")]
        public ActionResult EditSavePOST(int id)
        {
            if (!Services.Authorizer.Authorize(Permissions.ManageMobileThemes, T("Cannot manage mobile themes")))
                return new HttpUnauthorizedResult();

            var group = _deviceGroupService.Get(id, VersionOptions.DraftRequired);
            if (group == null)
                return HttpNotFound();

            dynamic model = Services.ContentManager.UpdateEditor(group, this);
            if (!ModelState.IsValid)
            {
                Services.TransactionManager.Cancel();
                // Casting to avoid invalid (under medium trust) reflection over the protected View method and force a static invocation.
                return View((object)model);
            }

            Services.ContentManager.Publish(group);

            return RedirectToAction("List");
        }

        [HttpPost, ActionName("Edit")]
        [FormValueRequired("submit.Delete")]
        public ActionResult EditDeletePOST(int id)
        {
            if (!Services.Authorizer.Authorize(Permissions.ManageMobileThemes, T("Cannot manage mobile themes")))
                return new HttpUnauthorizedResult();

            try
            {
                _deviceGroupService.DeleteGroup(id);
                Services.Notifier.Information(T("Group was successfully deleted"));
            }
            catch (Exception)
            {
                //this.Error(exception, T("Removing group failed: {0}", exception.Message), Logger, Services.Notifier);
            }

            return RedirectToAction("List");
        }

        public ActionResult Enable(int id)
        {
            if (!Services.Authorizer.Authorize(Permissions.ManageMobileThemes, T("Cannot manage mobile themes")))
                return new HttpUnauthorizedResult();

            var group = Services.ContentManager.Get<DeviceGroupPart>(id);
            if (group == null)
                return HttpNotFound();

            group.Record.Enabled = true;

            return RedirectToAction("List");
        }

        public ActionResult Disable(int id)
        {
            if (!Services.Authorizer.Authorize(Permissions.ManageMobileThemes, T("Cannot manage mobile themes")))
                return new HttpUnauthorizedResult();

            var group = Services.ContentManager.Get<DeviceGroupPart>(id);
            if (group == null)
                return HttpNotFound();

            group.Record.Enabled = false;

            return RedirectToAction("List");
        }

        public ActionResult SetPriority(int id, bool up)
        {
            if (!Services.Authorizer.Authorize(Permissions.ManageMobileThemes, T("Cannot manage mobile themes")))
                return new HttpUnauthorizedResult();

            _deviceGroupService.SetPosition(id, up);

            return RedirectToAction("List");
        }

        bool IUpdateModel.TryUpdateModel<TModel>(TModel model, string prefix, string[] includeProperties, string[] excludeProperties)
        {
            return TryUpdateModel(model, prefix, includeProperties, excludeProperties);
        }

        void IUpdateModel.AddModelError(string key, LocalizedString errorMessage)
        {
            ModelState.AddModelError(key, errorMessage.ToString());
        }
    }
}