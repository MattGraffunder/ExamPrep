using System;
namespace ExamPrep.Work
{
    public class FirstClass
    {
        int _TestInt = 0;

        public int Testing
        {
            get
            {
                return _TestInt;
            }
            set
            {
                _TestInt = value;
            }
        }

        public FirstClass() { }

        public string Something()
        {
            throw new ArgumentException();
        }
    }
}
