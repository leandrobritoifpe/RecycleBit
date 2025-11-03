using Autofac;
using HarpiaCommon.Services.Interfaces;

namespace RecycleBitBackEnd {
    public partial class Startup {
        public void StartSchedulers() {
            ISchedulerBO scheduler = Container.Resolve<ISchedulerBO>();

        }
    }
}