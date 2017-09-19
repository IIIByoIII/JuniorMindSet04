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
            var firstItem = new Item("apple", 3.2);
            Assert.AreEqual("apple", firstItem.name);
        }/*fe*/

        [TestMethod]
        public void SecondItemPrice()/*fs*/
        {
            var secondItem = new Item("pear", 3.8);
            Assert.AreEqual(3.8, secondItem.price);
        }/*fe*/

        public struct Item/*fs*/
        {
            public string name;
            public double price;

            public Item(string name, double price) {
                this.name = name;
                this.price = price;
            }
        }/*fe*/


    }
}
