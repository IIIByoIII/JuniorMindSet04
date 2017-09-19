using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Shopping
{
    [TestClass]
    public class ShoppingTests
    {
        [TestMethod]
        public void FirstItemName()/*fs*/
        {
            Assert.AreEqual("apple", A.name);
        }/*fe*/

        [TestMethod]
        public void SecondItemPrice()/*fs*/
        {
            Assert.AreEqual(3.8, B.price);
        }/*fe*/

        Item A = new Item { name = "apple", price = 3.2 };
        Item B = new Item { name = "pear", price = 3.8 };

        struct Item/*fs*/
        {
            public string name;
            public double price;
        }/*fe*/
    }
}
