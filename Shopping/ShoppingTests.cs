using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Shopping
{
    [TestClass]
    public class ShoppingTests
    {
        [TestMethod]
        public void FirstItemName()
        {
            var firstItem = new Item("apple", 3.2);
            Assert.AreEqual("apple", firstItem.name);
        }

        [TestMethod]
        public void SecondItemPrice()
        {
            var secondItem = new Item("pear", 3.8);
            Assert.AreEqual(3.8, secondItem.price);
        }

        public struct Item
        {
            public string name;
            public double price;

            public Item(string name, double price) {
                this.name = name;
                this.price = price;
            }
        }

        [TestMethod]
        public void TotalPrice()
        {
            var allItems = new Item[] { new Item("apple", 3.2), new Item("pear", 3.8) };
            Assert.AreEqual(7.0, GetTotalPrice(allItems));
        }

        static double GetTotalPrice(Item[] items)
        {
            double total = 0;
            for (int i = 0; i < items.Length; i++)
                total += items[i].price;
            return total;
        }
    }
}
