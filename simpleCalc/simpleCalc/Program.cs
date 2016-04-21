using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace simpleCalc
{
    public class Program
    {
        static void Main(string[] args)
        {
            string[] operations = new string[] { "+", "-", "*", "/", "%" };
            int lineNum = 0;
            decimal result = 0;  // have to initialize decimal to a number
            bool runState = true;
            Dictionary<string, decimal> constants = new Dictionary<string, decimal>();
            List<string> cmdList = new List<string>();

            while (runState)
            {
                Console.Write("[" + lineNum++.ToString() + "]> ");  // display the prompt, increment lineNum
                string inp = formatInp(Console.ReadLine());
                cmdList.Add(inp);  // add input to command history

                // was a command entered?
                switch (inp)
                {
                    case "quit":
                        Console.WriteLine("Bye!!");
                        runState = false;
                        continue;
                    case "exit":
                        Console.WriteLine("Bye!!");
                        runState = false;
                        continue;
                    case "last":
                        Console.WriteLine("   = " + result + "\n");
                        continue;
                    case "lastq":
                        Console.WriteLine(cmdList.Last() + "\n");
                        continue;
                    case "all":
                        // print out list of commands
                        for (int i = 0; i < cmdList.Count; i++)
                        {
                            Console.WriteLine("{0}. {1}", i, cmdList[i]);
                        }
                        Console.Write("Your choice: ");
                        string choice = Console.ReadLine();
                        inp = formatInp(cmdList[Int32.Parse(choice)]);  // should have some error handling here
                        break;
                    default:
                        break;
                }

                // are we referencing a constant only (i.e. to get its value)?
                if (Regex.IsMatch(inp, @"^[a-z]+$"))
                {
                    if (constants.ContainsKey(inp))  // has this constant been defined?
                    {
                        Console.WriteLine("   = " + constants[inp] + "\n");  // if so, display value of this constant
                    }
                    else
                    {
                        Console.WriteLine("Error: {0} is not a defined constant\n", inp);
                    }
                    continue;
                }

                // are we defining a constant?
                if (inp.IndexOf('=') > -1 )
                {
                    string[] defConstant = inp.Split('=');
                    //Console.WriteLine("Left side is {0}, right side is {1}",defConstant[0],defConstant[1]);

                    // determine whether the left and right sides of the equality are numbers or constants
                    decimal n = 0;
                    bool rightSideIsNumeric = decimal.TryParse(defConstant[1], out n);
                    bool leftSideIsNumeric = decimal.TryParse(defConstant[0], out n);
                    if (leftSideIsNumeric && Regex.IsMatch(defConstant[1], @"^[a-z]+$"))
                    {
                        if (constants.ContainsKey(defConstant[1]))
                        {
                            Console.WriteLine("Error: constant {0} has already been defined\n", defConstant[1]);
                            continue;
                        }
                        else
                        {
                            constants.Add(defConstant[1], decimal.Parse(defConstant[0]));
                            Console.WriteLine("Defined {0} as {1}\n", defConstant[1], constants[defConstant[1]]);
                            continue;
                        }
                    }
                    else if (rightSideIsNumeric && Regex.IsMatch(defConstant[0], @"^[a-z]+$"))
                    {
                        if (constants.ContainsKey(defConstant[0]))
                        {
                            Console.WriteLine("Error: constant {0} has already been defined\n", defConstant[0]);
                            continue;
                        }
                        else
                        {
                            constants.Add(defConstant[0], decimal.Parse(defConstant[1]));
                            Console.WriteLine("Defined {0} as {1}\n", defConstant[0], constants[defConstant[0]]);
                            continue;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Error: couldn't parse equality\n");
                        continue;
                    }
                }

                for (int i = 0; i < operations.Length; i++)  // for each type of operator
                {
                    if (inp.IndexOf(operations[i]) > -1)  // check to see if the input string contains this operator
                    {
                        string[] operands = inp.Split(operations[i][0]);   // first character of operations[i] is its only character
                        decimal leftOperand = 0, rightOperand = 0;

                        // check to see if we're using a constant
                        if (Regex.IsMatch(operands[0], @"^[a-z]+$"))  // left side contains a constant?
                        {
                            if (constants.ContainsKey(operands[0]))  // has this constant been defined?
                            {
                                leftOperand = constants[operands[0]];  // if so, assign value from dictionary
                            }
                            else
                            {
                                Console.WriteLine("Error: {0} is not a defined constant\n", operands[0]);
                                continue;
                            }
                        }
                        if (Regex.IsMatch(operands[1], @"^[a-z]+$"))  // right side contains a constant?
                        {
                            if (constants.ContainsKey(operands[1]))  // has this constant been defined?
                            {
                                rightOperand = constants[operands[1]];  // if so, assign value from dictionary
                            }
                            else
                            {
                                Console.WriteLine("Error: {0} is not a defined constant\n", operands[1]);
                                continue;
                            }
                        }

                        if (Regex.IsMatch(operands[0], @"^-?\d*\.?\d*$"))  // left side is numeric?
                        {
                            leftOperand = decimal.Parse(operands[0]);
                        }
                        if (Regex.IsMatch(operands[1], @"^-?\d*\.?\d*$"))  // right side is numeric?
                        {
                            rightOperand = decimal.Parse(operands[1]);
                        }

                        switch (operations[i])
                        {
                            case "+":
                                result = Add.Addition(leftOperand, rightOperand);
                                break;
                            case "-":
                                result = Sub.Subtraction(leftOperand, rightOperand);
                                break;
                            case "*":
                                result = Mul.Multiplication(leftOperand, rightOperand);
                                break;
                            case "/":
                                result = Div.Division(leftOperand, rightOperand);
                                break;
                            case "%":
                                result = Mod.Modulus(leftOperand, rightOperand);
                                break;
                            default:                 // this should never trigger
                                break;
                        }
                        Console.WriteLine("   = " + result + "\n");
                        continue;
                    }
                }
            }
        }

        public static string formatInp(string inp)
        {
            // convert to lowercase
            inp = inp.ToLower();

            // strip out spaces
            inp = Regex.Replace(inp, " ", "");

            return inp;
        }

    }
}
