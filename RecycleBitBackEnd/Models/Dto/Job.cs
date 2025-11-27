using Quartz;
using System.Collections.Generic;

namespace RecycleBitBackEnd.Models.Dto {
    public class Job {
        public string Name { get; set; }
        public bool RetryConfigured { get; set; }
        public int Tentatives { get; set; }
        public int WaitInterval { get; set; }
        public int MaxRetries { get; set; }
        public IJobDetail JobDetail { get; set; }
        public List<ITrigger> Triggers { get; set; }

        public Job() {
        }

        public Job(IJobDetail jobDetail, ITrigger trigger, bool handler = false, int waitInterval = 0, int maxRetries = 0) {
            this.WaitInterval = waitInterval;
            this.MaxRetries = maxRetries;
            this.Tentatives = 0;
            this.JobDetail = jobDetail;
            this.RetryConfigured = handler;
            this.Triggers = new List<ITrigger>() { trigger };
            this.Name = this.JobDetail.Key.Name;
            //string cronExpression,bool handler = false,int waitInterval = 10,int maxRetries = 5
        }

        public Job(string name, int tentatives, IJobDetail jobDetail, ITrigger trigger) {
            Name = name;
            Tentatives = tentatives;
            JobDetail = jobDetail;
            Triggers = new List<ITrigger>() { trigger };
        }

        public void UpdateInfo(IJobDetail jobDetail, ITrigger trigger) {
            Tentatives++;
        }

        public void NewTentative() {
            Tentatives++;
        }

        public void ResetTentatives() {
            Tentatives = 0;
        }
    }
}