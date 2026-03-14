using System.Collections.Generic;

namespace PushPullBlocksGame
{

    public static class Global
    {
        public static string BoardsFilePath = @"C:\Users\akats\source\repos\PushPullBlocksGame\Data\boardsData.json";
        public static int LevelsCount = 42;
        public static int NumberOfLevelsPerLine = 7;
        public static List<Board> Boards = null;
    }
}