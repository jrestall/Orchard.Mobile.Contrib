using System.Threading.Tasks;
using Autofac;
using OpenDDR.Service.Services;

namespace Contrib.Mobile.Autofac
{
    public class Registrar : Module {
        protected override void Load(ContainerBuilder builder) {
            base.Load(builder);

            //Initializing the OddrDeviceService will parse the xml files - This may take two-tree seconds.
            var initializeOddrDeviceService = new Task<OddrDeviceService>(() => new OddrDeviceService());
            initializeOddrDeviceService.Start();

            builder.Register(x => initializeOddrDeviceService.Result)
                   .As<IOddrDeviceService>()
                   .SingleInstance();
        }
    }
}