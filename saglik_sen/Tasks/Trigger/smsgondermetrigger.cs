using Quartz;
using Quartz.Impl;
using saglik_sen.Tasks.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace saglik_sen.Tasks.Trigger
{
    public class smsgondermetrigger
    {
        public static void olusturucu()
        {
            
            IScheduler zamanlayici = StdSchedulerFactory.GetDefaultScheduler();
            if (!zamanlayici.IsStarted)
            {
                zamanlayici.Start();

            }
            if (!zamanlayici.IsStarted)
            {
                zamanlayici.Start();
            }

            IJobDetail gorev = JobBuilder.Create<otosms>().Build();
            ICronTrigger tetikleyici = (ICronTrigger)TriggerBuilder.Create()
                .WithIdentity("otosms", "null")
                .WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(10,00))
                .Build();
            zamanlayici.ScheduleJob(gorev, tetikleyici);
        }
    } }

            
        
       