using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamPrep.Chapter_1
{
    public class JumpTesting
    {
        public static void JumpTest(int x)
        {
            if (x % 2 == 0)
            {
                Console.WriteLine("{0} is Even", x);
            }
            else
            {
                Console.WriteLine("{0} is Odd", x);
            }

            if (x == 5)
            {
                goto superJump;
            }

            return;

        superJump:
            Console.WriteLine("Jumped because of {0}!", x);
        }
    }

    public class SwitchTesting
    {
        public static void SwitchTest(int x)
        {
            switch (x)
            {
                case 1:
                case 2:
                    Console.WriteLine("{0} is a one or two.", x);
                    break;
                case 3:
                    Console.WriteLine("{0} should be a five.", x);
                    goto case 5;
                case 5:
                    Console.WriteLine("{0} is a five.", x);
                    break;
                default:
                    Console.WriteLine("I don't know what to do with {0}",x );
                    break;
            }
        }
    }
}
