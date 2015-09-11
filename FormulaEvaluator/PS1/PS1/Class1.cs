using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace FormulaEvaluator
{
    /// <summary>
    /// Wow! A class header! Documentation is great!
    /// </summary>
    public static class Evaluator
    {
        // Declaring a delegate is declaring a new "type" of variable
        // Lookup variables will contain methods that have String input and int output
        public delegate int Lookup(String var);

        /// <summary>
        /// This is the stub for an Evaluate method that doesn't actually
        /// evaluate anything. Neat!
        /// </summary>
        /// <param name="exp">The expression to be evaluated.</param>
        /// <param name="l">A function used to lookup variable values.</param>
        /// <returns></returns>
        public static int Evaluate(Dictionary<string, int> d, String exp, Lookup l)
        {
            // string[] substrings = Regex.Split(exp, "(\\()|(\\))|(-)|(\\+)|(\\*)|(/)");
            
            string[] subvariables = exp.Split("a b c d e f g h i j k l m n o p q r s t u v w x y z", " ");
            /*
                        Regex objects are used to define a pattern that strings can match
             Basic Regex symbols:
             [] Define a set of items
             *  Repeat 0 or more
             +  Repeat 1 or more
             ^  Start input
             $  End input
             */

            Regex r = new Regex(exp);

            Stack<String> Operators = new Stack<string>();

            Stack<String> Values = new Stack<string>();
            try {

                for (int t = substrings.Length - 1; t >= 0; t--)
                {
                    String last = "";

                    if (!subvariables.Contains(t) && !((exp[t].Equals("(")) || (exp[t].Equals(")") || (exp[t].Equals("+") || (exp[t].Equals("-") || (exp[t].Equals("/") || (exp[t].Equals("*")))
                    {

                        //division by zero exception
                        if (Operators.Peek() == ("*" || "/"))
                        {

                            String eventVal = t;
                            String eventVal2 = Values.Pop();
                            String eventOp = Operators.Pop();


                            if (Operators.Peek().Equals("*"))
                            {
                                int newVal = ToInt32(eventVal) * ToInt32(eventVal2);
                                String newValString = toString(newVal);
                                Values.Push(newValString);
                            }
                            else
                            {
                                int newVal = ToInt32(eventVal) / ToInt32(eventVal2);
                                String newValString = toString(newVal);
                                Values.Push(newValString);
                            }
                        }
                        else
                        {
                            Values.Push(t);
                        }
                    }
                    else if ((exp[t].Equals("(") || (t.Equals("/") || (exp[t].Equals("*"))
                    {
                        Operators.Push(exp[t]);
                    }
                    else if ((t.Equals("+") || (t.Equals("-"))
                    {
                        String eventVal = Values.Pop();
                        String eventVal2 = Values.Pop();
                        String eventOp = Operators.Pop();

                        if (Operators.Peek().Equals("+")) 
                        {
                            int newVal = ToInt32(eventVal) + ToInt32(eventVal2);
                            String newValString = toString(newVal);
                            Values.Push(newValString);
                        }
                        else
                        {
                            int newVal = ToInt32(eventVal) - ToInt32(eventVal2);
                            String newValString = toString(newVal);
                            Values.Push(newValString);
                        }
                        Operators.Push(t);
                    }
                    else if (exp[t].Equals(")"))
                    {
                        if (Operators.Peek() == ("+" || "-"))
                        {
                            String eventVal = Values.Pop();
                            String eventVal2 = Values.Pop();
                            String eventOp = Operators.Pop();

                            if (Operators.Peek() == "+")
                            {
                                int newVal = ToInt32(eventVal) + ToInt32(eventVal2);
                                String newValString = toString(newVal);
                                Values.Push(newValString);
                            }
                            else
                            {
                                int newVal = ToInt32(eventVal) - ToInt32(eventVal2);
                                String newValString = toString(newVal);
                                Values.Push(newValString);
                            }
                            Operators.Pop();
                        }
                        else if (Operators.Peek() == ("*" || "/"))
                        {
                            String eventVal = Values.Pop();
                            String eventVal2 = Values.Pop();
                            String eventOp = Operators.Pop();

                            if (Operators.Peek() == "*")
                            {
                                int newVal = ToInt32(eventVal) * ToInt32(eventVal2);
                                String newValString = toString(newVal);
                                Values.Push(newValString);
                            }
                            else
                            {
                                int newVal = ToInt32(eventVal) / ToInt32(eventVal2);
                                String newValString = toString(newVal);
                                Values.Push(newValString);
                            }
                        }
                        else
                        {
                            //variables
                            if (Operators.Peek() == ("*" || "/"))
                            {

                                String eventVal = t;
                                String eventVal2 = Values.Pop();
                                String eventOp = Operators.Pop();


                                if (tempOp == "*")
                                {
                                    int newVal = ToInt32(l(eventVal)) * ToInt32(l(eventVal2));
                                    String newValString = toString(newVal);
                                    Values.Push(newValString);
                                }
                                else
                                {
                                    int newVal = ToInt32(l(eventVal)) / ToInt32(l(eventVal2));
                                    String newValString = toString(newVal);
                                    Values.Push(newValString);
                                }
                            }
                            else
                            {
                                Values.Push(t);
                            }
                            if (Operators.Count() == 0)
                            {
                                String last = Operators.Pop();
                            }
                            else
                            {
                                String eventVal = Values.Pop();
                                String eventVal2 = Values.Pop();
                                String eventOp = Operators.Pop();


                                if (tempOp == "+")
                                {
                                    int newVal = ToInt32(eventVal) + ToInt32(eventVal2);
                                    String newValString = toString(newVal);
                                    last = newValString;
                                    Console.WriteLine(last);

                                }
                                else
                                {
                                    int newVal = ToInt32(eventVal) - ToInt32(eventVal2);
                                    String newValString = toString(newVal);
                                    last = newValString;
                                    Console.WriteLine(last);
                                }
                            }
                        }
                    }
                }
            }
            catch(DivideByZeroException e)
            {
                Console.WriteLine("DIVIDE BY ZERO EXCEPTION");

            }

            // Regex r = new Regex("^[abc]+a*$");





            // l is a Lookup variable. l itself is not the function, but it contains
            // a function passed in from the point Evaluate was called.

            // You can call the function l contains by using the same syntax as
            // a function call
            int result = l("a7");

            // Here is an example of using the extension defined below
            // Notice that myStack serves as the parameter labeled "this" below
            Stack<double> myStack = new Stack<double>();
            myStack.Push(1.6);
            double x = myStack.StackPeekExtension("a", "b");

            //// Casting a double to an int will truncate it
            return (int)x;
        }

        /// <summary>
        /// Example of an extension method. Notice how the first parameter is
        /// labeled "this", but you can provide other parameters too.
        /// </summary>
        /// <param name="stack"></param>
        /// <param name="op1">Ignored option</param>
        /// <param name="op2">Ignored option</param>
        /// <returns></returns>
        public static double StackPeekExtension(this Stack<double> stack, string op1, string op2)
        {
            // I don't like that Stacks throw exceptions if I peek when they are empty.
            // Why don't I make my own Peek method that returns NaN instead?
            // (Note: That would probably be a bad idea!)
            if (stack.Count > 0) return stack.Peek();
            return double.NaN;
        }
    }
}
