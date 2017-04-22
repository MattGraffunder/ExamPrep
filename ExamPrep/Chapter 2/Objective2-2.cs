using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamPrep.Chapter_2
{
    public class SomethingNumbery
    {
        public SomethingNumbery(decimal amount)
        {
            AmountOfThings = amount;
        }

        public decimal AmountOfThings { get; set; }

        public override string ToString()
        {
            return string.Format("There are {0} things", AmountOfThings);
        }

        public static implicit operator decimal(SomethingNumbery numbery)
        {
            return numbery.AmountOfThings;
        }

        public static explicit operator int(SomethingNumbery numbery)
        {
            return (int)numbery.AmountOfThings;
        }
    }

    public class SomethingDynamic
    {
        dynamic something = new DynamicTest();

        public void DoSomethingDynamic()
        {
            //Action a = delegate() { Console.WriteLine(""); };
            something.HamTown = (Action)delegate() { Console.WriteLine("This Is HAM-TOWN!"); };

            something.HamTown();
            //dynamic result = something.HamTime(4);

            //Console.WriteLine(result);
        }

        public void DoSomethingExpandy()
        {
            ExpandingObjectTester expandy = new ExpandingObjectTester();

            try
            {
                Console.WriteLine(expandy.Expando.ExpandedThing);
            }
            catch (RuntimeBinderException)
            {
                Console.WriteLine("Could not find ExpandedThing");
            }

            expandy.ExpandoTest();

            Console.WriteLine(expandy.Expando.ExpandedThing);

        }

        DynamicTest test = new DynamicTest();
        //test.A();
    }

    public class DynamicTest : DynamicObject
    {
        private Dictionary<string, Action> actions = new Dictionary<string, Action>();

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            if (value is Action)
            {
                actions[binder.Name] = (Action)value;

                return true;
            }
            else return false;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            if (actions.Keys.Contains(binder.Name))
            {
                result = actions[binder.Name];
                return true;
            }
            else
            {
                result = null;
                return false;
            }
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            result = null;

            if (actions.Keys.Contains(binder.Name))
            {
                try
                {
                    Console.WriteLine(string.Format("Invoking {0}", binder.Name));
                    actions[binder.Name].Invoke();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            else
            {
                Console.WriteLine(string.Format("{0} does not exist", binder.Name));
            }

            return false;
        }
    }

    public class ExpandingObjectTester
    {
        dynamic _expando = new ExpandoObject();

        public dynamic Expando { get; set; }

        public ExpandingObjectTester()
        {
            Expando = _expando;
        }
        
        public void ExpandoTest()
        {            
            _expando.ExpandedThing = "Expanded";
        }
    }
}