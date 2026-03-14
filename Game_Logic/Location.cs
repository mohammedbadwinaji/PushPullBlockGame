using System;
namespace PushPullBlocksGame
{

    public class Location
    {
        public int Row { get; set; }
        public int Col { get; set; }

        public Location(int row, int col)
        {
            this.Row = row;
            this.Col = col;
        }

        public override string ToString()
        {
            return $"({Row + 1} , {Col + 1})";
        }
    }
}