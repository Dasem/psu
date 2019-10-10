using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UP_Design_Maket
{
    public partial class ForOutputMatrix : Form
    {
        string inpmatname="";
        public ForOutputMatrix(string _inpmatname, string type_of_out)
        {
            InitializeComponent();
            richTextBox1.Clear();
            richTextBox1.MouseUp -= RichTextBox1_MouseUp;
            if (type_of_out == "matrix")
                richTextBox1.MouseUp += RichTextBox1_MouseUp;
            inpmatname = _inpmatname;
            Text = "Название матрицы: " + inpmatname;
        }

        private void RichTextBox1_MouseUp(object sender, MouseEventArgs e)
        {
            Text ="Название матрицы: "+inpmatname + ", X: " +(((richTextBox1.SelectionStart - richTextBox1.GetFirstCharIndexOfCurrentLine())/12)+1)+ ", Y: " + (richTextBox1.GetLineFromCharIndex(richTextBox1.SelectionStart) / 6 + 1);
        }

    }
}
