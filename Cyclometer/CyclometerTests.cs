using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

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

        public double Distance() {
            return revSec.Sum() * diameter * Math.PI;
        }

        public double MaxSpeed() {
            return revSec.Max() * diameter; 
        }

        public int MaxSpeedIndex() {
            return Array.IndexOf(revSec, revSec.Max());
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
            for (int i = 0; i < cyclists.Length; i++)
                totalDistance += cyclists[i].Distance();
            return totalDistance;
        }

        [TestMethod]
        public void GetFastestCyclist()
        {
            var cyclists = new Cyclist[] { 
                new Cyclist("Radu", 26.4, new int[] {2, 2, 3, 3, 4, 4}),
                new Cyclist("Alin", 24.0, new int[] {3, 3, 4, 4, 3, 3}),
                new Cyclist("Robi", 27.0, new int[] {1, 1, 2, 3, 4, 5})
            };
            Assert.AreEqual("Robi in second 6", FastestCyclist(cyclists));
        }

        string FastestCyclist(Cyclist[] cyclists)
        {
            double[] maxSpeed = new double[cyclists.Length];
            for (int i = 0; i < cyclists.Length; i++)
                maxSpeed[i] = cyclists[i].MaxSpeed();
            int fastest = Array.IndexOf(maxSpeed, maxSpeed.Max());
            return cyclists[fastest].name + " in second " + (cyclists[fastest].MaxSpeedIndex() + 1).ToString();
        }

        [TestMethod]
        public void GetBestAverageSpeed()
        {
            var cyclists = new Cyclist[] { 
                new Cyclist("Radu", 26.4, new int[] {2, 2, 3, 3, 4, 4}),
                new Cyclist("Alin", 24.0, new int[] {3, 3, 4, 4, 3, 3}),
                new Cyclist("Robi", 27.0, new int[] {1, 1, 2, 3, 4, 5})
            };
            Assert.AreEqual("The winner is Alin", BestAverage(cyclists));
        }

        string BestAverage(Cyclist[] cyclists)
        {
            double[] averages = new double[cyclists.Length];
            for (int i = 0; i < cyclists.Length; i++)
                averages[i] = cyclists[i].revSec.Sum() * cyclists[i].diameter / cyclists[i].revSec.Length; 
            return "The winner is " + cyclists[Array.IndexOf(averages, averages.Max())].name;
        }
    }
}


