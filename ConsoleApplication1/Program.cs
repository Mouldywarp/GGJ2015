using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTN
{
    abstract class Test
    {
        protected Object _o;
        public Type valueType { get { return _o.GetType(); } }
        public bool valueIs<T>() { return(_o is T); }
    }

    class Test<T> : Test
    {
        public Test(T value) { _o = value; }

        //T _t;
        public T value { get { return (T)_o; } }
    }

    class Bob { public virtual string Msg() { return "Is bob!"; } }
    class BobChild : Bob { public override string Msg() { return "Is bobchild!"; } }


    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Woild!");
            /*
            Datum[] data = new Datum[2];
            data[0] = new Datum<int>("Health", 5);
            data[1] = new Datum<string>("Name", "Ron");


            foreach (Datum datum in data)
            {
                if (datum.HasID("Name"))
                {
                    if (datum.GetValue<int>() < 10) Console.WriteLine("Unhealthy!");
                    else Console.WriteLine("Health Ok!");
                }
            }
             * 
             * */

            Test[] tests = new Test[4];
            tests[0] = new Test<int>(5);
            tests[1] = new Test<float>(5);
            tests[2] = new Test<string>("Cheese");
            tests[3] = new Test<Bob>(new BobChild());


            for (int i = 0; i < tests.Length; i++)
            {
                Console.WriteLine("Iter " + i);
                // If it's an int
                int poo = 5;
                if (tests[i].valueIs<int>())
                {
                    Test<int> test = (Test<int>)tests[i];
                    Console.WriteLine("Test is an int, value is " + test.value);
                }

                // If it's a float
                float wee = 5.3f;
                if (tests[i].valueIs<float>())
                {
                    Test<float> test = (Test<float>)tests[i];
                    Console.WriteLine("Test is a float, value is " + test.value);
                }

                // If it's a string
                string fart = "Hi";
                if (tests[i].valueIs<string>())
                {
                    Test<string> test = (Test<string>)tests[i];
                    Console.WriteLine("Test is a string, value is " + test.value);
                }

                // If it's a bob
                Bob bob = new Bob();
                if (tests[i].valueIs<Bob>())
                {
                    Console.WriteLine("Got me a bob!");
                    //Test<Bob> test = (Test<Bob>)tests[i];
                    //Console.WriteLine("Test is a bob, value is " + test.value.Msg());
                }

                /*
                // If it's a bobchild
                if (tests[i].valueIs<BobChild>())
                {
                    Test<BobChild> test = (Test<BobChild>)tests[i];
                    Console.WriteLine("Test is a bob, value is " + test.value.Msg());
                }
                 * */

                Console.Write("\n\n");
            }

        }


        void RuleBasedIteration(/* database, rules */)
        {
            // For every rule in rules

            // bindings = rule.ifClause.GetBinding(database)
            // Returns bindings from rules "if clause". This is a list of wildcards




        }
    }
}
