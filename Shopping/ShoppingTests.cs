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
            Assert.AreEqual("apple", A.name);
        }

        Item A = new Item { name = "apple", price = 3.2 };

        struct Item
        {
            public string name;
            public double price;
        }
    }
}
