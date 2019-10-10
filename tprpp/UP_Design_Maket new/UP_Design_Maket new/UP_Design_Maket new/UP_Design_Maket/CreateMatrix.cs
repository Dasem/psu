using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UP_Design_Maket
{
    public partial class CreateMatrix : Fparent
    {

        public CreateMatrix()
        {
            InitializeComponent();
            textBox2.Text = "Новая_Матрица_" + (number_matrix++);
        }

        private void AddFromFile(object sender, EventArgs e)
        {
            textBox4.Text = textBox4.Text.Trim();
            if (textBox3.Text==null || textBox3.Text == "")
            {
                MessageBox.Show("Поле \"Путь к файлу\" не заполнено", "Ошибка");
                return;
            }
            if (textBox4.Text == null || textBox4.Text == "")
            {
                MessageBox.Show("Поле \"Название матрицы\" не заполнено", "Ошибка");
                return;
            }
            if (textBox4.Text.Contains(' '))
            {
                MessageBox.Show("Название матрицы не должно содержать пробелов", "Ошибка");
                return;
            }
            foreach (var el in matrix_collection)
                if(el.name==textBox4.Text)
                {
                    MessageBox.Show("Матрица с таким названием уже существует", "Ошибка");
                    return;
                }
            Matrix temp = new Matrix();
            if (temp.TxtInput(textBox3.Text, textBox4.Text))
            {
                matrix_collection.Add(temp);
                names_collection.Add(temp.name + " (" + temp.sizey.ToString() +"x"+ temp.sizex.ToString()+")");

                ComboRefresh();

                if (MessageBox.Show("Матрица \"" + textBox4.Text + "\" успешно добавлена, вывести на экран?", "Успех", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    OperProgress prbarform = new OperProgress();
                    prbarform.Show();
                    prbarform.progressBar1.Maximum = temp.sizex * temp.sizey;
                    Matrix.Outparams forout = new Matrix.Outparams(new ForOutputMatrix(temp.name, "matrix"), prbarform);
                    ReadThisMatrix(temp);
                    temp.count_readers++;
                    new Thread(temp.OutputMatrix).Start(forout);
                }
                Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var fBDialog = new OpenFileDialog();
            fBDialog.Title = "Выберите файл";
            fBDialog.Filter = "Текстовые файлы|*.txt";
            if (fBDialog.ShowDialog() != DialogResult.OK)
                return;
            textBox3.Text = fBDialog.FileName;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text.Trim();
            if (textBox1.Text == null || textBox1.Text == "")
            {
                MessageBox.Show("Поле \"Название матрицы\" не заполнено", "Ошибка");
                return;
            }
            if (textBox1.Text.Contains(' '))
            {
                MessageBox.Show("Название матрицы не должно содержать пробелов", "Ошибка");
                return;
            }
            foreach (var el in matrix_collection)
                if (el.name == textBox1.Text)
                {
                    MessageBox.Show("Матрица с таким названием уже существует", "Ошибка");
                    return;
                }
            if (numericUpDown1.Value <= 0)
            {
                MessageBox.Show("Количество столбцов задано некорректно", "Ошибка");
                return;
            }
            if (numericUpDown4.Value <= 0)
            {
                MessageBox.Show("Количество строк задано некорректно", "Ошибка");
                return;
            }
            if (richTextBox1.Text == "" || richTextBox1.Text == null)
            {
                MessageBox.Show("Поле для координат и значений элементов не заполнено", "Ошибка");
                return;
            }
            Matrix temp = new Matrix(Convert.ToInt32(numericUpDown1.Value), Convert.ToInt32(numericUpDown4.Value), textBox1.Text);
            if(temp.InputMatrix(richTextBox1))
            {
                matrix_collection.Add(temp);
                names_collection.Add(temp.name + " (" + temp.sizey.ToString() + "x"+temp.sizex.ToString()+")");


                ComboRefresh();
                
                if (MessageBox.Show("Матрица \"" + textBox1.Text + "\" успешно добавлена, вывести на экран?", "Успех", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    OperProgress prbarform = new OperProgress();
                    prbarform.Show();
                    prbarform.progressBar1.Maximum = temp.sizex * temp.sizey;
                    Matrix.Outparams forout = new Matrix.Outparams(new ForOutputMatrix(temp.name, "matrix"), prbarform);
                    ReadThisMatrix(temp);
                    temp.count_readers++;
                    new Thread(temp.OutputMatrix).Start(forout);
                }
                Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Random R = new Random();
            int _sizex, _sizey, _count;
            textBox2.Text = textBox2.Text.Trim();
            if (textBox2.Text == null || textBox2.Text == "")
            {
                MessageBox.Show("Поле \"Название матрицы\" не заполнено", "Ошибка");
                return;
            }
            if (textBox2.Text.Contains(' '))
            {
                MessageBox.Show("Название матрицы не должно содержать пробелов", "Ошибка");
                return;
            }
            foreach (var el in matrix_collection)
                if (el.name == textBox2.Text)
                {
                    MessageBox.Show("Матрица с таким названием уже существует", "Ошибка");
                    return;
                }
            if (checkBox2.Checked == true)
            {
                _sizex = R.Next(5, 56);
                _sizey = R.Next(5, 56);
            }
            else
            {
                if (numericUpDown2.Value <= 0)
                {
                    MessageBox.Show("Размерность матрицы задана некорректно", "Ошибка");
                    return;
                }
                _sizex = Convert.ToInt32(numericUpDown2.Value);
                _sizey = Convert.ToInt32(numericUpDown5.Value);
            }
            if (checkBox1.Checked == true)
            {
                _count = R.Next(1, _sizex * _sizey / 10);
            }
            else
            {
                if(numericUpDown2.Value*numericUpDown5.Value<numericUpDown3.Value)
                {
                    MessageBox.Show("Количество ненулевых элементов больше размера матрицы", "Ошибка");
                    return;
                }
                if (numericUpDown3.Value <= 0)
                {
                    MessageBox.Show("Кол-во ненулевых элементов задано некорректно", "Ошибка");
                    return;
                }
                _count = Convert.ToInt32(numericUpDown3.Value);
                if (_count > _sizex * _sizey / 10)
                    MessageBox.Show("Количество ненулевых элементов слишком велико, производительность операций может быть значительно снижена", "Предупреждение");
            }
            Matrix temp = new Matrix(_sizex, _sizey, textBox2.Text);
            temp.RI(_count);
            matrix_collection.Add(temp);
            names_collection.Add(temp.name + " (" + temp.sizey.ToString() + "x" + temp.sizex.ToString() + ")");

            ComboRefresh();
            if (MessageBox.Show("Матрица \"" + textBox2.Text + "\" успешно добавлена, вывести на экран?", "Успех", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                OperProgress prbarform = new OperProgress();
                prbarform.Show();
                prbarform.progressBar1.Maximum = temp.sizex * temp.sizey;
                Matrix.Outparams forout = new Matrix.Outparams(new ForOutputMatrix(temp.name, "matrix"),prbarform);       
                ReadThisMatrix(temp);
                temp.count_readers++;
                new Thread(temp.OutputMatrix).Start(forout);
            }
            Close();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                numericUpDown2.Enabled = false;
                numericUpDown5.Enabled = false;
            }
            else
            {
                numericUpDown2.Enabled = true;
                numericUpDown5.Enabled = true;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
                numericUpDown3.Enabled = false;
            else
                numericUpDown3.Enabled = true;
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
    }
}
