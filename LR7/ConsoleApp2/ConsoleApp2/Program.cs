using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading;
using System.Runtime;


namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            //Путь до файла и его название
            var docPath = "C:\\Users\\Дмитрий\\Desktop\\Мои работы\\";
            var filename = "code.txt";

            string buf="";
            bool str =false;
            string[] lines = File.ReadAllLines($"{docPath}{filename}");
            //Считываем весь файл
            var dict = new Dictionary<string, string>();
            //Создание списка или как говорится в питоне словоря 
            foreach (string line in lines)//проходим весь код построчно
            {
                // Проверка на наличее ; 
                if (line[0] != ';') {
                    Console.WriteLine("Ошибка в написании кода");
                    break;
                }
                // Проверка на Вывод текста между !! ; 
                for (int i = 1; i <= line.Length-1; i++)
                {
                    if (line[i] == '!' )
                    {
                        buf = "";
                        if (line[i+1]== '!' )
                        {
                            i = i + 2;
                            while(line[i]!= '!')
                            {
                                str = true;
                                Console.Write(line[i]);
                                i++;
                            }
                            i = i + 2;
                        }
                    }
                    else
                    {
                        buf += line[i];   
                    }
                }
                //Присвоение переменной значения
                if (buf.Contains("const string") && buf.Contains("=") && buf.Contains("\""))
                {
                    if ((buf.Split('g')[1]).Contains("=") && (buf.Split('=')[1]).Contains("\"")) { 
                        dict = AddNewValueForString(dict, line);
                    }
                    else
                    {
                        Console.WriteLine("Вы допустили ошибку при написании программы");
                    }
                }
                //Умножение строк
                else if (buf.Contains("const string") && buf.Contains("*"))
                {
                    if ((buf.Split('g')[1]).Contains("=") && (buf.Split('=')[1]).Contains("*"))
                    {
                        dict = AddNewValueForStringPow(dict, line);
                    }
                    else
                    {
                        Console.WriteLine("Вы допустили ошибку при написании программы");
                    }

                }
                //Присвоение переменных типа инт
                else if (buf.Contains("int") && !buf.Contains("^"))
                {
                    if ((buf.Split('t')[1]).Contains("="))
                    {
                        dict = AddNewValueForInt(dict, line);
                    }
                    else
                    {
                        Console.WriteLine("Вы допустили ошибку при написании программы");
                    }
                }
                //Возведение числа в степень
                else if (buf.Contains("int") && buf.Contains("^") && buf.Contains("="))
                {
                    if ((buf.Split('t')[1]).Contains("=") && (buf.Split('=')[1]).Contains("^")) { 
                    dict = AddNewValueForIntPow(dict, line);
                    }
                    else
                    {
                        Console.WriteLine("Вы допустили ошибку при написании програсммы");
                    }
                }
                buf = "";
                Console.WriteLine("");
            }
            foreach (KeyValuePair<string, string> keyValue in dict)
            {
                Console.WriteLine(keyValue.Key + " - " + keyValue.Value);
            }
            Thread.Sleep(10000);
        }

        private static Dictionary<string, string> AddNewValueForString(Dictionary<string, string> dict, string line)
        {
            var expression = line.Split('\"')[1];
            expression = expression.Split('\"')[0];
            string str;
            str = line.Split('=')[0];
            str = str.Split('g')[1];
            str = str.Trim(new Char[] { ' ', '*', '.' });
            dict[str] = expression;
            return dict;
        }

        private static Dictionary<string, string> AddNewValueForStringPow(Dictionary<string, string> dict, string line)
        {
            string str;
            str = line.Split('=')[0];
            str = str.Split('g')[1];
            str = str.Trim(new Char[] { ' ', '*', '.' });

            var expression = line.Split('=')[1];

            string str2 = (expression.Split('*')[0]).Trim(new Char[] { ' ', '*', '.' }), realstr="";
            double ex = double.Parse((expression.Split('*')[1]).Trim(new Char[] { ' ', '*', '.' }));


            foreach (KeyValuePair<string, string> keyValue in dict)
            {
                if (str2 == keyValue.Key)
                    {
                    realstr = keyValue.Value;
                    }
            }
            str2 = null;
            for (double i = 0;i<=ex-1;i++)
            {
                str2 = str2 + realstr;
            }


            dict[str] = str2;
            return dict;
        }
        private static Dictionary<string, string> AddNewValueForInt(Dictionary<string, string> dict, string line)
        {

            var expression = line.Split('=')[1];
            expression = expression.Trim(new Char[] { ' ', '*', '.' });

            int a = 1 + (line.Split('=')[0]).IndexOf('t'), b = (line.Split('=')[0]).Length - 1 - a;
            string str;
            str = (line.Split('=')[0]).Substring(a, b);
            str = str.Trim(new Char[] { ' ', '*', '.' });
            dict[str] = expression;

            return dict;
        }

        private static Dictionary<string, string> AddNewValueForIntPow(Dictionary<string, string> dict, string line)
        {

            var expression = line.Split('=')[1];
            expression = expression.Trim(new Char[] { ' ', '*', '.' });
            double sum = 0f;
            string[] elems =  new string[expression.Split('^').Length];
            for (int i = 0; i <= expression.Split('^').Length-1; i++)
            {
                if (int.TryParse(expression.Split('^')[i], out int n))
                {
                    elems[i] = expression.Split('^')[i];
                }
                else if (dict.Count!=0)
                {
                    foreach (KeyValuePair<string, string> keyValue in dict)
                    {
                        if (expression.Split('^')[i] == keyValue.Key)
                        {
                            elems[i] = dict[expression.Split('^')[i]];
                        }
                    }
                }
                else
                {
                    Console.WriteLine("С переменными что то не так");
                    Thread.Sleep(10000);
                    Environment.Exit(0);
                }

            }
            for (int i = 0; i <= elems.Length - 2; i++)
            {
                    elems[i + 1] = Math.Pow(double.Parse(elems[i]), double.Parse(elems[i + 1])).ToString();
            }
            sum = double.Parse(elems[elems.Length - 1]);


            int a =1 + (line.Split('=')[0]).IndexOf('t'), b = (line.Split('=')[0]).Length-1 - a ;
            string str;
            str = (line.Split('=')[0]).Substring(a,b);
            str = str.Trim(new Char[] { ' ', '*', '.' });
            dict[str] = sum.ToString();
            return dict;
        }
    }
}
