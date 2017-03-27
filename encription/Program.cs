using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crypt
{
    class Program
    {
        static void Main(string[] args)
        {
            // Исходная строка
            string text = "Русский текст... English text...";
            Console.WriteLine("Original text: {0}\n", text);

            string str = null;                                      // Строка результата
            byte[] gamma;                                           // Гамма
            byte[] crypt = new byte[text.Length];                   // Зашифрованный массив байтов
            byte[] decrypt = new byte[text.Length];                 // Расшифрованный массив байтов
            byte[] txt = Encoding.GetEncoding(1251).GetBytes(text); // Преобразование из исходной строки в массив байтов

            // Случайное значение гаммы
            Random rnd = new Random();
            gamma = new byte[1];
            rnd.NextBytes(gamma);
            foreach (byte a in gamma)
            {
                Console.WriteLine("Gamma: {0}\n", a);
            }

            // Гаммирование и сдвиг
            for (int i = 0; i < text.Length; i++)
            {
                crypt[i] = (byte)(gamma[0] ^ txt[i]);
                crypt[i] = (byte)(crypt[i] << 3 | crypt[i] >> 5);
            }

            // Преобразование из массива байтов в строку
            //str = Encoding.GetEncoding(1251).GetString(crypt);
            // Отображение зашифрованного текста в двоичном виде
            Console.WriteLine("Cipher text:\n");
            foreach (var i in crypt)
            {
                Console.WriteLine("{0}\n", Convert.ToString(i, 2));
            }

            // Расшифровка
            for (int i = 0; i < text.Length; i++)
            {
                crypt[i] = (byte)(crypt[i] << 5 | crypt[i] >> 3);
                decrypt[i] = (byte)(gamma[0] ^ crypt[i]);
            }

            // Отображение расшифрованного текста
            str = Encoding.GetEncoding(1251).GetString(decrypt);
            Console.WriteLine("Decipher text: {0}\n", str);

            Console.ReadKey();
        }
    }
}
