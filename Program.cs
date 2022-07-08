

// задание №1

//using System;
//using System.IO;
//using System.Text;

//namespace FileTests
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            string[] files = Directory.GetFiles(@"C:\1\HomeTask");
//            foreach (string name in files)
//            {
//                FileStream fs = new FileStream(name, FileMode.Open, FileAccess.Read);
//                StreamReader sr = new StreamReader(fs);
//                string fileText = sr.ReadToEnd();
//                if (fileText.Contains("sharp"))
//                {
//                    Console.WriteLine(name);
//                }
//            }
//            Console.WriteLine();
//        }
//    }
//}

//////////////////////////////
///задание №2
///

//using System;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Collections;
//using System.Collections.Generic;

//namespace HomeTask2
//{
//    class Program
//    {
//        public static void Main()
//        {
//            string path = @"C:\1\book.txt";
//            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
//            StreamReader sr = new StreamReader(fs);
//            string text = sr.ReadToEnd();
//            string[] singleWords = text.Split(',', '.', '!', '?', '!', '"', ' ', '\n');
//            Dictionary<string, int> words = new Dictionary<string, int>();
//            for (int i = 0; i < singleWords.Length; i++)
//            {
//                if (!words.ContainsKey(singleWords[i]))
//                {
//                    if (singleWords[i].Length >= 4)
//                    {
//                        words.Add(singleWords[i], 1);
//                    }
//                }
//                else
//                {
//                    words[singleWords[i]] += 1;
//                }
//            }
//            int count = 0;
//            foreach (var item in words.OrderByDescending(item => item.Value))
//            {
//                count++;
//                Console.WriteLine(item.Key + " - " + item.Value);
//                if (count == 50) break;
//            }
//        }
//    }
//}

///////////////////////////////////////
///Задание №3
///

using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace FileTask
{
    class Program
    {
        public static void Search(DirectoryInfo dr,ref Dictionary<string, int> lib, ref Dictionary<string, long> libSt)
        {
            FileInfo[] files = dr.GetFiles();
            foreach (var file in files)
            {
                if (!lib.ContainsKey(file.Extension))
                {
                    lib.Add(file.Extension, 1);
                }
                else
                {
                    lib[file.Extension] += 1;
                }
                if (!libSt.ContainsKey(file.Extension))
                {
                    libSt.Add(file.Extension, file.Length);
                }
                else
                {
                    libSt[file.Extension] += file.Length;
                }
            }
            DirectoryInfo[] dirs = dr.GetDirectories();
            foreach (var directoryInfo in dirs)
            {
                try
                {
                    Search(directoryInfo,ref lib, ref libSt);
                }
                catch(UnauthorizedAccessException)
                {
                    continue;
                }
            }
        }

        public static void Main()
        {
            string path = @"C:\Users\";
            DirectoryInfo dr = new DirectoryInfo(path);
            Dictionary <string, int> libEx = new Dictionary<string, int>();
            Dictionary <string, long> libStorage = new Dictionary<string, long>();
            Search(dr,ref libEx, ref libStorage);
            Console.WriteLine("Вид разширения \t кол.файлов \t размер  ");
            foreach (var item in libEx.OrderByDescending(item=>item.Value))
            {
                foreach (var item2 in libStorage)
                {
                    if (item.Key == item2.Key)
                    {
                        Console.WriteLine(item.Key + "\t\t" + item.Value + "\t\t" + item2.Value);
                    }
                }
            }
        }
    }
}
