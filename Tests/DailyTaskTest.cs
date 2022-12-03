using NUnit.Framework;
using Daily;
using System;
using System.Threading;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }


        [Test]
        public void WhenCreateTask_ChekState()
        {

            DailyTask task = new DailyTask("task", "task task description");

            Assert.IsFalse(task.IsRunning);
            Assert.IsFalse(task.HasIntervals);
        }

        [Test]
        public void WhenAddOpenCloseInterval_ChekState()
        {

            DailyTask task = new DailyTask("task", "task task description");
            task.Start();

            Assert.IsTrue(task.HasIntervals);
            Assert.IsTrue(task.IsRunning);

            task.Stop();

            Assert.IsTrue(task.HasIntervals);
            Assert.IsFalse(task.IsRunning);
        }

        [Test]
        public void WhenOpenChild_OpenParent()
        {

            DailyTask parentTask = new DailyTask("parentTask", "parentTask task description");
            DailyTask firstLeveChildTask = new DailyTask("firstLeveChildTask", "firstLeveChildTask task description");
            DailyTask secondLeveChildTask = new DailyTask("secondLeveChildTask", "secondLeveChildTask task description");

            parentTask.AddTask(firstLeveChildTask);
            firstLeveChildTask.AddTask(secondLeveChildTask);

            Assert.IsFalse(secondLeveChildTask.IsRunning);
            Assert.IsFalse(firstLeveChildTask.IsRunning);
            Assert.IsFalse(parentTask.IsRunning);

            secondLeveChildTask.Start();

            Assert.IsTrue(secondLeveChildTask.IsRunning);
            Assert.IsTrue(firstLeveChildTask.IsRunning);
            Assert.IsTrue(parentTask.IsRunning);
        }


        [Test]
        public void RealDaily_Check()
        {

            DailyTask journey = new DailyTask(DateTime.Now.ToString("dd/MM/yyyy"), "Journey description");

            DailyTask userTEC_00001 = new DailyTask("TEC_00001", "TEC_00001 task description");
            DailyTask userTEC_00001_sub1 = new DailyTask("TEC_00001 1", "Pruebas TEC_00001");
            DailyTask userTEC_00001_sub2 = new DailyTask("TEC_00001 2", "Integrar staging");
            DailyTask userTEC_00001_sub3 = new DailyTask("TEC_00001 3", "Probar DEMO");
            DailyTask userTEC_00001_sub4 = new DailyTask("TEC_00001 4", "Integrar DEVELOP");
            DailyTask userTEC_00001_sub5 = new DailyTask("TEC_00001 5", "Probar PRE");

            DailyTask userTEC_00002 = new DailyTask("TEC_00002", "TEC_00002 task description");
            DailyTask userTEC_00002_sub1 = new DailyTask("TEC_00002 1", "Pruebas TEC_00002");
            DailyTask userTEC_00002_sub2 = new DailyTask("TEC_00002 2", "Integrar staging");
            DailyTask userTEC_00002_sub3 = new DailyTask("TEC_00002 3", "Probar DEMO");
            DailyTask userTEC_00002_sub4 = new DailyTask("TEC_00001 4", "Integrar DEVELOP");
            DailyTask userTEC_00002_sub5 = new DailyTask("TEC_00001 5", "Probar PRE");


            userTEC_00001.AddTask(userTEC_00001_sub1);
            userTEC_00001.AddTask(userTEC_00001_sub2);
            userTEC_00001.AddTask(userTEC_00001_sub3);
            userTEC_00001.AddTask(userTEC_00001_sub4);
            userTEC_00001.AddTask(userTEC_00001_sub5);

            userTEC_00002.AddTask(userTEC_00002_sub1);
            userTEC_00002.AddTask(userTEC_00002_sub2);
            userTEC_00002.AddTask(userTEC_00002_sub3);
            userTEC_00002.AddTask(userTEC_00002_sub4);
            userTEC_00002.AddTask(userTEC_00002_sub5);

            journey.AddTask(userTEC_00001);
            journey.AddTask(userTEC_00002);

            //
            Assert.IsFalse(journey.IsRunning);

            Assert.IsFalse(userTEC_00001.IsRunning);
            Assert.IsFalse(userTEC_00002.IsRunning);

            Assert.IsFalse(userTEC_00001_sub1.IsRunning);
            Assert.IsFalse(userTEC_00001_sub2.IsRunning);
            Assert.IsFalse(userTEC_00001_sub3.IsRunning);
            Assert.IsFalse(userTEC_00001_sub4.IsRunning);
            Assert.IsFalse(userTEC_00001_sub5.IsRunning);

            Assert.IsFalse(userTEC_00002_sub1.IsRunning);
            Assert.IsFalse(userTEC_00002_sub2.IsRunning);
            Assert.IsFalse(userTEC_00002_sub3.IsRunning);
            Assert.IsFalse(userTEC_00002_sub4.IsRunning);
            Assert.IsFalse(userTEC_00002_sub5.IsRunning);
            //

            userTEC_00001_sub1.Start();

            //
            Assert.IsTrue(journey.IsRunning);

            Assert.IsTrue(userTEC_00001.IsRunning);
            Assert.IsFalse(userTEC_00002.IsRunning);

            Assert.IsTrue(userTEC_00001_sub1.IsRunning);
            Assert.IsFalse(userTEC_00001_sub2.IsRunning);
            Assert.IsFalse(userTEC_00001_sub3.IsRunning);
            Assert.IsFalse(userTEC_00001_sub4.IsRunning);
            Assert.IsFalse(userTEC_00001_sub5.IsRunning);

            Assert.IsFalse(userTEC_00002_sub1.IsRunning);
            Assert.IsFalse(userTEC_00002_sub2.IsRunning);
            Assert.IsFalse(userTEC_00002_sub3.IsRunning);
            Assert.IsFalse(userTEC_00002_sub4.IsRunning);
            Assert.IsFalse(userTEC_00002_sub5.IsRunning);
            //
            userTEC_00001_sub2.Start();
            //
            Assert.IsTrue(journey.IsRunning);

            Assert.IsTrue(userTEC_00001.IsRunning);
            Assert.IsFalse(userTEC_00002.IsRunning);

            Assert.IsFalse(userTEC_00001_sub1.IsRunning);
            Assert.IsTrue(userTEC_00001_sub2.IsRunning);
            Assert.IsFalse(userTEC_00001_sub3.IsRunning);
            Assert.IsFalse(userTEC_00001_sub4.IsRunning);
            Assert.IsFalse(userTEC_00001_sub5.IsRunning);

            Assert.IsFalse(userTEC_00002_sub1.IsRunning);
            Assert.IsFalse(userTEC_00002_sub2.IsRunning);
            Assert.IsFalse(userTEC_00002_sub3.IsRunning);
            Assert.IsFalse(userTEC_00002_sub4.IsRunning);
            Assert.IsFalse(userTEC_00002_sub5.IsRunning);
            //
            userTEC_00001_sub3.Start();
            //
            Assert.IsTrue(journey.IsRunning);

            Assert.IsTrue(userTEC_00001.IsRunning);
            Assert.IsFalse(userTEC_00002.IsRunning);

            Assert.IsFalse(userTEC_00001_sub1.IsRunning);
            Assert.IsFalse(userTEC_00001_sub2.IsRunning);
            Assert.IsTrue(userTEC_00001_sub3.IsRunning);
            Assert.IsFalse(userTEC_00001_sub4.IsRunning);
            Assert.IsFalse(userTEC_00001_sub5.IsRunning);

            Assert.IsFalse(userTEC_00002_sub1.IsRunning);
            Assert.IsFalse(userTEC_00002_sub2.IsRunning);
            Assert.IsFalse(userTEC_00002_sub3.IsRunning);
            Assert.IsFalse(userTEC_00002_sub4.IsRunning);
            Assert.IsFalse(userTEC_00002_sub5.IsRunning);
            //
            userTEC_00001_sub4.Start();
            //
            Assert.IsTrue(journey.IsRunning);

            Assert.IsTrue(userTEC_00001.IsRunning);
            Assert.IsFalse(userTEC_00002.IsRunning);

            Assert.IsFalse(userTEC_00001_sub1.IsRunning);
            Assert.IsFalse(userTEC_00001_sub2.IsRunning);
            Assert.IsFalse(userTEC_00001_sub3.IsRunning);
            Assert.IsTrue(userTEC_00001_sub4.IsRunning);
            Assert.IsFalse(userTEC_00001_sub5.IsRunning);

            Assert.IsFalse(userTEC_00002_sub1.IsRunning);
            Assert.IsFalse(userTEC_00002_sub2.IsRunning);
            Assert.IsFalse(userTEC_00002_sub3.IsRunning);
            Assert.IsFalse(userTEC_00002_sub4.IsRunning);
            Assert.IsFalse(userTEC_00002_sub5.IsRunning);
            //
            userTEC_00001_sub5.Start();
            //
            Assert.IsTrue(journey.IsRunning);

            Assert.IsTrue(userTEC_00001.IsRunning);
            Assert.IsFalse(userTEC_00002.IsRunning);

            Assert.IsFalse(userTEC_00001_sub1.IsRunning);
            Assert.IsFalse(userTEC_00001_sub2.IsRunning);
            Assert.IsFalse(userTEC_00001_sub3.IsRunning);
            Assert.IsFalse(userTEC_00001_sub4.IsRunning);
            Assert.IsTrue(userTEC_00001_sub5.IsRunning);

            Assert.IsFalse(userTEC_00002_sub1.IsRunning);
            Assert.IsFalse(userTEC_00002_sub2.IsRunning);
            Assert.IsFalse(userTEC_00002_sub3.IsRunning);
            Assert.IsFalse(userTEC_00002_sub4.IsRunning);
            Assert.IsFalse(userTEC_00002_sub5.IsRunning);
            //
            userTEC_00002_sub1.Start();
            //
            Assert.IsTrue(journey.IsRunning);

            Assert.IsFalse(userTEC_00001.IsRunning);
            Assert.IsTrue(userTEC_00002.IsRunning);

            Assert.IsFalse(userTEC_00001_sub1.IsRunning);
            Assert.IsFalse(userTEC_00001_sub2.IsRunning);
            Assert.IsFalse(userTEC_00001_sub3.IsRunning);
            Assert.IsFalse(userTEC_00001_sub4.IsRunning);
            Assert.IsFalse(userTEC_00001_sub5.IsRunning);

            Assert.IsTrue(userTEC_00002_sub1.IsRunning);
            Assert.IsFalse(userTEC_00002_sub2.IsRunning);
            Assert.IsFalse(userTEC_00002_sub3.IsRunning);
            Assert.IsFalse(userTEC_00002_sub4.IsRunning);
            Assert.IsFalse(userTEC_00002_sub5.IsRunning);
            //
            userTEC_00002_sub2.Start();
            //
            Assert.IsTrue(journey.IsRunning);

            Assert.IsFalse(userTEC_00001.IsRunning);
            Assert.IsTrue(userTEC_00002.IsRunning);

            Assert.IsFalse(userTEC_00001_sub1.IsRunning);
            Assert.IsFalse(userTEC_00001_sub2.IsRunning);
            Assert.IsFalse(userTEC_00001_sub3.IsRunning);
            Assert.IsFalse(userTEC_00001_sub4.IsRunning);
            Assert.IsFalse(userTEC_00001_sub5.IsRunning);

            Assert.IsFalse(userTEC_00002_sub1.IsRunning);
            Assert.IsTrue(userTEC_00002_sub2.IsRunning);
            Assert.IsFalse(userTEC_00002_sub3.IsRunning);
            Assert.IsFalse(userTEC_00002_sub4.IsRunning);
            Assert.IsFalse(userTEC_00002_sub5.IsRunning);
            //
            userTEC_00002_sub3.Start();
            //
            Assert.IsTrue(journey.IsRunning);

            Assert.IsFalse(userTEC_00001.IsRunning);
            Assert.IsTrue(userTEC_00002.IsRunning);

            Assert.IsFalse(userTEC_00001_sub1.IsRunning);
            Assert.IsFalse(userTEC_00001_sub2.IsRunning);
            Assert.IsFalse(userTEC_00001_sub3.IsRunning);
            Assert.IsFalse(userTEC_00001_sub4.IsRunning);
            Assert.IsFalse(userTEC_00001_sub5.IsRunning);

            Assert.IsFalse(userTEC_00002_sub1.IsRunning);
            Assert.IsFalse(userTEC_00002_sub2.IsRunning);
            Assert.IsTrue(userTEC_00002_sub3.IsRunning);
            Assert.IsFalse(userTEC_00002_sub4.IsRunning);
            Assert.IsFalse(userTEC_00002_sub5.IsRunning);
            //
            userTEC_00002_sub4.Start();
            //
            Assert.IsTrue(journey.IsRunning);

            Assert.IsFalse(userTEC_00001.IsRunning);
            Assert.IsTrue(userTEC_00002.IsRunning);

            Assert.IsFalse(userTEC_00001_sub1.IsRunning);
            Assert.IsFalse(userTEC_00001_sub2.IsRunning);
            Assert.IsFalse(userTEC_00001_sub3.IsRunning);
            Assert.IsFalse(userTEC_00001_sub4.IsRunning);
            Assert.IsFalse(userTEC_00001_sub5.IsRunning);

            Assert.IsFalse(userTEC_00002_sub1.IsRunning);
            Assert.IsFalse(userTEC_00002_sub2.IsRunning);
            Assert.IsFalse(userTEC_00002_sub3.IsRunning);
            Assert.IsTrue(userTEC_00002_sub4.IsRunning);
            Assert.IsFalse(userTEC_00002_sub5.IsRunning);
            //
            userTEC_00002_sub5.Start();
            //
            Assert.IsTrue(journey.IsRunning);

            Assert.IsFalse(userTEC_00001.IsRunning);
            Assert.IsTrue(userTEC_00002.IsRunning);

            Assert.IsFalse(userTEC_00001_sub1.IsRunning);
            Assert.IsFalse(userTEC_00001_sub2.IsRunning);
            Assert.IsFalse(userTEC_00001_sub3.IsRunning);
            Assert.IsFalse(userTEC_00001_sub4.IsRunning);
            Assert.IsFalse(userTEC_00001_sub5.IsRunning);

            Assert.IsFalse(userTEC_00002_sub1.IsRunning);
            Assert.IsFalse(userTEC_00002_sub2.IsRunning);
            Assert.IsFalse(userTEC_00002_sub3.IsRunning);
            Assert.IsFalse(userTEC_00002_sub4.IsRunning);
            Assert.IsTrue(userTEC_00002_sub5.IsRunning);
            //
            journey.Stop();
            //
            Assert.IsFalse(journey.IsRunning);

            Assert.IsFalse(userTEC_00001.IsRunning);
            Assert.IsFalse(userTEC_00002.IsRunning);

            Assert.IsFalse(userTEC_00001_sub1.IsRunning);
            Assert.IsFalse(userTEC_00001_sub2.IsRunning);
            Assert.IsFalse(userTEC_00001_sub3.IsRunning);
            Assert.IsFalse(userTEC_00001_sub4.IsRunning);
            Assert.IsFalse(userTEC_00001_sub5.IsRunning);

            Assert.IsFalse(userTEC_00002_sub1.IsRunning);
            Assert.IsFalse(userTEC_00002_sub2.IsRunning);
            Assert.IsFalse(userTEC_00002_sub3.IsRunning);
            Assert.IsFalse(userTEC_00002_sub4.IsRunning);
            Assert.IsFalse(userTEC_00002_sub5.IsRunning);
            //


            Assert.AreEqual(journey.Intervals.Count, 10);
            Assert.AreEqual(userTEC_00001.Intervals.Count, 5);
            Assert.AreEqual(userTEC_00002.Intervals.Count, 5);
        }

    }
}