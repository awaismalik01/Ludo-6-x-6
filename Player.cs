using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Ludo
{
    class Player
    {
        public int ID;
        public Piece[] Pieces = new Piece[4];
        public int StartX = 0;
        public int StartY = 0;
        public bool IsWin = false;
        public void StartSet(int x, int y)
        {
            StartX = x;
            StartY = y;
        }
        public Player(int P, int S)
        {
            ID = P;
            for (int i = 0; i < 4; i++)
            {
                Pieces[i] = new Piece(ID, i + 1, S);
            }
        }
        
    }
}
