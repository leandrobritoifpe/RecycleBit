using Autofac;
using Autofac.Extras.Quartz;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Owin;
using RecycleBitBackEnd.Config;
using RecycleBitBackEnd.Util.AutoFac.Modules;
using RecycleBitBackEnd.Util.AutoFacModules;
using System.Web.Http;
using System.Web.Mvc;

namespace RecycleBitBackEnd {

    public partial class Startup {

        #region Private Properties

        private static IContainer Container { get; set; }

        #endregion Private Properties

        #region Public Methods

        public void ConfigureAutofac(IAppBuilder app) {
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

        #endregion Public Methods
    }
}