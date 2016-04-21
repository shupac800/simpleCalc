using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using simpleCalc;

namespace simpleCalcTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void testAddition()
        {
            decimal op1 = 2.1m;
            decimal op2 = 4.4m;
            Assert.AreEqual(op1 + op2, Add.Addition(op1,op2));
        }

        [TestMethod]
        public void testSubtraction()
        {
            decimal op1 = 16.6m;
            decimal op2 = 6.3m;
            Assert.AreEqual(op1 - op2, Sub.Subtraction(op1, op2));
        }
        [TestMethod]
        public void testMultiplication()
        {
            decimal op1 = 2.4m;
            decimal op2 = 5.8m;
            Assert.AreEqual(op1 * op2, Mul.Multiplication(op1, op2));
        }
        [TestMethod]
        public void testDivision()
        {
            decimal op1 = 14.4m;
            decimal op2 = 1.2m;
            Assert.AreEqual(op1 / op2, Div.Division(op1, op2));
        }
        [TestMethod]
        public void testModulus()
        {
            decimal op1 = 121;
            decimal op2 = 100;
            Assert.AreEqual(op1 % op2, Mod.Modulus(op1, op2));
        }
    }
}
