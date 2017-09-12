using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Password
{
        [TestClass]
        public class PasswordTests
        {
                [TestMethod]
                public void CreateCharList() // {{{
                {
                        CollectionAssert.AreEqual(new char[] { 'a', 'b' ,'c' }, CharArray('a', 'c'));
                } // }}}

                [TestMethod]
                public void CreateCharListReversed() // {{{
                {
                        CollectionAssert.AreEqual(new char[] {'c'}, CharArray('c', 'a'));
                } // }}}

                char[] CharArray(char start, char end) // {{{ 
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
                } // }}}
        }
}
