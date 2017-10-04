using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Alarm
{
    [TestClass]
    public class AlarmTests
    {
        [TestMethod]
        public void SetCheckWeekend()
        {
            Day days = Day.Saturday;
            days |= Day.Sunday;
            Assert.AreEqual(96, (byte)days);
        }

        [Flags]
        public enum Day
        {
            Monday = 1,
            Tuesday = 2,
            Wednesday = 4,
            Thursday = 8,
            Friday = 16,
            Saturday = 32,
            Sunday = 64
        }

        [TestMethod]
        public void SetCheckTuesdayThursday()
        {
            Day days = SetAlarmDay(Day.Tuesday);
            days |= SetAlarmDay(Day.Thursday);
            Assert.AreEqual(Day.Tuesday | Day.Thursday, days);
        }

        [TestMethod]
        public void SetCheckWeek()
        {
            Assert.AreEqual((Day)127, SetAlarmDay(Day.Monday, Day.Sunday));
        }

        Day SetAlarmDay(Day startDay, Day endDay = 0)
        {
            byte days = 0;
            if (startDay > endDay)
                endDay = startDay;
            for (byte i = (byte)startDay; i <= (byte)endDay; i *= 2)
                days += i;
            return (Day)days;
        }

        [TestMethod]
        public void AlarmIsOn()
        {
            Assert.AreEqual(true, IsAlarm(Day.Tuesday, (SetAlarmDay(Day.Monday, Day.Friday))));
        }

        [TestMethod]
        public void AlarmIsOff()
        {
            Assert.AreEqual(false, IsAlarm(Day.Sunday, (SetAlarmDay(Day.Monday, Day.Friday))));
        }

        bool IsAlarm(Day day, Day days)
        {
            return (day & days) != 0;
        }

        [TestMethod]
        public void Tuesday_6AM_Alarm()
        {
            object[] alarm = SetAlarm("06:00", Day.Monday, Day.Friday);
            Assert.AreEqual("06:00", alarm[0]);
            Assert.AreEqual(true, IsAlarm(Day.Tuesday, (Day)alarm[1]));
        }

        [TestMethod]
        public void Sunday_8AM_Alarm()
        {
            object[] alarm = SetAlarm("08:00", Day.Saturday, Day.Sunday);
            Assert.AreEqual("08:00", alarm[0]);
            Assert.AreEqual(true, IsAlarm(Day.Sunday, (Day)alarm[1]));
        }

        object[] SetAlarm(string time, Day startDay, Day endDay)
        {
            object[] result = new object[2];
            result[0] = time;
            result[1] = SetAlarmDay(startDay, endDay);
            return result;
        }
    }
}
