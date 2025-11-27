using Owin;

//[assembly: OwinStartup(typeof(RecycleBitBackEnd.Startup))]

namespace RecycleBitBackEnd {

    public partial class Startup {

        #region Public Methods

        public void Configuration(IAppBuilder app) {
            ConfigureVariables();
            ConfigureAutofac(app);
            //ConfigureAuth(app);
            //ConfigureLogger();
            StartSchedulers();
            ApplicationStartLog();
        }

        #endregion Public Methods

        #region Private Methods

        private void ApplicationStartLog() {
            // Se ao rodar ocorrer excecao, apenas de restart (Ctrl + Shift + F5 )
            //IHarpiaLoggerBO loggerBO = Container.Resolve<IHarpiaLoggerBO>();
            //loggerBO.SetDebugLog(new NewLogRequest(CommonConfig.ApplicationName, DateTime.Now, "Aplicação iniciada com sucesso!", string.Empty, string.Empty));
        }

        #endregion Private Methods
    }
}