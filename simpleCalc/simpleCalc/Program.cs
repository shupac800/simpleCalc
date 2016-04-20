using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simpleCalc
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] operations = new string[] { "+", "-", "*", "/", "%" };
            int lineNum = 0;
            double result = 0;  // have to initialize double to a number
            bool runState = true;
            string last_inp = null;

            while(runState)
            {
                string prompt = "[" + lineNum++.ToString() + "]> ";
                Console.Write(prompt);
                string inp = Console.ReadLine();
                // was a command entered?
                switch (inp)
                {
                    case "quit":
                        runState = false;
                        continue;
                    case "exit":
                        runState = false;
                        continue;
                    case "last":
                        Console.WriteLine("   = " + result);
                        continue;
                    case "lastq":
                        Console.WriteLine(last_inp);
                        continue;
                    default:
                        break;
                }
                last_inp = inp;

                for (int i = 0; i < operations.Length; i++)  // for each type of operator
                {
                    if (inp.IndexOf(operations[i]) > -1)  // check to see if the input string contains this operator
                    {
                        string[] operands = inp.Split(operations[i][0]);   // first character of operations[i] is its only character
                        double leftOperand = float.Parse(operands[0]);
                        double rightOperand = float.Parse(operands[1]);
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
                            default:
                                break;
                        }
                        Console.WriteLine("   = " + result);
                        i = operations.Length;  // no need to check for presence of other operators, so quit looping
                    }
                }
            }
        }
    }
}
