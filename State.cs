using System;
using System.Collections.Generic;
using System.Linq;

namespace PushPullBlocksGame
{
    public enum GenerateStateStatus
    {
        LeafState,
        NoClickProvided,
        CellNotClicable,
        CellOutOfBounds,
        NoStateGenerated,
        Success,
    }
    public class State
    {
        public int Depth { get; set; }
        public string Action;
        public Board board;
        public HashSet<State> derviedStates { get; set; }
        public bool IsGoal { get; set; }
        public bool IsLeaf { get; set; }
        public State parent;
        public int cost { get; set; }

        public static bool CheckGoal(State state)
        {
            return state.board.IsWin();
        }
        public static bool CheckLeaf(State state)
        {
            State Temp = new State(Board.Clone(state.board),state.parent,state.IsGoal,state.IsLeaf,state.derviedStates);

            return State.GenerateStates(Temp) == null;
        }


        
        public State(Board board,State parent =null,bool IsGoal = false,bool IsLeaf = false,HashSet<State> derivedStates = null,int Depth = 0)
        {
            this.Depth = Depth;
            this.board = board;
            this.parent = parent;
            this.IsGoal = IsGoal;
            this.IsLeaf = IsLeaf;
            this.Action = null;
            this.cost = 0;


            if(derviedStates == null)
            {
                this.derviedStates = new HashSet<State> ();
            } else
            {
                this.derviedStates = derivedStates;
            }
        }


        public static State Clone(State state)
        {
            if (state == null) return null;
            Board clonedBoard = Board.Clone(state.board);
            return new State(clonedBoard, parent: null, IsGoal: state.IsGoal, IsLeaf: state.IsLeaf, derivedStates: null);
        }

        public static State NextFirstState(State state)
        {
            if (state.derviedStates == null) return null;
            if (state.derviedStates.Count == 0) return null;
            return state.derviedStates.First();
        }


        public GenerateStateStatus GenerateState(Click click)
        {

            if(this.IsLeaf) return GenerateStateStatus.LeafState;
            if (click == null) return GenerateStateStatus.NoClickProvided;
            if (!Board.IsCellInBound(this.board, click.Location)) return GenerateStateStatus.CellOutOfBounds;
            if (!Board.IsCellClickable(this.board, click.Location)) return GenerateStateStatus.CellNotClicable;

            Board newBoard = Board.Clone(this.board);
            newBoard.ApplyClick(click);

            if(this.board.Equals(newBoard))
            {
                State Temp = new State(newBoard);
                if(State.GenerateStates(Temp) == null)
                {
                    this.IsLeaf = true;
                    this.IsGoal = State.CheckGoal(this);
                    this.Action = "click  " + click.Location;
                    this.Depth = this.parent.Depth + 1;
                    return GenerateStateStatus.LeafState;
                } else
                {
                    this.Action = "click " + click.Location;
                    return GenerateStateStatus.NoStateGenerated;
                }
            }

            State GeneratedState = new State(newBoard);
            GeneratedState.parent = this;
            GeneratedState.IsGoal = State.CheckGoal(GeneratedState);
            GeneratedState.IsLeaf = State.CheckLeaf(GeneratedState);

            GeneratedState.Action = "click " + click.Location;
            this.derviedStates.Add(GeneratedState);


            return GenerateStateStatus.Success;
        }

        
        public int ComputePathCost2()
        {
            if (parent == null) return 0;

            int modifiedCellsCount = 0;
            for(int i = 0;i  < this.board.Rows; i++)
            {
                for(int j = 0; j < this.board.Cols; j++)
                {
                    if (this.board.grid[i,j].Type != this.parent.board.grid[i,j].Type)
                    {
                        modifiedCellsCount++;
                    }
                }
            }
            return this.parent.cost + (modifiedCellsCount / 2) ;
        }
        public int ComputePathCost()
        {
            //return ComputePathCost2();
            if (this.parent == null) return 0;
            return parent.cost + 1;
        }
        public int ComputeHeuristicCost()
        {
            List<Location> blocksLocations = this.board.GetBlockCellsLocation();
            List<Location> goalsLocations = this.board.GetGoalCellsLocation();

            int cost = 0;
            foreach (Location blockCell in blocksLocations)
            {
                //Console.WriteLine($"\t\t\t\t\tblock Cell : {blockCell}");
                int minDist = int.MaxValue;
                foreach (Location goalCell in goalsLocations)
                {
                    //Console.WriteLine($"\t\t\t\t\t\tgoal Cell : {goalCell}");
                    int dist = Math.Abs(blockCell.Row - goalCell.Row)
                             + Math.Abs(blockCell.Col - goalCell.Col);
                    if (dist == 0)
                    {
                        minDist = dist;
                        break;
                        //continue;
                    }
                    if (this.board.IsGoalCellClosed(goalCell))
                    {
                        continue;
                    }
                    if (dist < minDist) minDist = dist;
                }
                //Console.WriteLine($"\t\t\t\t\tDistance between block cell {blockCell} and nearest goal is {minDist}");
                cost += minDist;
            }
            return cost; 
        }
        public static HashSet<State> GenerateStates(State state)
        {
            if(state ==null || state.IsLeaf || state.IsGoal)
            {
                return null;
            }

            List<Location> ClickableBlocksLocation = state.board.GetClickableBlocksLocation();


            if (ClickableBlocksLocation == null || ClickableBlocksLocation.Count == 0)
            {
                state.IsLeaf = true;
                state.IsGoal = State.CheckGoal(state);
                return null;
            }

            HashSet<State> GeneratedStatesResult = new HashSet<State>();

            foreach (Location clickableblockLocation in ClickableBlocksLocation)
            {
                Click click = new Click(clickableblockLocation);
                Board ClonedBoard = Board.Clone(state.board);

                ClonedBoard.ApplyClick(click);

                State GeneratedState = new State(ClonedBoard, state);

                

                if (!state.Equals(GeneratedState))
                {
                    GeneratedState.Action = "click " + click.Location;
                    GeneratedState.Depth = state.Depth + 1;
                    //GeneratedState.cost = state.cost + GeneratedState.ComputeCost();
                    GeneratedStatesResult.Add(GeneratedState);
                }
            }

            if (GeneratedStatesResult.Count == 0)
            {
                state.IsLeaf = true;
                state.IsGoal = State.CheckGoal(state);
                return null;
            }

            return GeneratedStatesResult;
        }
       
       
        public override string ToString()
        {
            string StateStr = "";
            StateStr += "\n\t\t\t\t\t\tDepth : " + Depth.ToString();
            StateStr += "\n\t\t\t\t\t\tAction : " + (this.Action != null ? Action: "No Action Generate This State");
            StateStr += "\n\t\t\t\t\t\tIs Goal State : " + (IsGoal ? "YES" : "NO");
            StateStr += "\n\t\t\t\t\t\tIs Leaf State : " + (IsLeaf ? "YES" : "NO");
            StateStr += "\n\t\t\t\t\t\tHas Parent : " + (parent == null ? "NO" : "YES");
            StateStr += "\n\n\t\t\t\t\t\tBoard";
            StateStr += ("\n\n" + this.board.ToString()+ "\n\n");

            return StateStr;
        }

        public bool Equals(State obj)
        {
            if (ReferenceEquals(obj, null)) return false;
            if (ReferenceEquals(this, obj)) return true;

            return Equals(this.board, obj.board);
        }
        public override bool Equals(object obj)
        {
            return Equals(obj as State);
        }
        public override int GetHashCode()
        {
            unchecked
            {
                return this.board?.GetHashCode() ?? 0;
            }
        }

    }
}