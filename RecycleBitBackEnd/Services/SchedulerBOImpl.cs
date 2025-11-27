using CronExpressionDescriptor;
using Quartz;
using Quartz.Impl;
using Quartz.Impl.Matchers;
using Quartz.Impl.Triggers;
using RecycleBitBackEnd.Config;
using RecycleBitBackEnd.Models.Dto;
using RecycleBitBackEnd.Services.Interfaces;
using RecycleBitBackEnd.Util.Exceptions;
using RecycleBitBackEnd.Util.Scheduler;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RecycleBitBackEnd.Services {
    public class SchedulerBOImpl : ISchedulerBO {
        //private readonly IHarpiaLoggerBO loggerBO;
        private readonly List<Job> ActiveJobs;

        private readonly IScheduler _scheduler;

        public SchedulerBOImpl(IScheduler scheduler) {
            _scheduler = scheduler;
            ActiveJobs = new List<Job>();
        }

        public void DeleteJobByJobKey(string jobName) {
            List<JobKey> jobKeys = _scheduler.GetJobKeys(GroupMatcher<JobKey>.AnyGroup()).Result.ToList();
            JobKey jobKey = jobKeys.FirstOrDefault(job => job.Name == jobName);
            if (jobKey == null) {
                Exception exception = new($"Job '{jobName}' não encontrado.");
                return;
            }

            var jobToRemoveFromActive = ActiveJobs.FirstOrDefault(job => job.Name.Equals(jobName));
            if (jobToRemoveFromActive != null)
                ActiveJobs.Remove(jobToRemoveFromActive);

            _scheduler.DeleteJob(jobKey);
        }

        public void DeleteAllJobs() {
            foreach (var item in GetCurrentJobsScheduled()) {
                DeleteJobByJobKey(item.Name);
            }
        }

        public List<JobModelDto> ReturnAllJobs(int offsetHours = 0) {
            List<JobModelDto> jobs = new();

            List<JobKey> jobKeys = _scheduler.GetJobKeys(GroupMatcher<JobKey>.AnyGroup()).Result.ToList();
            List<IJobDetail> jobDetails = new();

            jobKeys.ForEach(job => jobDetails.Add(_scheduler.GetJobDetail(job).Result));

            jobDetails.ForEach(job => jobs.Add(new JobModelDto(job, _scheduler.GetTriggersOfJob(job.Key).Result.ToList())));

            return GetJobsDetails(jobs, offsetHours);
        }

        private List<JobModelDto> GetJobsDetails(List<JobModelDto> jobList, int offsetHours = 0) {
            List<JobModelDto> jobs = new();
            foreach (JobModelDto currentJob in jobList) {
                foreach (JobTriggers trigger in currentJob.Triggers) {
                    try {
                        currentJob.ScheduledTime = GetCronDescription(ConvertCronUtcToLocal(trigger.CronExpression, offsetHours), "pt");
                        currentJob.NextRunDateTimeOffset = trigger.NextRunDateTime;
                        currentJob.NextExecution = trigger.NextRunDateTime.HasValue ? trigger.NextRunDateTime.Value.DateTime.ToString("dd/MM/yyyy HH:mm:ss") : string.Empty;
                        jobs.Add(new JobModelDto(currentJob, trigger));
                    } catch (System.Exception e) {
                    }
                }
            }
            return jobs;
        }

        public IList<JobModelDto> GetCurrentJobsScheduled(int offsetHours = 0) {
            return ReturnAllJobs(offsetHours).Where(j => j.Running).ToList();
        }

        private string GetCronDescription(string cronExpression, string locale = "pt") {
            if (string.IsNullOrWhiteSpace(cronExpression))
                return "Expressão cron vazia.";

            try {
                Options options = new() {
                    Locale = locale,
                    Use24HourTimeFormat = true
                };
                return ExpressionDescriptor.GetDescription(cronExpression, options);
            } catch (Exception ex) {
                return $"Expressão inválida: {ex.Message}";
            }
        }

        private string ConvertCronUtcToLocal(string cronExpression, int offsetHours) {
            var parts = cronExpression.Split(' ');
            if (parts.Length < 3)
                return cronExpression;

            var hourPart = parts[2];
            var hourValues = hourPart.Split(',');
            var localHours = new List<string>();

            foreach (var hourStr in hourValues) {
                if (int.TryParse(hourStr, out int utcHour)) {
                    int localHour = (utcHour + offsetHours + 24) % 24;
                    localHours.Add(localHour.ToString());
                } else {
                    localHours.Add(hourStr);
                }
            }

            parts[2] = string.Join(",", localHours);
            return string.Join(" ", parts);
        }


        public void PauseJobByJobName(string jobName) {
            List<JobKey> jobKeys = _scheduler.GetJobKeys(GroupMatcher<JobKey>.AnyGroup()).Result.ToList();
            JobKey jobKey = jobKeys.FirstOrDefault(job => job.Name == jobName);
            if (jobKey == null) {
                Exception exception = new($"Job '{jobName}' não encontrado.");
                return;
            }

            _scheduler.PauseJob(jobKey);
        }

        public void PauseAllJobs() {
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler().Result;
            scheduler.PauseAll().Wait();
        }

        public void ResumeJobByJobName(string jobName) {
            List<JobKey> jobKeys = _scheduler.GetJobKeys(GroupMatcher<JobKey>.AnyGroup()).Result.ToList();
            JobKey jobKey = jobKeys.FirstOrDefault(job => job.Name == jobName);
            if (jobKey == null) {
                Exception exception = new($"Job '{jobName}' não encontrado.");
                return;
            }

            _scheduler.ResumeJob(jobKey);
        }

        public void ResumeAllJobs() {
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler().Result;
            scheduler.ResumeAll().Wait();
        }

        public string StartJobByJobName(string jobName) {
            try {
                List<JobKey> jobKeys = _scheduler.GetJobKeys(GroupMatcher<JobKey>.AnyGroup()).Result.ToList();
                JobKey jobKey = jobKeys.FirstOrDefault(job => job.Name == jobName);

                if (jobKey == null) {
                    Exception exception = new($"Job '{jobName}' não encontrado.");
                    return $"Job '{jobName}' não encontrado.";
                }

                Job job = ActiveJobs.First(job => job.Name == jobName);

                _scheduler.DeleteJob(jobKey);

                IJobDetail jobDetail = _scheduler.GetJobDetail(job.JobDetail.Key).Result;

                foreach (var t in job.Triggers) {
                    ITrigger trigger = TriggerBuilder.Create().WithIdentity(t.Key.Name, BusinessConfig.DEFAULT_GROUP)
                                                              .WithCronSchedule(((CronTriggerImpl)t).CronExpressionString).ForJob(job.Name, BusinessConfig.DEFAULT_GROUP)
                                                              .Build();
                    _scheduler.ScheduleJob(t);
                }

                return "Job Iniciado com sucesso.";
            } catch (Exception exception) {
                return $"Erro ao iniciar o job '{jobName}': {exception.Message}";
            }
        }

        public string ExecuteJob(string jobName) {
            try {
                List<JobKey> jobKeys = _scheduler.GetJobKeys(GroupMatcher<JobKey>.AnyGroup()).Result.ToList();
                JobKey jobKey = jobKeys.FirstOrDefault(job => job.Name == jobName);

                if (jobKey == null) {
                    return $"Job '{jobName}' não encontrado.";
                }

                IJobDetail jobDetail = _scheduler.GetJobDetail(jobKey).Result;

                _scheduler.TriggerJob(jobKey);

                return $"Job '{jobName}' sendo executado agora.";
            } catch (Exception exception) {
                return $"Erro ao executar o job '{jobName}': {exception.Message}";
            }
        }

        /// <summary>Start job scheduler</summary>
        /// <typeparam name="T">IJob object</typeparam>
        /// <param name="cronExpression">Cron expression string</param>
        public void Start<T>(string cronExpression, bool handler = false, int waitInterval = 10, int maxRetries = 5) where T : IJob {
            Start<T>(cronExpression, null, null, handler, waitInterval, maxRetries);
        }

        /// <summary>Start job scheduler with aditional parameters</summary>
        /// <typeparam name="T">IJob object</typeparam>
        /// <param name="cronExpression">Cron expression string</param>
        public void Start<T>(string cronExpression, string jobName = null, JobDataMap jobDataMap = null, bool handler = false, int waitInterval = 10, int maxRetries = 5) where T : IJob {
            IJobDetail job;

            string triggerName = jobName == null ? typeof(T).Name + BusinessConfig.TRIGGER : jobName + BusinessConfig.TRIGGER;

            if (jobDataMap == null)
                job = JobBuilder.Create<T>().WithIdentity(jobName ?? typeof(T).Name, BusinessConfig.DEFAULT_GROUP).Build();
            else
                job = JobBuilder.Create<T>().WithIdentity(jobName ?? typeof(T).Name, BusinessConfig.DEFAULT_GROUP).UsingJobData(jobDataMap).Build();

            if (ActiveJobs.Any(job => job.Name == (jobName ?? typeof(T).Name))) {
                var triggers = _scheduler.GetTriggersOfJob(job.Key).Result;
                if (triggers != null && triggers.Count > 0)
                    triggerName = jobName == null ? typeof(T).Name + triggers.Count + BusinessConfig.TRIGGER : jobName + triggers.Count + BusinessConfig.TRIGGER;
            }

            ITrigger trigger = TriggerBuilder.Create().WithIdentity(triggerName, BusinessConfig.DEFAULT_GROUP)
                       .WithCronSchedule(cronExpression).ForJob(jobName ?? typeof(T).Name, BusinessConfig.DEFAULT_GROUP).Build();

            if (!ActiveJobs.Any(job => job.Name == (jobName ?? typeof(T).Name))) {
                ActiveJobs.Add(new Job(job, trigger, handler, waitInterval, maxRetries));
                _scheduler.Start();
                _scheduler.ScheduleJob(job, trigger);
            } else {
                if (!ActiveJobs.Any(job => job.Name == (jobName ?? typeof(T).Name) && job.Triggers.Any(t => ((CronTriggerImpl)t).CronExpressionString == cronExpression))) {
                    Job j = ActiveJobs.First(job => job.Name == (jobName ?? typeof(T).Name));
                    j.Triggers.Add(trigger);
                    _scheduler.ScheduleJob(trigger);
                }
            }

            if (handler) {
                _scheduler.ListenerManager.AddJobListener(new JobFailureHandler(BusinessConfig.DEFAULT_GROUP, waitInterval, maxRetries));
            }
        }

        /// <summary>Start job scheduler</summary>
        /// <typeparam name="T">IJob object</typeparam>
        /// <param name="cronExpression">Cron expression string</param>
        public void Start<T>(DateTimeOffset? dateTimeOffSet, TimeSpan timespan, bool handler = false, int waitInterval = 10, int maxRetries = 5) where T : IJob {
            Start<T>(dateTimeOffSet, timespan, null, null, handler, waitInterval, maxRetries);
        }

        /// <summary>Start job scheduler with aditional parameters</summary>
        /// <typeparam name="T">IJob object</typeparam>
        /// <param name="cronExpression">Cron expression string</param>
        public void Start<T>(DateTimeOffset? dateTimeOffSet, TimeSpan timespan, string jobName = null, JobDataMap jobDataMap = null, bool handler = false, int waitInterval = 10, int maxRetries = 5) where T : IJob {
            IJobDetail job;

            string triggerName = jobName == null ? typeof(T).Name + BusinessConfig.TRIGGER : jobName + BusinessConfig.TRIGGER;

            if (jobDataMap == null)
                job = JobBuilder.Create<T>().WithIdentity(jobName ?? typeof(T).Name, BusinessConfig.DEFAULT_GROUP).Build();
            else
                job = JobBuilder.Create<T>().WithIdentity(jobName ?? typeof(T).Name, BusinessConfig.DEFAULT_GROUP).UsingJobData(jobDataMap).Build();

            if (ActiveJobs.Any(job => job.Name == (jobName ?? typeof(T).Name))) {
                var triggers = _scheduler.GetTriggersOfJob(job.Key).Result;
                if (triggers != null && triggers.Count > 0)
                    triggerName = jobName == null ? typeof(T).Name + triggers.Count + BusinessConfig.TRIGGER : jobName + triggers.Count + BusinessConfig.TRIGGER;
            }

            if (!dateTimeOffSet.HasValue || dateTimeOffSet == null)
                dateTimeOffSet = DateBuilder.TodayAt(0, 0, 0);

            ITrigger trigger = TriggerBuilder.Create().WithIdentity(triggerName, BusinessConfig.DEFAULT_GROUP)
                       .StartAt(dateTimeOffSet.Value).WithSimpleSchedule(x => x.WithInterval(timespan).RepeatForever()).ForJob(jobName ?? typeof(T).Name, BusinessConfig.DEFAULT_GROUP).Build();

            if (!ActiveJobs.Any(job => job.Name == (jobName ?? typeof(T).Name))) {
                ActiveJobs.Add(new Job(job, trigger, handler, waitInterval, maxRetries));
                _scheduler.Start();
                _scheduler.ScheduleJob(job, trigger);
            } else {
                if (!ActiveJobs.Any(job => job.Name == (jobName ?? typeof(T).Name) && job.Triggers.Any(t => ((SimpleTriggerImpl)t).RepeatInterval == timespan && ((SimpleTriggerImpl)t).StartTimeUtc == dateTimeOffSet))) {
                    Job j = ActiveJobs.First(job => job.Name == (jobName ?? typeof(T).Name));
                    j.Triggers.Add(trigger);
                    _scheduler.ScheduleJob(trigger);
                }
            }

            if (handler) {
                _scheduler.ListenerManager.AddJobListener(new JobFailureHandler(BusinessConfig.DEFAULT_GROUP, waitInterval, maxRetries));
            }
        }

        /// <summary>Start job scheduler</summary>
        /// <typeparam name="T">IJob object</typeparam>
        /// <param name="cronExpression">Cron expression string</param>
        public void Start(Type jobClassType, string cronExpression, bool handler = false, int waitInterval = 10, int maxRetries = 5) {
            Start(jobClassType, cronExpression, null, null, handler, waitInterval, maxRetries);
        }

        /// <summary>Start job scheduler with aditional parameters</summary>
        /// <typeparam name="T">IJob object</typeparam>
        /// <param name="cronExpression">Cron expression string</param>
        public void Start(Type jobClassType, string cronExpression, string jobName = null, JobDataMap jobDataMap = null, bool handler = false, int waitInterval = 10, int maxRetries = 5) {
            if (!jobClassType.GetInterfaces().Contains(typeof(IJob)))
                throw new ProjectException("ERROR_INVALID_JOB_CLASS_TYPE");

            IJobDetail job;
            string triggerName = jobName == null ? jobClassType.Name + BusinessConfig.TRIGGER : jobName + BusinessConfig.TRIGGER;

            if (jobDataMap == null)
                job = JobBuilder.Create(jobClassType).WithIdentity(jobName ?? jobClassType.Name, BusinessConfig.DEFAULT_GROUP).Build();
            else
                job = JobBuilder.Create(jobClassType).WithIdentity(jobName ?? jobClassType.Name, BusinessConfig.DEFAULT_GROUP).UsingJobData(jobDataMap).Build();

            if (ActiveJobs.Any(job => job.Name == (jobName ?? jobClassType.Name))) {
                var triggers = _scheduler.GetTriggersOfJob(job.Key).Result;
                if (triggers != null && triggers.Count > 0)
                    triggerName = jobName == null ? jobClassType.Name + triggers.Count + BusinessConfig.TRIGGER : jobName + triggers.Count + BusinessConfig.TRIGGER;
            }

            ITrigger trigger = TriggerBuilder.Create().WithIdentity(triggerName, BusinessConfig.DEFAULT_GROUP)
                       .WithCronSchedule(cronExpression).ForJob(jobName ?? jobClassType.Name, BusinessConfig.DEFAULT_GROUP).Build();

            if (!ActiveJobs.Any(job => job.Name == (jobName ?? jobClassType.Name))) {
                ActiveJobs.Add(new Job(job, trigger, handler, waitInterval, maxRetries));
                _scheduler.Start();
                _scheduler.ScheduleJob(job, trigger);
            } else {
                if (!ActiveJobs.Any(job => job.Name == (jobName ?? jobClassType.Name) && job.Triggers.Any(t => ((CronTriggerImpl)t).CronExpressionString == cronExpression))) {
                    Job j = ActiveJobs.First(job => job.Name == (jobName ?? jobClassType.Name));
                    j.Triggers.Add(trigger);
                    _scheduler.ScheduleJob(trigger);
                }
            }

            if (handler) {
                _scheduler.ListenerManager.AddJobListener(new JobFailureHandler(BusinessConfig.DEFAULT_GROUP, waitInterval, maxRetries));
            }
        }

        /// <summary>Start job scheduler</summary>
        /// <typeparam name="T">IJob object</typeparam>
        /// <param name="cronExpression">Cron expression string</param>
        public void Start(Type jobClassType, DateTimeOffset? dateTimeOffSet, TimeSpan timespan, bool handler = false, int waitInterval = 10, int maxRetries = 5) {
            Start(jobClassType, dateTimeOffSet, timespan, null, null, handler, waitInterval, maxRetries);
        }

        /// <summary>Start job scheduler with aditional parameters</summary>
        /// <typeparam name="T">IJob object</typeparam>
        /// <param name="cronExpression">Cron expression string</param>
        public void Start(Type jobClassType, DateTimeOffset? dateTimeOffSet, TimeSpan timespan, string jobName = null, JobDataMap jobDataMap = null, bool handler = false, int waitInterval = 10, int maxRetries = 5) {
            if (!jobClassType.GetInterfaces().Contains(typeof(IJob)))
                throw new ProjectException("ERROR_INVALID_JOB_CLASS_TYPE");

            IJobDetail job;
            string triggerName = jobName == null ? jobClassType.Name + BusinessConfig.TRIGGER : jobName + BusinessConfig.TRIGGER;

            if (jobDataMap == null)
                job = JobBuilder.Create(jobClassType).WithIdentity(jobName ?? jobClassType.Name, BusinessConfig.DEFAULT_GROUP).Build();
            else
                job = JobBuilder.Create(jobClassType).WithIdentity(jobName ?? jobClassType.Name, BusinessConfig.DEFAULT_GROUP).UsingJobData(jobDataMap).Build();

            if (ActiveJobs.Any(job => job.Name == (jobName ?? jobClassType.Name))) {
                var triggers = _scheduler.GetTriggersOfJob(job.Key).Result;
                if (triggers != null && triggers.Count > 0)
                    triggerName = jobName == null ? jobClassType.Name + triggers.Count + BusinessConfig.TRIGGER : jobName + triggers.Count + BusinessConfig.TRIGGER;
            }

            if (!dateTimeOffSet.HasValue || dateTimeOffSet == null)
                dateTimeOffSet = DateBuilder.TodayAt(0, 0, 0);

            ITrigger trigger = TriggerBuilder.Create().WithIdentity(triggerName, BusinessConfig.DEFAULT_GROUP)
                 .StartAt(dateTimeOffSet.Value).WithSimpleSchedule(x => x.WithInterval(timespan).RepeatForever()).ForJob(jobName ?? jobClassType.Name, BusinessConfig.DEFAULT_GROUP).Build();

            if (!ActiveJobs.Any(job => job.Name == (jobName ?? jobClassType.Name))) {
                ActiveJobs.Add(new Job(job, trigger, handler, waitInterval, maxRetries));
                _scheduler.Start();
                _scheduler.ScheduleJob(job, trigger);
            } else {
                if (!ActiveJobs.Any(job => job.Name == (jobName ?? jobClassType.Name) && job.Triggers.Any(t => ((SimpleTriggerImpl)t).RepeatInterval == timespan && ((SimpleTriggerImpl)t).StartTimeUtc == dateTimeOffSet))) {
                    Job j = ActiveJobs.First(job => job.Name == (jobName ?? jobClassType.Name));
                    j.Triggers.Add(trigger);
                    _scheduler.ScheduleJob(trigger);
                }
            }

            if (handler) {
                _scheduler.ListenerManager.AddJobListener(new JobFailureHandler(BusinessConfig.DEFAULT_GROUP, waitInterval, maxRetries));
            }
        }

        //public static void SetExecutionInfo(IHarpiaLoggerBO loggerBO, ref DPABT_JOB_EXECUTION jobExecution, string status) {
        //    jobExecution.DPAB_END_EXECUTION_DATETIME = DateTime.Now;
        //    TimeSpan duration = (TimeSpan)(jobExecution.DPAB_END_EXECUTION_DATETIME - jobExecution.DPAB_START_EXECUTION_DATETIME);
        //    jobExecution.DPAB_STATUS = status;
        //    jobExecution.DPAB_DURATION_SECONDS = (float)(duration.TotalSeconds);
        //    if (status == Status.NOK) {
        //        int? id = null;
        //        DPAAT_LOG_ERROR error = loggerBO.FindLastJobErrorLog(jobExecution.DPAB_JOB_NAME);
        //        if (error != null)
        //            id = error.DPAA_LOG_ERROR_ID;
        //        jobExecution.DPAB_LOG_ERROR_ID = id;
        //    }
        //}
    }
}