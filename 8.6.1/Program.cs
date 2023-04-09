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
            try
            {
                string dirName = @"C:\Users\B1S3\Desktop\testFolder12"; // Прописываем путь к корневой директории
                long a = GetSizeFolder(dirName);
                Console.WriteLine("Текущий размер каталога {0} байт",a);
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
                long b = GetSizeFolder(dirName);
                Console.WriteLine("Особождено {0}",a - b);
                Console.WriteLine("Текущий размер каталога {0} байт", b);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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
            TimeSpan timeSpan = TimeSpan.FromMinutes(20);
            TimeSpan timeSpan1 = date1 - date2;
            if (timeSpan1 > timeSpan)
            {
                Console.WriteLine("Файл {0} удален, изменения были более 30 минут назад", s);
                File.Delete(s);
            }
        }
        public static long GetSizeFolder(string dirName)
        {
            long sizeFiles = 0;
            int files = 0;
            string[] fis = Directory.GetFiles(dirName);
            foreach (string fi in fis) 
            {
                sizeFiles += fi.Length;
                files++;
            }
            string[] dir = Directory.GetDirectories(dirName);
            foreach (var di in dir)
            {
                sizeFiles += GetSizeFolder(di);
            }
            return sizeFiles;
        }
    }
}
