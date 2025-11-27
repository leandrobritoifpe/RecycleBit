using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using System.Linq;
using System.Reflection;
using Module = Autofac.Module;

namespace RecycleBitBackEnd.Util.AutoFacModules {

    public class ControllerModule : Module {
        #region Protected Methods

        protected override void Load(ContainerBuilder builder) {
            Assembly dataAccess = Assembly.GetExecutingAssembly();

            foreach (var type in dataAccess.ExportedTypes.AsParallel().Where(s => s.Name.Contains("Controller") && (s.BaseType.Name == "Controller" || s.BaseType.Name == "ApiController")).ToList()) {
                if (type.BaseType.Name == "Controller") {
                    //if (type.Name == "AccountController") {
                    //    builder.RegisterType<AccountController>().AsSelf().InstancePerLifetimeScope();
                    //}
                    builder.RegisterControllers(type.Assembly).AsSelf();
                } else {
                    builder.RegisterApiControllers(type.Assembly).AsSelf().AutoActivate();
                }
            }
        }

        #endregion Protected Methods
    }
}