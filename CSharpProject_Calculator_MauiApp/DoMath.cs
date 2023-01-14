using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpProject_Calculator_MauiApp;

    public static class DoMath
    {
        public static double DoCalculation(double number1, double number2, string operatorMath)
        {
            double result = 0;

            switch (operatorMath)
            {
                case "/":
                    result = number1 / number2;
                    break;
                case "-":
                    result = number1 - number2;
                    break;
                case "x":
                    result = number1 * number2;
                    break;
                case "+":
                    result = number1 + number2;
                    break;
            default:
                    break;
            }

        
            return result;
        }
    }
