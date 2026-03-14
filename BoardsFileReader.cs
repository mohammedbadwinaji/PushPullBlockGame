using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Linq;
using System.Runtime.Remoting.Contexts;

namespace PushPullBlocksGame
{

    public class JsonCell
    {
        public int row { get; set; }
        public int col { get; set; }
        public int types { get; set; }
    }
    public class JsonBoard
    {
        public int rowsCount { get; set; }
        public int colsCount {  get; set; }
        public JsonCell[] cells { get; set; }

    }

    public static class BoardsFileReader
    {
      
        public static Board GetBoard(int Level)
        {
            if(Level < 0 || Level > Global.LevelsCount) return null;

            if(Global.Boards == null ||  Global.Boards.Count == 0)
            {
                Global.Boards = new List<Board>();

                string JsonContent = File.ReadAllText(Global.BoardsFilePath);
                List<JsonBoard> rawBoards = JsonSerializer.Deserialize<List<JsonBoard>>(JsonContent);

                foreach (JsonBoard jb in rawBoards)
                {
                    Board b = new Board(jb.rowsCount, jb.colsCount);

                    if (jb.cells != null)
                    {
                        foreach (JsonCell cell in jb.cells)
                            b.SetCell(cell.row, cell.col, cell.types);
                    }

                    Global.Boards.Add(b);
                }
            }
            
            return Global.Boards[Level - 1];
        }
    }   
}
    