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

        bool IsThereChar(char chr, char[] charArray)/*fs*/
        {
            for (int i = 0; i < charArray.Length; i++)
                if (chr == charArray[i])
                    return true;
            return false;
        }/*fe*/

        [TestMethod]
        public void Exists_x()/*fs*/ 
        {
            Assert.AreEqual(true, IsThereChar('x', SmallLetters()));
        }/*fe*/

        [TestMethod]
        public void Exists_X()/*fs*/ 
        {
            Assert.AreEqual(false, IsThereChar('X', SmallLetters()));
        }/*fe*/

        [TestMethod]
        public void Exists_l()/*fs*/ 
        {
            Assert.AreEqual(false, IsThereChar('l', SmallLetters()));
        }/*fe*/
        
        char[] SmallLetters()/*fs*/
        {
            var lettersArray = CharArray('a', 'z');
            foreach (char element in new char[] { 'l', 'o' })
                lettersArray = CharRemove(lettersArray, element);
            return lettersArray;
        }/*fe*/

        [TestMethod]
        public void Exists_B()/*fs*/ 
        {
            Assert.AreEqual(true, IsThereChar('B', CapitalLetters()));
        }/*fe*/

        [TestMethod]
        public void Exists_b()/*fs*/ 
        {
            Assert.AreEqual(false, IsThereChar('b', CapitalLetters()));
        }/*fe*/

        [TestMethod]
        public void Exists_I()/*fs*/ 
        {
            Assert.AreEqual(false, IsThereChar('I', CapitalLetters()));
        }/*fe*/

        char[] CapitalLetters()/*fs*/
        {
            var capitalsArray = CharArray('A', 'Z');
            foreach (char element in new char[] { 'I', 'O' })
                capitalsArray = CharRemove(capitalsArray, element);
            return capitalsArray;
        }/*fe*/

        [TestMethod]
        public void Exists_4()/*fs*/ 
        {
            Assert.AreEqual(true, IsThereChar('4', SomeNumbers()));
        }/*fe*/

        [TestMethod]
        public void Exists_0()/*fs*/ 
        {
            Assert.AreEqual(false, IsThereChar('0', SomeNumbers()));
        }/*fe*/

        char[] SomeNumbers()/*fs*/
        {
            return CharArray('2', '9');
        }/*fe*/

        [TestMethod]
        public void Exists_at()/*fs*/ 
        {
            Assert.AreEqual(true, IsThereChar('@', SomeSymbols()));
        }/*fe*/

        [TestMethod]
        public void Exists_percent()/*fs*/ 
        {
            Assert.AreEqual(true, IsThereChar('%', SomeSymbols()));
        }/*fe*/

        [TestMethod]
        public void Exists_equal()/*fs*/ 
        {
            Assert.AreEqual(true, IsThereChar('=', SomeSymbols()));
        }/*fe*/

        [TestMethod]
        public void Exists_Underscore()/*fs*/ 
        {
            Assert.AreEqual(true, IsThereChar('_', SomeSymbols()));
        }/*fe*/

        [TestMethod]
        public void Exists_LeftBracket()/*fs*/ 
        {
            Assert.AreEqual(false, IsThereChar('(', SomeSymbols()));
        }/*fe*/

        [TestMethod]
        public void Exists_LessThan()/*fs*/ 
        {
            Assert.AreEqual(false, IsThereChar('<', SomeSymbols()));
        }/*fe*/

        char[] SomeSymbols()/*fs*/
        {
            var symbolsArray = new char[] { '^', '_', '|' }.Concat(CharArray('!', '-').Concat(CharArray(':', '@')).ToArray()).ToArray();
            foreach (char element in new char[] { '{', '}', '[', ']', '(', ')', '/', '\\', '\'', '"', '~', ',', ';', '.', '<', '>' })
                symbolsArray = CharRemove(symbolsArray, element);
            return symbolsArray;
        }/*fe*/

        [TestMethod]
        public void FiveSmallLetters()/*fs*/ 
        {
            Assert.AreEqual(5, HowManyChars(SmallLetters(), GeneratePass(8, 1, 1, 1)));
        }/*fe*/

        [TestMethod]
        public void ThreeCapitalLetters()/*fs*/ 
        {
            Assert.AreEqual(3, HowManyChars(CapitalLetters(), GeneratePass(8, 3, 1, 1)));
        }/*fe*/

        [TestMethod]
        public void ThreeNumbers()/*fs*/ 
        {
            Assert.AreEqual(3, HowManyChars(SomeNumbers(), GeneratePass(8, 1, 3, 1)));
        }/*fe*/

        int HowManyChars(char[] toFind, string thePassword)/*fs*/
        {
            int result = 0;
            for (int i = 0; i < thePassword.Length; i++)
                for (int j = 0; j < toFind.Length; j++)
                    if (thePassword[i] == toFind[j])
                        result++;
            return result;
        }/*fe*/

        string GeneratePass(int passLength, int capitals, int numbers, int symbols)/*fs*/
        {
            Random rnd = new Random();
            var passChars = new char[passLength];
            for (int i = 0; i < passLength; i++) {
                if (capitals > 0) {
                    passChars[i] = CapitalLetters()[rnd.Next(0, CapitalLetters().Length)];
                    capitals--;
                }
                else if (numbers > 0) {
                    passChars[i] = SomeNumbers()[rnd.Next(0, SomeNumbers().Length)];
                    numbers--;
                }
                else if (symbols > 0) {
                    passChars[i] = SomeSymbols()[rnd.Next(0, SomeSymbols().Length)];
                    symbols--;
                }
                else
                    passChars[i] = SmallLetters()[rnd.Next(0, SmallLetters().Length)];
            }
            char[] shuffledChars = passChars.OrderBy(n => Guid.NewGuid()).ToArray();
            string result = new string(shuffledChars);
            return result;
        }/*fe*/
    }
}
