using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections;
public class Person
{
    public int Id { get; set; }
    public string Name { get; set; }
   
}
namespace PushPullBlocksGame
{
    
    internal class Program
    {

    
        public static void PrintActionPath(State state)
        {
            if (state == null) {
                Console.WriteLine("\t\t\t\tNo Path");
                return;
            };

            Stack<State> stack = new Stack<State>();

            while (state != null)
            {
                stack.Push(state);
                state = state.parent;
            }

            int Counter = 1;
            Console.WriteLine($"\t\t\t\tPath Length = {stack.Count - 1}");
            return;
            Console.WriteLine("\t\t\t\tActions : ");
            foreach (State st in stack)
            {
                if (st.Action == null) continue;
                Console.WriteLine($"\t\t\t\t[ {Counter} ] : {st.Action} ");
                Counter++;
            }
        }
        static void Main(string[] args)
        {
            Game.Open();
        }
    }
}
