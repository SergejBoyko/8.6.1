using System.IO;

namespace DirectoryManager
{
    class Program
    {
        static void Main(string[] args)
        {
            GetCatalogs(); //   Вызов метода получения директорий
            Console.ReadLine();
        }

        static void GetCatalogs()
        {
            string dirName = @"C:\Users\B1S3\Desktop\testFolder12"; // Прописываем путь к корневой директории
            if (Directory.Exists(dirName)) // Проверим, что директория существует
                GetFiles(dirName);
            {
                string[] dirs = Directory.GetDirectories(dirName);  // Получим все директории корневого каталога

                foreach (string d in dirs) // Выведем их все
                {
                    Console.WriteLine("Папки:");
                    Console.WriteLine(d);
                    GetFiles(d);
                    if (Directory.GetFiles(d).Length < 1)
                    {
                        Console.WriteLine("каталог {0} удален, так как в нем нет файлов", d);
                        Directory.Delete(d);
                    }
                }
            }
        }
        static void GetFiles(string d)
        {
            Console.WriteLine("Файлы:");
            string[] files = Directory.GetFiles(d);// Получим все файлы корневого каталога
            foreach (string s in files)   // Выведем их все
            {
                Console.WriteLine(s);
                DeleteOldFiles(s);
            }
        }
        static void DeleteOldFiles(string s)
        {
            DateTime date2 = File.GetLastAccessTime(s);
            DateTime date1 = DateTime.Now;
            TimeSpan timeSpan = TimeSpan.FromMinutes(30);
            TimeSpan timeSpan1 = date1 - date2;
            if (timeSpan1 > timeSpan)
            {
                Console.WriteLine("Файл {0} удален, изменения были более 30 минут назад", s);
                File.Delete(s);
            }
        }
    }
}