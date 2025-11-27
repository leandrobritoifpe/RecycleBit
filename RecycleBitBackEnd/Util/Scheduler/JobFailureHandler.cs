using Quartz;
using RecycleBitBackEnd.Config;
using RecycleBitBackEnd.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RecycleBitBackEnd.Util.Scheduler {
    public class JobFailureHandler : IJobListener {
        public string Name => "FailJobListener";
        public static int WaitInterval { get; set; }
        public static int MaxRetries { get; set; }
        public static readonly string NumTriesKey = "numTriesKey";
        public static string TriggerGroup { get; set; }

        private List<Job> Jobs;

        public JobFailureHandler() {
        }

        public JobFailureHandler(string triggerGroup, int waitInterval, int maxRetries) {
            WaitInterval = waitInterval;
            MaxRetries = maxRetries;
            TriggerGroup = triggerGroup;
            Jobs = new List<Job>();
        }

        public Task JobToBeExecuted(IJobExecutionContext context, CancellationToken cancellationToken = default) {
            if (!Jobs.Any(job => job.Name == context.JobDetail.Key.Name)) {
                Jobs.Add(new Job(context.JobDetail.Key.Name, 0, context.JobDetail, context.Trigger));
            }

            Job currentJob = Jobs.FirstOrDefault(job => job.Name == context.JobDetail.Key.Name);

            currentJob.NewTentative();

            return Task.CompletedTask;
        }

        public Task JobExecutionVetoed(IJobExecutionContext context, CancellationToken cancellationToken = default) {
            return Task.FromResult<object>(null);// Não fará nada
        }

        public async Task JobWasExecuted(IJobExecutionContext context, JobExecutionException jobException, CancellationToken cancellationToken = default) {
            Job currentJob = Jobs.FirstOrDefault(job => job.Name == context.JobDetail.Key.Name);

            if (jobException == null) {
                ResetSchedules(context);
                return;
            }

            if (currentJob.Tentatives >= MaxRetries) {
                ResetSchedules(context);
                return;
            }

            List<ITrigger> asd = context.Scheduler.GetTriggersOfJob(currentJob.JobDetail.Key).Result.ToList();

            ITrigger newTrigger = TriggerBuilder.Create()
                .WithIdentity(context.Trigger.Key.Name + $"-Retry{currentJob.Tentatives}", BusinessConfig.DEFAULT_GROUP)
                .StartAt(DateTimeOffset.Now.AddSeconds(WaitInterval))
                .ForJob(context.JobDetail.Key)
                .Build();

            try {
                if (currentJob.Tentatives == 1)
                    context.Scheduler.PauseTrigger(context.Trigger.Key).Wait();
                context.Scheduler.ScheduleJob(newTrigger).Wait();
            } catch (Exception e) {
                string a = e.Message;
            }
        }

        private async void ResetSchedules(IJobExecutionContext context) {
            Job currentJob = Jobs.FirstOrDefault(job => job.Name == context.JobDetail.Key.Name);

            //loggerBO.SetDebugLog(new Models.Request.NewLogRequest(CommonConfig.ApplicationName, DateTime.Now, $"Job {context.JobDetail.Key.Name} - Trigger Key: {context.Trigger.Key} - {context.Trigger.Key.Name}. Job Trigger: {currentJob.Triggers[0].Key} - {currentJob.Triggers[0].Key.Name} Próxima execução: {TimeZoneInfo.ConvertTime(context.Scheduler.GetTrigger(currentJob.Triggers[0].Key).Result.GetNextFireTimeUtc().GetValueOrDefault(), BusinessConfig.BRAZIL_TIMEZONE)}", string.Empty, "Teste de reexecução de Job"));

            context.Scheduler.ResumeTrigger(currentJob.Triggers[0].Key).Wait();

            //loggerBO.SetDebugLog(new Models.Request.NewLogRequest(CommonConfig.ApplicationName, DateTime.Now, $"Job {context.JobDetail.Key.Name} - Schedule Configurado. Próxima execução: {TimeZoneInfo.ConvertTime(context.Scheduler.GetTrigger(currentJob.Triggers[0].Key).Result.GetNextFireTimeUtc().GetValueOrDefault(), BusinessConfig.BRAZIL_TIMEZONE)}", string.Empty, "Teste de reexecução de Job"));

            currentJob.ResetTentatives();

            //loggerBO.SetDebugLog(new Models.Request.NewLogRequest(CommonConfig.ApplicationName, DateTime.Now, "Job resetado.", string.Empty, "Teste de reexecução de Job"));
        }
    }
}