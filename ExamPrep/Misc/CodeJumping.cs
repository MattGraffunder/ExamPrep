using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamPrep.Chapter_1
{
    public static class CodeJumping
    {
        public static void Jump()
        {
            Console.WriteLine("Starting");
            Console.WriteLine("Press Key to Check if it is a Vowel...");
            
            ConsoleKeyInfo closeKey = Console.ReadKey(true);

            if (closeKey.Key == ConsoleKey.Enter)
            {
                return;
            }

            if (closeKey.Key == ConsoleKey.K)
            {
                goto jumpLabel;
            }

            if (IsVowel(closeKey.KeyChar))
            {
                Console.WriteLine(closeKey.KeyChar + " is a vowel!");
            }
            else
            {
                Console.WriteLine(closeKey.KeyChar + " is a consonant.");
            }

        jumpLabel:
            Console.WriteLine("Code Jumped");

            Console.ReadKey(true);
        }

        static void DisplayWaitingForSomething()
        {
            Console.Write("");
        }

        static bool IsVowel(char c)
        {
            switch (c)
            {
                case 'a':
                case 'e':
                case 'i':
                case 'o':
                case 'u':
                    return true;
                default:
                    return false;
            }
        }
    }
}
