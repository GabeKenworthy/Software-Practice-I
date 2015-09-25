﻿using SpreadsheetUtilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace PS1GradingTests
{


    /// <summary>
    ///This is a test class for EvaluatorTest and is intended
    ///to contain all EvaluatorTest Unit Tests
    ///</summary>
    [TestClass()]
    public class EvaluatorTest
    {

        [TestMethod()]
        public void Test1()
        {
            Assert.AreEqual(5.0, new Formula("5").Evaluate(s => 0));
        }

        [TestMethod()]
        public void Test2()
        {
            Assert.AreEqual(13.0, new Formula("X5").Evaluate(s => 13));
        }

        [TestMethod()]
        public void Test3()
        {
            Assert.AreEqual(8.0, new Formula("5+3").Evaluate(s => 0));
        }

        [TestMethod()]
        public void Test4()
        {
            Assert.AreEqual(8.0, new Formula("18 - 10").Evaluate(s => 0));

        }

        [TestMethod()]
        public void Test5()
        {
            Assert.AreEqual(8.0, new Formula("2*4").Evaluate(s => 0));


        }

        [TestMethod()]
        public void Test6()
        {
            Assert.AreEqual(8.0, new Formula("16/2").Evaluate(s => 0));


        }

        [TestMethod()]
        public void Test7()
        {
            Assert.AreEqual(6.0, new Formula("2+X1").Evaluate(s => 4));

        }

        [TestMethod()]
        public void Test8()
        {
            Assert.AreEqual(new Formula("2+X1").Evaluate(s => { throw new ArgumentException("Unknown variable"); }).GetType(), typeof(FormulaError));

        }

        [TestMethod()]
        public void Test9()
        {
            Assert.AreEqual(15.0, new Formula("2*6+3").Evaluate(s => 0));

        }

        [TestMethod()]
        public void Test10()
        {
            Assert.AreEqual(20.0, new Formula("2+6*3").Evaluate(s => 0));

        }

        [TestMethod()]
        public void Test11()
        {
            Assert.AreEqual(24.0, new Formula("(2+6)*3").Evaluate(s => 0));

        }

        [TestMethod()]
        public void Test12()
        {
            Assert.AreEqual(16.0, new Formula("2*(3+5)").Evaluate(s => 0));

        }

        [TestMethod()]
        public void Test13()
        {
            Assert.AreEqual(10.0, new Formula("2+(3+5)").Evaluate(s => 0));

        }

        [TestMethod()]
        public void Test14()
        {
            Assert.AreEqual(50.0, new Formula("2+(3+5*9)").Evaluate(s => 0));

        }

        //[TestMethod()]
        //public void Test15()
        //{
        //    Assert.AreEqual(26, Evaluator.Evaluate("2+3*(3+5)", s => 0));
        //}

        //[TestMethod()]
        //public void Test16()
        //{
        //    Assert.AreEqual(194.0, Evaluator.Evaluate("2+3*5+(3+4*8)*5+2", s => 0));
        //}

        //[TestMethod()]
        //[ExpectedException(typeof(ArgumentException))]
        //public void Test17()
        //{
        //    Evaluator.Evaluate("5/0", s => 0);
        //}

        //[TestMethod()]
        //[ExpectedException(typeof(ArgumentException))]
        //public void Test18()
        //{
        //    Evaluator.Evaluate("+", s => 0);
        //}

        //[TestMethod()]
        //[ExpectedException(typeof(ArgumentException))]
        //public void Test19()
        //{
        //    Evaluator.Evaluate("2+5+", s => 0);
        //}

        //[TestMethod()]
        //[ExpectedException(typeof(ArgumentException))]
        //public void Test20()
        //{
        //    Evaluator.Evaluate("2+5*7)", s => 0);
        //}

        //[TestMethod()]
        //[ExpectedException(typeof(ArgumentException))]
        //public void Test21()
        //{
        //    Evaluator.Evaluate("xx", s => 0);
        //}

        //[TestMethod()]
        //[ExpectedException(typeof(ArgumentException))]
        //public void Test22()
        //{
        //    Evaluator.Evaluate("5+xx", s => 0);
        //}

        //[TestMethod()]
        //[ExpectedException(typeof(ArgumentException))]
        //public void Test23()
        //{
        //    Evaluator.Evaluate("5+7+(5)8", s => 0);
        //}

        //[TestMethod()]
        //[ExpectedException(typeof(ArgumentException))]
        //public void Test24()
        //{
        //    Evaluator.Evaluate("", s => 0);
        //}

        //[TestMethod()]
        //public void Test25()
        //{
        //    Assert.AreEqual(-12.0, Evaluator.Evaluate("y1*3-8/2+4*(8-9*2)/2*x7", s => (s == "x7") ? 1 : 4));
        //}

        //[TestMethod()]
        //public void Test26()
        //{
        //    Assert.AreEqual(6.0, Evaluator.Evaluate("x1+(x2+(x3+(x4+(x5+x6))))", s => 1));
        //}

        //[TestMethod()]
        //public void Test27()
        //{
        //    Assert.AreEqual(12.0, Evaluator.Evaluate("((((x1+x2)+x3)+x4)+x5)+x6", s => 2));
        //}

        //[TestMethod()]
        //public void Test28()
        //{
        //    Assert.AreEqual(0.0, Evaluator.Evaluate("a4-a4*a4/a4", s => 3));
        //}

    }
}
