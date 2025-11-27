using Autofac;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Module = Autofac.Module;

namespace RecycleBitBackEnd.Util.AutoFacModules {

    public class ServiceModule : Module {

        #region Protected Methods

        protected override void Load(ContainerBuilder builder) {
            List<string> MappingInterfaces = new() {
                    "recyclebitbackend.app_start.interfaces",
                    "recyclebitbackend.dao.interfaces",
                    "recyclebitbackend.services.interfaces"
                    };
            List<string> MappingImplementation = new() {
                    "recyclebitbackend.dao",
                    "recyclebitbackend.services"
                    };

            Assembly dataAccess = Assembly.GetExecutingAssembly();

            foreach (var type in dataAccess.ExportedTypes.AsParallel().Where(s => s.Namespace != null && MappingInterfaces.Contains(s.Namespace.ToLower())).ToList()) {
                builder.RegisterAssemblyTypes(type.Assembly).AsImplementedInterfaces().InstancePerLifetimeScope();
            }
            foreach (var type in dataAccess.ExportedTypes.AsParallel().Where(s => s.Namespace != null && MappingImplementation.Contains(s.Namespace.ToLower())).ToList()) {
                builder.RegisterAssemblyTypes(type.Assembly).InstancePerLifetimeScope();
            }
        }

        #endregion Protected Methods
    }
}