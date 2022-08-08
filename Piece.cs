using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Ludo
{
    class Piece
    {
        public bool Home = true;
        public bool Safe = true;
        public bool In = false;
        public int moves = 0;
        public int Pos = 0;
        public int PlayerID;
        public int PieceNo;
        public int X;
        public int Y;

        public void Reset(ref int x, ref int y)
        {
            x = X;
            y = Y;
        }
        public void Set(int x, int y)
        {
            X = x;
            Y = y;
        }
        public Piece(int ID, int N, int S)
        {
            PlayerID = ID;
            PieceNo = N;
            Pos = S;
        }
    }
}
