using Quartz;
using RecycleBitBackEnd.Models.Dto;
using System;
using System.Collections.Generic;

namespace RecycleBitBackEnd.Services.Interfaces {
    public interface ISchedulerBO {
        List<JobModelDto> ReturnAllJobs(int offsetHours = 0);

        IList<JobModelDto> GetCurrentJobsScheduled(int offsetHours = 0);

        void DeleteJobByJobKey(string jobName);

        void DeleteAllJobs();

        void PauseJobByJobName(string jobName);

        void PauseAllJobs();

        void ResumeJobByJobName(string jobName);

        void ResumeAllJobs();

        void Start<T>(string cronExpression, bool handler = false, int waitInterval = 10, int maxRetries = 5) where T : IJob;

        void Start<T>(string cronExpression, string jobName = null, JobDataMap jobDataMap = null, bool handler = false, int waitInterval = 10, int maxRetries = 5) where T : IJob;

        void Start<T>(DateTimeOffset? dateTimeOffSet, TimeSpan timespan, bool handler = false, int waitInterval = 10, int maxRetries = 5) where T : IJob;

        void Start<T>(DateTimeOffset? dateTimeOffSet, TimeSpan timespan, string jobName = null, JobDataMap jobDataMap = null, bool handler = false, int waitInterval = 10, int maxRetries = 5) where T : IJob;

        void Start(Type jobClassType, string cronExpression, bool handler = false, int waitInterval = 10, int maxRetries = 5);

        void Start(Type jobClassType, string cronExpression, string jobName = null, JobDataMap jobDataMap = null, bool handler = false, int waitInterval = 10, int maxRetries = 5);

        void Start(Type jobClassType, DateTimeOffset? dateTimeOffSet, TimeSpan timespan, bool handler = false, int waitInterval = 10, int maxRetries = 5);

        void Start(Type jobClassType, DateTimeOffset? dateTimeOffSet, TimeSpan timespan, string jobName = null, JobDataMap jobDataMap = null, bool handler = false, int waitInterval = 10, int maxRetries = 5);

        string StartJobByJobName(string jobName);
    }
}