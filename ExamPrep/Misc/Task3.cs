using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamPrep.Chapter_1
{
    class Task3
    {

    }

    class OtherDelegateTesting
    {
        public delegate string ShowClass(ChildClass parentClass);
        public delegate ParentClass GetClass();
        //public delegate string 

        public void DelegateTestingMethod()
        {
            GetClass c = GetTestClass;
            ShowClass s = GetClassName;
        }

        private ChildClass GetTestClass()
        {
            return new ChildClass();
        }

        private string GetClassName(ParentClass parentClass)
        {
            throw new NotImplementedException();
        }

        //private string GetClassName(ChildClass classToName)
        //{
        //    return classToName.ClassName;
        //}

    }

    class ParentClass
    {
        public virtual string ClassName
        {
            get
            {
                return "Parent Class";
            }
        }
    }

    class ChildClass : ParentClass
    {
        public override string ClassName
        {
            get
            {
                return "Child Class";
            }
        }
    }
}
