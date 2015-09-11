using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

using FormulaEvaluator;

namespace Tester
{
    /// <summary>
    /// Document your test classes too!
    /// </summary>
    public static class Tester
    {
        /// <summary>
        /// A function used to provide values to variables. 
        /// Throws an ArgumentException if the variable is not mapped to a value.
        /// </summary>
        /// <param name="var">The variable to be looked up.</param>
        /// <returns>An integer value for the variable</returns>
        private static int MyLookupFunction(String var)
        {
            // An arbitrary dictionary of values
            // The var keyword is shorthand for repeating the right-hand type
            var values = new Dictionary<string, int>
            {
                 {"a7", 23}, {"b6", 45}, {"c", 67}, {"d", 89}
            };

            if (!values.ContainsKey(var)) throw new ArgumentException("Variable has no value");
            return values[var];
        }

        /// <summary>
        /// Entry-point for this application.
        /// </summary>
        static void Main(string[] args)
        {
            // Passing in lookup function directly
            //Console.WriteLine(Evaluator.Evaluate("X1", MyLookupFunction));

            // Storing function in variable and passing in the variable
            Evaluator.Lookup l2 = MyLookupFunction;
             // Console.WriteLine(Evaluator.Evaluate("X1", l2));

            // Using lambda expression
           // try {
                Console.WriteLine(Evaluator.Evaluate("1+2", s => 1));

           // Console.WriteLine(Evaluator.Evaluate("", s => 1));
            //}
            //catch(FormatException)
            //{
            //    Console.WriteLine("FormatException");

            //}
            Console.Read();
        }
    }
}