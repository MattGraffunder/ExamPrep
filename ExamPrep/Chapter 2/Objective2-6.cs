using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamPrep.Chapter_2
{
    public class WeakTesting
    {
        int startInt = 1;
        WeakReference _weakArray;

        WeakReference<List<int>> weakInt = new WeakReference<List<int>>(new List<int>());

        public List<int> GetList()
        {
            if (_weakArray == null)
            {
                _weakArray = new WeakReference(GetNewArray());
            }

            if (_weakArray.Target == null)
            {
                _weakArray.Target = GetNewArray();
            }

            return (List<int>)_weakArray.Target;
        }

        private List<int> GetNewArray()
        {
            List<int> bigArray = new List<int>(1024);
            bigArray.Add(startInt);

            startInt++;

            return bigArray;
        }
    }
}
