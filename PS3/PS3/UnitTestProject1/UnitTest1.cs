﻿using SpreadsheetUtilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text.RegularExpressions;



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

        [TestMethod()]
        public void Test15()
        {
            Assert.AreEqual(26.0, new Formula("2+3*(3+5)").Evaluate(s => 0));


        }

        [TestMethod()]
        public void Test16()
        {
            Assert.AreEqual(194.0, new Formula("2+3*5+(3+4*8)*5+2").Evaluate(s => 0));

        }

        [TestMethod()]
        public void Test17()
        {
            Assert.AreEqual(new Formula("5/0").Evaluate(s => 0).GetType(),typeof(FormulaError));
        }

        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void Test18()
        {
            new Formula("+").Evaluate( s => 0);
    
        }

        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void Test19()
        {
            new Formula("2+5+").Evaluate(s => 0);

        }

        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void Test20()
        {
            new Formula("2+5*7)").Evaluate(s => 0);

        }

        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void Test21()
        {
            new Formula("xx",s=>s,s=> Regex.IsMatch(s, @"^[A-Za-z]+[\d]+$")).Evaluate(s => 0);

        }

        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void Test22()
        {
            new Formula("5+xx",s=>s,s => Regex.IsMatch(s, @"^[A-Za-z]+[\d]+$")).Evaluate(s => 0);

        }

        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void Test23()
        {
            new Formula("5+7+(5)8").Evaluate(s => 0);

        }

        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void Test24()
        {
            new Formula("").Evaluate(s => 0);

        }

        [TestMethod()]
        public void Test25()
        {
            Assert.AreEqual(-12.0, new Formula("y1*3-8/2+4*(8-9*2)/2*x7").Evaluate(s => (s == "x7") ? 1 : 4));

        }

        [TestMethod()]
        public void Test26()
        {
            Assert.AreEqual(6.0, new Formula("x1+(x2+(x3+(x4+(x5+x6))))").Evaluate(s => 1));

        }

        [TestMethod()]
        public void Test27()
        {
            Assert.AreEqual(12.0, new Formula("((((x1+x2)+x3)+x4)+x5)+x6").Evaluate(s => 2));

        }

        [TestMethod()]
        public void Test28()
        {
            Assert.AreEqual(0.0, new Formula("a4-a4*a4/a4").Evaluate(s => 3));

        }
        [TestMethod()]
        public void myTest1()
        {
            Formula f = new Formula("a4-a4*a4/a4");
            Assert.IsFalse(f.GetVariables().Equals(0));
               
        }
        [TestMethod()]
        public void myTest2()
        {
            Formula f = new Formula("a4-a4*a4/a4");
            Assert.IsTrue(f.ToString().Contains("a"));


        }
        [TestMethod()]
        public void myTest3()
        {
           

            Assert.IsTrue(new Formula("2.0 + x7").Equals(new Formula("2.000 + x7")));


        }
        [TestMethod()]
        public void myTest4()
        {
            Assert.IsFalse(new Formula("2.0 + x7").GetHashCode().Equals(0));


        }
  

    }
}
