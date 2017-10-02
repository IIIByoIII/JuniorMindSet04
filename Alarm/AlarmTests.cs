using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Alarm
{
    [TestClass]
    public class AlarmTests
    {
        [TestMethod]
        public void GetFriday()
        {
            Assert.AreEqual(4, (int)Day.Friday);
        }

        public enum Day
        {
            Monday,
            Tuesday,
            Wednesday,
            Thursday,
            Friday,
            Saturday,
            Sunday
        }

        [TestMethod]
        public void GetMondayAlarm()
        {
            string[] alarmTimes = SetAlarmTimes(new string[7], "06:00", (byte)Day.Monday);
            Assert.AreEqual("06:00", alarmTimes[(byte)Day.Monday]);
        }

        [TestMethod]
        public void GetFullWeekAlarm()
        {
            string[] alarmTimes = new string[7];
            alarmTimes = SetAlarmTimes(alarmTimes, "06:00", (byte)Day.Monday, (byte)Day.Friday);
            alarmTimes = SetAlarmTimes(alarmTimes, "08:00", (byte)Day.Saturday, (byte)Day.Sunday);
            CollectionAssert.AreEqual(new string[] {"06:00", "06:00", "06:00", "06:00", "06:00", "08:00", "08:00"}, alarmTimes);
        }

        string[] SetAlarmTimes(string[] prevTimes, string alarmTime, byte startDay, byte endDay = 0)
        {
            if (startDay > endDay)
                endDay = startDay;
            for (int i = startDay; i <= endDay; i++)
                prevTimes[i] = alarmTime;
            return prevTimes;
        }

        [TestMethod]
        public void AlarmIsOn()
        {
            string[] alarmTimes = new string[7];
            alarmTimes = SetAlarmTimes(alarmTimes, "06:00", (byte)Day.Monday, (byte)Day.Friday);
            alarmTimes = SetAlarmTimes(alarmTimes, "08:00", (byte)Day.Saturday, (byte)Day.Sunday);
            Assert.AreEqual(true, IsAlarmTime(alarmTimes, "06:00", (byte)Day.Tuesday));
        }

        bool IsAlarmTime(string[] alarmTimes, string time, byte day)
        {
            return (alarmTimes[day] == time);
        }
    }
}
