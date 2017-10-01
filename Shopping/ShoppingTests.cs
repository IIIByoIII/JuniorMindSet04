﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

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
        public void CheapestItem()
        {
            Assert.AreEqual("apple", Cheapest(cart));
        }

        string Cheapest(List<Item> items)
        {
            return items[MinMaxItemPriceIndex(items)[0]].name;
        }

        [TestMethod]
        public void MinMaxIndex()
        {
            CollectionAssert.AreEqual(new int[] {0, 2}, MinMaxItemPriceIndex(cart));
        }

        int[] MinMaxItemPriceIndex(List<Item> items)
        {
            double[] prices = new double [items.Count];
            for (int i = 0; i < items.Count; i++)
                prices[i] = items[i].price;
            int min = Array.IndexOf(prices, prices.Min());
            int max = Array.IndexOf(prices, prices.Max());
            return new int[] {min, max};
        }

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

        [TestMethod]
        public void ExpensiveRemovedPrice()
        {
            Assert.AreEqual(7.0, GetTotalPrice(RemoveMostExpensive(cart)));
        }

        List<Item> RemoveMostExpensive(List<Item> items)
        {
            items.RemoveAt(MinMaxItemPriceIndex(items)[1]);
            return items;
        }

        [TestMethod]
        public void TotalPriceWithNewItem()
        {
            Assert.AreEqual(14.0, GetTotalPrice(AddNewItem(cart, "banana", 3)));
        }

        List<Item> AddNewItem(List<Item> items, string itemName, double itemPrice)
        {
            items.Add(new Item(itemName, itemPrice));
            return items;
        }

        [TestMethod]
        public void AveragePrice()
        {
            Assert.AreEqual((11.0 / 3), GetAveragePrice(cart));
        }

        double GetAveragePrice(List<Item> items)
        {
            return (GetTotalPrice(items) / items.Count);
        }
    }
}
