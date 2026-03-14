using System.Collections.Generic;

namespace PushPullBlocksGame
{

    public abstract class MoveRule {



        protected Location getTargetLocation(Location loc, Direction dir)
        {

            switch (dir)
            {
                case Direction.Top:
                    return new Location(loc.Row - 1, loc.Col);
                case Direction.Down:
                    return new Location(loc.Row + 1, loc.Col);
                case Direction.Left:
                    return new Location(loc.Row, loc.Col - 1);
                case Direction.Right:
                    return new Location(loc.Row, loc.Col + 1);
                default:
                    return new Location(-1, -1);
            }
        }


        protected bool IsCellCanMove(Board board, Location loc, Direction dir)
        {
            Location TargetLocation = getTargetLocation(loc, dir);

            if (!Board.IsCellInBound(board, TargetLocation))
            {
                return false;
            }



            Cell locCell = board.grid[loc.Row, loc.Col];
            Cell targetCell = board.grid[TargetLocation.Row, TargetLocation.Col];


            if (
                    locCell.Type == (int)CellType.Empty
                || locCell.Type == (int)CellType.WhiteRounded
                )
            {
                return false;
            }


            if (
                   !(
                        targetCell.Type == (int)CellType.WhiteRounded
                    || targetCell.Type == (int)CellType.Empty
                    )
                )
            {
                return false;
            }

            return true;
        }

        protected void MoveCell(Board board, Location loc, Direction dir)
        {
            Location TargetLoc = getTargetLocation(loc, dir);

            Cell SourceCell = board.grid[loc.Row,loc.Col];
            Cell TargetCell = board.grid[TargetLoc.Row, TargetLoc.Col];

            if (TargetCell.Is(CellType.Empty))
            {
                if (SourceCell.Has(CellType.WhiteRounded))
                {
                    Cell Temp = new Cell(SourceCell.Type - (int)CellType.WhiteRounded);
                    SourceCell.Type = (int)CellType.WhiteRounded;
                    TargetCell.Type = Temp.Type;
                }
                else
                {
                    Cell Temp = new Cell(SourceCell.Type);
                    SourceCell.Type = TargetCell.Type;
                    TargetCell.Type = Temp.Type;
                }
            }
            else if (TargetCell.Has(CellType.WhiteRounded)) {
                if (SourceCell.Has(CellType.WhiteRounded)) { 
                    Cell Temp = new Cell(SourceCell.Type);
                    SourceCell.Type = (int)CellType.WhiteRounded;
                    TargetCell.Type = Temp.Type;    
                   
                } 
                else
                {
                    Cell Temp = new Cell(SourceCell.Type + (int)CellType.WhiteRounded);
                    SourceCell.Type = (int)CellType.Empty;
                    TargetCell.Type = Temp.Type;
                   
                }
            }


            


        }


        protected void PushLeftAll(Board board,Click click)
        {
            for (int c = 1; c < click.Location.Col; c++)
            {
                if (IsCellCanMove(board, new Location(click.Location.Row, c), Direction.Left))
                {
                    MoveCell(board, new Location(click.Location.Row, c), Direction.Left);
                }
            }

        }
        protected void PushRightAll(Board board,Click click)
        {

            for (int c = board.Cols - 1; c > click.Location.Col; c--)
            {
                if (IsCellCanMove(board, new Location(click.Location.Row, c), Direction.Right))
                {
                    MoveCell(board, new Location(click.Location.Row, c), Direction.Right);
                }
            }
        }
        protected void PushTopAll(Board board,Click click)
        {

            for (int r = 1; r < click.Location.Row; r++)
            {
                if (IsCellCanMove(board, new Location(r, click.Location.Col), Direction.Top))
                {
                    MoveCell(board, new Location(r, click.Location.Col), Direction.Top);
                }
            }

        }
        protected void PushDownAll(Board board,Click click)
        {

            for (int r = board.Rows - 1; r > click.Location.Row; r--)
            {
                if (IsCellCanMove(board, new Location(r, click.Location.Col), Direction.Down))
                {
                    MoveCell(board, new Location(r, click.Location.Col), Direction.Down);
                }
            }

        }


        protected void PullTopAll(Board board, Click click)
        {

            for (int r = click.Location.Row + 1; r < board.Rows; r++)
            {
                if (IsCellCanMove(board, new Location(r, click.Location.Col), Direction.Top))
                {
                    MoveCell(board, new Location(r, click.Location.Col), Direction.Top);
                }
            }

        }
        protected void PullDownAll(Board board,Click click)
        {

            for (int r = click.Location.Row - 1; r >= 0; r--)
            {
                if (IsCellCanMove(board, new Location(r, click.Location.Col), Direction.Down))
                {
                    MoveCell(board, new Location(r, click.Location.Col), Direction.Down);
                }
            }

        }
        protected void PullRightAll(Board board, Click click)
        {
            for (int c = click.Location.Col - 1; c >= 0; c--)
            {
                if (IsCellCanMove(board, new Location(click.Location.Row, c), Direction.Right))
                {
                    MoveCell(board, new Location(click.Location.Row, c), Direction.Right);
                }
            }
        }
        protected void PullLeftAll(Board board,Click click)
        {
            for (int c = click.Location.Col + 1; c < board.Cols; c++)
            {
                if (IsCellCanMove(board, new Location(click.Location.Row, c), Direction.Left))
                {
                    MoveCell(board, new Location(click.Location.Row, c), Direction.Left);

                }
            }
        }




