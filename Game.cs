using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Threading;
namespace PushPullBlocksGame
{

    public static class Game
    {
       
       
        private enum Player
        {
            Human = 1,
            Computer =2
        }

        private static Player ReadWhoWillPlay()
        {
            int WhoWillPlay = (int)Player.Computer;

            Console.WriteLine("\tChoose Play Mode");
            Console.WriteLine("\tEnter [1] To You Play");
            Console.WriteLine("\tEnter [2] To Computer Play");
            Console.Write("\n");


            while(true)
            {
                if(!int.TryParse(Console.ReadLine(), out WhoWillPlay))
                {
                    Console.Write("Invalid Input ,Enter Number : ");
                    continue;
                }
                if (WhoWillPlay > 2 || WhoWillPlay < 1)
                {
                    Console.Write("Invalid Input ,Enter [1] or [2] : ");
                    continue;
                }
                return (Player)WhoWillPlay;
            }
        }
        private static int ReadLevel()
        {
            int Level = 1;
            Console.Write("\tChoose Level :  ");

            while (true)
            {
                if(!int.TryParse(Console.ReadLine(), out Level))
                {
                    Console.Write("\tYou Entered UnValid Level , Enter Number :  ");

                }
                else if(Level < 0 || Level > Global.LevelsCount)
                {
                    Console.Write("\tthis Level Is Not Exists , Enter Another :  ");
                } else
                {
                    break;
                }
            }

            return Level;
        }

        private static void PrintLevels()
        {
            Console.WriteLine("\n\n\tLevels\n");
            Console.Write('\t');
            for (int i = 1; i <= Global.LevelsCount; i++)
            {
                Console.Write($"[ {i} ]");
                Console.Write('\t');
                if(i % Global.NumberOfLevelsPerLine  == 0)
                {
                    Console.WriteLine("\n");
                    Console.Write('\t');
                }
            }
            Console.Write("\n\n");
        }

        private static void PrintGameStates(State rootState)
        {
            Console.WriteLine("\n\n\t\t\t\t\t\t Game States \n\n");

            State Temp = rootState;
            int Count = 1;
            while (Temp != null) {
                Console.Write($"\n\t\t\t\t\t\t\t{Count}");
                Console.WriteLine(Temp);
                Temp = State.NextFirstState(Temp);
                Count++;
            }
            
        }

        private static void PlayHuman(int Level)
        {
            Console.Clear();
            Console.Write($"\n\n\t\t\t\t\t\t LEVEL : [ {Level} ]\n");


            State initialState = new State(BoardsFileReader.GetBoard(Level));
            State CurrentState = initialState;

            Console.WriteLine(CurrentState);
            while (CurrentState != null && (!CurrentState.IsLeaf))
            {
                int row = 0;
                Console.Write("\t Enter Row : ");
                int.TryParse(Console.ReadLine(), out row);
                int col = 0;
                Console.Write("\t Enter Col : ");
                int.TryParse(Console.ReadLine(), out col);

                Click click = new Click(row - 1, col - 1);
                GenerateStateStatus status = CurrentState.GenerateState(click);
                switch (status)
                {
                    case GenerateStateStatus.Success:
                        Console.WriteLine("\n\t\tNew State Generated");
                        CurrentState = State.NextFirstState(CurrentState);
                        break;
                    case GenerateStateStatus.LeafState:
                        Console.WriteLine($"\n\t\tGame Over");
                        break;
                    case GenerateStateStatus.CellOutOfBounds:
                        Console.WriteLine($"\n\t\tNo Cell In Location {click.Location} :  Is Out Of Bound");
                        break;
                    case GenerateStateStatus.CellNotClicable:
                        Console.WriteLine($"\n\t\tCell In Location  {click.Location} Is Not Clicable");
                        break;
                    case GenerateStateStatus.NoStateGenerated:
                        Console.WriteLine($"\n\t\tClick In Cell {click.Location} Generae No State");
                        break;
                }
                Console.WriteLine(CurrentState);
            }



            string GameStatus = "LOSE :(";
            Console.Beep();
            Console.Beep();
            if (CurrentState == null)
            {
                GameStatus = "LOSE :(";
            }
            else
            {
                if (CurrentState.IsLeaf)
                {
                    if (CurrentState.IsGoal)
                    {
                        GameStatus = "WIN :)";
                    }
                    else
                    {
                        GameStatus = "LOSE :(";
                    }
                }
            }

            

            Console.WriteLine("\x1b[3J");
            Console.Clear();

            Console.Write($"\n\n\t\t\t\t\t\tLEVEL : [ {Level} ]\n");
            Console.Write($"\n\n\t\t\t\t\t\tResult : You {GameStatus}");
            PrintGameStates(initialState);
        }

