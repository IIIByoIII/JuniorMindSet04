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

        [TestMethod]
        public void GetFastestCyclist()
        {
            var cyclists = new Cyclist[] { 
                new Cyclist("Radu", 26.4, new int[] {2, 2, 3, 3, 4, 4}),
                new Cyclist("Alin", 24.0, new int[] {3, 3, 4, 4, 3, 3}),
                new Cyclist("Robi", 27.0, new int[] {1, 1, 2, 3, 4, 5})
            };
            Assert.AreEqual("Robi in second 6", MaxSpeed(cyclists));
        }

        string MaxSpeed(Cyclist[] cyclists)
        {
            int[] maxRevIndex = new int[cyclists.Length];
            double[] maxSpeed = new double[cyclists.Length];
            for (int i = 0; i < cyclists.Length; i++) {
                maxRevIndex[i] = Array.IndexOf(cyclists[i].revSec, cyclists[i].revSec.Max());
                maxSpeed[i] = cyclists[i].diameter * cyclists[i].revSec.Max();
            }
            int fastest = Array.IndexOf(maxSpeed, maxSpeed.Max());
            return cyclists[fastest].name + " in second " + (maxRevIndex[fastest] + 1).ToString();
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


