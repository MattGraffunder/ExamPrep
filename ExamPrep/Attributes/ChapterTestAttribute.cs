using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamPrep.Attributes
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    internal sealed class ChapterTestAttribute : Attribute
    {       
        readonly int _objective;               

        internal int Objective { get { return _objective; } }

        public string Description { get; set; }

        internal ChapterTestAttribute(int objective)
        {
            _objective = objective;
        }
    }
}