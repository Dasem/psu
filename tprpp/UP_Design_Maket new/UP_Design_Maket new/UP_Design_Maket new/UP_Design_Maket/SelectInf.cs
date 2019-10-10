﻿using System;
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
    public partial class SelectInf : Form
    {
        public SelectInf(string lol)
        {
            InitializeComponent();
            Text = lol;

        }

        private void SelectInf_Load(object sender, EventArgs e)
        {
            switch (Inf.number)
            {
                case 1:
                    richTextBox1.Text = "\tДля ручного создания матрицы в главном окне программы необходимо перейти во вкладку 'Все матрицы'(1), после нажать кнопку 'Создание матрицы'(2).\n\t Далее перейти во вкладку 'Ручное'(3) в поле 'Название'(4) ввести название матрицы, в поле 'Количество столбцов'(5) ввести количество столбцов, в поле 'Количество строк'(6) ввести количество строк, правее в области 'Координаты и значения ненулевых элементов'(7) вводить построчно в первую позицию номер столбца, пробел, во вторую позицию номер строчки, пробел, в третью позицию значение элемента который хотите добавить, перейти на новую строчку и так далее сколько необходимо, остальные элементы заполнятся нулями, после нажать кнопку 'Добавить'(8)";
                    pictureBox1.Image = Properties.Resources._1;
                    pictureBox2.Image = Properties.Resources._1_1;//Image.FromFile("1.1.png");
                    break;
                case 2:
                    richTextBox1.Text = "\tДля автоматического создания в главном окне программы необходимо перейти во вкладку 'Все матрицы'(1), после нажать кнопку 'Создание матрицы'(2).\n\t Далее перейти во вкладку 'Автоматическое'(3) в поле 'Название'(4) ввести название матрицы, в поле 'Количество столбцов'(5) ввести количество столбцов, в поле 'Количество строк'(6) ввести количество строк либо установить галочку в поле случайного выбора размерности(7), также ввести количество ненулевых элементов(8), либо установить галочку в поле случайного выбора количества ненулевых элементов(9), остальные заполнятся нулями после нажать кнопку 'Добавить'(10)";
                    pictureBox1.Image = Properties.Resources._2;//Image.FromFile("2.png");
                    pictureBox2.Image = Properties.Resources._2_1;//Image.FromFile("2.1.png");
                    break;
                case 3:
                    richTextBox1.Text = "\tДля создания матрицы из файла в главном окне программы необходимо перейти во вкладку 'Все матрицы'(1), после нажать кнопку 'Создание матрицы'(2).\n\t Далее перейти во вкладку 'Из файла'(3) в поле 'Название'(4) ввести название матрицы, в поле 'Путь к файлу'(5) ввести путь к файлу хранящемуся в системе, далее  нажать кнопку 'Добавить'(6)";
                    pictureBox1.Image = Properties.Resources._3;// Image.FromFile("3.png");
                    pictureBox2.Image = Properties.Resources._3_1;//Image.FromFile("3.1.png");
                    break;
                case 4:
                    richTextBox1.Text = "\tДля удаления матрицы необходимо в главном окне программы перейти во вкладку 'Все матрицы'(1), после  в выпадающем меню 'Матрица'(2) выбрать матрицу, далее нажать кнопку 'Удалить'(3)";
                    pictureBox1.Image = Properties.Resources._4;//Image.FromFile("4.png");
                    pictureBox2.Image = Properties.Resources._4_1;//Image.FromFile("4.1.png");
                    break;
                case 5:
                    richTextBox1.Text = "\tДля добавления элемента в матрицу необходимо в главном окне программы перейти во вкладку 'Все матрицы'(1), после в выпадающем меню 'Матрица'(2) выбрать матрицу.\n\t Далее вводить координаты новых ненулевых элементов в области 'Добавление/изменение значений элементов'(3) построчно(через пробел) номер столбца, номер строки, значение элемента, перейти на новую строчку и т.д, после чего нажать кнопку 'Добавить/Изменить'(4)";
                    pictureBox1.Image = Properties.Resources._5;//Image.FromFile("5.png");
                    pictureBox2.Image = Properties.Resources._5_1;//Image.FromFile("5.1.png");
                    break;
                case 6:
                    richTextBox1.Text = "\tДля изменения элемента в матрице необходимо в главном окне программы перейти во вкладку 'Все матрицы'(1), после в выпадающем меню 'Матрица'(2) выбрать матрицу.\n\t Далее вводить координаты элементов которые ходите изменить в области 'Добавление/изменение значений элементов'(3) построчно(через пробел) номер столбца, номер строки, значение элемента, перейти на новую строчку и т.д, после чего нажать кнопку 'Добавить/Изменить'(4)";
                    pictureBox1.Image = Properties.Resources._6;//Image.FromFile("6.png");
                    pictureBox2.Image = Properties.Resources._6_1;//Image.FromFile("6.1.png");
                    break;
                case 7:
                    richTextBox1.Text = "\tДля удаления элемента в матрице необходимо в главном окне программы перейти во вкладку 'Все матрицы'(1), после в выпадающем меню 'Матрица'(2) выбрать матрицу.\n\t Далее вводить координаты элементов которые ходите удалить в области 'Добавление/изменение значений элементов'(3) построчно(через пробел) номер столбца, номер строки, 0, перейти на новую строчку и т.д после чего нажать кнопку 'Добавить/Изменить'";
                    pictureBox1.Image = Properties.Resources._7;//Image.FromFile("7.png");
                    pictureBox2.Image = Properties.Resources._7_1; //Image.FromFile("7.1.png");
                    break;
                case 8:
                    richTextBox1.Text = "\tДля умножения матриц необходимо в главном окне программы выбрать вкладку 'Операции'(1).\n\t В выпадающем меню 'Левая матрица'(2) выбрать матрицу, в выпадающем меню 'Правая матрица'(3) выбрать матрицу для который необходимо произвести операцию, в выпадающем меню 'Операция'(4) выбрать умножение, если результат необходимо поместить в существующую матрицу выбрать её в выпадающем списке 'Результат в'(5)(размерность матрицы изменится и будет соответствовать минимальной необходимой размерности для результата) и нажать кнопку 'Ок'(6) справа или же если необходимо создать новую матрицу размерность которой будет задана автоматически с учетом операции и операндов то в поле 'Новую матрицу'(7) ввести название новой матрицы и нажать кнопку 'Ок'(8) справа";
                    pictureBox1.Image = Properties.Resources._8; //Image.FromFile("8.png");
                    break;
                case 9:
                    richTextBox1.Text = "\tДля сложения матриц необходимо в главном окне программы выбрать вкладку 'Операции'(1).\n\t В выпадающем меню 'Левая матрица'(2) выбрать матрицу, в выпадающем меню 'Правая матрица'(3) выбрать матрицу для который необходимо произвести операцию, в выпадающем меню 'Операция'(4) выбрать сложение, если результат необходимо поместить в существующую матрицу выбрать её в выпадающем списке 'Результат в'(5)(размерность матрицы изменится и будет соответствовать минимальной необходимой размерности для результата) и нажать кнопку 'Ок'(6) справа или же если необходимо создать новую матрицу размерность которой будет задана автоматически с учетом операции и операндов то в поле 'Новую матрицу'(7) ввести название новой матрицы и нажать кнопку 'Ок'(8) справа";
                    pictureBox1.Image = Properties.Resources._9; //Image.FromFile("9.png");
                    break;
                case 10:
                    richTextBox1.Text = "\tДля вычитания матриц необходимо в главном окне программы выбрать вкладку 'Операции'(1).\n\t В выпадающем меню 'Левая матрица'(2) выбрать матрицу, в выпадающем меню 'Правая матрица'(3) выбрать матрицу для который необходимо произвести операцию, в выпадающем меню 'Операция'(4) выбрать вычитание, если результат необходимо поместить в существующую матрицу выбрать её в выпадающем списке 'Результат в'(5)(размерность матрицы изменится и будет соответствовать минимальной необходимой размерности для результата) и нажать кнопку 'Ок'(6) справа или же если необходимо создать новую матрицу размерность которой будет задана автоматически с учетом операции и операндов то в поле 'Новую матрицу'(7) ввести название новой матрицы и нажать кнопку 'Ок'(8) справа";
                    pictureBox1.Image = Properties.Resources._10; //Image.FromFile("10.png");
                    break;
                case 11:
                    richTextBox1.Text = "\tДля нахождения обратной матрицы необходимо в главном окне программы выбрать вкладку 'Операции'(1).\n\t В выпадающем меню 'Левая матрица'(2) выбрать матрицу для которой необходимо произвести операцию в выпадающем меню 'Операция'(3) нахождение обратной, если результат необходимо поместить в существующую матрицу выбрать её в выпадающем списке 'Результат в'(4) и нажать кнопку 'Ок'(5) справа или же если необходимо создать новую матрицу размерность которой будет задана автоматически с учетом операции и операндов то в поле 'Новую матрицу'(6) ввести название новой матрицы и нажать кнопку 'Ок'(7) справа";
                    pictureBox1.Image = Properties.Resources._11; //Image.FromFile("11.png");
                    break;
                case 12:
                    richTextBox1.Text = "\tДля вывода матрицы в файл необходимо в главном окне программы выбрать вкладку 'Вывод'(1).\n\t В поле 'Матрица'(2) необходимо выбрать матрицу, которую хотите вывести, после выбрать пункт 'В файл'(3) и указать путь к папке в которую выхотите вывести(4), далее указать то в каком виде вы хотите вывести см.пункты 4.3-4.5.\n\t Нажать кнопку 'Вывести'(5) ";
                    pictureBox1.Image = Properties.Resources._12; //Image.FromFile("12.png");
                    pictureBox2.Image = Properties.Resources._12_1; //Image.FromFile("12.1.png");
                    break;
                case 13:
                    richTextBox1.Text = "\tДля вывода матрицы на экран необходимо в главном окне программы выбрать вкладку 'Вывод'(1).\n\t В поле 'Матрица'(2) необходимо выбрать матрицу, которую хотите вывести, после выбрать пункт 'На экран'(3) далее указать то в каком виде вы хотите вывести см.пункты 4.3-4.5.\n\t Нажать кнопку 'Вывести'(4)";
                    pictureBox1.Image = Properties.Resources._13; //Image.FromFile("13.png");
                    break;
                case 14:
                    richTextBox1.Text = "\tДля вывода матрицы в виде матрицы или двумерного массива необходимо в главном окне программы выбрать вкладку 'Вывод'(1).\n\t В поле 'Матрица'(2) необходимо выбрать матрицу, которую хотите вывести выбрать пункт 'В виде матрицы'(3) далее см. пункты 4.1-4.2.\n\t Нажать кнопку 'Вывести'(4)";
                    pictureBox1.Image = Properties.Resources._14; //Image.FromFile("14.png");
                    break;
                case 15:
                    richTextBox1.Text = "\tДля вывода матрицы с расположением в ОП необходимо в главном окне программы выбрать вкладку 'Вывод'(1).\n\t В поле 'Матрица'(2) необходимо выбрать матрицу, которую хотите вывести выбрать пункт 'С расположением в ОП'(3) далее см. пункты 4.1-4.2.\n\t Нажать кнопку 'Вывести'(4)";
                    pictureBox1.Image = Properties.Resources._15; //Image.FromFile("15.png");
                    break;
                case 16:
                    richTextBox1.Text = "\tДля вывода матрицы в упакованной форме необходимо в главном окне программы выбрать вкладку 'Вывод'(1).\n\t В поле 'Матрица'(2) необходимо выбрать матрицу, которую хотите вывести выбрать пункт 'В упакованной форме'(3) далее см. пункты 4.1-4.2.\n\t Нажать кнопку 'Вывести'(4)";
                    pictureBox1.Image = Properties.Resources._16; //Image.FromFile("16.png");
                    break;
            }


        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
