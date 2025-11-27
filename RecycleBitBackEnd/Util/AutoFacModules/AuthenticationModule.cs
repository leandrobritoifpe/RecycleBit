using Autofac;

namespace RecycleBitBackEnd.Util.AutoFacModules {
    public class AuthenticationModule : Module {
        #region Protected Methods

        protected override void Load(ContainerBuilder builder) {
            //builder.RegisterType<ApplicationUserStore>().As<IUserStore<ApplicationUser>>().InstancePerLifetimeScope();
            //builder.RegisterType<ApplicationUserManager>().AsSelf().InstancePerLifetimeScope();
            //builder.RegisterType<ApplicationSignInManager>().AsSelf().InstancePerLifetimeScope();
            //builder.Register<IAuthenticationManager>(c => HttpContext.Current.GetOwinContext().Authentication).InstancePerLifetimeScope();
        }

        #endregion Protected Methods
    }
}