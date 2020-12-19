using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string dirName = textBox1.Text; //берем путь с текстового поля
            listBox1.Items.Clear();
            string s2 = "";
            DirectoryInfo dirInfo = new DirectoryInfo(dirName); // Создаем обект класса DirectoryInfo
            if (Directory.Exists(dirName))//Узнаем есть ли такая папка
            {

                //Выводим папки
                listBox1.Items.Add("Подкаталоги:");
                string[] dirs = Directory.GetDirectories(dirName);
                foreach (string s in dirs)
                {
                    listBox1.Items.Add(s);
                }
                Console.WriteLine();

                //Выводим файлы
                listBox1.Items.Add("Файлы:");
                string[] files = Directory.GetFiles(dirName);
                foreach (string s in files)
                {
                    FileInfo fileInf = new FileInfo(s);

                    s2 = "Имя файла: " + fileInf.Name + " ";
                    s2 += "Время создания: " + fileInf.CreationTime + " ";
                    s2 += "Размер: " + fileInf.Length + " ";
                    listBox1.Items.Add(s2);
                }
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
