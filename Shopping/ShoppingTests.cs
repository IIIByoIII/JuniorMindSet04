using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Shopping
{
    [TestClass]
    public class ShoppingTests
    {
        [TestMethod]
        public void FirstItemName()
        {
            Assert.AreEqual("apple", cart[0].name);
        }

        [TestMethod]
        public void SecondItemPrice()
        {
            Assert.AreEqual(3.8, cart[1].price);
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

        List<Item> cart = new List<Item> {new Item("apple", 3.2), new Item("pear", 3.8), new Item("orange", 4)};

        [TestMethod]
        public void TotalPrice()
        {
            Assert.AreEqual(11.0, GetTotalPrice(cart));
        }

        static double GetTotalPrice(List<Item> items)
        {
            double total = 0;
            for (int i = 0; i < items.Count; i++)
                total += items[i].price;
            return total;
        }
    }
}
