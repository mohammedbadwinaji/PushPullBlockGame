using System;
using System.Collections.Generic;
using System.Threading;
using PushPullBlocksGame;

namespace PushPullBlocksGame
{


    public sealed class Board : IEquatable<Board>
    {
        public Cell[,] grid { get; }
        public int Rows { get; }
        public int Cols { get; }


        public List<Location> GetGoalCellsLocation()
        {
            List<Location> goalCellsLocation = new List<Location>();
            for (int r = 0; r < this.Rows; r++)
            {
                for (int c = 0; c < this.Cols; c++)
                {
                    if (this.grid[r, c].Has(CellType.WhiteRounded))
                    {
                        goalCellsLocation.Add(new Location(r,c));
                    }
                }
            }

            return goalCellsLocation;
        }
        public bool IsGoalCellClosed(Location goalLoc)
        {
            Cell goalCell =  this.grid[goalLoc.Row, goalLoc.Col];
            if ( (int)goalCell.Type > (int)CellType.WhiteRounded )
            {
                return true;
            }
            return false;
        }
        public List<Location> GetBlockCellsLocation()
        {
            List<Location> blockCellsLocation = new List<Location>();
            for (int r = 0; r < this.Rows; r++)
            {
                for (int c = 0; c < this.Cols; c++)
                {
                    if ( (!this.grid[r,c].Is(CellType.Empty) )&&
                         (!this.grid[r, c].Is(CellType.WhiteRounded))
                        )
                    {
                        blockCellsLocation.Add(new Location(r, c));
                    }
                    
                }
            }

            return blockCellsLocation;
        }
        public static int GetWhiteRoundedCellCount(Board board)
        {
            int count = 0;
            for (int r = 0; r < board.Rows; r++)
            {
                for (int c = 0; c < board.Cols; c++)
                {
                    if(board.grid[r, c].Is(CellType.WhiteRounded))
                    {
                        count++;
                    }
                }
            }
            return count;
        }
        private void SetBoardDefaultValue()
        {
            for(int r = 0 ; r < Rows; r++)
            {
                for(int c = 0;  c < Cols; c++)
                {
                    this.grid[r, c] = new Cell((int)CellType.Empty);
                }
            }
        }
        public Board(int Rows, int Cols)
        {

            grid = new Cell[Rows, Cols];
            this.Rows = Rows;
            this.Cols = Cols;   
            SetBoardDefaultValue();
        }

        public static Board Clone(Board board)
        {
            if (board == null) return null;

            Board ClonedBoard = new Board(board.Rows, board.Cols);

            for (int r = 0; r < board.Rows; r++)
            {
                for(int c = 0; c < board.Cols; c++)
                {
                    ClonedBoard.grid[r, c] = new Cell(board.grid[r,c].Type);
                }
            }
            return ClonedBoard;
        }
        public List<Location> GetClickableBlocksLocation()
        {
            List<Location> result = new List<Location>();

            for(int r = 0; r < this.Rows;r++)
            {
                for(int c = 0; c < this.Cols; c++)
                {
                    if(IsCellClickable(this,r,c))
                    {
                        result.Add(new Location(r,c));
                    }
                }
            }
            return result;
        }


        public static bool IsCellClickable(Board board, Location location)
        {
            return IsCellClickable(board, location.Row, location.Col);
        }
        public static bool IsCellClickable(Board board, int row, int col)
        {
            Cell cell = board.grid[row, col];
            if (
                  cell.Is(CellType.Empty)
                || cell.Is(CellType.WhiteRounded)
                || cell.Is(CellType.Block)
                || cell.Is((int)CellType.Block + CellType.WhiteRounded)
            )
            {
                return false;
            }

            return true;
        }




        
        public static bool IsCellInBound(Board board,Location location)
        {
            return IsCellInBound(board, location.Row, location.Col);
        }
        public static bool IsCellInBound(Board board,int row, int col) 
        {
            return  row >= 0 && row < board.Rows
                    && col >= 0 && col < board.Cols;
        }

        private bool IsTypesFits(int types)
        {
            if(
                ((types | (int)CellType.Empty) == types) &&
                !( types == (int)CellType.Empty)
            )
            {
                return false;
            }

            if (
                ((types | (int)CellType.Block) == types) &&
                !(types == (int)CellType.Block || types == (int) CellType.WhiteRounded + (int) CellType.Block)
            )
            {
                return false;
            }

            return true;


        }
        public ActionResult SetCell(int row, int col,int Types)
        {
            if (!IsCellInBound(this,row, col)) {

                return ActionResult.Fail("Cell Out Of Bound");
            }
            if(!IsTypesFits(Types))
            {
                return ActionResult.Fail("These Types Cannot Fits To Gether");
            }

            
            this.grid[row,col] = new Cell(Types);

            return ActionResult.Ok("Cell Type Updated Successfully");
        }


        public ActionResult ApplyClick(Click click,MoveRule moveRule = null)
        {
            if(moveRule == null)
            {
                moveRule = Settings.defaultMoveRule;
            }
            return moveRule.Apply(this, click);
        }

        public bool IsWin()
        {
            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Cols; c++) {
                    if(this.grid[r,c].Is(CellType.WhiteRounded))
                    {
                        return false;
                    }
                }
            }
            return true;
        }



        private string Repeat(string str,int count)
        {
            string repeatedStr = "";
            for (int i = 0; i < count; i++) {
                repeatedStr += str;
            }
            return repeatedStr;
        }
        public override string ToString()
        {
            string BoardStr = "";
            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Cols; c++)
                {
                    BoardStr += '\t';
                    BoardStr += this.grid[r, c];
                    BoardStr += Repeat(" ", 12 - this.grid[r, c].ToString().Length);
                }
                BoardStr += "\n\n";
            }

            return  BoardStr;
        }




        public bool Equals(Board obj)
        {
            if(ReferenceEquals(obj,null)) return false;
            if(ReferenceEquals(this,obj)) return true;

            if (this.Rows != obj.Rows) return false;
            if (this.Cols != obj.Cols) return false;

            for(int r = 0; r < this.Rows; r++)
            {
                for(int c = 0; c <  this.Cols; c++)
                {
                    Cell CellA = this.grid[r, c];   
                    Cell CellB = obj.grid[r, c];
                    int TypeA = CellA?.Type ?? -1;
                    int TypeB = CellB?.Type ?? -1;

                    if (TypeA != TypeB) return false;
                }
            }
            return true;
        }
        public override bool Equals(object obj)
        {
            return this.Equals(obj as Board);
        }


        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 31 + Rows;
                hash = hash * 31 + Cols;

                for (int r = 0; r < Rows; r++)
                {
                    for (int c = 0; c < Cols; c++)
                    {
                        int cellType = this.grid[r, c]?.Type ?? -1;
                        hash = hash * 31 + cellType;
                    }
                }
                

                return hash;
            }
        }

    }
}