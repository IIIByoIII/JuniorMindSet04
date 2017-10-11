using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Intersection
{
    enum Vector {R, U, L, D};

    struct Coord
    {
        public int? x, y;

        public Coord(int? x, int? y) {
            this.x = x;
            this.y = y;
        }
    }

    [TestClass]
    public class IntersectionTests
    {
        [TestMethod]
        public void StringToCoordinates()
        {
            CollectionAssert.AreEqual(new List<Coord> { new Coord(0, 0), new Coord(1, 0), new Coord(1, 1) }, GetCoords("RU"));
        }

        List<Coord> GetCoords(string vectors)
        {
            int x = 0;
            int y = 0;
            string direction;
            var result = new List<Coord>();
            result.Add(new Coord(x, y));
            for (int i = 0; i < vectors.Length; i++) {
                direction = vectors[i].ToString();
                Vector vector = (Vector)Enum.Parse(typeof(Vector), direction);
                switch ((int)vector)
                {
                    case 0:
                        x++;
                        break;
                    case 1:
                        y++;
                        break;
                    case 2:
                        x--;
                        break;
                    default:
                        y--;
                        break;
                }
                result.Add(new Coord(x, y));
            }
            return result;
        }

    }
}
