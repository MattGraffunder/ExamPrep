using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamPrep.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    internal sealed class ChapterTestClassAttribute:Attribute
    {
        readonly int _chapter;

        internal int Chapter { get { return _chapter; } }

        internal ChapterTestClassAttribute(int chapter)
        {
            _chapter = chapter;
        }
    }
}