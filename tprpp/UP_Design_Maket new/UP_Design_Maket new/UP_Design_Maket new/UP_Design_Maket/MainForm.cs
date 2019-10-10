using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Documents;
using System.IO;
using System.Threading;

namespace UP_Design_Maket
{
    public partial class MainForm : Fparent
    {

        int value = 0;
        public MainForm()
        {
            InitializeComponent();
            ComboOperation.SelectedIndex = 0;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (ComboMainFormOutMatrix.SelectedItem.ToString().Split(' ').Length == 3 && ComboMainFormOutMatrix.SelectedItem.ToString().Split(' ')[2] == "(запись)")
            {
                MessageBox.Show("В матрицу ещё производится запись,пожалуйста, подождите", "Ошибка");
                return;
            }
            if (RadioOutFile.Checked && RadioOutMatrix.Checked)
            {
                if (ComboMainFormOutMatrix.SelectedIndex == -1)
                {
                    MessageBox.Show("Матрица не выбрана", "Ошибка");
                    return;
                }
                if (textBox1.Text == null || textBox1.Text == "")
                {
                    MessageBox.Show("Поле \"Путь к файлу\" не заполнено", "Ошибка");
                    return;
                }
                string[] combo_line = ComboMainFormOutMatrix.SelectedItem.ToString().Split(' ');
                foreach (var el in matrix_collection)
                    if (el.name == combo_line[0])
                    {
                        if (el.TxtOutputMatrix(textBox1.Text))
                            MessageBox.Show("Матрица \"" + combo_line[0] + "\" выведена в файл \"" + textBox1.Text + "\"");
                        return;
                    }
            }
            else if (RadioOutMonitor.Checked && RadioOutMatrix.Checked)
            {
                if (ComboMainFormOutMatrix.SelectedIndex == -1)
                {
                    MessageBox.Show("Матрица не выбрана", "Ошибка");
                    return;
                }
                string[] combo_line = ComboMainFormOutMatrix.SelectedItem.ToString().Split(' ');
                foreach (var el in matrix_collection)
                    if (el.name == combo_line[0])
                    {
                        OperProgress prbarform = new OperProgress();
                        prbarform.Show();
                        prbarform.progressBar1.Maximum = el.sizex * el.sizey;
                        Matrix.Outparams forout = new Matrix.Outparams(new ForOutputMatrix(el.name, "matrix"), prbarform);
                        ReadThisMatrix(el);
                        el.count_readers++;
                        threads.Add(new Thread(el.OutputMatrix));
                        Fparent.threads.Last().Start(forout);
                        return;
                    }
            }
            else if (RadioOutMemory.Checked && RadioOutMonitor.Checked)
            {
                if (ComboMainFormOutMatrix.SelectedIndex == -1)
                {
                    MessageBox.Show("Матрица не выбрана", "Ошибка");
                    return;
                }
                string[] combo_line = ComboMainFormOutMatrix.SelectedItem.ToString().Split(' ');
                foreach (var el in matrix_collection)
                    if (el.name == combo_line[0])
                    {
                        OperProgress prbarform = new OperProgress();
                        prbarform.Show();
                        prbarform.progressBar1.Maximum = el.sizex;
                        Matrix.Outparams forout = new Matrix.Outparams(new ForOutputMatrix(el.name,"memory"), prbarform);
                        ReadThisMatrix(el);
                        el.count_readers++;
                        threads.Add(new Thread(el.StructOutput));
                        Fparent.threads.Last().Start(forout);
                        return;
                    }
            }
            else if (RadioOutCompress.Checked && RadioOutMonitor.Checked)
            {
                if (ComboMainFormOutMatrix.SelectedIndex == -1)
                {
                    MessageBox.Show("Матрица не выбрана", "Ошибка");
                    return;
                }
                string[] combo_line = ComboMainFormOutMatrix.SelectedItem.ToString().Split(' ');
                foreach (var el in matrix_collection)
                    if (el.name == combo_line[0])
                    {
                        OperProgress prbarform = new OperProgress();
                        prbarform.Show();
                        prbarform.progressBar1.Maximum = el.sizex;
                        Matrix.Outparams forout = new Matrix.Outparams(new ForOutputMatrix(el.name, "compress"), prbarform);
                        ReadThisMatrix(el);
                        el.count_readers++;
                        threads.Add(new Thread(el.OutputMatrixPacked));
                        Fparent.threads.Last().Start(forout);
                        return;
                    }
            }
            else if (RadioOutCompress.Checked && RadioOutFile.Checked)
            {
                if (ComboMainFormOutMatrix.SelectedIndex == -1)
                {
                    MessageBox.Show("Матрица не выбрана", "Ошибка");
                    return;
                }
                if (textBox1.Text == null || textBox1.Text == "")
                {
                    MessageBox.Show("Поле \"Путь к файлу\" не заполнено", "Ошибка");
                    return;
                }
                string[] combo_line = ComboMainFormOutMatrix.SelectedItem.ToString().Split(' ');
                foreach (var el in matrix_collection)
                    if (el.name == combo_line[0])
                    {
                        if (el.TxtOutputMatrixPacked(textBox1.Text))
                            MessageBox.Show("Матрица \"" + combo_line[0] + "\" выведена в файл \"" + textBox1.Text + "\"");
                        return;
                    }
            }
            else if (RadioOutMemory.Checked && RadioOutFile.Checked)
            {
                if (ComboMainFormOutMatrix.SelectedIndex == -1)
                {
                    MessageBox.Show("Матрица не выбрана", "Ошибка");
                    return;
                }
                if (textBox1.Text == null || textBox1.Text == "")
                {
                    MessageBox.Show("Поле \"Путь к файлу\" не заполнено", "Ошибка");
                    return;
                }
                string[] combo_line = ComboMainFormOutMatrix.SelectedItem.ToString().Split(' ');
                foreach (var el in matrix_collection)
                    if (el.name == combo_line[0])
                    {
                        if (el.TxtOutputMemory(textBox1.Text))
                            MessageBox.Show("Матрица \"" + combo_line[0] + "\" выведена в файл \"" + textBox1.Text + "\"");
                        return;
                    }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CreateMatrix f1 = new CreateMatrix();
            f1.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
                if (ComboMainFormSelectMatrix.SelectedIndex == -1)
            {
                MessageBox.Show("Матрица не выбрана", "Ошибка");
                return;
            }
            string[] combo_line = ComboMainFormSelectMatrix.SelectedItem.ToString().Split(' ');
            if (MessageBox.Show("Удалить матрицу \"" + combo_line[0] + "\"?", "Предупреждение", MessageBoxButtons.YesNo) == DialogResult.No)
                return;
            Matrix temp = null;
            foreach (var el in matrix_collection)
                if (el.name == combo_line[0])
                {
                    temp = el;
                    break;
                }
            if (combo_line.Length == 3)
            {
                MessageBox.Show("Матрица \""+temp.name+"\" находится в обработке, пожалуйста, подождите");
                return;
            }
            names_collection.Remove(temp.name + " (" + temp.sizey.ToString() + "x" + temp.sizex.ToString() + ")");
            matrix_collection.Remove(temp);

            ComboRefresh();

            MessageBox.Show("Матрица \"" + temp.name + "\" удалена");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var fBDialog = new OpenFileDialog();
            fBDialog.Title = "Выберите файл";
            fBDialog.Filter = "Текстовые файлы|*.txt";
            if (fBDialog.ShowDialog() != DialogResult.OK)
                return;
            textBox1.Text = fBDialog.FileName;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioOutFile.Checked == true)
            {
                textBox1.Enabled = true;
                ButtonPath.Enabled = true;
            }
            else
            {
                textBox1.Enabled = false;
                ButtonPath.Enabled = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (ComboMainFormSelectMatrix.SelectedIndex == -1)
            {
                MessageBox.Show("Матрица не выбрана", "Ошибка");
                return;
            }
            string[] combo_line = ComboMainFormSelectMatrix.SelectedItem.ToString().Split(' ');
            if (MessageBox.Show("Изменить матрицу \"" + combo_line[0] + "\"?", "Предупреждение", MessageBoxButtons.YesNo) == DialogResult.No)
                return;
            foreach (var el in matrix_collection)
                if (el.name == combo_line[0])
                {
                    if (el.InputMatrix(richTextBox1))
                    {
                        MessageBox.Show("Изменения в матрице \"" + el.name + "\" внесены");
                        richTextBox1.Clear();
                    }
                    break;
                }
        }

        private void ReadLeft(Matrix mat)
        {
            if (ComboLeftMatrix.SelectedItem.ToString().Split(' ').Length != 3)
                names_collection[ComboLeftMatrix.SelectedIndex] = mat.name + " (" + mat.sizey.ToString() + "x" + mat.sizex.ToString() + ") (чтение)";
            ComboRefresh();
        }

        private void ReadRight(Matrix mat)
        {
            if (ComboRightMatrix.SelectedItem.ToString().Split(' ').Length != 3)
                names_collection[ComboRightMatrix.SelectedIndex] = mat.name + " (" + mat.sizey.ToString() + "x" + mat.sizex.ToString() + ") (чтение)";
            ComboRefresh();
        }

        private void WriteResult(Matrix mat)
        {
            if (ComboResult.SelectedItem.ToString().Split(' ').Length != 3)
                names_collection[ComboResult.SelectedIndex] = mat.name + " (" + mat.sizey.ToString() + "x" + mat.sizex.ToString() + ") (запись)";
            ComboRefresh();
        }

        private bool CheckLeft()
        {
            if (ComboLeftMatrix.SelectedItem.ToString().Split(' ').Length == 3 && ComboLeftMatrix.SelectedItem.ToString().Split(' ')[2] == "(запись)")
            {
                MessageBox.Show("Для левой матрицы ещё не окончены вычисления, пожалуйста, подождите", "Ошибка");
                return false;
            }
            return true;
        }

        private bool CheckRight()
        {
            if (ComboRightMatrix.SelectedItem.ToString().Split(' ').Length == 3 && ComboRightMatrix.SelectedItem.ToString().Split(' ')[2] == "(запись)")
            {
                MessageBox.Show("Для правой матрицы ещё не окончены вычисления, пожалуйста, подождите", "Ошибка");
                return false;
            }
            return true;
        }

        private bool CheckResult()
        {
            if (ComboResult.SelectedItem.ToString().Split(' ').Length == 3)
            {
                MessageBox.Show("Для результирующей матрицы ещё не окончены вычисления, пожалуйста, подождите", "Ошибка");
                return false;
            }
            return true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(!CheckLeft())
                return;
            if(!CheckResult())
                return;
            string rname = "";
            string lname = "";
            string oper = "";
            string resultname = "";
            rname = ComboRightMatrix.SelectedItem.ToString().Split(' ')[0];
            lname = ComboLeftMatrix.SelectedItem.ToString().Split(' ')[0];
            oper = ComboOperation.SelectedItem.ToString();
            resultname = ComboResult.SelectedItem.ToString().Split(' ')[0];
            OperProgress prbar = new OperProgress();
            if (matrix_collection.Count == 0)
            {
                MessageBox.Show("Ни одной матрицы не создано", "Ошибка");
                return;
            }
            if (MessageBox.Show("Перезаписать матрицу \"" + resultname + "\"?", "Предупреждение", MessageBoxButtons.YesNo) == DialogResult.No)
                return;
            Matrix left = null, right = null, result = null;
            foreach (var matrix in matrix_collection)
                if (matrix.name == lname)
                    left = matrix;
            switch (oper)
            {
                case "*":
                    if (!CheckRight())
                        return;
                    foreach (var matrix in matrix_collection)
                        if (matrix.name == rname)
                            right = matrix;
                    if (left.sizex != right.sizey)
                    {
                        MessageBox.Show("Неверные размерности входных матриц", "Ошибка");
                        return;
                    }
                    if (left.name == right.name)
                        left.count_readers++;
                    else
                    {
                        left.count_readers++;
                        right.count_readers++;
                    }

                    result = new Matrix(right.sizex, left.sizey, resultname);

                    WriteResult(result);
                    ReadLeft(left);
                    ReadRight(right);

                    prbar.Text = "Умножение, " + left.name + " * " + right.name + " = " + result.name;
                    prbar.progressBar1.Maximum = result.sizey;
                    prbar.Show();
                    Parms par = new Parms(left, right, result, prbar);
                    threads.Add(new Thread(Matrix.MultMatrix));
                    threads.Last().Start(par);
                    break;
                case "+":
                    if(!CheckRight())
                        return;
                    foreach (var matrix in matrix_collection)
                        if (matrix.name == rname)
                            right = matrix;
                    if (left.sizex != right.sizex || left.sizey != right.sizey)
                    {
                        MessageBox.Show("Неверные размерности входных матриц", "Ошибка");
                        return;
                    }

                    if (left.name == right.name)
                        left.count_readers++;
                    else
                    {
                        left.count_readers++;
                        right.count_readers++;
                    }

                    result = new Matrix(right.sizex, left.sizey, resultname);

                    WriteResult(result);
                    ReadLeft(left);
                    ReadRight(right);

                    prbar.Text = "Сложение матриц, " + left.name + " + " + right.name + " = " + result.name;
                    prbar.progressBar1.Maximum = result.sizex;
                    prbar.Show();
                    Parms par2 = new Parms(left, right, result, prbar);
                    threads.Add(new Thread(Matrix.AddMatrix));
                    threads.Last().Start(par2);
                    break;
                case "-":
                    if(!CheckRight())
                        return;
                    foreach (var matrix in matrix_collection)
                        if (matrix.name == rname)
                            right = matrix;
                    if (left.sizex != right.sizex || left.sizey != right.sizey)
                    {
                        MessageBox.Show("Неверные размерности входных матриц", "Ошибка");
                        return;
                    }

                    if (left.name == right.name)
                        left.count_readers++;
                    else
                    {
                        left.count_readers++;
                        right.count_readers++;
                    }

                    result = new Matrix(right.sizex, left.sizey, resultname);

                    WriteResult(result);
                    ReadLeft(left);
                    ReadRight(right);

                    prbar.Text = "Вычитание матриц, " + left.name + " - " + right.name + " = " + result.name;
                    prbar.progressBar1.Maximum = result.sizex;
                    prbar.Show();
                    Parms par3 = new Parms(left, right, result, prbar);
                    threads.Add(new Thread(Matrix.SubMatrix));
                    threads.Last().Start(par3);
                    break;
                case "нахождение обратной":
                    if (left.sizex != left.sizey)
                    {
                        MessageBox.Show("Обратную матрицу возможно найти только для квадратной");
                        return;
                    }

                    left.count_readers++;

                    result = new Matrix(left.sizex, left.sizey, resultname);

                    WriteResult(result);
                    ReadLeft(left);

                    prbar.Text = "Обратная матрица, от " + left.name + " = " + result.name;
                    prbar.progressBar1.Maximum = result.sizey * result.sizex;
                    prbar.Show();
                    Parms par4 = new Parms(left, right, result, prbar);
                    threads.Add(new Thread(Matrix.ReverseMatrix));
                    threads.Last().Start(par4);
                    while (par4.isDetermNull == 2)
                        Thread.Sleep(100);
                    if (par4.isDetermNull == 1)
                    {
                        //MessageBox.Show("Определитель матрицы равен нулю, невозможно найти обратную", "Ошибка");
                        return;
                    }
                    break;
            }

            string combo_line = resultname;
            Matrix temp = null;
            foreach (var el in matrix_collection)
                if (el.name == combo_line)
                {
                    temp = el;
                    break;
                }
            matrix_collection.Remove(temp);
            matrix_collection.Add(result);
            ComboRefresh();
        }


        private void ComboOperation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ComboOperation.SelectedItem.ToString() == "нахождение обратной")
                ComboRightMatrix.Enabled = false;
            else
                ComboRightMatrix.Enabled = true;
        }
        private void button4_Click_1(object sender, EventArgs e)
        {
            string rname = "";
            string lname = "";
            string oper = "";
            string resultname = "";

            if(!CheckLeft())
                return;

            rname = ComboRightMatrix.SelectedItem.ToString().Split(' ')[0];
            lname = ComboLeftMatrix.SelectedItem.ToString().Split(' ')[0];
            oper = ComboOperation.SelectedItem.ToString();
            resultname = EditOperNewMatrix.Text;
            OperProgress prbar = new OperProgress();
            if (matrix_collection.Count == 0)
            {
                MessageBox.Show("Ни одной матрицы не создано", "Ошибка");
                return;
            }
            if (EditOperNewMatrix.Text == "" || EditOperNewMatrix.Text == null)
            {
                MessageBox.Show("Не задано имя результирующей матрицы");
                return;
            }

            if (EditOperNewMatrix.Text.Contains(' '))
            {
                MessageBox.Show("Название матрицы не должно содержать пробелов", "Ошибка");
                return;
            }

            foreach (var el in matrix_collection)
                if (el.name == EditOperNewMatrix.Text)
                {
                    MessageBox.Show("Матрица с таким названием уже существует", "Ошибка");
                    return;
                }

            Matrix left = null, right = null, result = null;
            foreach (var matrix in matrix_collection)
                if (matrix.name == lname)
                    left = matrix;
            switch (oper)
            {
                case "*":
                    if(!CheckRight())
                        return;
                    foreach (var matrix in matrix_collection)
                        if (matrix.name == rname)
                            right = matrix;
                    if (left.sizex != right.sizey)
                    {
                        MessageBox.Show("Неверные размерности входных матриц", "Ошибка");
                        return;
                    }

                    if (left.name == right.name)
                        left.count_readers++;
                    else
                    {
                        left.count_readers++;
                        right.count_readers++;
                    }

                    ReadLeft(left);
                    ReadRight(right);
                    result = new Matrix(right.sizex, left.sizey, resultname);

                    prbar.Text = "Умножение, " + left.name + " * " + right.name + " = " + result.name;
                    prbar.progressBar1.Maximum = result.sizey;
                    prbar.Show();
                    Parms par = new Parms(left, right, result, prbar);
                    threads.Add(new Thread(Matrix.MultMatrix));
                    threads.Last().Start(par);
                    break;
                case "+":
                    if(!CheckRight())
                        return;
                    foreach (var matrix in matrix_collection)
                        if (matrix.name == rname)
                            right = matrix;
                    if (left.sizex != right.sizex || left.sizey != right.sizey)
                    {
                        MessageBox.Show("Неверные размерности входных матриц", "Ошибка");
                        return;
                    }

                    if (left.name == right.name)
                        left.count_readers++;
                    else
                    {
                        left.count_readers++;
                        right.count_readers++;
                    }

                    ReadLeft(left);
                    ReadRight(right);

                    result = new Matrix(right.sizex, left.sizey, resultname);
                    prbar.Text = "Сложение матриц, " + left.name + " + " + right.name + " = " + result.name;
                    prbar.progressBar1.Maximum = result.sizex;
                    prbar.Show();
                    Parms par2 = new Parms(left, right, result, prbar);
                    threads.Add(new Thread(Matrix.AddMatrix));
                    threads.Last().Start(par2);
                    break;
                case "-":
                    if(!CheckRight())
                        return;
                    foreach (var matrix in matrix_collection)
                        if (matrix.name == rname)
                            right = matrix;
                    if (left.sizex != right.sizex || left.sizey != right.sizey)
                    {
                        MessageBox.Show("Неверные размерности входных матриц", "Ошибка");
                        return;
                    }

                    if (left.name == right.name)
                        left.count_readers++;
                    else
                    {
                        left.count_readers++;
                        right.count_readers++;
                    }

                    ReadLeft(left);
                    ReadRight(right);

                    result = new Matrix(right.sizex, left.sizey, resultname);
                    prbar.Text = "Вычитание матриц, " + left.name + " - " + right.name + " = " + result.name;
                    prbar.progressBar1.Maximum = result.sizex;
                    prbar.Show();
                    Parms par3 = new Parms(left, right, result, prbar);
                    threads.Add(new Thread(Matrix.SubMatrix));
                    threads.Last().Start(par3);
                    break;
                case "нахождение обратной":
                    if (left.sizex != left.sizey)
                    {
                        MessageBox.Show("Обратную матрицу возможно найти только для квадратной");
                        return;
                    }

                    left.count_readers++;

                    ReadLeft(left);

                    result = new Matrix(left.sizex, left.sizey, resultname);
                    prbar.Text = "Обратная матрица, от " + left.name + " = " + result.name;
                    prbar.progressBar1.Maximum = result.sizey * result.sizex;
                    prbar.Show();
                    Parms par4 = new Parms(left, right, result, prbar);
                    threads.Add(new Thread(Matrix.ReverseMatrix));
                    threads.Last().Start(par4);
                    while (par4.isDetermNull == 2)
                        Thread.Sleep(100);
                    if (par4.isDetermNull == 1)
                    {
                        //MessageBox.Show("Определитель матрицы равен нулю, невозможно найти обратную", "Ошибка");
                        return;
                    }
                    break;
            }
            matrix_collection.Add(result);
            names_collection.Add(result.name + " (" + result.sizey.ToString() + "x" + result.sizex.ToString() + ") (запись)");
            ComboRefresh();
        }

        private void справкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form Inf = new Inf();
            Inf.Show();
        }


        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
@"Программа для работа с матрицами ""ПРМ""
Создатели:
Дровосеков Д.А.
Тарарков А.В.
Чернов П.К.
Контактный e-mail: gfhflbuvf46@mail.ru
Апрель, 2018 (с) Все права защищены
v2.9008"
);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (var thr in threads)
                thr.Abort();
            Application.Exit();
        }
        public void InvokeUI(Action a)
        {
            this.BeginInvoke(new MethodInvoker(a));
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            Inf.number = 6;
            new SelectInf("Изменение элемента").Show();
        }
    }



    public class Parms
    {
        public Matrix left;
        public Matrix right;
        public Matrix result;
        public OperProgress prbar;
        public int isDetermNull = 2;

        public Parms(Matrix _left, Matrix _right, Matrix _result, OperProgress _prbar)
        {

            left = _left;
            right = _right;
            result = _result;
            prbar = _prbar;
        }
    }

}
