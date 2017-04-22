using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamPrep.Chapter_2
{
    public class OptionalArgumentsTesting
    {
        public string optionalArgs(int notOptional, string someText = "Default", bool notNeeded = true)
        {
            string isneeded = notNeeded ? "Not Needed" : "Needed";

            return string.Format("The string {0}, is {1} {2} times.", someText, isneeded, notOptional);
        }
    }

    public class IndexerCollection<S> where S : Something
    {
        public ICollection<S> Somethings { get; private set; }

        public IndexerCollection()
        {
            Somethings = new List<S>();
        }

        public IndexerCollection(IEnumerable<S> starters)
            : this()
        {
            foreach (var thing in starters)
            {
                Somethings.Add(thing);
            }
        }

        public S this[int index]
        {
            get { return Somethings.ElementAt(index); }
        }

        public void MakeUniform(string thingToBe)
        {
            foreach (var thing in Somethings)
            {
                thing.TheThing = thingToBe;
            }
        }
    }

    public class Something
    {
        public virtual string TheThing { get; set; }

        public override string ToString()
        {
            return TheThing;
        }
    }

    public class Somethinger : Something
    {
        string thing;
        public override string TheThing
        {
            get
            {
                return thing;
            }
            set
            {
                thing = value + "er";
            }
        }
    }

    public static class SomethingExtension
    {
        public static string SomethingMore(this Something something)
        {
            return something.TheThing + " More";
        }
    }	
}