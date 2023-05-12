using org.mariuszgromada.math.mxparser.mathcollection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Z.Expressions;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace Numerical_Analysis
{
    public partial class Form1 : Form
    {
        string[] method = Method.method;
        string selectedMethod = Method.method[0];
        DataTable dataTable;


        public Form1()
        {
            InitializeComponent();
            label3.Text = "Xl";
            label4.Text = "Xu";
            label7.Text = "%";

        }

        // An example function whose
        // solution is determined using
        // Bisection Method. The function
        // is x^3 - x^2 + 2
        static double func(double x)
        {
            return x * x * x -
                   x * x + 2;
        }

        // Prints root of func(x)
        // with error of EPSILON
        private void HideLables()
        {
            label3.Visible = false;
            label4.Visible = false;
            label7.Visible = false;

            textBox2.Visible = false;
            textBox3.Visible = false;
            textBox4.Visible = false;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            HideLables();
            label3.Visible = true;
            label4.Visible = true;
            label7.Visible = true;
            textBox2.Visible = true;
            textBox3.Visible = true;
            textBox4.Visible = true;

            label3.Text = "Xl";
            label4.Text = "Xu";
            label7.Text = "%";

            label1.Text = selectedMethod = method[0];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            HideLables();
            label3.Visible = true;
            label4.Visible = true;
            label7.Visible = true;
            textBox2.Visible = true;
            textBox3.Visible = true;
            textBox4.Visible = true;

            label3.Text = "Xl";
            label4.Text = "Xu";
            label7.Text = "%";

            label1.Text = selectedMethod = method[1];
        }

        private void button3_Click(object sender, EventArgs e)
        {
            HideLables();
            label3.Visible = true;
            label7.Visible = true;
            textBox2.Visible = true;
            textBox4.Visible = true;

            label3.Text = "X0";
            label7.Text = "%";

            label1.Text = selectedMethod = method[2];
        }

        private void button4_Click(object sender, EventArgs e)
        {
            HideLables();
            label3.Visible = true;
            label7.Visible = true;
            textBox2.Visible = true;
            textBox4.Visible = true;

            label3.Text = "X0";
            label7.Text = "%";

            label1.Text = selectedMethod = method[3];
        }

        private void button5_Click(object sender, EventArgs e)
        {
            HideLables();
            label3.Visible = true;
            label4.Visible = true;
            label7.Visible = true;
            textBox2.Visible = true;
            textBox3.Visible = true;
            textBox4.Visible = true;

            label3.Text = "X-1";
            label4.Text = "X0";
            label7.Text = "%";

            label1.Text = selectedMethod = method[4];

        }

        public void calculat_Click(object sender, EventArgs e)
        {
            string equation = "";
            double a = 0;
            double b = 0;
            double epsilon =0;
            try
            {
                 equation = textBox1.Text.ToString();
                 a = double.Parse(textBox2.Text);
                 b = 0;
                if (selectedMethod == method[0] || selectedMethod == method[1])
                {
                    b = double.Parse(textBox3.Text);
                }
                 epsilon = double.Parse(textBox4.Text);
            }
            catch {
                MessageBox.Show("pls Enter valed date");
                return;
            }


            if (selectedMethod == method[0])
                label6.Text = Method.bisection(equation, a, b, epsilon).ToString();
            else if (selectedMethod == method[1])
                label6.Text = Method.false_position(equation, a, b, epsilon).ToString();
            else if (selectedMethod == method[2])
                label6.Text = Method.FixedPointMethod(equation, a, epsilon).ToString();
            else if (selectedMethod == method[3])
                label6.Text = Method.Newton(equation, a, epsilon).ToString();
            else if (selectedMethod == method[4])
                label6.Text = Method.Secant(equation, a, b, epsilon).ToString();

            dataGridView1.DataSource = Method.dataTable;

        }
    }
}
