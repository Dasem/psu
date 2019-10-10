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

    public partial class Inf : Form
    {
        public static int number;
        TreeNode Inform1 = null;
        TreeNode Inform2 = null;
        TreeNode Inform3 = null;
        TreeNode Inform4 = null;
        public Inf()
        {
            InitializeComponent();
            Inform1 = new TreeNode("1.Создание/удаление массива");
            treeView1.Nodes.Add(Inform1);
            Inform2 = new TreeNode("2.Редактирование массива");
            treeView1.Nodes.Add(Inform2);
            Inform3 = new TreeNode("3.Операции над массивами");
            treeView1.Nodes.Add(Inform3);
            Inform4 = new TreeNode("4.Вывод массива");
            treeView1.Nodes.Add(Inform4);

            TreeNode a = Inform1.Nodes.Add("1.Ручное создание");
            TreeNode b = Inform1.Nodes.Add("2.Автоматическое создание");
            TreeNode c = Inform1.Nodes.Add("3.Создание из файла");
            TreeNode d = Inform1.Nodes.Add("4.Удаление");

            TreeNode e = Inform2.Nodes.Add("1.Добавление элементов");
            TreeNode f = Inform2.Nodes.Add("2.Изменение элементов");
            TreeNode g = Inform2.Nodes.Add("3.Удаление элементов");

            TreeNode a2 = Inform3.Nodes.Add("1.Умножение");
            TreeNode b2 = Inform3.Nodes.Add("2.Сложение");
            TreeNode c2 = Inform3.Nodes.Add("3.Вычитание");
            TreeNode d2 = Inform3.Nodes.Add("4.Нахождение обратной");

            TreeNode a3 = Inform4.Nodes.Add("1.В файл");
            TreeNode b3 = Inform4.Nodes.Add("2.На экран");
            TreeNode c3 = Inform4.Nodes.Add("3.В виде матрицы");
            TreeNode d3 = Inform4.Nodes.Add("4.С расположением в ОП");
            TreeNode e3 = Inform4.Nodes.Add("5.В упакованной форме");


        }



        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {

        }

        private void Inf_Load(object sender, EventArgs e)
        {

        }

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            switch (e.Node.Text)
            {
                case "1.Ручное создание":
                    number = 1;
                    break;
                case "2.Автоматическое создание":
                    number = 2;
                    break;
                case "3.Создание из файла":
                    number = 3;
                    break;
                case "4.Удаление":
                    number = 4;
                    break;
                case "1.Добавление элементов":
                    number = 5;
                    break;
                case "2.Изменение элементов":
                    number = 6;
                    break;
                case "3.Удаление элементов":
                    number = 7;
                    break;
                case "1.Умножение":
                    number = 8;
                    break;
                case "2.Сложение":
                    number = 9;
                    break;
                case "3.Вычитание":
                    number = 10;
                    break;
                case "4.Нахождение обратной":
                    number = 11;
                    break;
                case "1.В файл":
                    number = 12;
                    break;
                case "2.На экран":
                    number = 13;
                    break;
                case "3.В виде матрицы":
                    number = 14;
                    break;
                case "4.С расположением в ОП":
                    number = 15;
                    break;
                case "5.В упакованной форме":
                    number = 16;
                    break;
                default: return;
            }
            SelectInf kek = new SelectInf(e.Node.Text);
            kek.Show();
            while (ActiveForm != kek)
                kek.Activate();
        }
    }
}
