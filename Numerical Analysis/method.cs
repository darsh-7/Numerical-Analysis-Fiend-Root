using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numerical_Analysis
{
    public class Method
    {
        public static DataTable dataTable;
        public static DataRow myDataRow;

        public static string[] method = { "bisection", "false position", "Simple Fixed", "Newton", "Secant" };
        public static string selectedMethod = "bisection";

        public static double bisection(string equation, double a, double b, double epsilon)
        {
            setupTable(method[0]);
            int i = 0;
            Func<double, double> f = x => EvaluateMethod.EvaluateEquation(equation, x);
            double c = 0, cT = 0;
            while (Math.Abs(b - a) >= epsilon)
            {
                c = (a + b) / 2;
                myDataRow = dataTable.NewRow();
                myDataRow["i"] = i++;
                myDataRow["F(X1)"] = f(a);
                myDataRow["F(X2)"] = f(b);
                myDataRow["F(Xr)"] = f(c);
                myDataRow["X1"] = a;
                myDataRow["X2"] = b;
                myDataRow["Xr"] = c;
                if (i != 1) myDataRow["%"] = (Math.Abs((c - cT) / c)) * 100;
                dataTable.Rows.Add(myDataRow);

                if (f(c) == 0)
                {
                    break;
                }
                else if (f(c) * f(a) < 0)
                {
                    b = c;
                }
                else
                {
                    a = c;
                }
                cT = c;
            }
            c = (a + b) / 2;
            myDataRow = dataTable.NewRow();
            myDataRow["i"] = i++;
            myDataRow["F(X1)"] = f(a);
            myDataRow["F(X2)"] = f(b);
            myDataRow["F(Xr)"] = f(c);
            myDataRow["X1"] = a;
            myDataRow["X2"] = b;
            myDataRow["Xr"] = c;
            if (i != 1) myDataRow["%"] = (Math.Abs((c - cT) / c)) * 100;
            dataTable.Rows.Add(myDataRow);

            return c;
        }
        public static double false_position(string equation, double a, double b, double epsilon)
        {
            setupTable(method[1]);
            int i = 0;
            Func<double, double> f = x => EvaluateMethod.EvaluateEquation(equation, x);
            double c = 0, cT = 0;
            double fc = double.PositiveInfinity;
            while (Math.Abs(fc) >= epsilon)
            {
                c = (a * f(b) - b * f(a)) / (f(b) - f(a));
                fc = f(c);
                myDataRow = dataTable.NewRow();
                myDataRow["i"] = i++;
                myDataRow["F(X1)"] = f(a);
                myDataRow["F(X2)"] = f(b);
                myDataRow["F(Xr)"] = f(c);
                myDataRow["X1"] = a;
                myDataRow["X2"] = b;
                myDataRow["Xr"] = c;
                if (i != 1) myDataRow["%"] = (Math.Abs((c - cT) / c)) * 100;
                dataTable.Rows.Add(myDataRow);

                if (fc == 0)
                {
                    break;
                }
                else if (fc * f(a) < 0)
                {
                    b = c;
                }
                else
                {
                    a = c;
                }
                cT = c;
            }
            return c;
        }
        //public static double Simple_Fixed(string equation, double x0, double epsilon)
        //{
        //    Func<double, double> f = x => EvaluateMethod.EvaluateEquation(equation, x);
        //    double c = x0, cT = 0; ;
        //    double xn1 = f(c);

        //    setupTable(method[2]);
        //    int i = 0;


        //    while (Math.Abs(xn1 - c) >= epsilon)
        //    {
        //        myDataRow = dataTable.NewRow();
        //        myDataRow["i"] = i++;
        //        myDataRow["F(X1)"] = f(c);
        //        myDataRow["X1"] = c;
        //        if (i != 1) myDataRow["%"] = (Math.Abs((c - cT) / c)) * 100;
        //        dataTable.Rows.Add(myDataRow);

        //        c = xn1;
        //        xn1 = f(c);
        //    }
        //    return xn1;
        //}
        public static double FixedPointMethod(string equation, double x0, double tolerance)
        {
            Func<double, double> f =y=> EvaluateMethod.EvaluateEquation(equation, y);
            Func<double, double> fprime = y => EvaluateMethod.EvaluateDerivative(equation, y);

            double x = x0; // initial guess
            double error = double.MaxValue;


            int iteration = 1;
            while (error > tolerance)
            {
                double func = f(x);
                double func_prime = fprime(x);
                double x_new = x - func / func_prime;
                error = Math.Abs(x_new - x);
                x = x_new;

                iteration++;
            }

            return x;
        }
        public static double Newton(string equation, double x0, double epsilon)
        {
            setupTable(method[3]);
            int i = 0;

            Func<double, double> f = x => EvaluateMethod.EvaluateEquation(equation, x);
            Func<double, double> fprime = x => EvaluateMethod.EvaluateDerivative(equation, x);
            double xn = x0 ;
            double xn1 = xn - f(xn) / fprime(xn);
            myDataRow = dataTable.NewRow();
            myDataRow["i"] = i++;
            myDataRow["F(X1)"] = f(xn);
            myDataRow["F`(X1)"] = fprime(xn);
            myDataRow["X1"] = xn;
            dataTable.Rows.Add(myDataRow);

            while (Math.Abs(xn1 - xn) >= epsilon)
            {
                xn = xn1;
                xn1 = xn - f(xn) / fprime(xn);
                myDataRow = dataTable.NewRow();
                myDataRow["i"] = i++;
                myDataRow["F(X1)"] = f(xn);
                myDataRow["F`(X1)"] = fprime(xn);
                myDataRow["X1"] = xn;
                dataTable.Rows.Add(myDataRow);
            }
            myDataRow = dataTable.NewRow();
            myDataRow["i"] = i++;
            myDataRow["%"] = (Math.Abs((xn1 - xn) / xn1)) * 100;
            myDataRow["X1"] = xn1;
            dataTable.Rows.Add(myDataRow);
            return xn1;
        }
        public static double Secant(string equation, double x0, double x1, double epsilon)
        {
            setupTable(method[4]);
            int i = 0;

            Func<double, double> f = x => EvaluateMethod.EvaluateEquation(equation, x);
            double xn = x0;
            double xn1 = x1;
            double xn2 = xn1 - f(xn1) * (xn1 - xn) / (f(xn1) - f(xn));
            while (Math.Abs((xn2 - xn1)/xn2) >= epsilon)
            {
                //myDataRow = dataTable.NewRow();
                //myDataRow["i"] = i++;
                //myDataRow["F(X1)"] = f(xn);
                //myDataRow["F(X2)"] = f(xn1);
                //myDataRow["F(Xr)"] = f(xn2);
                //myDataRow["X1"] = xn;
                //myDataRow["X2"] = xn1;
                //myDataRow["Xr"] = xn2;
                //if (i != 1) myDataRow["%"] = (Math.Abs((xn - xn1) / xn1)) * 100;
                //dataTable.Rows.Add(myDataRow);

                xn = xn1;
                xn1 = xn2;
                xn2 = xn1 - f(xn1) * (xn1 - xn) / (f(xn1) - f(xn));
            }
            return xn2;
        }

        public static void setupTable(string m)
        {
            dataTable = new DataTable();

            DataColumn dtColumn;

            // Create i column
            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(Int32);
            dtColumn.ColumnName = "i";
            dtColumn.ReadOnly = false;
            dtColumn.Unique = true;

            dataTable.Columns.Add(dtColumn);

            // Create X1 column
            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(double);
            dtColumn.ColumnName = "X1";
            dtColumn.ReadOnly = false;
            dtColumn.Unique = false;

            dataTable.Columns.Add(dtColumn);

            // Create X1 column
            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(double);
            dtColumn.ColumnName = "F(X1)";
            dtColumn.ReadOnly = false;
            dtColumn.Unique = false;

            dataTable.Columns.Add(dtColumn);

            if (m == method[0] || m == method[1] || m == method[4])
            {

                // Create X1 column
                dtColumn = new DataColumn();
                dtColumn.DataType = typeof(double);
                dtColumn.ColumnName = "X2";
                dtColumn.ReadOnly = false;
                dtColumn.Unique = false;

                dataTable.Columns.Add(dtColumn);


                // Create X1 column
                dtColumn = new DataColumn();
                dtColumn.DataType = typeof(double);
                dtColumn.ColumnName = "F(X2)";
                dtColumn.ReadOnly = false;
                dtColumn.Unique = false;

                dataTable.Columns.Add(dtColumn);

                // Create X1 column
                dtColumn = new DataColumn();
                dtColumn.DataType = typeof(double);
                dtColumn.ColumnName = "Xr";
                dtColumn.ReadOnly = false;
                dtColumn.Unique = false;

                dataTable.Columns.Add(dtColumn);


                // Create X1 column
                dtColumn = new DataColumn();
                dtColumn.DataType = typeof(double);
                dtColumn.ColumnName = "F(Xr)";
                dtColumn.ReadOnly = false;
                dtColumn.Unique = false;

                dataTable.Columns.Add(dtColumn);

            }else if (m == method[3])
            {
                // Create X1 column
                dtColumn = new DataColumn();
                dtColumn.DataType = typeof(double);
                dtColumn.ColumnName = "F`(X1)";
                dtColumn.ReadOnly = false;
                dtColumn.Unique = false;

                dataTable.Columns.Add(dtColumn);
            }

            // Create X1 column
            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(double);
            dtColumn.ColumnName = "%";
            dtColumn.ReadOnly = false;
            dtColumn.Unique = false;

            dataTable.Columns.Add(dtColumn);


        }


    }
}
