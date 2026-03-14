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

        //public static int O(int Index)
        //{
        //    return Index - 1;
        //}

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


            // Test Heuristic Function
            //Board board = BoardsFileReader.GetBoard(12);
            //foreach (Board b in Global.Boards)
            //{
            //    State initialState = new State(b);
            //    Console.WriteLine(initialState);
            //    Console.WriteLine("\t\t\t\t\tCost = " + initialState.ComputeHeuristicCost());

            //    Console.WriteLine("**************************************");
            //    Console.WriteLine("**************************************");
            //}





            //int Counter = 1;
            //BoardsFileReader.GetBoard(41);

            //foreach (Board board in Global.Boards)
            //{
            //    Console.WriteLine("**************************************");
            //    Console.WriteLine("\t\t\t\t\t\tLevel " + Counter);
            //    Console.WriteLine(board);
            //    State initialState = new State(board);
            //    State goalStateUsingDFS = Algorithm.DFS(initialState);
            //    State goalStateUsingBFS = Algorithm.BFS(initialState);
            //    State goalStateUsingUCS = Algorithm.UCS(initialState);
            //    State goalStateUsingHillClimbing = Algorithm.HillClimbing(initialState);
            //    State goalStateUsingAStar = Algorithm.AStar(initialState);

            //    Console.WriteLine("\n\t\t\t\t\t\tDFS");
            //    PrintActionPath(goalStateUsingDFS);
            //    Console.WriteLine("\n\t\t\t\t\t\tBFS");
            //    PrintActionPath(goalStateUsingBFS);
            //    Console.WriteLine("\n\t\t\t\t\t\tUCS");
            //    PrintActionPath(goalStateUsingUCS);
            //    Console.WriteLine("\n\t\t\t\t\t\tHill Climbing");
            //    PrintActionPath(goalStateUsingHillClimbing);
            //    Console.WriteLine("\n\t\t\t\t\t\tA Star");
            //    PrintActionPath(goalStateUsingAStar);
            //    Console.WriteLine("**************************************");
            //    Counter++;
            //}




        }
    }
}
