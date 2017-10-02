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

        string[] SetAlarmTimes(string[] prevTimes, string alarmTime, byte startDay, byte endDay = 0)
        {
            if (startDay > endDay)
                endDay = startDay;
            for (int i = startDay; i <= endDay; i++)
                prevTimes[i] = alarmTime;
            return prevTimes;
        }
    }
}
