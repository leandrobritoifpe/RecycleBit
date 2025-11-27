using HarpiaCommon.Models;
using HarpiaCommon.Util.Logger;

namespace RecycleBitBackEnd {

    public partial class Startup {

        public void ConfigureLogger() {
            LogConfig nLogConfig = new();
            nLogConfig.SetConfigLog(new NLogConfigurationModel());
        }
    }
}