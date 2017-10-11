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
            var result = new List<Coord>{ new Coord(x, y) };
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

        [TestMethod]
        public void IntersectingAt11()
        {
            var coords = GetCoords("URURDLDL");
            Assert.AreEqual(new Coord(1, 1), GetIntersection(coords));
        }

        Coord GetIntersection(List<Coord> coords)
        {
            for (int i = 0; i < coords.Count; i++)
                for (int j = i - 1; j >= 0 ; j--)
                    if ((coords[i].x == coords[j].x) && (coords[i].y == coords[j].y))
                        return coords[j];
            return new Coord(null, null);
        }
    }
}
