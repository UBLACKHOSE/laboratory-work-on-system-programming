using System;
using System.IO;
using System.IO.Compression;



namespace ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
        {
            string sourceFile = "C://Users/Дмитрий/Desktop/Мои работы/Файлы приходят/log.txt"; // Путь до файла 
            string compressedFile;// Путь до сжатого файла (gz это gzip)
            DateTime now;//задем переменную даты
            while (true)
            {
                System.Threading.Thread.Sleep(50000);//задаем интервал выполнения
                now = DateTime.Now;//задаем интервал выполнения
                //формируем название сжатого файла
                compressedFile = "C://Users/Дмитрий/Desktop/Мои работы/Файлы уходят/"+ now.ToString("dd.MM.yyyy")+"__"+ now.ToString("hh.mm.ss")+"__"+"log" + ".gz";
                // создание сжатого файла
                Compress(sourceFile, compressedFile);
                File.Delete(sourceFile);//Удаляем текущий файл с логами
           }
            Console.ReadLine();
        }
        public static void Compress(string sourceFile, string compressedFile)
        {
            // поток для чтения исходного файла
            using (FileStream sourceStream = new FileStream(sourceFile, FileMode.OpenOrCreate))
            {
                // поток для записи сжатого файла
                using (FileStream targetStream = File.Create(compressedFile))
                {
                    // поток архивации
                    using (GZipStream compressionStream = new GZipStream(targetStream, CompressionMode.Compress,false))
                    {
                        sourceStream.CopyTo(compressionStream); // копируем байты из одного потока в другой
                        Console.WriteLine("Сжатие файла {0} завершено. Исходный размер: {1}  сжатый размер: {2}.",
                            sourceFile, sourceStream.Length.ToString(), targetStream.Length.ToString());
                    }
                }
            }
        }
    }
}
