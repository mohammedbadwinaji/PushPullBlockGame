namespace PushPullBlocksGame
{

    public class Click
    {
        public Location Location { get; set; }

        public Click(Location location)
        {
            Location = location;
        }

        public Click(int Row, int Col)
        {
            Location = new Location(Row, Col);
        }


        public override string ToString() {
            return $"( {Location.Row + 1} , {Location.Col + 1} ) ";
        }

    }
}