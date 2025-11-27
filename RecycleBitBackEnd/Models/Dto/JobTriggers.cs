using Quartz;
using Quartz.Impl;
using RecycleBitBackEnd.Config;
using System;

namespace RecycleBitBackEnd.Models.Dto {
    public class JobTriggers {
        public DateTimeOffset? NextRunDateTime { get; set; }

        public TriggerState State { get; set; }

        public string CronExpression { get; set; }

        public JobTriggers() {
        }

        public JobTriggers(ITrigger trigger) {
            IScheduler result = StdSchedulerFactory.GetDefaultScheduler().Result;
            State = result.GetTriggerState(trigger.Key).Result;
            CronExpression = (trigger as ICronTrigger)?.CronExpressionString;
            if (trigger.GetNextFireTimeUtc().HasValue) {
                NextRunDateTime = TimeZoneInfo.ConvertTime(trigger.GetNextFireTimeUtc().Value, BusinessConfig.BRAZIL_TIMEZONE);
            }
        }
    }
}