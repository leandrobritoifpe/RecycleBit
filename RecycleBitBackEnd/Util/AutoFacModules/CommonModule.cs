using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Module = Autofac.Module;

namespace RecycleBitBackEnd.Util.AutoFac.Modules {

    public class CommonModule : Module {

        #region Protected Methods

        protected override void Load(ContainerBuilder builder) {

            var loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
            var loadedPaths = loadedAssemblies.Where(
                search => search.FullName.Contains("RecycleBitBackEnd")
            ).Select(a => a.Location).ToArray();
            Assembly dataAccessCommon = AppDomain.CurrentDomain.Load(AssemblyName.GetAssemblyName(loadedPaths.First()));

            //builder.RegisterAssemblyModules(dataAccessCommon);

            List<string> MappingInterfaces = new() {
               "recyclebitbackend.dao.interfaces",
               "recyclebitbackend.services.interfaces",
               "recyclebitbackend.security.interfaces",
               "recyclebitbackend.util.dtoconverters.interfaces",
               "recyclebitbackend.connectors.interfaces"
            };
            List<string> MappingImplementation = new() {
               "recyclebitbackend.dao",
               "recyclebitbackend.services",
               "recyclebitbackend.connectors",
               "recyclebitbackend.security",
               "recyclebitbackend.jobs",
               "recyclebitbackend.util.dtoconverters"
            };

            foreach (var type in dataAccessCommon.ExportedTypes.AsParallel().Where(s => s.Namespace != null && MappingInterfaces.Contains(s.Namespace.ToLower())).ToList()) {
                builder.RegisterAssemblyTypes(type.Assembly).AsImplementedInterfaces().InstancePerLifetimeScope();
            }
            foreach (var type in dataAccessCommon.ExportedTypes.AsParallel().Where(s => s.Namespace != null && MappingImplementation.Contains(s.Namespace.ToLower())).ToList()) {
                builder.RegisterAssemblyTypes(type.Assembly).InstancePerLifetimeScope();
            }
        }

        #endregion Protected Methods
    }
}