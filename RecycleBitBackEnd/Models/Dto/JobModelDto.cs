using Quartz;
using RecycleBitBackEnd.Config;
using System;
using System.Collections.Generic;

namespace RecycleBitBackEnd.Models.Dto {
    public class JobModelDto {
        public string Name { get; set; }

        public string Group { get; set; }

        public bool Running { get; set; }

        public bool LastRunSuccess { get; set; }

        public DateTimeOffset? NextRunDateTimeOffset { get; set; }

        public string ScheduledTime { get; set; }

        public string NextExecution { get; set; }

        public List<JobTriggers> Triggers { get; set; }

        public JobModelDto() {
        }

        public JobModelDto(IJobDetail job, List<ITrigger> triggers) {
            Triggers = new List<JobTriggers>();
            Name = job.Key.Name;
            Group = job.Key.Group;
            triggers.ForEach(delegate (ITrigger trigger) {
                DateTimeOffset? nextFireTimeUtc = trigger.GetNextFireTimeUtc();
                if (nextFireTimeUtc.HasValue) {
                    Running = true;
                    NextRunDateTimeOffset = TimeZoneInfo.ConvertTime(nextFireTimeUtc.Value, BusinessConfig.BRAZIL_TIMEZONE);
                    NextExecution = (nextFireTimeUtc.HasValue ? TimeZoneInfo.ConvertTime(nextFireTimeUtc.Value, BusinessConfig.BRAZIL_TIMEZONE).ToString("dd/MM/yyyy HH:mm:ss") : string.Empty);
                    LastRunSuccess = trigger.GetPreviousFireTimeUtc().HasValue;
                }

                Triggers.Add(new JobTriggers(trigger));
            });
        }

        public JobModelDto(object dto, JobTriggers trigger) {
            JobModelDto jobModelDto = dto as JobModelDto;
            Name = jobModelDto.Name;
            Group = jobModelDto.Group;
            Running = jobModelDto.Running;
            NextRunDateTimeOffset = jobModelDto.NextRunDateTimeOffset;
            NextExecution = jobModelDto.NextExecution;
            ScheduledTime = jobModelDto.ScheduledTime;
            Triggers = new List<JobTriggers> { trigger };
        }
    }
}