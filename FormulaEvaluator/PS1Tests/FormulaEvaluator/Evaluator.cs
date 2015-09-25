//Gabriel Kenworthy
//CS3500
//Fall 2015
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace FormulaEvaluator
{
    /// <summary>
    /// This is the Evaluator Class
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
        public static int Evaluate(String exp, Lookup l)
        {
            ///<summary>
            ///  This is our initiation of the variable we use to track the current number to find the solution. Also the end result which is why it's named "Last"
            ///</summary>

            String last = "";
            //string[] substrings = Regex.Split(exp, "(\\()|(\\))|(-)|(\\+)|(\\*)|(/)");

            // string[] subvariables = Regex.Split("a b c d e f g h i j k l m n o p q r s t u v w x y z", " ");
            // var errorCounter = Regex.Matches(exp, @"[a-zA-Z]").Count;

            ///<summary>
            /// Create a Regex object to use expression methods to track our expression with less code and hastle. Create two stacks to process the integers/operators
            ///</summary>
            Regex r = new Regex(exp);

            Stack<String> Operators = new Stack<string>();

            Stack<String> Values = new Stack<string>();
            try {

                for (int t = exp.Length - 1; t >= 0; t--)
                {
                    ///<summary>
                    /// iterate through a loop decide which values to push onto a stack.
                    ///</summary>

                    if (!r.IsMatch(exp[t].ToString()) && !((exp[t].Equals("(")) || (exp[t].Equals(")") || (exp[t].Equals("+") || (exp[t].Equals("-") || (exp[t].Equals("/") || (exp[t].Equals("*"))))))))
                    {

                        //division by zero exception
                        try
                        {
                            if (Operators.Count > 0)
                            { ///<summary>
                              /// put a try/catch statement to prevent crashing by dividing by a possible 0;
                              ///</summary>
                                if (Operators.Peek().Equals("*") || Operators.Peek().Equals("/"))
                                {

                                    String eventVal = exp[t].ToString();
                                    String eventVal2 = Values.Pop();
                                    String eventOp = Operators.Pop();


                                    if (Operators.Peek().Equals("*"))
                                    {
                                        ///<summary>
                                        /// Peek at the top of the stack to determine whether or not to multiply or divide based on the character.
                                        ///</summary>
                                        int reslt = 0;
                                        if (int.TryParse(eventVal,out reslt) && (int.TryParse(eventVal2, out reslt)))
                                        {
                                            int newVal = Int32.Parse(eventVal) * Int32.Parse(eventVal2);
                                            String newValString = newVal.ToString();
                                            Values.Push(newValString);
                                        }
                                    
                                    }
                                    else
                                    {
                                        ///<summary>
                                        /// I tried this method below to prevent format errors. Never found out why but it helped me. It allows the code to continue.
                                        ///</summary>
                                        int rsult = 0;
                                        if (int.TryParse(eventVal, out rsult) && (int.TryParse(eventVal2, out rsult)))
                                        {
                                            int newVal = Int32.Parse(eventVal) / Int32.Parse(eventVal2);
                                            String newValString = newVal.ToString();
                                            Values.Push(newValString);
                                        }
                                    }
                                }
                                else
                                {
                                    Values.Push(exp[t].ToString());
                                }
                            }
                        }
                        ///<summary>
                        /// convert the spot at the array to string to push back onto the stack. A char cannot be pushed onto a string array stack, so we convert it. Try to catch an InvalidOperationException in case a character isn't handled right.
                        ///</summary>
                        catch (InvalidOperationException)
                        {
                            Console.WriteLine("InvalidOperationException");

                        }
                    }
              
                    else if (exp[t].Equals("(") || t.Equals("/") || exp[t].Equals("*"))
                    {
                        Operators.Push(exp[t].ToString());
                    }
                    else if (t.Equals("+") || t.Equals("-"))
                    {
                        ///<summary>
                        /// keep track of variable to do basic notation for math.
                        ///</summary>
                        String eventVal = Values.Pop();
                        String eventVal2 = Values.Pop();
                        String eventOp = Operators.Pop();

                        if (Operators.Peek().Equals("+")) 
                        {
                            int rsltt = 0;
                            if (int.TryParse(eventVal, out rsltt) && (int.TryParse(eventVal2, out rsltt)))
                            {
                                int newVal = Int32.Parse(eventVal) + Int32.Parse(eventVal2);
                                String newValString = newVal.ToString();
                                Values.Push(newValString);
                            }
                        }
                        else
                        {
                            int rrslt = 0;
                            if (int.TryParse(eventVal, out rrslt) && (int.TryParse(eventVal2, out rrslt)))
                            {
                                int newVal = Int32.Parse(eventVal) - Int32.Parse(eventVal2);
                                String newValString = newVal.ToString();
                                Values.Push(newValString);
                            }
                        }
                        Operators.Push(exp[t].ToString());
                    }
                    else if (exp[t].Equals(")"))
                    {
                        ///<summary>
                         /// keep track of variable to do basic notation for math.

                        ///</summary>
                        if (Operators.Peek().Equals("+") || Operators.Peek().Equals("-"))
                        {
                            String eventVal = Values.Pop();
                            String eventVal2 = Values.Pop();
                            String eventOp = Operators.Pop();

                         

                                if (Operators.Peek() == "+")
                            {
                                int rsslt = 0;
                                if (int.TryParse(eventVal, out rsslt) && (int.TryParse(eventVal2, out rsslt)))
                                {
                                    int newVal = Int32.Parse(eventVal) + Int32.Parse(eventVal2);
                                    String newValString = newVal.ToString();
                                    Values.Push(newValString);
                                }
                            }
                            else
                            {
                                ///<summary>
                                /// keep track of variable to do basic notation for math.

                                ///</summary>
                                int rsllt = 0;
                                if (int.TryParse(eventVal, out rsllt) && (int.TryParse(eventVal2, out rsllt)))
                                {
                                    int newVal = Int32.Parse(eventVal) - Int32.Parse(eventVal2);
                                    String newValString = newVal.ToString();
                                    Values.Push(newValString);
                                }
                            }
                            Operators.Pop();
                        }
                        else if(Operators.Peek().Equals("*") || Operators.Peek().Equals("/"))
                            {
                            
                            String eventVal = Values.Pop();
                            String eventVal2 = Values.Pop();
                            String eventOp = Operators.Pop();

                            int rslt1 = 0;
                            if (int.TryParse(eventVal, out rslt1) && (int.TryParse(eventVal2, out rslt1)))
                            {
                                if (Operators.Peek() == "*")
                                {
                                    int newVal = Int32.Parse(eventVal) * Int32.Parse(eventVal2);
                                    String newValString = newVal.ToString();
                                    Values.Push(newValString);
                                }
                                else
                                {
                                    int newVal = Int32.Parse(eventVal) / Int32.Parse(eventVal2);
                                    String newValString = newVal.ToString();
                                    Values.Push(newValString);
                                }
                            }
                        }
                        else
                        {
                        
                            //variables
                            if (Operators.Peek().Equals("*") || Operators.Peek().Equals("/"))
                            {

                                String eventVal = exp[t].ToString();
                                String eventVal2 = Values.Pop();
                                String eventOp = Operators.Pop();
                                ///<summary>
                                /// if a variable shows up in the expression rather than an integer or operator, treat it the same but reference through our Lookup method L. The delegate makes this convenient to communicate different methods across classes.
                                ///</summary>
                                int rslt2 = 0;
                                if (int.TryParse(eventVal, out rslt2) && (int.TryParse(eventVal2, out rslt2)))
                                {
                                    if (Operators.Peek().Equals("*"))
                                    {
                                        int newVal = l(eventVal) * l(eventVal2);
                                        String newValString = newVal.ToString();
                                        Values.Push(newValString);
                                    }
                                    else
                                    {
                                        int newVal = l(eventVal) / l(eventVal2);
                                        String newValString = newVal.ToString();
                                        Values.Push(newValString);
                                    }
                                }
                            }
                            else
                            {
                           
                                Values.Push(exp[t].ToString());
                            }
                            if (Operators.Count() == 0)
                            {
                                last = Operators.Pop();
                            }
                            else
                            {
                                String eventVal = Values.Pop();
                                String eventVal2 = Values.Pop();
                                String eventOp = Operators.Pop();

                                int rslt3 = 0;
                                if (int.TryParse(eventVal, out rslt3) && (int.TryParse(eventVal2, out rslt3)))
                                {
                                    if (Operators.Peek().Equals("+"))
                                    {
                                        int newVal = Int32.Parse(eventVal) + Int32.Parse(eventVal2);
                                        String newValString = newVal.ToString();
                                        last = newValString;
                                        Console.WriteLine(last);
                                        ///<summary>
                                        /// set last to the remaining number on the stack.
                                        ///</summary>

                                    }
                                    else
                                    {
                                        int newVal = Int32.Parse(eventVal) - Int32.Parse(eventVal2);
                                        String newValString = newVal.ToString();
                                        last = newValString;
                                        Console.WriteLine(last);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            ///<summary>
            /// throw another dividebyzero catch statment to ensure less crashes.
            ///</summary>
            catch (DivideByZeroException)
            {
                Console.WriteLine("DIVIDE BY ZERO EXCEPTION");

            }

            // Regex r = new Regex("^[abc]+a*$");

            // l is a Lookup variable. l itself is not the function, but it contains
            // a function passed in from the point Evaluate was called.

            // You can call the function l contains by using the same syntax as
            // a function call
            //int result = l("a7");

            // Here is an example of using the extension defined below
            // Notice that myStack serves as the parameter labeled "this" below
            Stack<double> myStack = new Stack<double>();
            myStack.Push(1.6);
            // double x = myStack.StackPeekExtension("a", "b");

            //// Casting a double to an int will truncate it
            ///<summary>
            /// put an if statement to ensure parsing a string to an int will convert successfully.
            ///</summary>
            int rslt = 0;
                if (int.TryParse(last, out rslt))
                    return Int32.Parse(last);
                else
                    return 0;
            

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
