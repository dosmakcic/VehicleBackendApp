using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Services
{
    public class NinjectBindings : NinjectModule
    {
        public override void Load()
        {
            Bind<IVehicleService>().To<VehicleService>().InTransientScope();
        }
    }
}
