using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Password
{
    [TestClass]
    public class PasswordTests
    {
        [TestMethod]
        public void CreateCharList()
        {
            CollectionAssert.AreEqual(new char[] { 'a', 'b' ,'c' }, CharArray('a', 'c'));
        }

        [TestMethod]
        public void CreateCharListReversed()
        {
            CollectionAssert.AreEqual(new char[] {'c'}, CharArray('c', 'a'));
        }

        char[] CharArray(char start, char end)
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
        }

        [TestMethod]
        public void RemoveFromCharList()
        {
            CollectionAssert.AreEqual(new char[] { 'a', 'c' }, CharRemove(CharArray('a', 'c'), 'b'));
        }

        char[] CharRemove(char[] theArray, char toRemove)
        {
            return theArray.Where(val => val != toRemove).ToArray();
        }

        bool IsThereChar(char chr, char[] charArray)
        {
            for (int i = 0; i < charArray.Length; i++)
                if (chr == charArray[i])
                    return true;
            return false;
        }

        [TestMethod]
        public void Exists_x() 
        {
            Assert.AreEqual(true, IsThereChar('x', SmallLetters()));
        }

        [TestMethod]
        public void Exists_X() 
        {
            Assert.AreEqual(false, IsThereChar('X', SmallLetters()));
        }

        [TestMethod]
        public void Exists_l() 
        {
            Assert.AreEqual(false, IsThereChar('l', SmallLetters()));
        }
        
        char[] SmallLetters()
        {
            var lettersArray = CharArray('a', 'z');
            foreach (char element in new char[] { 'l', 'o' })
                lettersArray = CharRemove(lettersArray, element);
            return lettersArray;
        }

        [TestMethod]
        public void Exists_B() 
        {
            Assert.AreEqual(true, IsThereChar('B', CapitalLetters()));
        }

        [TestMethod]
        public void Exists_b() 
        {
            Assert.AreEqual(false, IsThereChar('b', CapitalLetters()));
        }

        [TestMethod]
        public void Exists_I() 
        {
            Assert.AreEqual(false, IsThereChar('I', CapitalLetters()));
        }

        char[] CapitalLetters()
        {
            var capitalsArray = CharArray('A', 'Z');
            foreach (char element in new char[] { 'I', 'O' })
                capitalsArray = CharRemove(capitalsArray, element);
            return capitalsArray;
        }

        [TestMethod]
        public void Exists_4() 
        {
            Assert.AreEqual(true, IsThereChar('4', SomeNumbers()));
        }

        [TestMethod]
        public void Exists_0() 
        {
            Assert.AreEqual(false, IsThereChar('0', SomeNumbers()));
        }

        char[] SomeNumbers()
        {
            return CharArray('2', '9');
        }

        [TestMethod]
        public void Exists_at() 
        {
            Assert.AreEqual(true, IsThereChar('@', SomeSymbols()));
        }

        [TestMethod]
        public void Exists_percent() 
        {
            Assert.AreEqual(true, IsThereChar('%', SomeSymbols()));
        }

        [TestMethod]
        public void Exists_equal() 
        {
            Assert.AreEqual(true, IsThereChar('=', SomeSymbols()));
        }

        [TestMethod]
        public void Exists_Underscore() 
        {
            Assert.AreEqual(true, IsThereChar('_', SomeSymbols()));
        }

        [TestMethod]
        public void Exists_LeftBracket() 
        {
            Assert.AreEqual(false, IsThereChar('(', SomeSymbols()));
        }

        [TestMethod]
        public void Exists_LessThan() 
        {
            Assert.AreEqual(false, IsThereChar('<', SomeSymbols()));
        }

        char[] SomeSymbols()
        {
            var symbolsArray = new char[] { '^', '_', '|' }.Concat(CharArray('!', '-').Concat(CharArray(':', '@')).ToArray()).ToArray();
            foreach (char element in new char[] { '{', '}', '[', ']', '(', ')', '/', '\\', '\'', '"', '~', ',', ';', '.', '<', '>' })
                symbolsArray = CharRemove(symbolsArray, element);
            return symbolsArray;
        }

        [TestMethod]
        public void FiveSmallLetters() 
        {
            Assert.AreEqual(5, HowManyChars(SmallLetters(), GeneratePass(8, 1, 1, 1)));
        }

        [TestMethod]
        public void ThreeCapitalLetters() 
        {
            Assert.AreEqual(3, HowManyChars(CapitalLetters(), GeneratePass(8, 3, 1, 1)));
        }

        [TestMethod]
        public void ThreeNumbers() 
        {
            Assert.AreEqual(3, HowManyChars(SomeNumbers(), GeneratePass(8, 1, 3, 1)));
        }

        [TestMethod]
        public void ThreeSymbols() 
        {
            Assert.AreEqual(3, HowManyChars(SomeSymbols(), GeneratePass(8, 1, 1, 3)));
        }

        [TestMethod]
        public void NoSmallLetters() 
        {
            Assert.AreEqual(0, HowManyChars(SmallLetters(), GeneratePass(8, 4, 4, 4)));
        }

        [TestMethod]
        public void NoSymbols() 
        {
            Assert.AreEqual(0, HowManyChars(SomeSymbols(), GeneratePass(8, 4, 4, 4)));
        }

        int HowManyChars(char[] toFind, string thePassword)
        {
            int result = 0;
            for (int i = 0; i < thePassword.Length; i++)
                for (int j = 0; j < toFind.Length; j++)
                    if (thePassword[i] == toFind[j])
                        result++;
            return result;
        }

        string GeneratePass(int passLength, int capitals, int numbers, int symbols)
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
        }
    }
}
