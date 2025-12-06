using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourierServiceConsApp.Presentation
{
    public static class InputValidator
    {
        public static int ReadInt(string message)
        {
            Console.Write(message);
            string? input = Console.ReadLine();

            while (!int.TryParse(input, out int result) || result < 0)
            {
                Console.Write("Invalid number. Try again: ");
                input = Console.ReadLine();
            }
            return Convert.ToInt32(input);
        }
    }
}
