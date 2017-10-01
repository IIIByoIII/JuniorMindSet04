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
            Assert.AreEqual(5, (int)Day.Friday);
        }

        enum Day
        {
            Monday = 1,
            Tuesday,
            Wednesday,
            Thursday,
            Friday,
            Saturday,
            Sunday
        }
    }
}