        protected void PushLeftPartial(Board board, Click click)
        {
            List<Location> locations = new List<Location>();

            for (int c = 1; c < click.Location.Col; c++)
            {
                if (IsCellCanMove(board, new Location(click.Location.Row, c), Direction.Left))
                {
                    locations.Add(new Location(click.Location.Row, c));
                    //MoveCell(board,new Location(click.Location.Row, c), Direction.Left);
                }
            }

            foreach (Location location in locations)
            {
                MoveCell(board, location, Direction.Left);
            }

        }
        protected void PushRightPartial(Board board, Click click)
        {
            List<Location> locations = new List<Location>();
            for (int c = board.Cols - 1; c > click.Location.Col; c--)
            {
                if (IsCellCanMove(board, new Location(click.Location.Row, c), Direction.Right))
                {
                    locations.Add(new Location(click.Location.Row, c));
                    //MoveCell(board,new Location(click.Location.Row, c), Direction.Right);
                }
            }

            foreach (Location location in locations)
            {
                MoveCell(board, location, Direction.Right);
            }
        }
        protected void PushTopPartial(Board board, Click click)
        {

            List<Location> locations = new List<Location>();

            for (int r = 1; r < click.Location.Row; r++)
            {
                if (IsCellCanMove(board, new Location(r, click.Location.Col), Direction.Top))
                {
                    locations.Add(new Location(r, click.Location.Col));
                    //MoveCell(board, new Location(r, click.Location.Col), Direction.Top);
                }
            }

            foreach (Location location in locations)
            {
                MoveCell(board, location, Direction.Top);
            }
        }
        protected void PushDownPartial(Board board, Click click)
        {

            List<Location> locations = new List<Location>();

            for (int r = board.Rows - 1; r > click.Location.Row; r--)
            {
                if (IsCellCanMove(board, new Location(r, click.Location.Col), Direction.Down))
                {
                    locations.Add(new Location(r, click.Location.Col));
                    //MoveCell(board,new Location(r, click.Location.Col), Direction.Down);
                }
            }
            foreach (Location location in locations)
            {
                MoveCell(board, location, Direction.Down);
            }
        }
        protected void PullTopPartial(Board board, Click click)
        {
            List<Location> locations = new List<Location>();
            for (int r = click.Location.Row + 1; r < board.Rows; r++)
            {
                if (IsCellCanMove(board, new Location(r, click.Location.Col), Direction.Top))
                {
                    locations.Add(new Location(r, click.Location.Col));
                    //MoveCell(board, new Location(r, click.Location.Col), Direction.Top);
                }
            }
            foreach (Location location in locations)
            {
                MoveCell(board, location, Direction.Top);
            }
        }
        protected void PullDownPartial(Board board, Click click)
        {

            List<Location> locations = new List<Location>();
            for (int r = click.Location.Row - 1; r >= 0; r--)
            {
                if (IsCellCanMove(board, new Location(r, click.Location.Col), Direction.Down))
                {
                    locations.Add(new Location(r, click.Location.Col));
                    //MoveCell(board,new Location(r, click.Location.Col), Direction.Down);
                }
            }

            foreach (Location location in locations)
            {
                MoveCell(board, location, Direction.Down);
            }

        }
        protected void PullRightPartial(Board board, Click click)
        {
            List<Location> locations = new List<Location>();

            for (int c = click.Location.Col - 1; c >= 0; c--)
            {
                if (IsCellCanMove(board, new Location(click.Location.Row, c), Direction.Right))
                {
                    locations.Add(new Location(click.Location.Row, c));
                    //MoveCell(board,new Location(click.Location.Row, c), Direction.Right);
                }
            }

            foreach (Location location in locations)
            {
                MoveCell(board, location, Direction.Right);
            }
        }
        protected void PullLeftPartial(Board board, Click click)
        {
            List<Location> locations = new List<Location>();

            for (int c = click.Location.Col + 1; c < board.Cols; c++)
            {
                if (IsCellCanMove(board, new Location(click.Location.Row, c), Direction.Left))
                {
                    locations.Add(new Location(click.Location.Row, c));
                    //MoveCell(board,new Location(click.Location.Row, c), Direction.Left);

                }
            }

            foreach (Location location in locations)
            {
                MoveCell(board, location, Direction.Left);
            }
        }




        public abstract void PushTop(Board board, Click click);
        public abstract void PushDown(Board board, Click click);
        public abstract void PushRight(Board board, Click click);
        public abstract void PushLeft(Board board, Click click);
        public abstract void PullTop(Board board, Click click);
        public abstract void PullDown(Board board, Click click);
        public abstract void PullRight(Board board, Click click);
        public abstract void PullLeft(Board board, Click click);



        protected void HandleMove(Board board, Click click)
        {
            Cell cell = board.grid[click.Location.Row, click.Location.Col];
            if (cell.Has(CellType.TopPush))
            {
                PushTop(board, click);
            }
            if (cell.Has(CellType.BottomPush))
            {
                PushDown(board, click);
            }
            if (cell.Has(CellType.LeftPush))
            {
                PushLeft(board, click);
            }
            if (cell.Has(CellType.RightPush))
            {
                PushRight(board, click);
            }
            if (cell.Has(CellType.TopPull))
            {
                PullTop(board, click);
            }
            if (cell.Has(CellType.BottomPull))
            {
                PullDown(board, click);
            }
            if (cell.Has(CellType.LeftPull))
            {
                PullLeft(board, click);
            }
            if (cell.Has(CellType.RightPull))
            {
                PullRight(board, click);
            }


        }

        public  ActionResult Apply(Board board, Click click)
        {

            if(!Board.IsCellInBound(board,click.Location))
            {
                return ActionResult.Fail("Cell Out Of Bound");
            }
            if (!Board.IsCellClickable(board, click.Location))
            {
                return ActionResult.Fail("This Cell Is Not Clickable");
            }
            HandleMove(board, click);
            return ActionResult.Ok("Success");
        }
    }
}