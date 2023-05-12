using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace Numerical_Analysis
{
    class EvaluateMethod
    {

        public static double EvaluateDerivative(string equation, double x)
        {
            double h = 0.0001;
            return (EvaluateEquation(equation, x + h) - EvaluateEquation(equation, x - h)) / (2 * h);
        }

        //public static double EvaluateEquation(string equation, double x)
        //{
        //    // Create a new DataTable
        //    DataTable table = new DataTable();

        //    // Replace the variable 'x' in the equation with the provided value
        //    equation = equation.Replace("x", x.ToString());

        //    // Compute the result of the equation using the DataTable
        //    object result = table.Compute(equation, "");

        //    // Convert the result to a double and return it
        //    return Convert.ToDouble(result);
        //}
        public static double EvaluateEquation(string expression, double x)
        {
            // Replace the pi constant with its value
            expression = expression.Replace("pi", Math.PI.ToString());

            // Replace the e constant with its value
            expression = expression.Replace("e", Math.E.ToString());

            // Replace the x variable with its value
            expression = expression.Replace("x", x.ToString());

            // Use a regular expression to match and replace exponentiation
            expression = Regex.Replace(expression, @"(\d+(\.\d+)?)\s*\^\s*(\d+(\.\d+)?)", match =>
            {
                double a = double.Parse(match.Groups[1].Value);
                double b = double.Parse(match.Groups[3].Value);
                return Math.Pow(a, b).ToString();
            });

            // Evaluate the expression using the DataTable class
            DataTable table = new DataTable();
            object result = table.Compute(expression, "");
            return Math.Round( Convert.ToDouble(result),3);
        }

    }
}
