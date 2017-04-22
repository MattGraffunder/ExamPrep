using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamPrep.Chapter_2
{
    public class EnumerableTesting
    {
        HamManager hamManager = new HamManager(3);

        public void EnumerateHams()
        {
            IEnumerator<Ham> hamEnumerator = hamManager.GetEnumerator();

            foreach (Ham ham in hamManager)
            {
                Console.WriteLine("I got ham {0}!", ham.HamNumber);
            }
        }
    }

    public class HamManager : IEnumerable<Ham>
    {
        List<Ham> _hams = new List<Ham>();

        public HamManager(int numberOfHams)
        {
            for (int i = 0; i < numberOfHams; i++)
            {
                _hams.Add(new Ham(i));
            }
        }

        public IEnumerator<Ham> GetEnumerator()
        {
            for (int i = 0; i < _hams.Count; i++)
            {
                yield return _hams[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class Ham
    {
        public int HamNumber {get;set;}

        public Ham(int hamNumber)
        {
            HamNumber = hamNumber;
        }
    }
}
