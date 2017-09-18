using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Password
{
    [TestClass]
    public class PasswordTests
    {
        [TestMethod]
        public void CreateCharList()/*fs*/
        {
            CollectionAssert.AreEqual(new char[] { 'a', 'b' ,'c' }, CharArray('a', 'c'));
        }/*fe*/

        [TestMethod]
        public void CreateCharListReversed()/*fs*/
        {
            CollectionAssert.AreEqual(new char[] {'c'}, CharArray('c', 'a'));
        }/*fe*/

        char[] CharArray(char start, char end)/*fs*/
        {
            if (start <= end) {
                char jChar = start;
                char[] result = new char[end + 1 - start];
                for (int i = 0; i < (end + 1 - start); i++) {
                    result[i] = jChar;
                    jChar++;
                }
                return result;
            }
            return new char[] {start};
        }/*fe*/

        [TestMethod]
        public void RemoveFromCharList()/*fs*/
        {
            CollectionAssert.AreEqual(new char[] { 'a', 'c' }, CharRemove(CharArray('a', 'c'), 'b'));
        }/*fe*/

        char[] CharRemove(char[] theArray, char toRemove)/*fs*/
        {
            return theArray.Where(val => val != toRemove).ToArray();
        }/*fe*/

        [TestMethod]
        public void Exists_x()/*fs*/ 
        {
            Assert.AreEqual(true, IsThereChar('x', SmallLetters()));
        }/*fe*/

        bool IsThereChar(char chr, char[] charArray)/*fs*/
        {
            for (int i = 0; i < charArray.Length; i++)
                if (chr == charArray[i])
                    return true;
            return false;
        }/*fe*/
        
        char[] SmallLetters()/*fs*/
        {
            var lettersArray = CharArray('a', 'z');
            foreach (char element in new char[] { 'l', 'o' })
                lettersArray = CharRemove(lettersArray, element);
            return lettersArray;
        }/*fe*/
    }
}
