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
            Assert.AreEqual("abc", CharArray('a', 'c'));
        }

        [TestMethod]
        public void CreateCharListReversed()
        {
            Assert.AreEqual("", CharArray('c', 'a'));
        }

        string CharArray(char start, char end)
        {
            string result = "";
            for (char c = start; c <= end; c++)
                result += c;
            return result;
        }

        [TestMethod]
        public void RemoveFromCharList()
        {
            Assert.AreEqual("ac", CharRemove(CharArray('a', 'c'), "b"));
        }

        string CharRemove(string theArray, string toRemove = "loIO01{}[]()/\\\'\"~,;.<>")
        {
            for (int i = 0; i < toRemove.Length; i++)
                theArray = theArray.Replace(toRemove[i].ToString(), "");
            return theArray;
        }

        bool IsThereChar(char chr, string charArray)
        {
            return (charArray.IndexOf(chr) > -1);
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
            Assert.AreEqual(false, IsThereChar('l', CharRemove(SmallLetters())));
        }

        string SmallLetters()
        {
            return CharArray('a', 'z');
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
            Assert.AreEqual(false, IsThereChar('I', CharRemove(CapitalLetters())));
        }

        string CapitalLetters()
        {
            return CharArray('A', 'Z');
        }

        [TestMethod]
        public void Exists_4() 
        {
            Assert.AreEqual(true, IsThereChar('4', SomeNumbers()));
        }

        [TestMethod]
        public void Exists_0() 
        {
            Assert.AreEqual(false, IsThereChar('0', CharRemove(SomeNumbers())));
        }

        string SomeNumbers()
        {
            return CharArray('0', '9');
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
            Assert.AreEqual(false, IsThereChar('(', CharRemove(SomeSymbols())));
        }

        [TestMethod]
        public void Exists_LessThan() 
        {
            Assert.AreEqual(false, IsThereChar('<', CharRemove(SomeSymbols())));
        }

        string SomeSymbols()
        {
            return String.Concat(CharArray((char)33, (char)47), CharArray((char)58, (char)64), CharArray((char)91, (char)96), CharArray((char)123, (char)127));
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

        int HowManyChars(string toFind, string thePassword)
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
            for (int i = 0; i < passLength; i++)
                passChars = SwapChars(passChars, i, rnd.Next(passLength));
            return new string(passChars);
        }

        [TestMethod]
        public void SwapCharAC() 
        {
            CollectionAssert.AreEqual(new char[]{'C', 'B', 'A'}, SwapChars(new char[]{'A', 'B', 'C'}, 0, 2));
        }

        char[] SwapChars(char[] chars, int a, int b)
        {
            char temp = chars[a];
            chars[a] = chars[b];
            chars[b] = temp;
            return chars;
        }
    }
}
