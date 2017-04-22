using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamPrep.Chapter_1
{
    public class DelegateTesting
    {
        public delegate int Calculate(int x, int y);
        public delegate void ConsoleDelegate();

        public delegate SuperClass CovarianceDelegate();
        public delegate void ContravarianceDelegate(DerivedClass d);

        public Func<int, int, int> FuncTest;

        public void CustomDelegate(int x, int y)
        {
            Calculate calc = Add;
            Console.WriteLine("Added: {0} + {1} = {2}", x, y, calc(x,y));
            
            calc = Subtract;
            Console.WriteLine("Subtracted: {0} - {1} = {2}", x, y, calc(x, y));
        }

        public void MulitcastDelegate()
        {
            ConsoleDelegate cd = ConsoleOne;
            cd += ConsoleTwo;

            Console.WriteLine("Multicast will invoke {0} methods", cd.GetInvocationList().GetLength(0));

            cd();
        }

        public void LambdaDelegate(int x, int y)
        {
            Calculate calc = (a, b) => (int)Math.Pow(a, b);
            Console.WriteLine("Powered: {0} ^ {1} = {2}", x, y, calc(x,y));
            
            calc = (a, b) => 2*x +3*y;
            Console.WriteLine("Mathed: 2*{0} + 3*{1} = {2}", x, y, calc(x, y));
        }

        public void CovarianceDelegateTest()
        {
            // Covariance is when the return type of delegate is less derived 
            // than the return type of the method.

            CovarianceDelegate cd = CovarianceTesting;

            SuperClass super = cd();
        }

        public void ContravarianceDelegateTest()
        {
            // Contravariance is when the parameter type of a delegate is more 
            // derived than the parameter of the delegate.

            ContravarianceDelegate cd = ContravarianceTesting;

            DerivedClass d = new DerivedClass();

            cd(d);
        }

        public void GenericDelegate(int x, int y)
        {
            FuncTest = Add;
            Console.WriteLine("Added: {0} + {1} = {2} Generically", x, y, FuncTest(x, y));

            FuncTest = Subtract;
            Console.WriteLine("Subtracted: {0} - {1} = {2} Generically", x, y, FuncTest(x, y));

        }

        public void Closure()
        {
            //Using Variables outside the scope of the delegate generates a closure
        }

        private int Add(int x, int y)
        {
            return x + y;
        }

        private int Subtract(int x, int y)
        {
            return x - y;
        }

        private void ConsoleOne()
        {
            Console.WriteLine("Console Method One");
        }

        private void ConsoleTwo()
        {
            Console.WriteLine("Console Method Two");
        }

        private DerivedClass CovarianceTesting()
        {
            return new DerivedClass();
        }

        private void ContravarianceTesting(SuperClass d)
        {

        }
    }

    public class SuperClass
    {

    }

    public class DerivedClass : SuperClass
    {

    }

    public class EventClass
    {
        private event EventHandler _OnCustomFire = delegate { };
        
        public event EventHandler OnFire = delegate { };

        public event EventHandler OnCustomFire
        {
            add
            {
                lock (_OnCustomFire)
                {
                    _OnCustomFire += value;
                }
            }
            remove
            {
                lock (_OnCustomFire)
                {
                    _OnCustomFire -= value;
                }
            }            
        }

        public void Fire()
        {
            Console.WriteLine("Firing Event");
            OnFire(this, new EventArgs());
        }

        public void CustomFire()
        {
            List<Exception> exceptions = new List<Exception>();
            
            //Console.WriteLine("Custom Firing Event");

            foreach (Delegate handler in _OnCustomFire.GetInvocationList())
            {
                try
                {
                    Console.WriteLine("Custom Firing Event");
                    handler.DynamicInvoke(this, EventArgs.Empty);
                }
                catch (Exception ex)
                {
                    exceptions.Add(ex);
                }
            }

            if (exceptions.Any())
            {
                throw new AggregateException(exceptions);
            }
        }
    }

    public class EventListener
    {
        public EventListener(EventClass eventClass)
        {
            eventClass.OnFire += Handler;

            //Event Exception Handling
            eventClass.OnCustomFire += (sender, e) => Console.WriteLine("First Event");
            eventClass.OnCustomFire += (sender, e) => { throw new Exception("Second Event"); };
            eventClass.OnCustomFire += (sender, e) => Console.WriteLine("Third Event");
        }

        public void Handler(object o, EventArgs e)
        {
            Console.WriteLine("Event Handled");
        }
    }

    public class EventController
    {
        public EventClass eventClass = new EventClass();
        EventListener listener;

        public void TestFire()
        {
            Console.WriteLine("Firing Event without Listeners");
            eventClass.Fire();

            listener = new EventListener(eventClass);

            Console.WriteLine("Firing Event with Listeners");
            eventClass.Fire();
        }

        public void CustomTestFire()
        {
            listener = new EventListener(eventClass);

            Console.WriteLine("Firing Custom Event");

            try
            {
                eventClass.CustomFire();
            }
            catch (AggregateException ex)
            {
                Console.WriteLine("Exception: {0}", ex.InnerExceptions.First().InnerException.Message);
            }
        }
    }
}
