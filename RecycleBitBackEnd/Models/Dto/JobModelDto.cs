using Quartz;
using RecycleBitBackEnd.Config;
using System;
using System.Collections.Generic;

namespace RecycleBitBackEnd.Models.Dto {

    /// <summary>
    ///     Class responsible per for Job Model Data Transfer Object
    /// </summary>
    public class JobModelDTO {

        #region Attributes Properties

        /// <summary>
        ///     Attribute Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Attribute Group
        /// </summary>
        public string Group { get; set; }

        /// <summary>
        ///  Attribute Running
        /// </summary>
        public bool Running { get; set; }

        /// <summary>
        ///     Atribute LastRunSuccess
        /// </summary>
        public bool LastRunSuccess { get; set; }

        /// <summary>
        ///     Attribute NextRunDateTimeOffset
        /// </summary>
        public DateTimeOffset? NextRunDateTimeOffset { get; set; }

        /// <summary>
        ///     Attribute ScheduledTime
        /// </summary>
        public string ScheduledTime { get; set; }

        /// <summary>
        ///     Attribute Next
        /// </summary>
        public string NextExecution { get; set; }

        /// <summary>
        ///     Attribute Triggers
        /// </summary>
        public List<JobTriggers> Triggers { get; set; }

        #endregion Attributes Properties

        #region Constructors

        /// <summary>
        ///     Constructor JobModelDto
        /// </summary>
        public JobModelDTO() {
        }

        #endregion Constructors

        #region Public Methods

        /// <summary>
        ///     Method responsible for mapping JobModelDto
        /// </summary>
        /// <param name="job"></param>
        /// <param name="triggers"></param>
        public JobModelDTO(IJobDetail job, List<ITrigger> triggers) {
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

        /// <summary>
        ///     Method responsible for mapping JobModelDto
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="trigger"></param>
        public JobModelDTO(object dto, JobTriggers trigger) {
            JobModelDTO jobModelDto = dto as JobModelDTO;
            Name = jobModelDto.Name;
            Group = jobModelDto.Group;
            Running = jobModelDto.Running;
            NextRunDateTimeOffset = jobModelDto.NextRunDateTimeOffset;
            NextExecution = jobModelDto.NextExecution;
            ScheduledTime = jobModelDto.ScheduledTime;
            Triggers = new List<JobTriggers> { trigger };
        }

        #endregion Public Methods
    }
}