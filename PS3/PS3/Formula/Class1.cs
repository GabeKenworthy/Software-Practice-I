// Skeleton written by Joe Zachary for CS 3500, September 2013
// Read the entire skeleton carefully and completely before you
// do anything else!

// Version 1.1 (9/22/13 11:45 a.m.)

// Change log:
//  (Version 1.1) Repaired mistake in GetTokens
//  (Version 1.1) Changed specification of second constructor to
//                clarify description of how validation works

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SpreadsheetUtilities
{
    /// <summary>
    /// Represents formulas written in standard infix notation using standard precedence
    /// rules.  The allowed symbols are non-negative numbers written using double-precision 
    /// floating-point syntax; variables that consist of a letter or underscore followed by 
    /// zero or more letters, underscores, or digits; parentheses; and the four operator 
    /// symbols +, -, *, and /.  
    /// 
    /// Spaces are significant only insofar that they delimit tokens.  For example, "xy" is
    /// a single variable, "x y" consists of two variables "x" and y; "x23" is a single variable; 
    /// and "x 23" consists of a variable "x" and a number "23".
    /// 
    /// Associated with every formula are two delegates:  a normalizer and a validator.  The
    /// normalizer is used to convert variables into a canonical form, and the validator is used
    /// to add extra restrictions on the validity of a variable (beyond the standard requirement 
    /// that it consist of a letter or underscore followed by zero or more letters, underscores,
    /// or digits.)  Their use is described in detail in the constructor and method comments.
    /// </summary>
    public class Formula
    {
        private string[] str
        {
            get;
            set;
        }
        /// <summary>
        /// Creates a Formula from a string that consists of an infix expression written as
        /// described in the class comment.  If the expression is syntactically invalid,
        /// throws a FormulaFormatException with an explanatory Message.
        ///
        /// 
        /// The associated normalizer is the identity function, and the associated validator
        /// maps every string to true.  
        /// </summary>
        public Formula(String formula) :
        this(formula, s => s, s => true)
        {
        }
        //http://stackoverflow.com/questions/14659287/regex-for-english-characters-hyphen-and-underscore
        private bool checkVariable(string s)
        {
            // "*" means zero or more
            // "+ means 1 or more"
            //starts with (a-z or A-z) but can end with a number, letter, or underscore
            return Regex.IsMatch(s, @"^[A-Za-z_]+[A-Za-z\d_]*$", RegexOptions.IgnorePatternWhitespace);
        }




        /// <summary>
        /// Creates a Formula from a string that consists of an infix expression written as
        /// described in the class comment.  If the expression is syntactically incorrect,
        /// throws a FormulaFormatException with an explanatory Message.
        /// 
        /// The associated normalizer and validator are the second and third parameters,
        /// respectively.  
        /// 
        /// If the formula contains a variable v such that normalize(v) is not a legal variable, 
        /// throws a FormulaFormatException with an explanatory message. 
        /// 
        /// If the formula contains a variable v such that isValid(normalize(v)) is false,
        /// throws a FormulaFormatException with an explanatory message.
        /// 
        /// Suppose that N is a method that converts all the letters in a string to upper case, and
        /// that V is a method that returns true only if a string consists of one letter followed
        /// by one digit.  Then:
        /// 
        /// new Formula("x2+y3", N, V) should succeed
        /// new Formula("x+y3", N, V) should throw an exception, since V(N("x")) is false
        /// new Formula("2x+y3", N, V) should throw an exception, since "2x+y3" is syntactically incorrect.
        /// </summary>
        /// 
        public Formula(String formula, Func<string, string> normalize, Func<string, bool> isValid)
        {

            str = GetTokens(formula).ToArray<string>();
            if (str.Length == 0)
            {
                throw new FormulaFormatException("Formula cannot be empty.");
            }
            double d;
            if (!double.TryParse(str[0], out d) && (str[0] != "(") && (checkVariable(str[0]) == false))
            {
                throw new FormulaFormatException("Formula must start with left parenthesis, number, or variable");
            }
            int countLeft = 0;
            int countRight = 0;
            for (int i = 0; i < str.Length; i++)
            {
                string token = str[i];
                if (token == "(")
                {
                    countLeft++;
                }
                if (token == ")")
                {
                    countRight++;
                }
                if (countRight > countLeft)
                {
                    throw new FormulaFormatException("Missing left paranthesis");
                }
                if (token == "(" || token == "+" || token == "/" || token == "*" || token == "-")
                {
                    if (i<str.Length-1&&(str[i + 1] != "(") && (checkVariable(str[i + 1]) == false) && (!double.TryParse(str[i + 1], out d)))
                    {
                        throw new FormulaFormatException("A number, left parenthesis, or variable must follow a left parenthesis or operator.");
                    }
                }
                else if (token == ")" || double.TryParse(token, out d) || checkVariable(token))
                {
                    if (i < str.Length - 1 && (str[i + 1] != ")" && str[i + 1] != "+" && str[i + 1] != "-" && str[i + 1] != "/" && str[i + 1] != "*"))
                    {
                        throw new FormulaFormatException("A right parenthesis, or operator must follow a variable,number or right parenthesis");
                    }
                }
                else
                {
                    throw new FormulaFormatException("Illegal token entered.");
                }
                //truncates and eliminates extra zeroes in double
                if (double.TryParse(token, out d))
                {
                    str[i] = d.ToString();
                }
                if (checkVariable(token) && !isValid(token))
                {
                    throw new FormulaFormatException("Entered variables are not valid.");
                }
            }
            if (countLeft != countRight)
            {
                throw new FormulaFormatException("Parenthesis to not match up");
            }
            if ((str[str.Length - 1] != ")") && !double.TryParse(str[str.Length - 1], out d) && !checkVariable(str[str.Length - 1]))
            {
                throw new FormulaFormatException("Last Token is not a variable, right parenthesis, or number");
            }
        }


        /// <summary>
        /// Evaluates this Formula, using the lookup delegate to determine the values of
        /// variables.  When a variable symbol v needs to be determined, it should be looked up
        /// via lookup(normalize(v)). (Here, normalize is the normalizer that was passed to 
        /// the constructor.)
        /// 
        /// For example, if L("x") is 2, L("X") is 4, and N is a method that converts all the letters 
        /// in a string to upper case:
        /// 
        /// new Formula("x+7", N, s => true).Evaluate(L) is 11
        /// new Formula("x+7").Evaluate(L) is 9
        /// 
        /// Given a variable symbol as its parameter, lookup returns the variable's value 
        /// (if it has one) or throws an ArgumentException (otherwise).
        /// 
        /// If no undefined variables or divisions by zero are encountered when evaluating 
        /// this Formula, the value is returned.  Otherwise, a FormulaError is returned.  
        /// The Reason property of the FormulaError should have a meaningful explanation.
        ///
        /// This method should never throw an exception.
        /// </summary>
        public object Evaluate(Func<string, double> lookup)
        {
            Stack<String> Operators = new Stack<string>();

            Stack<double> Values = new Stack<double>();
            double d = 0;
            foreach (string s in str)
            {

                if (double.TryParse(s, out d))
                {
                    if (Operators.Count > 0 && (Operators.Peek() == "*" || Operators.Peek() == "/"))
                    {
                        if (Operators.Peek() == "*")
                        {
                            double x1 = d * Values.Pop();
                            Values.Push(x1);
                        }
                        else if (Operators.Peek() == "/")
                        {
                            if (d != 0)
                            {
                                double x1 = Values.Pop() / d;
                                Values.Push(x1);
                            }
                            else
                            {
                                return new FormulaError("divide by zero error");
                            }
                        }
                        Operators.Pop();
                    }
                    else
                    {
                        Values.Push(d);
                    }
                }
                else if (checkVariable(s))
                {

                    try
                    {

                        d = lookup(s);
                    }
                    catch
                    {
                        return new FormulaError("Variable not found");
                    }
                    if (Operators.Count > 0 && (Operators.Peek() == "*" || Operators.Peek() == "/"))
                    {
                        if (Operators.Peek() == "*")
                        {
                            double x1 = d * Values.Pop();
                            Values.Push(x1);
                        }
                        else if (Operators.Peek() == "/")
                        {
                            if (d != 0)
                            {
                                double x1 = Values.Pop() / d;
                                Values.Push(x1);
                            }
                            else
                            {
                                return new FormulaError("divide by zero error");
                            }
                        }
                        Operators.Pop();
                    }
                    else
                        Values.Push(d);
                }
                else if ( s == "+" || s == "-")
                {
                    if(Operators.Count > 0 && (Operators.Peek() == "+" || Operators.Peek() == "-"))
                    {
                        if(Operators.Peek() == "+")
                        {
                            double x1 = Values.Pop() + Values.Pop();
                            Values.Push(x1);

                        }
                        if (Operators.Peek() == "-")
                        {
                            double x1 = Values.Pop() - Values.Pop();
                            
                            x1 *= -1;
                            Values.Push(x1);

                        }
                        Operators.Pop();
                        Operators.Push(s);

                    }

                    else
                    {
                        Operators.Push(s);
                    }
                    


                    
                }
                if(s == "*"|| s == "/"|| s == "(")
                {
                    Operators.Push(s);

                }
                if(s == ")")
                {
                    if (Operators.Count > 0 && (Operators.Peek() == "+" || Operators.Peek() == "-"))
                    {
                        if (Operators.Peek() == "+")
                        {
                            double x1 = Values.Pop() + Values.Pop();
                            Values.Push(x1);

                        }
                        if (Operators.Peek() == "-")
                        {
                            double x1 = Values.Pop() - Values.Pop();

                            x1 *= -1;
                            Values.Push(x1);

                        }
                        Operators.Pop();

                    }
                    Operators.Pop();
                    if (Operators.Count > 0 && (Operators.Peek() == "*" || Operators.Peek() == "/"))
                    {
                        d = Values.Pop();
                        if (Operators.Peek() == "*")
                        {
                            double x1 = d * Values.Pop();
                            Values.Push(x1);
                        }
                        else if (Operators.Peek() == "/")
                        {
                            if (d != 0)
                            {
                                double x1 = Values.Pop() / d;
                                Values.Push(x1);
                            }
                            else
                            {
                                return new FormulaError("divide by zero error");
                            }
                        }
                        Operators.Pop();
                    }

                }
            }
            if (Operators.Count > 0)
            {
                if (Operators.Peek() == "+")
                {
                    double x1 = Values.Pop() + Values.Pop();
                    Values.Push(x1);

                }
                if (Operators.Peek() == "-")
                {
                    double x1 = Values.Pop() - Values.Pop();

                    x1 *= -1;
                    Values.Push(x1);

                }
                
            }
            return Values.Pop();
        }


        /// <summary>
        /// Enumerates the normalized versions of all of the variables that occur in this 
        /// formula.  No normalization may appear more than once in the enumeration, even 
        /// if it appears more than once in this Formula.
        /// 
        /// For example, if N is a method that converts all the letters in a string to upper case:
        /// 
        /// new Formula("x+y*z", N, s => true).GetVariables() should enumerate "X", "Y", and "Z"
        /// new Formula("x+X*z", N, s => true).GetVariables() should enumerate "X" and "Z".
        /// new Formula("x+X*z").GetVariables() should enumerate "x", "X", and "z".
        /// </summary>
        public IEnumerable<String> GetVariables()
        {
            //hashsets won't let you put in same variable twice, reasonable dataset.
            HashSet<string> v = new HashSet<string>();
            foreach (string token in str)
            {
                if (checkVariable(token))
                {
                    v.Add(token);
                }
            }
            return v;
        }

        /// <summary>
        /// Returns a string containing no spaces which, if passed to the Formula
        /// constructor, will produce a Formula f such that this.Equals(f).  All of the
        /// variables in the string should be normalized.
        /// 
        /// For example, if N is a method that converts all the letters in a string to upper case:
        /// 
        /// new Formula("x + y", N, s => true).ToString() should return "X+Y"
        /// new Formula("x + Y").ToString() should return "x+Y"
        /// </summary>
        public override string ToString()
        {
            //join() goes through inenumerable str and adds empty strings inbetween each value, then returns it as a string.
            return string.Join(string.Empty, str);
        }

        /// <summary>
        /// If obj is null or obj is not a Formula, returns false.  Otherwise, reports
        /// whether or not this Formula and obj are equal.
        /// 
        /// Two Formulae are considered equal if they consist of the same tokens in the
        /// same order.  To determine token equality, all tokens are compared as strings 
        /// except for numeric tokens, which are compared as doubles, and variable tokens,
        /// whose normalized forms are compared as strings.
        /// 
        /// For example, if N is a method that converts all the letters in a string to upper case:
        ///  
        /// new Formula("x1+y2", N, s => true).Equals(new Formula("X1  +  Y2")) is true
        /// new Formula("x1+y2").Equals(new Formula("X1+Y2")) is false
        /// new Formula("x1+y2").Equals(new Formula("y2+x1")) is false
        /// new Formula("2.0 + x7").Equals(new Formula("2.000 + x7")) is true
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof(Formula))
            {
                return false;
            }
            if (obj == null)
            {
                return false;
            }
            //checking to see if the current string is equal to the object string.
            return ToString().Equals(obj.ToString());
        }

        /// <summary>
        /// Reports whether f1 == f2, using the notion of equality from the Equals method.
        /// Note that if both f1 and f2 are null, this method should return true.  If one is
        /// null and one is not, this method should return false.
        /// </summary>
        public static bool operator ==(Formula f1, Formula f2)
        {
            //casting into objects so it can use "==" and to prevent recursion.
            if ((object)f1 == null && (object)f2 == null)
            {
                return true;
            }
            else if ((object)f1 == null || (object)f2 == null)
            {
                return false;
            }
            return f1.Equals(f2);
        }

        /// <summary>
        /// Reports whether f1 != f2, using the notion of equality from the Equals method.
        /// Note that if both f1 and f2 are null, this method should return false.  If one is
        /// null and one is not, this method should return true.
        /// </summary>
        public static bool operator !=(Formula f1, Formula f2)
        {
            //calls double equals method, and makes it not equal.
            return !(f1 == f2);
        }

        /// <summary>
        /// Returns a hash code for this Formula.  If f1.Equals(f2), then it must be the
        /// case that f1.GetHashCode() == f2.GetHashCode().  Ideally, the probability that two 
        /// randomly-generated unequal Formulae have the same hash code should be extremely small.
        /// </summary>
        public override int GetHashCode()
        {
            //takes string of formula and hashes it. Hashcodes will be the same if tostrings are the same.
            return ToString().GetHashCode();
        }

        /// <summary>
        /// Given an expression, enumerates the tokens that compose it.  Tokens are left paren;
        /// right paren; one of the four operator symbols; a string consisting of a letter or underscore
        /// followed by zero or more letters, digits, or underscores; a double literal; and anything that doesn't
        /// match one of those patterns.  There are no empty tokens, and no token contains white space.
        /// </summary>
        private static IEnumerable<string> GetTokens(String formula)
        {
            // Patterns for individual tokens
            String lpPattern = @"\(";
            String rpPattern = @"\)";
            String opPattern = @"[\+\-*/]";
            String varPattern = @"[a-zA-Z_](?: [a-zA-Z_]|\d)*";
            String doublePattern = @"(?: \d+\.\d* | \d*\.\d+ | \d+ ) (?: [eE][\+-]?\d+)?";
            String spacePattern = @"\s+";

            // Overall pattern
            String pattern = String.Format("({0}) | ({1}) | ({2}) | ({3}) | ({4}) | ({5})",
            lpPattern, rpPattern, opPattern, varPattern, doublePattern, spacePattern);

            // Enumerate matching tokens that don't consist solely of white space.
            foreach (String s in Regex.Split(formula, pattern, RegexOptions.IgnorePatternWhitespace))
            {
                if (!Regex.IsMatch(s, @"^\s*$", RegexOptions.Singleline))
                {
                    yield return s;
                }
            }
        }
    }

    /// <summary>
    /// Used to report syntactic errors in the argument to the Formula constructor.
    /// </summary>
    public class FormulaFormatException :
      Exception
    {
        /// <summary>
        /// Constructs a FormulaFormatException containing the explanatory message.
        /// </summary>
        public FormulaFormatException(String message)
    :
          base(message)
        {
        }
    }

    /// <summary>
    /// Used as a possible return value of the Formula.Evaluate method.
    /// </summary>
    public struct FormulaError
    {
        /// <summary>
        /// Constructs a FormulaError containing the explanatory reason.
        /// </summary>
        /// <param name="reason"></param>
        public FormulaError(String reason)
    :
          this()
        {
            Reason = reason;
        }

        /// <summary>
        ///  The reason why this FormulaError was created.
        /// </summary>
        public string Reason
        {
            get;
            private set;
        }
    }
}
