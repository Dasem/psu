using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace UP_Design_Maket
{



    public partial class Fparent : Form
    {
        public void ReadThisMatrix(Matrix mat)
        {
            try
            {
                names_collection[names_collection.IndexOf(mat.name + " (" + mat.sizey.ToString() + "x" + mat.sizex.ToString() + ")")] = mat.name + " (" + mat.sizey.ToString() + "x" + mat.sizex.ToString() + ") (чтение)";
                ComboRefresh();
            }
            catch (Exception) { }
        }

        public static int number_matrix = 1;
        public static List<Matrix> matrix_collection = new List<Matrix>();

        public static List<string> names_collection = new List<string>();
        public static List<string> names_collection_create = new List<string>();
        public static List<string> names_collection_left = new List<string>();
        public static List<string> names_collection_right = new List<string>();
        public static List<string> names_collection_result = new List<string>();
        public static List<string> names_collection_out = new List<string>();

        public static List<Thread> threads = new List<Thread>();

        public void ComboRefresh()
        {
            Program.fr2.ComboMainFormSelectMatrix.DataSource = null;
            CopyList(names_collection, names_collection_create);
            Program.fr2.ComboMainFormSelectMatrix.DataSource = names_collection_create;

            Program.fr2.ComboMainFormOutMatrix.DataSource = null;
            CopyList(names_collection, names_collection_out);
            Program.fr2.ComboMainFormOutMatrix.DataSource = names_collection_out;

            Program.fr2.ComboLeftMatrix.DataSource = null;
            CopyList(names_collection, names_collection_left);
            Program.fr2.ComboLeftMatrix.DataSource = names_collection_left;

            Program.fr2.ComboRightMatrix.DataSource = null;
            CopyList(names_collection, names_collection_right);
            Program.fr2.ComboRightMatrix.DataSource = names_collection_right;

            Program.fr2.ComboResult.DataSource = null;
            CopyList(names_collection, names_collection_result);
            Program.fr2.ComboResult.DataSource = names_collection_result;
        }

        public Fparent()
        {
            InitializeComponent();
        }

        public void CopyList(List<string> l1, List<string> l2)
        {
            l2.Clear();
            foreach (var el in l1)
                l2.Add(el);
        }

    }



    public class Link
    {
        public int line_number;
        public int inf;
        public Link next = null;

        public void Insert(int y, int inf)
        {
            Link a, b;
            a = this;
            b = this.next;
            bool F = true;
            while (b != null && F)
            {
                if (b.line_number < y)
                {//Перемещение указателей к месту вставки
                    b = b.next;
                    a = a.next;
                }
                else F = false;
            }
            if (b != null && b.line_number == y)
                if (inf != 0)
                {
                    b.inf = inf;
                }
                else
                {
                    a.next = b.next;
                }
            else
                if (inf != 0)
            {
                Link temp = new Link();
                temp.inf = inf;//Задание начальных значений вставляемому элементу
                temp.line_number = y;
                a.next = temp;//Вставка нового элемента
                temp.next = b;
            }
        }

    }


    public class Matrix
    {
        public string name;
        public Link[] columns;
        public int sizex;
        public int sizey;
        public int count_readers = 0;

        public Matrix(int _sizex, int _sizey, string _name)
        {
            this.name = _name;
            this.sizex = _sizex;
            this.sizey = _sizey;
            this.columns = new Link[this.sizex];
            for (int i = 0; i < sizex; i++)
            {
                this.columns[i] = new Link();
                this.columns[i].line_number = -1;
                this.columns[i].next = null;
            }
        }
        public Matrix()
        {

        }

        public bool OutOfRange(int x, int y)
        {
            if (x > this.sizex || y > this.sizey)
                return true;
            else return false;
        }
        public bool InputMatrix(RichTextBox inp)
        {
            int string_num = 0;
            string[] separator = { " ", "\t" };
            try
            {
                int x, y, value;
                string curstring;
                string[] masstring;
                foreach (var line in inp.Lines)
                {
                    string_num++;
                    curstring = line.Trim();
                    if (curstring != "")
                    {
                        masstring = curstring.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                        x = Convert.ToInt32(masstring[0]);
                        y = Convert.ToInt32(masstring[1]);
                        if (this.OutOfRange(x, y))
                        {
                            MessageBox.Show("Указаны некорректные индексы (строка: " + Convert.ToString(string_num) + ")", "Ошибка");
                            return false;
                        }
                        value = Convert.ToInt32(masstring[2]);
                        this.columns[x - 1].Insert(y - 1, value);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("В поле для координат и значений элементов введены некорректные данные (строка: " + Convert.ToString(string_num) + ")", "Ошибка");
                return false;
            }
        }

        private string TabSpaces(int number)
        {
            string ret = number + "";
            for (int i = 0; i < 12 - number.ToString().Length; ++i)
                ret += " ";
            return ret;
        }

        public void OutputMatrix(Object _Outparams)
        {
            Outparams outpar = (Outparams)_Outparams;
            RichTextBox ForOut = outpar.ForOutForm.richTextBox1;
            OperProgress prbarform = outpar.prbarform;
            Link temp;
            int count = 0;
            for (int i = 0; i < this.sizey; ++i)
            {
                for (int j = 0; j < this.sizex; ++j)
                {
                    if (!prbarform.IsDisposed)
                    {
                        if (!prbarform.IsDisposed)
                            prbarform.InvokeUI(() => { prbarform.progressBar1.Value = count; });
                        count++;
                    }
                    temp = this.columns[j];
                    bool F = true;
                    if (temp.next == null)//Если столбец пустой
                        ForOut.AppendText(TabSpaces(0));
                    else
                    {
                        while (temp.next != null && F)
                            if (temp.next.line_number == i)
                            {
                                ForOut.AppendText(TabSpaces(temp.next.inf));
                                F = false;
                            }
                            else
                                temp = temp.next;
                        if (F)
                            ForOut.AppendText(TabSpaces(0));
                    }
                }
                ForOut.AppendText("\n\n\n\n\n\n");
            }
            if (!prbarform.IsDisposed)
                prbarform.InvokeUI(() => { prbarform.Close(); });
            if (--this.count_readers == 0)
            {
                Fparent.names_collection[Fparent.names_collection.IndexOf(this.name + " (" + this.sizey.ToString() + "x" + this.sizex.ToString() + ") (чтение)")] = this.name + " (" + this.sizey.ToString() + "x" + this.sizex.ToString() + ")";
                Program.fr2.InvokeUI(() => { Program.fr2.ComboRefresh(); });
            }
            outpar.ForOutForm.ShowDialog();
        }
        public bool TxtInput(string direction, string _name)
        {
            if (!System.IO.File.Exists(direction))
            {
                MessageBox.Show("Путь к файу указан не верно", "Ошибка");
                return false;
            }
            if (Path.GetExtension(direction) != ".txt")
            {
                MessageBox.Show("Неверное расширение файла", "Ошибка");
                return false;
            }
            int string_num = 1;
            string[] separator = { " ", "\t" };
            try
            {
                using (var inp = new StreamReader(direction))
                {
                    int x, y, value;
                    string[] curstring;
                    string[] masstring;
                    curstring = inp.ReadLine().Trim().Split();
                    this.sizex = Convert.ToInt32(curstring[0]);
                    this.sizey = Convert.ToInt32(curstring[1]);
                    this.name = _name;
                    this.columns = new Link[this.sizex];
                    for (int i = 0; i < sizex; i++)
                    {
                        this.columns[i] = new Link();
                        this.columns[i].line_number = -1;
                        this.columns[i].next = null;
                    }
                    string new_el;
                    while (!inp.EndOfStream)
                    {
                        string_num++;
                        new_el = inp.ReadLine().Trim();
                        if (new_el != "")
                        {
                            masstring = new_el.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                            x = Convert.ToInt32(masstring[0]);
                            y = Convert.ToInt32(masstring[1]);
                            if (this.OutOfRange(x, y))
                            {
                                MessageBox.Show("В файле указаны некорректные индексы (строка: " + Convert.ToString(string_num) + ")", "Ошибка");
                                return false;
                            }
                            value = Convert.ToInt32(masstring[2]);
                            columns[x - 1].Insert(y - 1, value);
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Файл содержит некорректные данные (строка: " + Convert.ToString(string_num) + ")", "Ошибка");
                return false;
            }
        }
        public bool TxtOutputMatrixPacked(string path)
        {
            if (Path.GetExtension(path) != ".txt")
            {
                MessageBox.Show("Неверное расширение файла", "Ошибка");
                return false;
            }
            try
            {
                using (var outp = new StreamWriter(path))
                {
                    Link temp;
                    outp.WriteLine(this.sizex + " " + this.sizey);
                    for (int j = 0; j < sizex; ++j)
                    {
                        temp = columns[j];
                        if (!(temp.next == null))
                        {
                            while (temp.next != null)
                            {
                                outp.Write(j + 1 + " ");
                                outp.Write(temp.next.line_number + 1 + " ");
                                outp.Write(temp.next.inf);
                                temp = temp.next;
                                outp.WriteLine();
                            }
                        }
                    }
                    outp.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Неверно задан путь к файлу", "Ошибка");
                return false;
            }
        }
        public bool TxtOutputMemory(string path)
        {
            if (Path.GetExtension(path) != ".txt")
            {
                MessageBox.Show("Неверное расширение файла", "Ошибка");
                return false;
            }
            try
            {
                using (var outp = new StreamWriter(path))
                {
                    outp.WriteLine("Вывод расположения элементов в памяти матрицы " + this.name);
                    Link temp;
                    int memory_size = 0;
                    for (int j = 0; j < this.sizex; ++j)
                    {
                        temp = this.columns[j];
                        while (temp.next != null)
                        {
                            memory_size += 12;
                            var safeptr = SafePtr.Create(temp.next);
                            outp.Write("Элемент " + temp.next.inf + " с координатами X:" + (j + 1) + " Y:" + (1 + temp.next.line_number) + " находится по адресу " + safeptr.IntPtr.ToString("x"));
                            outp.WriteLine();
                            temp = temp.next;
                        }
                    }
                    outp.WriteLine("\nОбщий объём памяти, занимаемый матрицой: " + memory_size / 1024.0 + " Кбайт");
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Неверно задан путь к файлу", "Ошибка");
                return false;
            }
        }
        public void OutputMatrixPacked(Object _Outparams)
        {
            Outparams outpar = (Outparams)_Outparams;
            RichTextBox outp = outpar.ForOutForm.richTextBox1;
            OperProgress prbarform = outpar.prbarform;
            Link temp;
            int count = 0;
            outp.AppendText("Размерность матрицы, X:" + this.sizex.ToString() + ", Y:" + this.sizey.ToString() + "\n(X, Y, Значение)\n");
            for (int j = 0; j < sizex; ++j)
            {
                if (!prbarform.IsDisposed)
                {
                    prbarform.InvokeUI(() => { prbarform.progressBar1.Value = count; });
                    count++;
                }
                temp = columns[j];
                if (!(temp.next == null))
                {
                    while (temp.next != null)
                    {
                        outp.AppendText(j + 1 + " ");
                        outp.AppendText(temp.next.line_number + 1 + " ");
                        outp.AppendText(temp.next.inf.ToString() + "\n");
                        temp = temp.next;
                    }
                }
            }
            if (!prbarform.IsDisposed)
                prbarform.InvokeUI(() => { prbarform.Close(); });
            if (--this.count_readers == 0)
            {
                Fparent.names_collection[Fparent.names_collection.IndexOf(this.name + " (" + this.sizey.ToString() + "x" + this.sizex.ToString() + ") (чтение)")] = this.name + " (" + this.sizey.ToString() + "x" + this.sizex.ToString() + ")";
                Program.fr2.InvokeUI(() => { Program.fr2.ComboRefresh(); });
            }
            outpar.ForOutForm.ShowDialog();
        }

        public void RI(int count)
        {
            Random rnd = new Random();
            int x = rnd.Next(0, this.sizex);
            int y = rnd.Next(0, this.sizey);
            OperProgress progr = new OperProgress();
            progr.Text = "Создание матрицы \"" + this.name + "\"";
            progr.progressBar1.Maximum = count;
            progr.Show();
            for (int i = 0; i < count; i++)
            {
                progr.progressBar1.Value = i;
                while (NotZero(x, y))
                {
                    x = rnd.Next(0, this.sizex);
                    y = rnd.Next(0, this.sizey);
                }
                this.columns[x].Insert(y, rnd.Next(-50, 51));
            }
            progr.Close();
        }

        public int Exist(int j, int i, ref Link current_pointer)
        {
            Link temp;
            if (current_pointer != null)
                temp = current_pointer;
            else
                temp = this.columns[j];
            while (temp != null && temp.line_number < i)
                temp = temp.next;
            if (temp != null && temp.line_number == i)
            {
                current_pointer = temp;
                return temp.inf;
            }
            else
                return 0;
        }

        public int Exist(int j, int i)
        {
            Link temp;
            temp = this.columns[j];
            while (temp != null && temp.line_number < i)
                temp = temp.next;
            if (temp != null && temp.line_number == i)
                return temp.inf;
            else
                return 0;
        }

        public bool NotZero(int j, int i)
        {
            Link temp = this.columns[j];
            while (temp != null && temp.line_number < i)
                temp = temp.next;
            if (temp != null && temp.line_number == i)
                return true;
            else
                return false;
        }


        public bool TxtOutputMatrix(string path)
        {
            if (Path.GetExtension(path) != ".txt")
            {
                MessageBox.Show("Неверное расширение файла", "Ошибка");
                return false;
            }
            try
            {
                using (var outp = new StreamWriter(path))
                {
                    Link temp;
                    for (int i = 0; i < this.sizey; ++i)
                    {
                        for (int j = 0; j < this.sizex; ++j)
                        {
                            temp = columns[j];
                            bool F = true;
                            if (temp.next == null)//Если столбец пустой
                                outp.Write("0\t");
                            else
                            {
                                while (temp.next != null && F)
                                    if (temp.next.line_number == i)
                                    {
                                        outp.Write(temp.next.inf + "\t");
                                        F = false;
                                    }
                                    else
                                        temp = temp.next;
                                if (F)
                                    outp.Write("0\t");
                            }
                        }
                        outp.WriteLine();
                        outp.WriteLine();
                        outp.WriteLine();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Неверно задан путь к файлу", "Ошибка");
                return false;
            }
        }
        public void StructOutput(Object _Outparams)
        {
            Outparams outpar = (Outparams)_Outparams;
            RichTextBox outp = outpar.ForOutForm.richTextBox1;
            OperProgress prbarform = outpar.prbarform;
            Link temp;
            int count = 0;
            int memory_size = 0;
            for (int j = 0; j < this.sizex; ++j)
            {
                if (!prbarform.IsDisposed)
                {
                    prbarform.InvokeUI(() => { prbarform.progressBar1.Value = count; });
                    count++;
                }
                temp = this.columns[j];
                while (temp.next != null)
                {
                    memory_size += 12;
                    var safeptr = SafePtr.Create(temp.next);
                    outp.AppendText("Элемент " + temp.next.inf + " с координатами X:" + (j + 1) + " Y:" + (1 + temp.next.line_number) + " находится по адресу " + safeptr.IntPtr.ToString("x") + "\n");
                    temp = temp.next;
                }
            }
            if (!prbarform.IsDisposed)
                prbarform.InvokeUI(() => { prbarform.Close(); });
            outp.AppendText("\nОбщий объём памяти, занимаемый матрицой: " + memory_size / 1024.0 + " Кбайт\n");
            if (--this.count_readers == 0)
            {
                Fparent.names_collection[Fparent.names_collection.IndexOf(this.name + " (" + this.sizey.ToString() + "x" + this.sizex.ToString() + ") (чтение)")] = this.name + " (" + this.sizey.ToString() + "x" + this.sizex.ToString() + ")";
                Program.fr2.InvokeUI(() => { Program.fr2.ComboRefresh(); });
            }
            outpar.ForOutForm.ShowDialog();
        }

        public void Cut(int column, int line)
        {
            Link elem;
            int size = sizex;//Тут это допустимо, так как обрезание всё равно только для квадратных матриц
            for (int j = column; j < size - 1; j++)
                this.columns[j] = this.columns[j + 1];
            for (int j = 0; j < size - 1; j++)
            {
                if (this.Exist(line, j) != 0)
                {
                    elem = this.columns[j];
                    while (elem.next.line_number != line)
                        elem = elem.next;
                    elem.next = elem.next.next;
                }
                elem = this.columns[j];
                while (elem.line_number > -1 && elem.line_number < line && elem != null)
                    elem = elem.next;
                while (elem != null)
                {
                    elem.line_number--;
                    elem = elem.next;
                }
            }
            this.sizex--;
            this.sizey--;
        }

        public static void AddMatrix(Object para)
        {
            Parms par = (Parms)para;
            Matrix mat1 = par.left, mat2 = par.right, result = par.result;
            OperProgress prbar = par.prbar;
            Link temp;
            int count = 0;
            while (!prbar.IsHandleCreated)
                Thread.Sleep(10);
            for (int i = 0; i < result.sizex; ++i)
            {
                if (!prbar.IsDisposed)
                    prbar.InvokeUI(() => { prbar.progressBar1.Value = count; });
                count++;
                temp = mat1.columns[i].next;
                while (temp != null)
                {
                    result.columns[i].Insert(temp.line_number, mat1.Exist(i, temp.line_number) + mat2.Exist(i, temp.line_number));
                    temp = temp.next;
                }
                temp = mat2.columns[i].next;
                while (temp != null)
                {
                    if (mat1.Exist(i, temp.line_number) == 0)
                        result.columns[i].Insert(temp.line_number, mat2.Exist(i, temp.line_number));
                    temp = temp.next;
                }
            }

            Fparent.names_collection[Fparent.names_collection.IndexOf(result.name + " (" + result.sizey.ToString() + "x" + result.sizex.ToString() + ") (запись)")] = result.name + " (" + result.sizey.ToString() + "x" + result.sizex.ToString() + ")";

            if (mat1.name == mat2.name)
                mat1.count_readers--;
            else
            {
                mat1.count_readers--;
                mat2.count_readers--;
            }

            if (mat1.count_readers == 0)
                if (mat1.name != result.name)//для сброса наименования и возможности дальнейшей работы с матрицами
                    try
                    {
                        Fparent.names_collection[Fparent.names_collection.IndexOf(mat1.name + " (" + mat1.sizey.ToString() + "x" + mat1.sizex.ToString() + ") (чтение)")] = mat1.name + " (" + mat1.sizey.ToString() + "x" + mat1.sizex.ToString() + ")";
                    }
                    catch
                    {
                        Fparent.names_collection[Fparent.names_collection.IndexOf(mat1.name + " (" + mat1.sizey.ToString() + "x" + mat1.sizex.ToString() + ") (запись)")] = mat1.name + " (" + mat1.sizey.ToString() + "x" + mat1.sizex.ToString() + ")";
                    }
            if (mat2.count_readers == 0)
                if (mat2.name != result.name && mat2.name != mat1.name)
                    try
                    {
                        Fparent.names_collection[Fparent.names_collection.IndexOf(mat2.name + " (" + mat2.sizey.ToString() + "x" + mat2.sizex.ToString() + ") (чтение)")] = mat2.name + " (" + mat2.sizey.ToString() + "x" + mat2.sizex.ToString() + ")";
                    }
                    catch
                    {
                        Fparent.names_collection[Fparent.names_collection.IndexOf(mat2.name + " (" + mat2.sizey.ToString() + "x" + mat2.sizex.ToString() + ") (запись)")] = mat2.name + " (" + mat2.sizey.ToString() + "x" + mat2.sizex.ToString() + ")";
                    }

            Program.fr2.InvokeUI(() => { Program.fr2.ComboRefresh(); });

            if (MessageBox.Show("Матрица \"" + result.name + "\" успешно добавлена, вывести результат?", "Успех", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Program.fr2.InvokeUI(() => { prbar.progressBar1.Maximum = result.sizex * result.sizey; });
                Matrix.Outparams forout = new Matrix.Outparams(new ForOutputMatrix(result.name, "matrix"), prbar);
                //   result.count_readers++;
                Fparent.names_collection[Fparent.names_collection.IndexOf(result.name + " (" + result.sizey.ToString() + "x" + result.sizex.ToString() + ")")] = result.name + " (" + result.sizey.ToString() + "x" + result.sizex.ToString() + ") (чтение)";
                Program.fr2.InvokeUI(() => { Program.fr2.ComboRefresh(); });
                result.count_readers++;
                Fparent.threads.Add(new Thread(result.OutputMatrix));
                Fparent.threads.Last().Start(forout);
            }
            else
            if (!prbar.IsDisposed)
                prbar.InvokeUI(() => { prbar.Close(); });

        }

        public static void SubMatrix(Object para)
        {
            Parms par = (Parms)para;
            Matrix mat1 = par.left, mat2 = par.right, result = par.result;
            OperProgress prbar = par.prbar;
            Link temp;
            int count = 0;
            while (!prbar.IsHandleCreated)
                Thread.Sleep(10);
            for (int i = 0; i < result.sizex; ++i)
            {
                if (!prbar.IsDisposed)
                    prbar.InvokeUI(() => { prbar.progressBar1.Value = count; });
                count++;
                temp = mat1.columns[i].next;
                while (temp != null)
                {
                    result.columns[i].Insert(temp.line_number, mat1.Exist(i, temp.line_number) - mat2.Exist(i, temp.line_number));
                    temp = temp.next;
                }
                temp = mat2.columns[i].next;
                while (temp != null)
                {
                    if (mat1.Exist(i, temp.line_number) == 0)
                        result.columns[i].Insert(temp.line_number, -mat2.Exist(i, temp.line_number));
                    temp = temp.next;
                }
            }

            Fparent.names_collection[Fparent.names_collection.IndexOf(result.name + " (" + result.sizey.ToString() + "x" + result.sizex.ToString() + ") (запись)")] = result.name + " (" + result.sizey.ToString() + "x" + result.sizex.ToString() + ")";

            if (mat1.name == mat2.name)
                mat1.count_readers--;
            else
            {
                mat1.count_readers--;
                mat2.count_readers--;
            }

            if (mat1.count_readers == 0)
                if (mat1.name != result.name)//для сброса наименования и возможности дальнейшей работы с матрицами
                    try
                    {
                        Fparent.names_collection[Fparent.names_collection.IndexOf(mat1.name + " (" + mat1.sizey.ToString() + "x" + mat1.sizex.ToString() + ") (чтение)")] = mat1.name + " (" + mat1.sizey.ToString() + "x" + mat1.sizex.ToString() + ")";
                    }
                    catch
                    {
                        Fparent.names_collection[Fparent.names_collection.IndexOf(mat1.name + " (" + mat1.sizey.ToString() + "x" + mat1.sizex.ToString() + ") (запись)")] = mat1.name + " (" + mat1.sizey.ToString() + "x" + mat1.sizex.ToString() + ")";
                    }
            if (mat2.count_readers == 0)
                if (mat2.name != result.name && mat2.name != mat1.name)
                    try
                    {
                        Fparent.names_collection[Fparent.names_collection.IndexOf(mat2.name + " (" + mat2.sizey.ToString() + "x" + mat2.sizex.ToString() + ") (чтение)")] = mat2.name + " (" + mat2.sizey.ToString() + "x" + mat2.sizex.ToString() + ")";
                    }
                    catch
                    {
                        Fparent.names_collection[Fparent.names_collection.IndexOf(mat2.name + " (" + mat2.sizey.ToString() + "x" + mat2.sizex.ToString() + ") (запись)")] = mat2.name + " (" + mat2.sizey.ToString() + "x" + mat2.sizex.ToString() + ")";
                    }

            Program.fr2.InvokeUI(() => { Program.fr2.ComboRefresh(); });

            if (MessageBox.Show("Матрица \"" + result.name + "\" успешно добавлена, вывести результат?", "Успех", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Program.fr2.InvokeUI(() => { prbar.progressBar1.Maximum = result.sizex * result.sizey; });
                Matrix.Outparams forout = new Matrix.Outparams(new ForOutputMatrix(result.name, "matrix"), prbar);
                //   result.count_readers++;
                Fparent.names_collection[Fparent.names_collection.IndexOf(result.name + " (" + result.sizey.ToString() + "x" + result.sizex.ToString() + ")")] = result.name + " (" + result.sizey.ToString() + "x" + result.sizex.ToString() + ") (чтение)";
                Program.fr2.InvokeUI(() => { Program.fr2.ComboRefresh(); });
                result.count_readers++;
                Fparent.threads.Add(new Thread(result.OutputMatrix));
                Fparent.threads.Last().Start(forout);
            }
            else
            if (!prbar.IsDisposed)
                prbar.InvokeUI(() => { prbar.Close(); });
        }


        public static void MultMatrix(Object para)
        {
            Parms par = (Parms)para;
            Matrix mat1 = par.left, mat2 = par.right, result = par.result;
            OperProgress prbar = par.prbar;
            int count = 0;
            while (!prbar.IsHandleCreated)
                Thread.Sleep(10);
            for (int i = 0; i < result.sizey; ++i)
            {
                if (!prbar.IsDisposed)
                    prbar.InvokeUI(() => { prbar.progressBar1.Value = count; });
                count++;
                for (int j = 0; j < result.sizex; ++j)
                {
                    int sum = 0;
                    Link temp = mat2.columns[j].next;
                    Link current_pointer = temp;
                    while (temp != null)
                    {
                        sum += mat2.Exist(j, temp.line_number, ref current_pointer) * mat1.Exist(temp.line_number, i);
                        //sum += mat2.Exist(j, temp.line_number) * mat1.Exist(temp.line_number, i);
                        temp = temp.next;
                    }
                    if (sum != 0)
                        result.columns[j].Insert(i, sum);
                }
            }
            Fparent.names_collection[Fparent.names_collection.IndexOf(result.name + " (" + result.sizey.ToString() + "x" + result.sizex.ToString() + ") (запись)")] = result.name + " (" + result.sizey.ToString() + "x" + result.sizex.ToString() + ")";

            if (mat1.name == mat2.name)
                mat1.count_readers--;
            else
            {
                mat1.count_readers--;
                mat2.count_readers--;
            }
            if (mat1.count_readers == 0)
                if (mat1.name != result.name)//для сброса наименования и возможности дальнейшей работы с матрицами
                    try
                    {
                        Fparent.names_collection[Fparent.names_collection.IndexOf(mat1.name + " (" + mat1.sizey.ToString() + "x" + mat1.sizex.ToString() + ") (чтение)")] = mat1.name + " (" + mat1.sizey.ToString() + "x" + mat1.sizex.ToString() + ")";
                    }
                    catch
                    {
                        Fparent.names_collection[Fparent.names_collection.IndexOf(mat1.name + " (" + mat1.sizey.ToString() + "x" + mat1.sizex.ToString() + ") (запись)")] = mat1.name + " (" + mat1.sizey.ToString() + "x" + mat1.sizex.ToString() + ")";
                    }

            if (mat2.count_readers == 0)
                if (mat2.name != result.name && mat2.name != mat1.name)
                    try
                    {
                        Fparent.names_collection[Fparent.names_collection.IndexOf(mat2.name + " (" + mat2.sizey.ToString() + "x" + mat2.sizex.ToString() + ") (чтение)")] = mat2.name + " (" + mat2.sizey.ToString() + "x" + mat2.sizex.ToString() + ")";
                    }
                    catch
                    {
                        Fparent.names_collection[Fparent.names_collection.IndexOf(mat2.name + " (" + mat2.sizey.ToString() + "x" + mat2.sizex.ToString() + ") (запись)")] = mat2.name + " (" + mat2.sizey.ToString() + "x" + mat2.sizex.ToString() + ")";
                    }
            Program.fr2.InvokeUI(() => { Program.fr2.ComboRefresh(); });



            if (MessageBox.Show("Матрица \"" + result.name + "\" успешно добавлена, вывести результат?", "Успех", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Program.fr2.InvokeUI(() => { prbar.progressBar1.Maximum = result.sizex * result.sizey; });
                Matrix.Outparams forout = new Matrix.Outparams(new ForOutputMatrix(result.name, "matrix"), prbar);
                //   result.count_readers++;
                Fparent.names_collection[Fparent.names_collection.IndexOf(result.name + " (" + result.sizey.ToString() + "x" + result.sizex.ToString() + ")")] = result.name + " (" + result.sizey.ToString() + "x" + result.sizex.ToString() + ") (чтение)";
                Program.fr2.InvokeUI(() => { Program.fr2.ComboRefresh(); });
                result.count_readers++;
                Fparent.threads.Add(new Thread(result.OutputMatrix));
                Fparent.threads.Last().Start(forout);
            }
            else
            if (!prbar.IsDisposed)
                prbar.InvokeUI(() => { prbar.Close(); });
        }

        public int Minor(int row, int col, bool[] rows, bool[] cols, int cur_size)//Минор (для обратной матрицы)
        {
            rows[row] = false;
            cols[col] = false;
            if ((row + col) % 2 == 0)
                return this.DetMatrix(rows, cols, cur_size - 1);//Возвращает значение определителя матрицы без строки и столбца
            else
                return -this.DetMatrix(rows, cols, cur_size - 1);
        }


        public int AlgComp(Matrix m, int row, bool[] rows, bool[] cols, int cur_size)//Алгебраическое дополнение
        {
            int last = 0;
            for (int i = cols.Length - 1; i >= 0; --i)//поиск индекса последнего столбца
                if (cols[i])
                {
                    Link temp = this.columns[i].next;
                    while (temp != null)
                        if (rows[temp.line_number])
                            break;
                        else
                            temp = temp.next;
                    if (temp != null)
                    {
                        last = i;
                        break;
                    }
                }
            rows[row] = false;
            cols[last] = false;
            return m.DetMatrix(rows, cols, cur_size - 1);//Возвращает значение определителя матрицы без строки и столбца
        }



        public static void ReverseMatrix(Object para)
        {
            Parms par = (Parms)para;
            Matrix mat = par.left, result = par.result;
            OperProgress prbar = par.prbar;
            bool[] cols = new bool[mat.sizex];
            bool[] rows = new bool[mat.sizex];
            for (int i = 0; i < cols.Length; ++i)
                cols[i] = rows[i] = true;
            int det = mat.DetMatrix(rows, cols, mat.sizex);
            if (det == 0)
            {
                try
                {
                    Fparent.names_collection[Fparent.names_collection.IndexOf(result.name + " (" + result.sizey.ToString() + "x" + result.sizex.ToString() + ") (запись)")] = result.name + " (" + result.sizey.ToString() + "x" + result.sizex.ToString() + ")";
                }
                catch
                {
                    Thread.Sleep(200);
                    try
                    {
                        Fparent.names_collection[Fparent.names_collection.IndexOf(result.name + " (" + result.sizey.ToString() + "x" + result.sizex.ToString() + ") (запись)")] = result.name + " (" + result.sizey.ToString() + "x" + result.sizex.ToString() + ")";
                    }
                    catch { }
                }
                --mat.count_readers;
                if (mat.count_readers == 0)
                    if (mat.name != result.name)
                        try
                        {
                            Fparent.names_collection[Fparent.names_collection.IndexOf(mat.name + " (" + mat.sizey.ToString() + "x" + mat.sizex.ToString() + ") (чтение)")] = mat.name + " (" + mat.sizey.ToString() + "x" + mat.sizex.ToString() + ")";
                        }
                        catch
                        {
                            Fparent.names_collection[Fparent.names_collection.IndexOf(mat.name + " (" + mat.sizey.ToString() + "x" + mat.sizex.ToString() + ") (запись)")] = mat.name + " (" + mat.sizey.ToString() + "x" + mat.sizex.ToString() + ")";
                        }

                Program.fr2.InvokeUI(() => { Program.fr2.ComboRefresh(); });

                if (!prbar.IsDisposed)
                    prbar.InvokeUI(() => { prbar.Close(); });
                par.isDetermNull = 1;
                MessageBox.Show("Определитель матрицы равен нулю, невозможно найти обратную", "Ошибка");
                return;
            }
            else
                par.isDetermNull = 0;
            int k = 0;
            int count = 0;
            while (!prbar.IsHandleCreated)
                Thread.Sleep(10);
            for (int i = 0; i < result.sizey; ++i)
                for (int j = 0; j < result.sizex; ++j)
                {
                    if (!prbar.IsDisposed)
                        prbar.InvokeUI(() => { prbar.progressBar1.Value = count; });
                    count++;
                    result.columns[i].Insert(j, Convert.ToInt32((mat.Minor(i, j, (bool[])rows.Clone(), (bool[])cols.Clone(), mat.sizex)) / det));
                    Console.WriteLine(++k);
                }
            try
            {
                Fparent.names_collection[Fparent.names_collection.IndexOf(result.name + " (" + result.sizey.ToString() + "x" + result.sizex.ToString() + ") (запись)")] = result.name + " (" + result.sizey.ToString() + "x" + result.sizex.ToString() + ")";
            }
            catch
            {
                Thread.Sleep(200);
                Fparent.names_collection[Fparent.names_collection.IndexOf(result.name + " (" + result.sizey.ToString() + "x" + result.sizex.ToString() + ") (запись)")] = result.name + " (" + result.sizey.ToString() + "x" + result.sizex.ToString() + ")";
            }
            --mat.count_readers;
            if (mat.count_readers == 0)
                if (mat.name != result.name)
                    try
                    {
                        Fparent.names_collection[Fparent.names_collection.IndexOf(mat.name + " (" + mat.sizey.ToString() + "x" + mat.sizex.ToString() + ") (чтение)")] = mat.name + " (" + mat.sizey.ToString() + "x" + mat.sizex.ToString() + ")";
                    }
                    catch
                    {
                        Fparent.names_collection[Fparent.names_collection.IndexOf(mat.name + " (" + mat.sizey.ToString() + "x" + mat.sizex.ToString() + ") (запись)")] = mat.name + " (" + mat.sizey.ToString() + "x" + mat.sizex.ToString() + ")";
                    }
            Program.fr2.InvokeUI(() => { Program.fr2.ComboRefresh(); });

            if (MessageBox.Show("Матрица \"" + result.name + "\" успешно добавлена, вывести результат?", "Успех", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Program.fr2.InvokeUI(() => { prbar.progressBar1.Maximum = result.sizex * result.sizey; });
                Matrix.Outparams forout = new Matrix.Outparams(new ForOutputMatrix(result.name, "matrix"), prbar);
                //   result.count_readers++;
                Fparent.names_collection[Fparent.names_collection.IndexOf(result.name + " (" + result.sizey.ToString() + "x" + result.sizex.ToString() + ")")] = result.name + " (" + result.sizey.ToString() + "x" + result.sizex.ToString() + ") (чтение)";
                Program.fr2.InvokeUI(() => { Program.fr2.ComboRefresh(); });
                result.count_readers++;
                Fparent.threads.Add(new Thread(result.OutputMatrix));
                Fparent.threads.Last().Start(forout);
            }
            else
            if (!prbar.IsDisposed)
                prbar.InvokeUI(() => { prbar.Close(); });
        }



        public int DetMatrix(bool[] rows, bool[] cols, int cur_size)
        {
            int size = this.sizex;
            bool zero_line;
            int sum = 0;
            Link column;

            if (cur_size == 1)//Когда из матрицы удалятся все элементы и она станет одинарной
            {
                for (int i = 0; i < this.sizex; ++i)
                    if (cols[i])
                    {
                        Link temp = this.columns[i];
                        temp = this.columns[i].next;
                        while (!rows[temp.line_number])
                        {
                            temp = temp.next;
                            if (temp == null)
                                return 0;
                        }
                        return temp.inf;
                    }
                throw new Exception("Как ты тут оказался?");//Место программы, в которое невозможно зайти в теории
            }

            for (int j = 0; j < size; j++) // проверка на наличие нулевых столбцов
            {
                if (cols[j])
                {
                    Link temp = this.columns[j].next;
                    while (temp != null)
                        if (rows[temp.line_number])
                            break;
                        else
                            temp = temp.next;
                    if (temp == null)
                        return 0;
                }
            }


            for (int i = 0; i < size; i++) // проверка на наличие нулевых строк
            {
                if (rows[i])
                {
                    zero_line = true;
                    for (int j = 0; zero_line && j < size; j++)
                        if (cols[j])
                            if (this.NotZero(j, i)) zero_line = false;
                    if (zero_line) return 0;
                }
            }


            int last = 0;
            for (int i = cols.Length - 1; i >= 0; --i)//поиск индекса последнего столбца
                if (cols[i])
                {
                    Link temp = this.columns[i].next;
                    while (temp != null)
                        if (rows[temp.line_number])
                            break;
                        else
                            temp = temp.next;
                    if (temp != null)
                    {
                        last = i;
                        break;
                    }
                }
            column = this.columns[last].next; // получение суммы произведений эл-ов последнего стоблца на их алг. дополнения
            while (column != null)
            {
                int znak;
                if ((last + column.line_number) % 2 == 0)
                    znak = 1;
                else
                    znak = -1;
                if (rows[column.line_number])
                    sum += column.inf * AlgComp(this, column.line_number, (bool[])rows.Clone(), (bool[])cols.Clone(), cur_size) * znak;
                column = column.next;
            }
            return sum;
        }

        public class Outparams
        {
            public ForOutputMatrix ForOutForm;
            public OperProgress prbarform;

            public Outparams(ForOutputMatrix _ForOutForm, OperProgress _prbarform)
            {
                ForOutForm = _ForOutForm;
                prbarform = _prbarform;
            }
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct SafePtr//Это для вывода адресов (копипаст хабра)
        {
            // (1)
            public class ReferenceType
            {
                public object Reference;
            }

            // (2)
            public class IntPtrWrapper
            {
                public IntPtr IntPtr;
            }

            // (3)
            [FieldOffset(0)]
            private ReferenceType Obj;

            // (4)
            [FieldOffset(0)]
            private IntPtrWrapper Pointer;

            public static SafePtr Create(object obj)
            {
                return new SafePtr { Obj = new ReferenceType { Reference = obj } };
            }

            public static SafePtr Create(IntPtr rIntPtr)
            {
                return new SafePtr { Pointer = new IntPtrWrapper { IntPtr = rIntPtr } };
            }

            // (5)
            public IntPtr IntPtr
            {
                get { return Pointer.IntPtr; }
                set { Pointer.IntPtr = value; }
            }

            // (6)
            public Object Object
            {
                get { return Obj.Reference; }
                set { Obj.Reference = value; }
            }

            public void SetPointer(SafePtr another)
            {
                Pointer.IntPtr = another.Pointer.IntPtr;
            }
        }
    }
}
