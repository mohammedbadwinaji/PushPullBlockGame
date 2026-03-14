using System;
using System.Runtime.CompilerServices;

namespace PushPullBlocksGame
{

    public class Cell {

        private int _Type;

        public int Type
        {
            get {
                return _Type;
            }
            set {
                this._Type = value;
            }
        }

        public bool Has(CellType type)
        {
            return ( this.Type & (int)type ) == (int)type;
        }

        public Cell(int type)
        {
            this.Type = type;   
        }

        public bool Is(CellType type)
        {
            return this.Type == (int)type;
        }
        private string GetBlockTypes()
        {
            string Type = "";

            if(this.Has(CellType.Block))
            {
                Type += "#";
            }
            if (this.Has(CellType.LeftPush))
            {
                Type += "<h";
            }
            if (this.Has(CellType.LeftPull))
            {
                Type += "<l";
            }
            if (this.Has(CellType.TopPush))
            {
                Type += "^h";
            }
            if (this.Has(CellType.TopPull))
            {
                Type += "^l";
            }
            if (this.Has(CellType.BottomPush))
            {
                Type += "vh";
            }
            if (this.Has(CellType.BottomPull))
            {
                Type += "vl";
            }
            if (this.Has(CellType.RightPush))
            {
                Type += ">h";
            }
            if (this.Has(CellType.RightPull))
            {
                Type += ">l";
            }
            return Type;
        }
        private string GetTypeString()
        {
            string Type = "";
            
            if (this.Is(CellType.Empty))
            {
                return ".";
            }
            else if (this.Has(CellType.WhiteRounded))
            {

                return "o" + GetBlockTypes();
            }
            else if (this.Is(CellType.Block))
            {
                return "#";
            }
            else Type =  GetBlockTypes();
         
            return Type; 
        }


        public override string ToString()
        {
            return $"{GetTypeString()}";
        }
    }

}