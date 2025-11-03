using Autofac;
using Autofac.Extras.Quartz;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using HarpiaCommon.Config;
using HarpiaCommon.Models.Dto;
using HarpiaCommon.Util.AutoFac.Modules;
using HarpiaCommon.Util.AutoFacModules;
using Owin;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using RecycleBitBackEnd.Config;

namespace RecycleBitBackEnd {

    public partial class Startup {

        #region Private Properties

        private static IContainer Container { get; set; }

        #endregion Private Properties

        #region Public Methods

        public void ConfigureAutofac(IAppBuilder app) {           

            CommonConfig.ExecutingAssembly = Assembly.GetExecutingAssembly();
            CommonConfig.NameSpacesMap = getNameSpacesMap("RecycleBitBackEnd");

            ContainerBuilder builder = new();
            builder.RegisterModule(new CommonModule());
            builder.RegisterModule(new ControllerModule());
            builder.RegisterModule(new ServiceModule());
            builder.RegisterModule(new QuartzAutofacFactoryModule());

            Container = builder.Build(Autofac.Builder.ContainerBuildOptions.None);
            //app.UseAutofacMiddleware(Container);// TODO leonardo.lira para qunado for ativado o Owin
            //app.UseAutofacMvc();// TODO leonardo.lira para qunado for ativado o Owin

            DependencyResolver.SetResolver(new AutofacDependencyResolver(Container));

            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(Container);
            //app.UseAutofacWebApi(GlobalConfiguration.Configuration); // TODO leonardo.lira para qunado for ativado o Owin

            ApplicationConfig.Container = Container;
        }

        private NameSpacesMap getNameSpacesMap(string nameSpaceBase) {
            NameSpacesMap nameSpacesMap = new() {
                MappingControllers = new List<string> {
            nameSpaceBase + ".controllers"
            },
                MappingApiControllers = new List<string> {
            nameSpaceBase + ".apicontrollers"
            },
                MappingInterfaces = new List<string> {
                    nameSpaceBase + ".dao.interfaces",
                    nameSpaceBase + ".services.interfaces",
                    nameSpaceBase + ".security.interfaces",
                    nameSpaceBase + ".util.dtoconverters.interfaces"
            },
                MappingImplementation = new List<string> {
                    nameSpaceBase + ".dao",
                    nameSpaceBase + ".services",
                    nameSpaceBase + ".security",
                    nameSpaceBase + ".util.dtoconverters"
                }
            };
            return nameSpacesMap;
        }

        #endregion Public Methods
    }
}