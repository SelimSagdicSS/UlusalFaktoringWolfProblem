using System;
using System.Collections.Generic;
using System.Linq;

namespace WolfProblem
{
    class Program
    {

        static char[] ids = new char[5] { '1', '2', '3', '4', '5' };
        static void Main(string[] args)
        {
            ConsoleKeyInfo cki;


            bool esc = false;

            while (!esc)
            {
                bool enter = false;
                string input = "";
                Console.WriteLine("\nProgramı sonlandırmak için ESC tuşuna, işlemi tamamlamak için ENTER tuşuna basınız. \n");
                Console.Write("Lütfen dizi boyutunu giriniz : ");

                ConsoleKeyInfo key;
                string inputvalue = "";
                do
                {
                    key = Console.ReadKey(true);
                    if (key.Key != ConsoleKey.Backspace)
                    {
                        double val = 0;
                        bool _x = double.TryParse(key.KeyChar.ToString(), out val);
                        if (_x)
                        {
                            inputvalue += key.KeyChar;
                            Console.Write(key.KeyChar);
                        }
                    }
                    else
                    {
                        if (key.Key == ConsoleKey.Backspace && inputvalue.Length > 0)
                        {
                            inputvalue = inputvalue.Substring(0, (inputvalue.Length - 1));
                            Console.Write("\b \b");
                        }
                    }
                }
                // Stops Receving Keys Once Enter is Pressed
                while (key.Key != ConsoleKey.Enter);
                Console.WriteLine();

                var arrayLengthInput = inputvalue.Trim();
                int arrayLength;
                int inputLength = 0;
                Int32.TryParse(arrayLengthInput, out arrayLength);
                if (arrayLength > 4 && arrayLength < (2 * Math.Pow(10, 5) + 1))
                {
                    Console.WriteLine("Lütfen diziyi boşluklu bir şekilde ve boyut adedince giriniz.");
                    while (!enter)
                    {
                        cki = Console.ReadKey(true);


                        if (ids.Contains(cki.KeyChar) || (cki.Key == ConsoleKey.Spacebar && input != ""))
                        {
                            if (cki.Key == ConsoleKey.Spacebar && ids.Contains(input[input.Length - 1]) && inputLength < arrayLength)
                            {
                                input += cki.KeyChar;
                                Console.Write(cki.KeyChar);
                            }
                            else if (ids.Contains(cki.KeyChar) && (input == "" || input[input.Length - 1] == ' ') && inputLength < arrayLength)
                            {
                                input += cki.KeyChar;
                                inputLength++;
                                Console.Write(cki.KeyChar);
                            }
                            else
                            {
                                Beep();
                            }

                        }

                        if (cki.Key == ConsoleKey.Backspace)
                        {
                            if (input != "")
                            {
                                Console.Write("\b \b");
                                input = input.Remove(input.Length - 1, 1);
                                if (input == "" || input[input.Length - 1] == ' ') inputLength--;
                            }
                        }

                        if (cki.Key == ConsoleKey.Enter)
                        {
                            if (inputLength == arrayLength)
                            {
                                GetTheWolf(input);
                                enter = true;
                            }
                            else Beep();
                        }
                        if (cki.Key == ConsoleKey.Escape)
                        {
                            enter = true;
                            esc = true;
                        }

                    }
                    Console.WriteLine();
                }
                else
                {
                    Beep();
                }
            }

        }

        static void GetTheWolf(string input)
        {
            var wolfs = input?.Split(' ')?.Select(Int32.Parse)?.ToList();

            IEnumerable<int> top = wolfs
            .GroupBy(i => i)
            .OrderByDescending(g => g.Count())
            .ThenBy(c => c.Key)
            .Take(5)
            .Select(g => g.Key);
            Console.WriteLine();
            Console.WriteLine($"En çok tespit edilen kurt türü : {top.First()}");
        }

        static void Beep()
        {
            Console.Beep(2000, 500);
        }
    }
}
