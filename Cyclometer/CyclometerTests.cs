using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cyclometer
{
    public struct Cyclist
    {
        public string name;
        public double diameter;
        public int[] revSec;
        public Cyclist(string name, double diameter, int[] revSec) {
            this.name = name;
            this.diameter = diameter;
            this.revSec = revSec;
        }
    }

    [TestClass]
    public class CyclometerTests
    {
        [TestMethod]
        public void GetTotalDistance()
        {
            var cyclists = new Cyclist[] { 
                new Cyclist("Radu", 26.4, new int[] {2, 2, 3, 3, 4, 4}),
                new Cyclist("Alin", 24.0, new int[] {3, 3, 4, 4, 3, 3}),
                new Cyclist("Robi", 27.0, new int[] {1, 1, 2, 3, 4, 5})
            }; // total 18+20+16=54 revolutions
            Assert.AreEqual(((18*26.4)+(20*24.0)+(16*27.0))*Math.PI, TotalDistance(cyclists));
        }

        double TotalDistance(Cyclist[] cyclists)
        {
            double totalDistance = 0;
            double cyclistRevolutions;
            for (int i = 0; i < cyclists.Length; i++) {
                cyclistRevolutions = 0;
                for (int j = 0; j < cyclists[i].revSec.Length; j++)
                    cyclistRevolutions += cyclists[i].revSec[j];
                totalDistance += cyclistRevolutions * cyclists[i].diameter;
            }
            totalDistance *= Math.PI;
            return totalDistance;
        }
    }
}


