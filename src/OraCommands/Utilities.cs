
namespace Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    internal static class Utilities
    {
        internal static void ShowMenu(String title,string[] collection)
        {
            Console.Clear();

            int width = Console.WindowWidth - 4;
            int i = 0;
            Console.WriteLine("+" + new string('-', width - 2) + "+");
            Console.WriteLine("|" + CenterText(title, width - 2) + "|");
            Console.WriteLine("|" + new string(' ', width - 2) + "|");
            foreach (var item in collection)
            {
                i++;
                Console.WriteLine("| " + AlignLeftText(i + ")  " + item, width - 3) + "|");   
            }
            Console.WriteLine("| " + AlignLeftText("0. Exit", width - 3) + "|");
            Console.WriteLine("|" + new string(' ', width - 2) + "|");
            Console.WriteLine("+" + new string('-', width - 2) + "+");
        }

        internal static string? Scanf(string message)
        {
            Console.Write("\t[ " + message + " ]\t");
            return Console.ReadLine();
        }

        internal static void Pause()
        {
            Console.WriteLine();
            Console.WriteLine("\tPress [Enter] to continue...");
            Console.ReadLine();
        }

        internal static string? Continue()
        {
            string? option = "NO";
            Console.Write("Continue ? YES/NO > ");
            option = Console.ReadLine();
            return option;
        }

        internal static void PrintMessage(string message)
        {
            Console.WriteLine();
            Console.WriteLine("\t+--------------------------------------+");
            Console.WriteLine("\t|" + message);
            Console.WriteLine("\t+--------------------------------------+");
            Console.WriteLine();
        }

        static string CenterText(string text, int width)
        {
            if (text.Length >= width)
                return text.Substring(0, width); // Truncate if text is longer than width

            int spaces = width - text.Length;
            int padLeft = spaces / 2;
            int padRight = spaces - padLeft;

            return new string(' ', padLeft) + text + new string(' ', padRight);
        }

        static string AlignLeftText(string text, int width)
        {
            if (text.Length >= width)
                return text.Substring(0, width); // Truncate if text is longer than width

            int spaces = width - text.Length;
            return text + new string(' ', spaces);
        }

        internal static Employee ScanEmployee()
        {
            var e = new Employee();
            string[] labels = {
                "Employee ID: "
                ,"First name: "
                ,"Last name: "
                ,"Email: "
                ,"Salary: "
                
            };
            string?[] fields = new string[labels.Length];
            for (var i = 0; i < labels.Length; i++)
            {
                fields[i] = Scanf(labels[i]);
            }
            if(!string.IsNullOrEmpty(fields[0]))
            	e.Id = Convert.ToInt32(fields[0]);
            e.FirstName = fields[1];
            e.LastName = fields[2];
            e.Email = fields[3];
            e.Salary = fields[4];
            return e;
        }
    }
}
