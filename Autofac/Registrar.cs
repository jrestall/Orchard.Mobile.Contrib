using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Autofac;
using OpenDDR.Service.Services;

namespace Contrib.Mobile.Autofac
{
    public class Registrar : Module {
        protected override void Load(ContainerBuilder builder) {
            base.Load(builder);

            //Register 
            builder.Register(x => new OddrDeviceService())
                   .As<IOddrDeviceService>()
                   .SingleInstance();

            //Starting to resolve the OddrService in a single task - so that the service is already initialized when it is requested.
            //new Task(() => builder.Build().Resolve<IOddrDeviceService>()).Start();
        }
    }
}