namespace PushPullBlocksGame
{

    public enum CellType
    {
        Empty = 1,
        WhiteRounded = 2,
        RightPush = 4,
        LeftPush = 8,
        TopPush = 16,
        BottomPush = 32,
        RightPull = 64,
        LeftPull = 128,
        TopPull = 256,
        BottomPull = 512,
        Block= 1024,
    }
}