        private static void _PrintActionPath(State state)
        {
            if (state == null) return;

            Stack<State> stack = new Stack<State>();

            while (state != null)
            {
                stack.Push(state);
                state = state.parent;
            }

            int Counter = 1;
            Console.WriteLine("\t\t\t\tActions : ");
            foreach (State st in stack)
            {
                if (st.Action == null) continue;
                Console.WriteLine($"\t\t\t\t[ {Counter} ] : {st.Action} ");
                Counter++;
            }
        }
        private static void _PrintPath(State state)
        {
            if (state == null) return;

            Stack<State> stack = new Stack<State>();

            while (state != null)
            {
                stack.Push(state);
                state = state.parent;
            }

            int Counter = 1;
            foreach (State st in stack)
            {
                Console.Write("\n\n\t\t\t\t\t#####################");
                Console.Write("\n\t\t\t\t\t\t\t" + Counter);
                Console.WriteLine(st.ToString());
                Console.Write("\t\t\t\t\t#####################\n\n");
                Counter++;
            }
        }


        private static void PlayComputer(int Level)
        {
            

            Console.WriteLine("\x1b[3J");
            Console.Clear();

            //Console.WriteLine("\n\n\t\tThis Feature Is Not Provided ");
            //return;

            State initialState = new State(BoardsFileReader.GetBoard(Level));
            Console.WriteLine("\n\t\t\t\t\tLevel = " + Level);
            Console.WriteLine("\n\t\t\t\t\t###############################");
            Console.WriteLine(initialState.ToString());
            Console.WriteLine("\t\t\t\t\t###############################");

            Console.Write("\n\t\t\t\tChoose Algorithm :   ");
            Console.Write("\n\t\t\t\t[1] DFS , [2] BFS ,");
            Console.Write("\n\t\t\t\t[3] UCS , [4] Hill Climbing , [5] A*\n\t\t\t\t");

            State GoalNode = null;
            if (int.TryParse(Console.ReadLine(), out int algo))
            {
                if (algo == 1)
                {
                    GoalNode = Algorithm.DFS(initialState);
                }
                else if (algo == 2)   
                {
                    GoalNode = Algorithm.BFS(initialState);
                }
                else if (algo == 3)
                {
                    GoalNode = Algorithm.UCS(initialState);
                }
                else if (algo == 4)
                {
                    GoalNode = Algorithm.HillClimbing(initialState);
                }
                else if (algo == 5)
                {
                    GoalNode = Algorithm.AStar(initialState);
                }else
                {GoalNode = Algorithm.DFS(initialState);

                }

            }
            else
            {
                GoalNode = Algorithm.BFS(initialState);
            }
            
            

            


            if (GoalNode == null)
            {
                Console.WriteLine("\n\t\t\tNo Way to Win This Game Using This Algorithm!\n");
            }
            else
            {

                _PrintPath(GoalNode);
                Console.WriteLine();
                _PrintActionPath(GoalNode); 
               
            }
        }
        private static void OpenMenue()
        {
            
            PrintLevels();
            int Level = ReadLevel();
            Player player = ReadWhoWillPlay();

            switch(player)
            {
                case Player.Human:
                    PlayHuman(Level);
                    break;
                case Player.Computer:
                    PlayComputer(Level);
                    break;
                default:
                    PlayComputer(Level);
                    break;
            }
        }
        public static void Open()
        {
            string RePlay = "NO";
            while (true)
            {
                Console.Clear();
                OpenMenue();

                Console.Write("\n\n\t\tDo You Want To Play Again ? YES/NO :  ");
                RePlay = Console.ReadLine();
                if(RePlay.ToUpper() == "NO" || RePlay.ToUpper() == "N")
                {
                    break;
                }
            }
        }


    }
}