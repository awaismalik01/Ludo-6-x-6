using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Ludo
{
    public partial class Ludo : Form
    {
        Player[] Players = new Player[6];
        static Random rnd = new Random();
        bool Restart = true, Again = true, DidMove = true, Roll = false, GameEnd = false;
        int[] Array = new int[78];
        int P = 0, N = 0;
        int Dice = 0, Turn = 0, NOP = 0;
        int[] Win = new int[6];
        int WinIndex = 0;

        public Ludo()
        {
            InitializeComponent();
        }

        private void Ludo_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 6; i++)
            {
                Players[i] = new Player(i + 1, i * 13);
            }

            Players[0].StartSet(412, 512);
            Players[0].Pieces[0].Set(B1.Location.X, B1.Location.Y);
            Players[0].Pieces[1].Set(B2.Location.X, B2.Location.Y);
            Players[0].Pieces[2].Set(B3.Location.X, B3.Location.Y);
            Players[0].Pieces[3].Set(B4.Location.X, B4.Location.Y);

            Players[1].StartSet(156, 479);
            Players[1].Pieces[0].Set(Y1.Location.X, Y1.Location.Y);
            Players[1].Pieces[1].Set(Y2.Location.X, Y2.Location.Y);
            Players[1].Pieces[2].Set(Y3.Location.X, Y3.Location.Y);
            Players[1].Pieces[3].Set(Y4.Location.X, Y4.Location.Y);

            Players[2].StartSet(62, 255);
            Players[2].Pieces[0].Set(P1.Location.X, P1.Location.Y);
            Players[2].Pieces[1].Set(P2.Location.X, P2.Location.Y);
            Players[2].Pieces[2].Set(P3.Location.X, P3.Location.Y);
            Players[2].Pieces[3].Set(P4.Location.X, P4.Location.Y);

            Players[3].StartSet(220, 65);
            Players[3].Pieces[0].Set(R1.Location.X, R1.Location.Y);
            Players[3].Pieces[1].Set(R2.Location.X, R2.Location.Y);
            Players[3].Pieces[2].Set(R3.Location.X, R3.Location.Y);
            Players[3].Pieces[3].Set(R4.Location.X, R4.Location.Y);

            Players[4].StartSet(476, 99);
            Players[4].Pieces[0].Set(G1.Location.X, G1.Location.Y);
            Players[4].Pieces[1].Set(G2.Location.X, G2.Location.Y);
            Players[4].Pieces[2].Set(G3.Location.X, G3.Location.Y);
            Players[4].Pieces[3].Set(G4.Location.X, G4.Location.Y);

            Players[5].StartSet(570, 323);
            Players[5].Pieces[0].Set(O1.Location.X, O1.Location.Y);
            Players[5].Pieces[1].Set(O2.Location.X, O2.Location.Y);
            Players[5].Pieces[2].Set(O3.Location.X, O3.Location.Y);
            Players[5].Pieces[3].Set(O4.Location.X, O4.Location.Y);
        }

        private void RollDice_Click(object sender, EventArgs e)
        {

            if (NOP == 0)
            {
                MessageBox.Show("Select Number of players...!!!");
                return;
            }

            if (DidMove == false)
            {
                MessageBox.Show("Please make a move");
                return;
            }

            DidMove = false;

            Dice = rnd.Next(1, 7);
            DiceDisplay.Text = Dice.ToString();
            if (Turn == 0)
            {
                DiceDisplay.BackColor = Color.SkyBlue;
            }
            if (Turn == 1)
            {
                DiceDisplay.BackColor = Color.Yellow;
            }
            if (Turn == 2)
            {
                DiceDisplay.BackColor = Color.Purple;
            }
            if (Turn == 3)
            {
                DiceDisplay.BackColor = Color.Red;
            }
            if (Turn == 4)
            {
                DiceDisplay.BackColor = Color.LightGreen;
            }
            if (Turn == 5)
            {
                DiceDisplay.BackColor = Color.Orange;
            }
            Roll = true;
            if (Dice == 6)
                Again = true;
            else
                Again = false;



            if(AllHome() && Dice != 6)
            {
                PlayMoves();
            }
        }

        private bool AllHome()
        {
            for(int i = 0; i<4; i++)
            {
                if (Players[Turn].Pieces[i].Home == false && Players[Turn].Pieces[i].In == false)
                    return false;
            }
            return true;
        }

        private bool AllHome2(int index)
        {
            for (int i = 0; i < 4; i++)
            {
                if (i == index)
                {
                    i++;
                    if (i > 3)
                        return true;
                }

                if (Players[Turn].Pieces[i].Home == false && Players[Turn].Pieces[i].In == false)
                    return false;
            }
            return true;
        }

        private bool AllIn(int index)
        {
            for (int i = 0; i < 4; i++)
            {
                if (i == index)
                {
                    i++;
                    if (i > 3)
                        return true;
                }

                if (Players[Turn].Pieces[i].In == false)
                    return false;
            }
            return true;
        }

        private void Wins()
        {
            for (int i = 0; i < 4; i++)
            {
                if (Players[Turn].Pieces[i].In == false)
                    return;
            }
            Win[WinIndex++] = Turn;
            //MessageBox.Show("Player " + (Turn + 1) + " is Position: " + WinIndex);
        }

        private void PlayMoves()
        {
            if (Roll == false)
            {
                MessageBox.Show("Please Roll the Dice");
                return;
            }
            Dice = 0;
            if (Again == false)
            {
                Wins();
                if(WinIndex == 5)
                {
                    MessageBox.Show("Player " + (Turn + 1) + " Lost");
                    GameEnd = true;
                    return;
                    
                }
                Turn = (Turn + 1) % NOP;
                //Turn = 0;
                for (int i = 0; i<WinIndex; i++)
                {
                    if(Win[i] == Turn)
                    {
                        Turn = (Turn + 1) % NOP;
                        i = -1;
                    }

                }
                
                if (Turn == 0)
                {
                    Dice1.BackColor = Color.SkyBlue;
                }
                if (Turn == 1)
                {
                    Dice1.BackColor = Color.Yellow;
                }
                if (Turn == 2)
                {
                    Dice1.BackColor = Color.Purple;
                }
                if (Turn == 3)
                {
                    Dice1.BackColor = Color.Red;
                }
                if (Turn == 4)
                {
                    Dice1.BackColor = Color.LightGreen;
                }
                if (Turn == 5)
                {
                    Dice1.BackColor = Color.Orange;
                }
                
            }
            Roll = false;
            DidMove = true;
        }

        private void DiceDisplay_TextChanged(object sender, EventArgs e)
        {

        }

        private new void Move(ref int X, ref int Y)
        {
            //MessageBox.Show(X.ToString() + " " + Y.ToString());

            bool skip = false;

            for (int i = 1; i <= Dice; i++)
            {
                skip = false;
                if (Y == 323 && (X >= 428 && X <= 605))
                {
                    X = X - 35;
                    if (X < 428)
                    {
                        X = 404;
                        Y = 364;
                        skip = true;
                    }
                }

                if ((X >= 404 && X <= 494) && (Y >= 364 && Y <= 509) && skip == false)
                {
                    X = X + 18;
                    Y = Y + 29;
                    skip = true;
                    if (X > 494 && Y > 509)
                    {
                        X = 462;
                        Y = 525;
                        skip = true;
                    }
                }

                if ((X >= 430 && X <= 494) && (Y >= 509 && Y <= 542) && skip == false)
                {
                    X = X - 32;
                    Y = Y + 17;
                    skip = true;
                    if (X < 430 && Y > 542)
                    {
                        X = 412;
                        Y = 512;
                        skip = true;
                    }
                }

                if ((X >= 340 && X <= 412) && (Y >= 396 && Y <= 512) && skip == false)
                {
                    skip = true;
                    X = X - 18;
                    Y = Y - 29;
                    if (X < 340 && Y < 396)
                    {
                        X = 292;
                        Y = 397;
                        skip = true;
                    }
                }

                if ((X >= 202 && X <= 292) && (Y >= 396 && Y <= 542) && skip == false)
                {
                    X = X - 18;
                    Y = Y + 29;
                    if (X < 202 && Y > 542)
                    {
                        X = 170;
                        Y = 525;
                        skip = true;
                    }
                }

                if ((X >= 138 && X <= 170) && (Y >= 508 && Y <= 525) && skip == false)
                {
                    X = X - 32;
                    Y = Y - 17;
                    skip = true;
                    if (X < 138 && Y < 508)
                    {
                        X = 156;
                        Y = 479;
                        skip = true;
                    }
                }

                if ((X >= 156 && X <= 228) && (Y >= 363 && Y <= 479) && skip == false)
                {
                    X = X + 18;
                    Y = Y - 29;
                    if (X > 228 && Y < 363)
                    {
                        X = 204;
                        Y = 323;
                        skip = true;
                    }
                }

                if (Y == 323 && (X >= 27 && X <= 204) && skip == false)
                {
                    X = X - 35;
                    if (X < 27)
                    {
                        X = 27;
                        Y = 289;
                        skip = true;
                    }
                }

                if ((Y >= 255 && Y <= 323) && X == 27 && skip == false)
                {
                    Y = Y - 34;
                    skip = true;
                    if (Y < 254)
                    {
                        X = 27 + 35;
                        Y = 255;
                        skip = true;
                    }
                }

                if (Y == 255 && (X >= 27 && X <= 204) && skip == false)
                {
                    X = X + 35;
                    if (X > 204)
                    {
                        X = 228;
                        Y = 215;
                        skip = true;
                    }
                }

                if ((Y <= 215 && Y >= 70) && (X >= 138 && X <= 228) && skip == false)
                {
                    X = X - 18;
                    Y = Y - 29;
                    if (Y < 70 && X < 140)
                    {
                        X = 140 + 32;
                        Y = 70 - 17;
                        skip = true;
                    }
                }

                if ((Y <= 70 && Y >= 35) && (X >= 140 && X <= 204) && skip == false)
                {
                    X = X + 32;
                    Y = Y - 17;
                    if (Y < 35 && X > 202)
                    {
                        X = 202 + 18;
                        Y = 36 + 29;
                        skip = true;
                    }
                }

                if ((Y <= 182 && Y >= 65) && (X >= 202 && X <= 292) && skip == false)
                {
                    X = X + 18;
                    Y = Y + 29;
                    if (Y > 181 && X > 292)
                    {
                        X = 340;
                        Y = 181;
                        skip = true;
                    }
                }

                if ((Y <= 181 && Y >= 36) && (X >= 340 && X <= 430) && skip == false)
                {
                    X = X + 18;
                    Y = Y - 29;
                    skip = true;
                    if (X > 430 && Y < 36)
                    {
                        X = 430 + 32;
                        Y = 36 + 17;
                        skip = true;
                    }
                }

                if ((Y <= 70 && Y >= 36) && (X >= 430 && X <= 494) && skip == false)
                {
                    X = X + 32;
                    Y = Y + 17;
                    skip = true;
                    if (X > 494 && Y > 70)
                    {
                        X = 494 - 18;
                        Y = 70 + 29;
                        skip = true;
                    }
                }

                if ((Y >= 70 && Y <= 215) && (X <= 494 && X >= 404) && skip == false)
                {
                    X = X - 18;
                    Y = Y + 29;
                    skip = true;
                    if (Y > 215 && X < 494)
                    {
                        X = 430;
                        Y = 255;
                        skip = true;
                    }
                }

                if ((X >= 428 && X <= 605) && Y == 255 && skip == false)
                {
                    X = X + 35;
                    skip = true;
                    if (X > 605)
                    {
                        X = 605;
                        Y = 255 + 34;
                        skip = true;
                    }
                }

                if (X == 605 && (Y >= 255 && Y <= 323) && skip == false)
                {
                    Y = Y + 34;
                }
            }
        }

        private void MoveIn(ref int X, ref int Y, int Extra)
        {
            MessageBox.Show(X.ToString() + " " + Y.ToString());
            for (int i = 1; i <= Extra; i++)
            {
                if ((Y <= 215 && Y >= 53) && (X >= 170 && X <= 236))
                {
                    X = X + 18;
                    Y = Y + 29;
                }

                if ((Y >= 53 && Y <= 215) && (X <= 462 && X >= 404 - 18))
                {
                    X = X - 18;
                    Y = Y + 29;
                }

                if ((X >= 428 && X <= 605) && Y == 289)
                {
                    X = X - 35;
                }

                if (Y == 289 && (X >= 27 && X <= 204))
                {
                    X = X + 35;
                }

                
                if ((Y >= 300 && Y <= 525) && (X >= 156 && X <= 228))
                {
                    X = X + 18;
                    Y = Y - 29;
                }

                if ((X >= 300 && X <= 462) && (Y >= 380 && Y <= 525))
                {
                   
                    X = X - 18;
                    Y = Y - 29;
                    
                }

            }
        }

        private void O1_Click(object sender, EventArgs e)
        {
            if (Turn + 1 == Players[5].ID)
            {
                if (Players[5].Pieces[0].Home == false)
                {
                    Players[5].Pieces[0].moves += Dice;
                    int X = O1.Location.X;
                    int Y = O1.Location.Y;
                    if (Players[5].Pieces[0].moves <= 76)
                    {
                        Move(ref X, ref Y);
                        O1.Location = new Point(X, Y);             //total 76
                        PlayMoves();
                    }
                    else
                    {
                        int Extra = Players[5].Pieces[0].moves - 76;

                        if (Players[5].Pieces[0].moves > 82)
                        {
                            Players[5].Pieces[0].moves -= Dice;
                            MessageBox.Show("Extra Move");
                            if ((AllHome2(0) && Dice != 6) || AllIn(0))
                                PlayMoves();
                            return;
                        }

                        Dice -= Extra;
                        if (Dice > 0)
                        {
                            Move(ref X, ref Y);
                            O1.Location = new Point(X, Y);             //total 76
                        }
                        else
                        {
                            Extra += Dice;
                        }
                        MessageBox.Show(Extra.ToString());
                        MoveIn(ref X, ref Y, Extra);
                        O1.Location = new Point(X, Y);
                        PlayMoves();

                        if (Players[5].Pieces[0].moves == 82)
                        {
                            Players[5].Pieces[0].In = true;
                            O1.Visible = false;
                        }
                    }
                }
                else
                {
                    if (Dice == 6)
                    {
                        int X = Players[5].StartX;
                        int Y = Players[5].StartY;
                        Players[5].Pieces[0].Home = false;
                        O1.Location = new Point(X, Y);
                        PlayMoves();
                    }
                }
            }
            else
                MessageBox.Show("Its Player " + (Turn + 1).ToString() + " Turn");
        }

        private void O2_Click(object sender, EventArgs e)
        {
            if (Turn + 1 == Players[5].ID)
            {
                if (Players[5].Pieces[1].Home == false)
                {
                    Players[5].Pieces[1].moves += Dice;
                    int X = O2.Location.X;
                    int Y = O2.Location.Y;
                    if (Players[5].Pieces[1].moves <= 76)
                    {
                        Move(ref X, ref Y);
                        O2.Location = new Point(X, Y);             //total 76
                        PlayMoves();
                    }
                    else
                    {
                        int Extra = Players[5].Pieces[1].moves - 76;


                        if (Players[5].Pieces[1].moves > 82)
                        {
                            Players[5].Pieces[1].moves -= Dice;
                            MessageBox.Show("Extra Move");
                            if ((AllHome2(1) && Dice != 6) || AllIn(1))
                                PlayMoves();
                            return;
                        }

                        Dice -= Extra;
                        if (Dice > 0)
                        {
                            Move(ref X, ref Y);
                            O2.Location = new Point(X, Y);             //total 76
                        }
                        else
                        {
                            Extra += Dice;
                        }
                        MessageBox.Show(Extra.ToString());
                        MoveIn(ref X, ref Y, Extra);
                        O2.Location = new Point(X, Y);
                        PlayMoves();

                        if (Players[5].Pieces[1].moves == 82)
                        {
                            Players[5].Pieces[1].In = true;
                            O2.Visible = false;
                        }
                    }
                }
                else
                {
                    if (Dice == 6)
                    {
                        int X = Players[5].StartX;
                        int Y = Players[5].StartY;
                        Players[5].Pieces[1].Home = false;
                        O2.Location = new Point(X, Y);
                        PlayMoves();
                    }
                }
            }
            else
                MessageBox.Show("Its Player " + (Turn + 1).ToString() + " Turn");
        }

        private void O3_Click(object sender, EventArgs e)
        {
            if (Turn + 1 == Players[5].ID)
            {
                if (Players[5].Pieces[2].Home == false)
                {
                    Players[5].Pieces[2].moves += Dice;
                    int X = O3.Location.X;
                    int Y = O3.Location.Y;
                    if (Players[5].Pieces[2].moves <= 76)
                    {
                        Move(ref X, ref Y);
                        O3.Location = new Point(X, Y);             //total 76
                        PlayMoves();
                    }
                    else
                    {
                        int Extra = Players[5].Pieces[2].moves - 76;

                        if (Players[5].Pieces[2].moves > 82)
                        {
                            Players[5].Pieces[2].moves -= Dice;
                            MessageBox.Show("Extra Move");
                            if ((AllHome2(2) && Dice != 6) || AllIn(2))
                                PlayMoves();
                            return;
                        }

                        Dice -= Extra;
                        if (Dice > 0)
                        {
                            Move(ref X, ref Y);
                            O3.Location = new Point(X, Y);             //total 76
                        }
                        else
                        {
                            Extra += Dice;
                        }
                        MessageBox.Show(Extra.ToString());
                        MoveIn(ref X, ref Y, Extra);
                        O3.Location = new Point(X, Y);
                        PlayMoves();

                        if (Players[5].Pieces[2].moves == 82)
                        {
                            Players[5].Pieces[2].In = true;
                            O3.Visible = false;
                        }
                    }
                }
                else
                {
                    if (Dice == 6)
                    {
                        int X = Players[5].StartX;
                        int Y = Players[5].StartY;
                        Players[5].Pieces[2].Home = false;
                        O3.Location = new Point(X, Y);
                        PlayMoves();
                    }
                }
            }
            else
                MessageBox.Show("Its Player " + (Turn + 1).ToString() + " Turn");
        }

        private void O4_Click(object sender, EventArgs e)
        {
            if (Turn + 1 == Players[5].ID)
            {
                if (Players[5].Pieces[3].Home == false)
                {
                    Players[5].Pieces[3].moves += Dice;
                    int X = O4.Location.X;
                    int Y = O4.Location.Y;
                    if (Players[5].Pieces[3].moves <= 76)
                    {
                        Move(ref X, ref Y);
                        O4.Location = new Point(X, Y);             //total 76
                        PlayMoves();
                    }
                    else
                    {
                        int Extra = Players[5].Pieces[3].moves - 76;

                        if (Players[5].Pieces[3].moves > 82)
                        {
                            Players[5].Pieces[3].moves -= Dice;
                            MessageBox.Show("Extra Move");
                            if ((AllHome2(3) && Dice != 6) || AllIn(3))
                                PlayMoves();
                            return;
                        }

                        Dice -= Extra;
                        if (Dice > 0)
                        {
                            Move(ref X, ref Y);
                            O4.Location = new Point(X, Y);             //total 76
                        }
                        else
                        {
                            Extra += Dice;
                        }
                        MessageBox.Show(Extra.ToString());
                        MoveIn(ref X, ref Y, Extra);
                        O4.Location = new Point(X, Y);
                        PlayMoves();

                        if (Players[5].Pieces[3].moves == 82)
                        {
                            Players[5].Pieces[3].In = true;
                            O4.Visible = false;
                        }
                    }
                }
                else
                {
                    if (Dice == 6)
                    {
                        int X = Players[5].StartX;
                        int Y = Players[5].StartY;
                        Players[5].Pieces[3].Home = false;
                        O4.Location = new Point(X, Y);
                        PlayMoves();
                    }
                }
            }
            else
                MessageBox.Show("Its Player " + (Turn + 1).ToString() + " Turn");
        }

        private void G1_Click(object sender, EventArgs e)
        {
            //if (Turn + 1 == Players[4].ID)
            //{
            //    if (Players[4].Pieces[0].Home == false)
            //    {
                    
            //        Players[4].Pieces[0].moves += Dice;
            //        int X = G1.Location.X;
            //        int Y = G1.Location.Y;
            //        Move(ref X, ref Y);
            //        G1.Location = new Point(X, Y);
            //        PlayMoves();
            //    }
            //    else
            //    {
                    
            //        if (Dice == 6)
            //        {
            //            int X = Players[4].StartX;
            //            int Y = Players[4].StartY;
            //            Players[4].Pieces[0].Home = false;
            //            G1.Location = new Point(X, Y);
            //            PlayMoves();
            //        }
            //    }
            //}
            //else
            //    MessageBox.Show("Its Player " + (Turn + 1).ToString() + " Turn");
            if (Turn + 1 == Players[4].ID)
            {
                if (Players[4].Pieces[0].Home == false)
                {
                    Players[4].Pieces[0].moves += Dice;
                    int X = G1.Location.X;
                    int Y = G1.Location.Y;
                    if (Players[4].Pieces[0].moves <= 76)
                    {
                        Move(ref X, ref Y);
                        G1.Location = new Point(X, Y);             //total 76
                        PlayMoves();
                    }
                    else
                    {
                        int Extra = Players[4].Pieces[0].moves - 76;

                        if (Players[4].Pieces[0].moves > 82)
                        {
                            Players[4].Pieces[0].moves -= Dice;
                            MessageBox.Show("Extra Move");
                            if (AllHome2(0) && Dice != 6)
                                PlayMoves();
                            return;
                        }

                        Dice -= Extra;
                        if (Dice > 0)
                        {
                            Move(ref X, ref Y);
                            G1.Location = new Point(X, Y);             //total 76
                        }
                        else
                        {
                            Extra += Dice;
                        }

                        MoveIn(ref X, ref Y, Extra);
                        G1.Location = new Point(X, Y);
                        PlayMoves();

                        if (Players[4].Pieces[0].moves == 82)
                        {
                            Players[4].Pieces[0].In = true;
                            G1.Visible = false;
                        }
                    }
                }
                else
                {
                    if (Dice == 6)
                    {
                        int X = Players[4].StartX;
                        int Y = Players[4].StartY;
                        Players[4].Pieces[0].Home = false;
                        G1.Location = new Point(X, Y);
                        PlayMoves();
                    }
                }
            }
            else
                MessageBox.Show("Its Player " + (Turn + 1).ToString() + " Turn");
        }

        private void G2_Click(object sender, EventArgs e)
        {
            //if (Turn + 1 == Players[4].ID)
            //{
            //    if (Players[4].Pieces[1].Home == false)
            //    {
                   
            //        Players[4].Pieces[1].moves += Dice;
            //        int X = G2.Location.X;
            //        int Y = G2.Location.Y;
            //        Move(ref X, ref Y);
            //        G2.Location = new Point(X, Y);
            //        PlayMoves();
            //    }
            //    else
            //    {
                    
            //        if (Dice == 6)
            //        {
            //            int X = Players[4].StartX;
            //            int Y = Players[4].StartY;
            //            Players[4].Pieces[1].Home = false;
            //            G2.Location = new Point(X, Y);
            //            PlayMoves();
            //        }
            //    }
            //}
            //else
            //    MessageBox.Show("Its Player " + (Turn + 1).ToString() + " Turn");

            if (Turn + 1 == Players[4].ID)
            {
                if (Players[4].Pieces[1].Home == false)
                {
                    Players[4].Pieces[1].moves += Dice;
                    int X = G2.Location.X;
                    int Y = G2.Location.Y;
                    if (Players[4].Pieces[1].moves <= 76)
                    {
                        Move(ref X, ref Y);
                        G2.Location = new Point(X, Y);             //total 76
                        PlayMoves();
                    }
                    else
                    {
                        int Extra = Players[4].Pieces[1].moves - 76;


                        if (Players[4].Pieces[1].moves > 82)
                        {
                            Players[4].Pieces[1].moves -= Dice;
                            MessageBox.Show("Extra Move");
                            if (AllHome2(1) && Dice != 6)
                                PlayMoves();
                            return;
                        }

                        Dice -= Extra;
                        if (Dice > 0)
                        {
                            Move(ref X, ref Y);
                            G2.Location = new Point(X, Y);             //total 76
                        }
                        else
                        {
                            Extra += Dice;
                        }

                        MoveIn(ref X, ref Y, Extra);
                        G2.Location = new Point(X, Y);
                        PlayMoves();

                        if (Players[4].Pieces[1].moves == 82)
                        {
                            Players[4].Pieces[1].In = true;
                            G2.Visible = false;
                        }
                    }
                }
                else
                {
                    if (Dice == 6)
                    {
                        int X = Players[4].StartX;
                        int Y = Players[4].StartY;
                        Players[4].Pieces[1].Home = false;
                        G2.Location = new Point(X, Y);
                        PlayMoves();
                    }
                }
            }
            else
                MessageBox.Show("Its Player " + (Turn + 1).ToString() + " Turn");
        }

        private void G3_Click(object sender, EventArgs e)
        {
            //if (Turn + 1 == Players[4].ID)
            //{
            //    if (Players[4].Pieces[2].Home == false)
            //    {
                    
            //        Players[4].Pieces[2].moves += Dice;
            //        int X = G3.Location.X;
            //        int Y = G3.Location.Y;
            //        Move(ref X, ref Y);
            //        G3.Location = new Point(X, Y);
            //        PlayMoves();
            //    }
            //    else
            //    {
                    
            //        if (Dice == 6)
            //        {
            //            int X = Players[4].StartX;
            //            int Y = Players[4].StartY;
            //            Players[4].Pieces[2].Home = false;
            //            G3.Location = new Point(X, Y);
            //            PlayMoves();
            //        }
            //    }
            //}
            //else
            //    MessageBox.Show("Its Player " + (Turn + 1).ToString() + " Turn");

            if (Turn + 1 == Players[4].ID)
            {
                if (Players[4].Pieces[2].Home == false)
                {
                    Players[4].Pieces[2].moves += Dice;
                    int X = G3.Location.X;
                    int Y = G3.Location.Y;
                    if (Players[4].Pieces[2].moves <= 76)
                    {
                        Move(ref X, ref Y);
                        G3.Location = new Point(X, Y);             //total 76
                        PlayMoves();
                    }
                    else
                    {
                        int Extra = Players[4].Pieces[2].moves - 76;

                        if (Players[4].Pieces[2].moves > 82)
                        {
                            Players[4].Pieces[2].moves -= Dice;
                            MessageBox.Show("Extra Move");
                            if (AllHome2(2) && Dice != 6)
                                PlayMoves();
                            return;
                        }

                        Dice -= Extra;
                        if (Dice > 0)
                        {
                            Move(ref X, ref Y);
                            G3.Location = new Point(X, Y);             //total 76
                        }
                        else
                        {
                            Extra += Dice;
                        }

                        MoveIn(ref X, ref Y, Extra);
                        G3.Location = new Point(X, Y);
                        PlayMoves();

                        if (Players[4].Pieces[2].moves == 82)
                        {
                            Players[4].Pieces[2].In = true;
                            G3.Visible = false;
                        }
                    }
                }
                else
                {
                    if (Dice == 6)
                    {
                        int X = Players[4].StartX;
                        int Y = Players[4].StartY;
                        Players[4].Pieces[2].Home = false;
                        G3.Location = new Point(X, Y);
                        PlayMoves();
                    }
                }
            }
            else
                MessageBox.Show("Its Player " + (Turn + 1).ToString() + " Turn");
        }

        private void G4_Click(object sender, EventArgs e)
        {
            //if (Turn + 1 == Players[4].ID)
            //{
            //    if (Players[4].Pieces[3].Home == false)
            //    {
                    
            //        Players[4].Pieces[3].moves += Dice;
            //        int X = G4.Location.X;
            //        int Y = G4.Location.Y;
            //        Move(ref X, ref Y);
            //        G4.Location = new Point(X, Y);
            //        PlayMoves();
            //    }
            //    else
            //    {
                    
            //        if (Dice == 6)
            //        {
            //            int X = Players[4].StartX;
            //            int Y = Players[4].StartY;
            //            Players[4].Pieces[3].Home = false;
            //            G4.Location = new Point(X, Y);
            //            PlayMoves();
            //        }
            //    }
            //}
            //else
            //    MessageBox.Show("Its Player " + (Turn + 1).ToString() + " Turn");

            if (Turn + 1 == Players[4].ID)
            {
                if (Players[4].Pieces[3].Home == false)
                {
                    Players[4].Pieces[3].moves += Dice;
                    int X = G4.Location.X;
                    int Y = G4.Location.Y;
                    if (Players[4].Pieces[3].moves <= 76)
                    {
                        Move(ref X, ref Y);
                        G4.Location = new Point(X, Y);             //total 76
                        PlayMoves();
                    }
                    else
                    {
                        int Extra = Players[4].Pieces[3].moves - 76;

                        if (Players[4].Pieces[3].moves > 82)
                        {
                            Players[4].Pieces[3].moves -= Dice;
                            MessageBox.Show("Extra Move");
                            if (AllHome2(3) && Dice != 6)
                                PlayMoves();
                            return;
                        }

                        Dice -= Extra;
                        if (Dice > 0)
                        {
                            Move(ref X, ref Y);
                            G4.Location = new Point(X, Y);             //total 76
                        }
                        else
                        {
                            Extra += Dice;
                        }

                        MoveIn(ref X, ref Y, Extra);
                        G4.Location = new Point(X, Y);
                        PlayMoves();

                        if (Players[4].Pieces[3].moves == 82)
                        {
                            Players[4].Pieces[3].In = true;
                            G4.Visible = false;
                        }
                    }
                }
                else
                {
                    if (Dice == 6)
                    {
                        int X = Players[4].StartX;
                        int Y = Players[4].StartY;
                        Players[4].Pieces[3].Home = false;
                        G4.Location = new Point(X, Y);
                        PlayMoves();
                    }
                }
            }
            else
                MessageBox.Show("Its Player " + (Turn + 1).ToString() + " Turn");
        }

        private void R1_Click(object sender, EventArgs e)
        {
            //if (Turn + 1 == Players[3].ID)
            //{
            //    if (Players[3].Pieces[0].Home == false)
            //    {
                    
            //        Players[3].Pieces[0].moves += Dice;
            //        int X = R1.Location.X;
            //        int Y = R1.Location.Y;
            //        Move(ref X, ref Y);
            //        R1.Location = new Point(X, Y);
            //        PlayMoves();
            //    }
            //    else
            //    {
                    
            //        if (Dice == 6)
            //        {
            //            int X = Players[3].StartX;
            //            int Y = Players[3].StartY;
            //            Players[3].Pieces[0].Home = false;
            //            R1.Location = new Point(X, Y);
            //            PlayMoves();
            //        }
            //    }
            //}
            //else
            //    MessageBox.Show("Its Player " + (Turn + 1).ToString() + " Turn");

            if (Turn + 1 == Players[3].ID)
            {
                if (Players[3].Pieces[0].Home == false)
                {
                    Players[3].Pieces[0].moves += Dice;
                    int X = R1.Location.X;
                    int Y = R1.Location.Y;
                    if (Players[3].Pieces[0].moves <= 76)
                    {
                        Move(ref X, ref Y);
                        R1.Location = new Point(X, Y);             //total 76
                        PlayMoves();
                    }
                    else
                    {
                        int Extra = Players[3].Pieces[0].moves - 76;

                        if (Players[3].Pieces[0].moves > 82)
                        {
                            Players[3].Pieces[0].moves -= Dice;
                            MessageBox.Show("Extra Move");
                            if (AllHome2(0) && Dice != 6)
                                PlayMoves();
                            return;
                        }

                        Dice -= Extra;
                        if (Dice > 0)
                        {
                            Move(ref X, ref Y);
                            R1.Location = new Point(X, Y);             //total 76
                        }
                        else
                        {
                            Extra += Dice;
                        }

                        MoveIn(ref X, ref Y, Extra);
                        R1.Location = new Point(X, Y);
                        PlayMoves();

                        if (Players[3].Pieces[0].moves == 82)
                        {
                            Players[3].Pieces[0].In = true;
                            R1.Visible = false;
                        }
                    }
                }
                else
                {
                    if (Dice == 6)
                    {
                        int X = Players[3].StartX;
                        int Y = Players[3].StartY;
                        Players[3].Pieces[0].Home = false;
                        R1.Location = new Point(X, Y);
                        PlayMoves();
                    }
                }
            }
            else
                MessageBox.Show("Its Player " + (Turn + 1).ToString() + " Turn");
        }

        private void R2_Click(object sender, EventArgs e)
        {
            //if (Turn + 1 == Players[3].ID)
            //{
            //    if (Players[3].Pieces[1].Home == false)
            //    {
                    
            //        Players[3].Pieces[1].moves += Dice;
            //        int X = R2.Location.X;
            //        int Y = R2.Location.Y;
            //        Move(ref X, ref Y);
            //        R2.Location = new Point(X, Y);
            //        PlayMoves();
            //    }
            //    else
            //    {
                   
            //        if (Dice == 6)
            //        {
            //            int X = Players[3].StartX;
            //            int Y = Players[3].StartY;
            //            Players[3].Pieces[1].Home = false;
            //            R2.Location = new Point(X, Y);
            //            PlayMoves();
            //        }
            //    }
            //}
            //else
            //    MessageBox.Show("Its Player " + (Turn + 1).ToString() + " Turn");

            if (Turn + 1 == Players[3].ID)
            {
                if (Players[3].Pieces[1].Home == false)
                {
                    Players[3].Pieces[1].moves += Dice;
                    int X = R2.Location.X;
                    int Y = R2.Location.Y;
                    if (Players[3].Pieces[1].moves <= 76)
                    {
                        Move(ref X, ref Y);
                        R2.Location = new Point(X, Y);             //total 76
                        PlayMoves();
                    }
                    else
                    {
                        int Extra = Players[3].Pieces[1].moves - 76;


                        if (Players[3].Pieces[1].moves > 82)
                        {
                            Players[3].Pieces[1].moves -= Dice;
                            MessageBox.Show("Extra Move");
                            if (AllHome2(1) && Dice != 6)
                                PlayMoves();
                            return;
                        }

                        Dice -= Extra;
                        if (Dice > 0)
                        {
                            Move(ref X, ref Y);
                            R2.Location = new Point(X, Y);             //total 76
                        }
                        else
                        {
                            Extra += Dice;
                        }

                        MoveIn(ref X, ref Y, Extra);
                        R2.Location = new Point(X, Y);
                        PlayMoves();

                        if (Players[3].Pieces[1].moves == 82)
                        {
                            Players[3].Pieces[1].In = true;
                            R2.Visible = false;
                        }
                    }
                }
                else
                {
                    if (Dice == 6)
                    {
                        int X = Players[3].StartX;
                        int Y = Players[3].StartY;
                        Players[3].Pieces[1].Home = false;
                        R2.Location = new Point(X, Y);
                        PlayMoves();
                    }
                }
            }
            else
                MessageBox.Show("Its Player " + (Turn + 1).ToString() + " Turn");
        }

        private void R3_Click(object sender, EventArgs e)
        {
            //if (Turn + 1 == Players[3].ID)
            //{
            //    if (Players[3].Pieces[2].Home == false)
            //    {
                    
            //        Players[3].Pieces[2].moves += Dice;
            //        int X = R3.Location.X;
            //        int Y = R3.Location.Y;
            //        Move(ref X, ref Y);
            //        R3.Location = new Point(X, Y);
            //        PlayMoves();
            //    }
            //    else
            //    {
                    
            //        if (Dice == 6)
            //        {
            //            int X = Players[3].StartX;
            //            int Y = Players[3].StartY;
            //            Players[3].Pieces[2].Home = false;
            //            R3.Location = new Point(X, Y);
            //            PlayMoves();
            //        }
            //    }
            //}
            //else
            //    MessageBox.Show("Its Player " + (Turn + 1).ToString() + " Turn");

            if (Turn + 1 == Players[3].ID)
            {
                if (Players[3].Pieces[2].Home == false)
                {
                    Players[3].Pieces[2].moves += Dice;
                    int X = R3.Location.X;
                    int Y = R3.Location.Y;
                    if (Players[3].Pieces[2].moves <= 76)
                    {
                        Move(ref X, ref Y);
                        R3.Location = new Point(X, Y);             //total 76
                        PlayMoves();
                    }
                    else
                    {
                        int Extra = Players[3].Pieces[2].moves - 76;

                        if (Players[3].Pieces[2].moves > 82)
                        {
                            Players[3].Pieces[2].moves -= Dice;
                            MessageBox.Show("Extra Move");
                            if (AllHome2(2) && Dice != 6)
                                PlayMoves();
                            return;
                        }

                        Dice -= Extra;
                        if (Dice > 0)
                        {
                            Move(ref X, ref Y);
                            R3.Location = new Point(X, Y);             //total 76
                        }
                        else
                        {
                            Extra += Dice;
                        }

                        MoveIn(ref X, ref Y, Extra);
                        R3.Location = new Point(X, Y);
                        PlayMoves();

                        if (Players[3].Pieces[2].moves == 82)
                        {
                            Players[3].Pieces[2].In = true;
                            R3.Visible = false;
                        }
                    }
                }
                else
                {
                    if (Dice == 6)
                    {
                        int X = Players[3].StartX;
                        int Y = Players[3].StartY;
                        Players[3].Pieces[2].Home = false;
                        R3.Location = new Point(X, Y);
                        PlayMoves();
                    }
                }
            }
            else
                MessageBox.Show("Its Player " + (Turn + 1).ToString() + " Turn");
        }

        private void R4_Click(object sender, EventArgs e)
        {
            //if (Turn + 1 == Players[3].ID)
            //{
            //    if (Players[3].Pieces[3].Home == false)
            //    {
                   
            //        Players[3].Pieces[3].moves += Dice;
            //        int X = R4.Location.X;
            //        int Y = R4.Location.Y;
            //        Move(ref X, ref Y);
            //        R4.Location = new Point(X, Y);
            //        PlayMoves();
            //    }
            //    else
            //    {
                    
            //        if (Dice == 6)
            //        {
            //            int X = Players[3].StartX;
            //            int Y = Players[3].StartY;
            //            Players[3].Pieces[3].Home = false;
            //            R4.Location = new Point(X, Y);
            //            PlayMoves();
            //        }
            //    }
            //}
            //else
            //    MessageBox.Show("Its Player " + (Turn + 1).ToString() + " Turn");

            if (Turn + 1 == Players[3].ID)
            {
                if (Players[3].Pieces[3].Home == false)
                {
                    Players[3].Pieces[3].moves += Dice;
                    int X = R4.Location.X;
                    int Y = R4.Location.Y;
                    if (Players[3].Pieces[3].moves <= 76)
                    {
                        Move(ref X, ref Y);
                        R4.Location = new Point(X, Y);             //total 76
                        PlayMoves();
                    }
                    else
                    {
                        int Extra = Players[3].Pieces[3].moves - 76;

                        if (Players[3].Pieces[3].moves > 82)
                        {
                            Players[3].Pieces[3].moves -= Dice;
                            MessageBox.Show("Extra Move");
                            if (AllHome2(3) && Dice != 6)
                                PlayMoves();
                            return;
                        }

                        Dice -= Extra;
                        if (Dice > 0)
                        {
                            Move(ref X, ref Y);
                            R4.Location = new Point(X, Y);             //total 76
                        }
                        else
                        {
                            Extra += Dice;
                        }

                        MoveIn(ref X, ref Y, Extra);
                        R4.Location = new Point(X, Y);
                        PlayMoves();

                        if (Players[3].Pieces[3].moves == 82)
                        {
                            Players[3].Pieces[3].In = true;
                            R4.Visible = false;
                        }
                    }
                }
                else
                {
                    if (Dice == 6)
                    {
                        int X = Players[3].StartX;
                        int Y = Players[3].StartY;
                        Players[3].Pieces[3].Home = false;
                        R4.Location = new Point(X, Y);
                        PlayMoves();
                    }
                }
            }
            else
                MessageBox.Show("Its Player " + (Turn + 1).ToString() + " Turn");
        }

        private void B1_Click(object sender, EventArgs e)
        {
            //if (Turn + 1 == Players[0].ID)
            //{
            //    if (Players[0].Pieces[0].Home == false)
            //    {
                   
            //        Players[0].Pieces[0].moves += Dice;
            //        int X = B1.Location.X;
            //        int Y = B1.Location.Y;
            //        Move(ref X, ref Y);
            //        B1.Location = new Point(X, Y);
            //        PlayMoves();
            //    }
            //    else
            //    {
                    
            //        if (Dice == 6)
            //        {
            //            int X = Players[0].StartX;
            //            int Y = Players[0].StartY;
            //            Players[0].Pieces[0].Home = false;
            //            B1.Location = new Point(X, Y);
            //            PlayMoves();
            //        }
            //    }
            //}
            //else
            //    MessageBox.Show("Its Player " + (Turn + 1).ToString() + " Turn");

            if (Turn + 1 == Players[0].ID)
            {
                if (Players[0].Pieces[0].Home == false)
                {
                    Players[0].Pieces[0].moves += Dice;
                    int X = B1.Location.X;
                    int Y = B1.Location.Y;
                    if (Players[0].Pieces[0].moves <= 76)
                    {
                        P = Players[0].Pieces[0].Pos;
                        Players[0].Pieces[0].Pos += Dice;
                        N = Players[0].Pieces[0].Pos;
                        Array[P] = 0;
                        Array[N % 78] = Players[0].ID;
                        Move(ref X, ref Y);
                        B1.Location = new Point(X, Y);             //total 76
                        PlayMoves();
                    }
                    else
                    {
                        int Extra = Players[0].Pieces[0].moves - 76;

                        if (Players[0].Pieces[0].moves > 82)
                        {
                            Players[0].Pieces[0].moves -= Dice;
                            MessageBox.Show("Extra Move");
                            if (AllHome2(0) && Dice != 6)
                                PlayMoves();
                            return;
                        }

                        Dice -= Extra;
                        if (Dice > 0)
                        {
                            Move(ref X, ref Y);
                            B1.Location = new Point(X, Y);             //total 76
                        }
                        else
                        {
                            Extra += Dice;
                        }

                        MoveIn(ref X, ref Y, Extra);
                        B1.Location = new Point(X, Y);
                        PlayMoves();

                        if (Players[0].Pieces[0].moves == 82)
                        {
                            Players[0].Pieces[0].In = true;
                            B1.Visible = false;
                        }
                    }
                }
                else
                {
                    if (Dice == 6)
                    {
                        int X = Players[0].StartX;
                        int Y = Players[0].StartY;
                        Players[0].Pieces[0].Home = false;
                        B1.Location = new Point(X, Y);
                        PlayMoves();
                    }
                }
            }
            else
                MessageBox.Show("Its Player " + (Turn + 1).ToString() + " Turn");
        }

        private void B2_Click(object sender, EventArgs e)
        {
            //if (Turn + 1 == Players[0].ID)
            //{
            //    if (Players[0].Pieces[1].Home == false)
            //    {

            //        Players[0].Pieces[1].moves += Dice;
            //        int X = B2.Location.X;
            //        int Y = B2.Location.Y;
            //        Move(ref X, ref Y);
            //        B2.Location = new Point(X, Y);
            //        PlayMoves();
            //    }
            //    else
            //    {

            //        if (Dice == 6)
            //        {
            //            int X = Players[0].StartX;
            //            int Y = Players[0].StartY;
            //            Players[0].Pieces[1].Home = false;
            //            B2.Location = new Point(X, Y);
            //            PlayMoves();
            //        }
            //    }
            //}
            //else
            //    MessageBox.Show("Its Player " + (Turn + 1).ToString() + " Turn");

            if (Turn + 1 == Players[0].ID)
            {
                if (Players[0].Pieces[1].Home == false)
                {
                    Players[0].Pieces[1].moves += Dice;
                    int X = B2.Location.X;
                    int Y = B2.Location.Y;
                    if (Players[0].Pieces[1].moves <= 76)
                    {
                        P = Players[0].Pieces[1].Pos;
                        Players[0].Pieces[1].Pos += Dice;
                        N = Players[0].Pieces[1].Pos;
                        Array[P] = 0;
                        Array[N % 78] = Players[0].ID;
                        Move(ref X, ref Y);
                        B2.Location = new Point(X, Y);             //total 76
                        PlayMoves();
                    }
                    else
                    {
                        int Extra = Players[0].Pieces[1].moves - 76;


                        if (Players[0].Pieces[1].moves > 82)
                        {
                            Players[0].Pieces[1].moves -= Dice;
                            MessageBox.Show("Extra Move");
                            if (AllHome2(1) && Dice != 6)
                                PlayMoves();
                            return;
                        }

                        Dice -= Extra;
                        if (Dice > 0)
                        {
                            Move(ref X, ref Y);
                            B2.Location = new Point(X, Y);             //total 76
                        }
                        else
                        {
                            Extra += Dice;
                        }

                        MoveIn(ref X, ref Y, Extra);
                        B2.Location = new Point(X, Y);
                        PlayMoves();

                        if (Players[0].Pieces[1].moves == 82)
                        {
                            Players[0].Pieces[1].In = true;
                            B2.Visible = false;
                        }
                    }
                }
                else
                {
                    if (Dice == 6)
                    {
                        int X = Players[0].StartX;
                        int Y = Players[0].StartY;
                        Players[0].Pieces[1].Home = false;
                        B2.Location = new Point(X, Y);
                        PlayMoves();
                    }
                }
            }
            else
                MessageBox.Show("Its Player " + (Turn + 1).ToString() + " Turn");
        }

        private void B3_Click(object sender, EventArgs e)
        {
            //if (Turn + 1 == Players[0].ID)
            //{
            //    if (Players[0].Pieces[2].Home == false)
            //    {

            //        Players[0].Pieces[2].moves += Dice;
            //        int X = B3.Location.X;
            //        int Y = B3.Location.Y;
            //        Move(ref X, ref Y);
            //        B3.Location = new Point(X, Y);
            //        PlayMoves();
            //    }
            //    else
            //    {

            //        if (Dice == 6)
            //        {
            //            int X = Players[0].StartX;
            //            int Y = Players[0].StartY;
            //            Players[0].Pieces[2].Home = false;
            //            B3.Location = new Point(X, Y);
            //            PlayMoves();
            //        }
            //    }
            //}
            //else
            //    MessageBox.Show("Its Player " + (Turn + 1).ToString() + " Turn");

            if (Turn + 1 == Players[0].ID)
            {
                if (Players[0].Pieces[2].Home == false)
                {
                    Players[0].Pieces[2].moves += Dice;
                    int X = B3.Location.X;
                    int Y = B3.Location.Y;
                    if (Players[0].Pieces[2].moves <= 76)
                    {
                        P = Players[0].Pieces[2].Pos;
                        Players[0].Pieces[2].Pos += Dice;
                        N = Players[0].Pieces[2].Pos;
                        Array[P] = 0;
                        Array[N % 78] = Players[0].ID;
                        Move(ref X, ref Y);
                        B3.Location = new Point(X, Y);             //total 76
                        PlayMoves();
                    }
                    else
                    {
                        int Extra = Players[0].Pieces[2].moves - 76;

                        if (Players[0].Pieces[2].moves > 82)
                        {
                            Players[0].Pieces[2].moves -= Dice;
                            MessageBox.Show("Extra Move");
                            if (AllHome2(2) && Dice != 6)
                                PlayMoves();
                            return;
                        }

                        Dice -= Extra;
                        if (Dice > 0)
                        {
                            Move(ref X, ref Y);
                            B3.Location = new Point(X, Y);             //total 76
                        }
                        else
                        {
                            Extra += Dice;
                        }

                        MoveIn(ref X, ref Y, Extra);
                        B3.Location = new Point(X, Y);
                        PlayMoves();

                        if (Players[0].Pieces[2].moves == 82)
                        {
                            Players[0].Pieces[2].In = true;
                            B3.Visible = false;
                        }
                    }
                }
                else
                {
                    if (Dice == 6)
                    {
                        int X = Players[0].StartX;
                        int Y = Players[0].StartY;
                        Players[0].Pieces[2].Home = false;
                        B3.Location = new Point(X, Y);
                        PlayMoves();
                    }
                }
            }
            else
                MessageBox.Show("Its Player " + (Turn + 1).ToString() + " Turn");
        }

        private void B4_Click(object sender, EventArgs e)
        {
            //if (Turn + 1 == Players[0].ID)
            //{
            //    if (Players[0].Pieces[3].Home == false)
            //    {

            //        Players[0].Pieces[3].moves += Dice;
            //        int X = B4.Location.X;
            //        int Y = B4.Location.Y;
            //        Move(ref X, ref Y);
            //        B4.Location = new Point(X, Y);
            //        PlayMoves();
            //    }
            //    else
            //    {

            //        if (Dice == 6)
            //        {
            //            int X = Players[0].StartX;
            //            int Y = Players[0].StartY;
            //            Players[0].Pieces[3].Home = false;
            //            B4.Location = new Point(X, Y);
            //            PlayMoves();
            //        }
            //    }
            //}
            //else
            //    MessageBox.Show("Its Player " + (Turn + 1).ToString() + " Turn");

            if (Turn + 1 == Players[0].ID)
            {
                if (Players[0].Pieces[3].Home == false)
                {
                    Players[0].Pieces[3].moves += Dice;
                    int X = B4.Location.X;
                    int Y = B4.Location.Y;
                    if (Players[0].Pieces[3].moves <= 76)
                    {
                        P = Players[0].Pieces[3].Pos;
                        Players[0].Pieces[3].Pos += Dice;
                        N = Players[0].Pieces[3].Pos;
                        Array[P] = 0;
                        Array[N % 78] = Players[0].ID;
                        Move(ref X, ref Y);
                        B4.Location = new Point(X, Y);             //total 76
                        PlayMoves();
                    }
                    else
                    {
                        int Extra = Players[0].Pieces[3].moves - 76;

                        if (Players[0].Pieces[3].moves > 82)
                        {
                            Players[0].Pieces[3].moves -= Dice;
                            MessageBox.Show("Extra Move");
                            if (AllHome2(3) && Dice != 6)
                                PlayMoves();
                            return;
                        }

                        Dice -= Extra;
                        if (Dice > 0)
                        {
                            Move(ref X, ref Y);
                            B4.Location = new Point(X, Y);             //total 76
                        }
                        else
                        {
                            Extra += Dice;
                        }

                        MoveIn(ref X, ref Y, Extra);
                        B4.Location = new Point(X, Y);
                        PlayMoves();

                        if (Players[0].Pieces[3].moves == 82)
                        {
                            Players[0].Pieces[3].In = true;
                            B4.Visible = false;
                        }
                    }
                }
                else
                {
                    if (Dice == 6)
                    {
                        int X = Players[0].StartX;
                        int Y = Players[0].StartY;
                        Players[0].Pieces[3].Home = false;
                        B4.Location = new Point(X, Y);
                        PlayMoves();
                    }
                }
            }
            else
                MessageBox.Show("Its Player " + (Turn + 1).ToString() + " Turn");
        }

        private void Y1_Click(object sender, EventArgs e)
        {
            //if (Turn + 1 == Players[1].ID)
            //{
            //    if (Players[1].Pieces[0].Home == false)
            //    {

            //        Players[1].Pieces[0].moves += Dice;
            //        int X = Y1.Location.X;
            //        int Y = Y1.Location.Y;
            //        Move(ref X, ref Y);
            //        Y1.Location = new Point(X, Y);
            //        PlayMoves();
            //    }
            //    else
            //    {

            //        if (Dice == 6)
            //        {
            //            int X = Players[1].StartX;
            //            int Y = Players[1].StartY;
            //            Players[1].Pieces[0].Home = false;
            //            Y1.Location = new Point(X, Y);
            //            PlayMoves();
            //        }
            //    }
            //}
            //else
            //    MessageBox.Show("Its Player " + (Turn + 1).ToString() + " Turn");

            if (Turn + 1 == Players[1].ID)
            {
                if (Players[1].Pieces[0].Home == false)
                {
                    Players[1].Pieces[0].moves += Dice;
                    int X = Y1.Location.X;
                    int Y = Y1.Location.Y;
                    if (Players[1].Pieces[0].moves <= 76)
                    {
                        Move(ref X, ref Y);
                        Y1.Location = new Point(X, Y);             //total 76
                        PlayMoves();
                    }
                    else
                    {
                        int Extra = Players[1].Pieces[0].moves - 76;

                        if (Players[1].Pieces[0].moves > 82)
                        {
                            Players[1].Pieces[0].moves -= Dice;
                            MessageBox.Show("Extra Move");
                            if (AllHome2(0) && Dice != 6)
                                PlayMoves();
                            return;
                        }

                        Dice -= Extra;
                        if (Dice > 0)
                        {
                            Move(ref X, ref Y);
                            Y1.Location = new Point(X, Y);             //total 76
                        }
                        else
                        {
                            Extra += Dice;
                        }

                        MoveIn(ref X, ref Y, Extra);
                        Y1.Location = new Point(X, Y);
                        PlayMoves();

                        if (Players[1].Pieces[0].moves == 82)
                        {
                            Players[1].Pieces[0].In = true;
                            Y1.Visible = false;
                        }
                    }
                }
                else
                {
                    if (Dice == 6)
                    {
                        int X = Players[1].StartX;
                        int Y = Players[1].StartY;
                        Players[1].Pieces[0].Home = false;
                        Y1.Location = new Point(X, Y);
                        PlayMoves();
                    }
                }
            }
            else
                MessageBox.Show("Its Player " + (Turn + 1).ToString() + " Turn");
        }

        private void Y2_Click(object sender, EventArgs e)
        {
            //if (Turn + 1 == Players[1].ID)
            //{
            //    if (Players[1].Pieces[1].Home == false)
            //    {

            //        Players[1].Pieces[1].moves += Dice;
            //        int X = Y2.Location.X;
            //        int Y = Y2.Location.Y;
            //        Move(ref X, ref Y);
            //        Y2.Location = new Point(X, Y);
            //        PlayMoves();
            //    }
            //    else
            //    {

            //        if (Dice == 6)
            //        {
            //            int X = Players[1].StartX;
            //            int Y = Players[1].StartY;
            //            Players[1].Pieces[1].Home = false;
            //            Y2.Location = new Point(X, Y);
            //            PlayMoves();
            //        }
            //    }
            //}
            //else
            //    MessageBox.Show("Its Player " + (Turn + 1).ToString() + " Turn");

            if (Turn + 1 == Players[1].ID)
            {
                if (Players[1].Pieces[1].Home == false)
                {
                    Players[1].Pieces[1].moves += Dice;
                    int X = Y2.Location.X;
                    int Y = Y2.Location.Y;
                    if (Players[1].Pieces[1].moves <= 76)
                    {
                        Move(ref X, ref Y);
                        Y2.Location = new Point(X, Y);             //total 76
                        PlayMoves();
                    }
                    else
                    {
                        int Extra = Players[1].Pieces[1].moves - 76;


                        if (Players[1].Pieces[1].moves > 82)
                        {
                            Players[1].Pieces[1].moves -= Dice;
                            MessageBox.Show("Extra Move");
                            if (AllHome2(1) && Dice != 6)
                                PlayMoves();
                            return;
                        }

                        Dice -= Extra;
                        if (Dice > 0)
                        {
                            Move(ref X, ref Y);
                            Y2.Location = new Point(X, Y);             //total 76
                        }
                        else
                        {
                            Extra += Dice;
                        }

                        MoveIn(ref X, ref Y, Extra);
                        Y2.Location = new Point(X, Y);
                        PlayMoves();

                        if (Players[1].Pieces[1].moves == 82)
                        {
                            Players[1].Pieces[1].In = true;
                            Y2.Visible = false;
                        }
                    }
                }
                else
                {
                    if (Dice == 6)
                    {
                        int X = Players[1].StartX;
                        int Y = Players[1].StartY;
                        Players[1].Pieces[1].Home = false;
                        Y2.Location = new Point(X, Y);
                        PlayMoves();
                    }
                }
            }
            else
                MessageBox.Show("Its Player " + (Turn + 1).ToString() + " Turn");
        }

        private void Y3_Click(object sender, EventArgs e)
        {
            //if (Turn + 1 == Players[1].ID)
            //{
            //    if (Players[1].Pieces[2].Home == false)
            //    {

            //        Players[1].Pieces[2].moves += Dice;
            //        int X = Y3.Location.X;
            //        int Y = Y3.Location.Y;
            //        Move(ref X, ref Y);
            //        Y3.Location = new Point(X, Y);
            //        PlayMoves();
            //    }
            //    else
            //    {

            //        if (Dice == 6)
            //        {
            //            int X = Players[1].StartX;
            //            int Y = Players[1].StartY;
            //            Players[1].Pieces[2].Home = false;
            //            Y3.Location = new Point(X, Y);
            //            PlayMoves();
            //        }
            //    }
            //}
            //else
            //    MessageBox.Show("Its Player " + (Turn + 1).ToString() + " Turn");

            if (Turn + 1 == Players[1].ID)
            {
                if (Players[1].Pieces[2].Home == false)
                {
                    Players[1].Pieces[2].moves += Dice;
                    int X = Y3.Location.X;
                    int Y = Y3.Location.Y;
                    if (Players[1].Pieces[2].moves <= 76)
                    {
                        Move(ref X, ref Y);
                        Y3.Location = new Point(X, Y);             //total 76
                        PlayMoves();
                    }
                    else
                    {
                        int Extra = Players[1].Pieces[2].moves - 76;

                        if (Players[1].Pieces[2].moves > 82)
                        {
                            Players[1].Pieces[2].moves -= Dice;
                            MessageBox.Show("Extra Move");
                            if (AllHome2(2) && Dice != 6)
                                PlayMoves();
                            return;
                        }

                        Dice -= Extra;
                        if (Dice > 0)
                        {
                            Move(ref X, ref Y);
                            Y3.Location = new Point(X, Y);             //total 76
                        }
                        else
                        {
                            Extra += Dice;
                        }

                        MoveIn(ref X, ref Y, Extra);
                        Y3.Location = new Point(X, Y);
                        PlayMoves();

                        if (Players[1].Pieces[2].moves == 82)
                        {
                            Players[1].Pieces[2].In = true;
                            Y3.Visible = false;
                        }
                    }
                }
                else
                {
                    if (Dice == 6)
                    {
                        int X = Players[1].StartX;
                        int Y = Players[1].StartY;
                        Players[1].Pieces[2].Home = false;
                        Y3.Location = new Point(X, Y);
                        PlayMoves();
                    }
                }
            }
            else
                MessageBox.Show("Its Player " + (Turn + 1).ToString() + " Turn");
        }
        
        private void Y4_Click(object sender, EventArgs e)
        {
            //if (Turn + 1 == Players[1].ID)
            //{
            //    if (Players[1].Pieces[3].Home == false)
            //    {

            //        Players[1].Pieces[3].moves += Dice;
            //        int X = Y4.Location.X;
            //        int Y = Y4.Location.Y;
            //        Move(ref X, ref Y);
            //        Y4.Location = new Point(X, Y);
            //        PlayMoves();
            //    }
            //    else
            //    {

            //        if (Dice == 6)
            //        {
            //            int X = Players[1].StartX;
            //            int Y = Players[1].StartY;
            //            Players[1].Pieces[3].Home = false;
            //            Y4.Location = new Point(X, Y);
            //            PlayMoves();
            //        }
            //    }
            //}
            //else
            //    MessageBox.Show("Its Player " + (Turn + 1).ToString() + " Turn");

            if (Turn + 1 == Players[1].ID)
            {
                if (Players[1].Pieces[3].Home == false)
                {
                    Players[1].Pieces[3].moves += Dice;
                    int X = Y4.Location.X;
                    int Y = Y4.Location.Y;
                    if (Players[1].Pieces[3].moves <= 76)
                    {
                        Move(ref X, ref Y);
                        Y4.Location = new Point(X, Y);             //total 76
                        PlayMoves();
                    }
                    else
                    {
                        int Extra = Players[1].Pieces[3].moves - 76;

                        if (Players[1].Pieces[3].moves > 82)
                        {
                            Players[1].Pieces[3].moves -= Dice;
                            MessageBox.Show("Extra Move");
                            if (AllHome2(3) && Dice != 6)
                                PlayMoves();
                            return;
                        }

                        Dice -= Extra;
                        if (Dice > 0)
                        {
                            Move(ref X, ref Y);
                            Y4.Location = new Point(X, Y);             //total 76
                        }
                        else
                        {
                            Extra += Dice;
                        }

                        MoveIn(ref X, ref Y, Extra);
                        Y4.Location = new Point(X, Y);
                        PlayMoves();

                        if (Players[1].Pieces[3].moves == 82)
                        {
                            Players[1].Pieces[3].In = true;
                            Y4.Visible = false;
                        }
                    }
                }
                else
                {
                    if (Dice == 6)
                    {
                        int X = Players[1].StartX;
                        int Y = Players[1].StartY;
                        Players[1].Pieces[3].Home = false;
                        Y4.Location = new Point(X, Y);
                        PlayMoves();
                    }
                }
            }
            else
                MessageBox.Show("Its Player " + (Turn + 1).ToString() + " Turn");
        }

        private void P1_Click(object sender, EventArgs e)
        {
            //if (Turn + 1 == Players[2].ID)
            //{
            //    if (Players[2].Pieces[0].Home == false)
            //    {

            //        Players[2].Pieces[0].moves += Dice;
            //        int X = P1.Location.X;
            //        int Y = P1.Location.Y;
            //        Move(ref X, ref Y);
            //        P1.Location = new Point(X, Y);
            //        PlayMoves();
            //    }
            //    else
            //    {

            //        if (Dice == 6)
            //        {
            //            int X = Players[2].StartX;
            //            int Y = Players[2].StartY;
            //            Players[2].Pieces[0].Home = false;
            //            P1.Location = new Point(X, Y);
            //            PlayMoves();
            //        }
            //    }
            //}
            //else
            //    MessageBox.Show("Its Player " + (Turn + 1).ToString() + " Turn");

            if (Turn + 1 == Players[2].ID)
            {
                if (Players[2].Pieces[0].Home == false)
                {
                    Players[2].Pieces[0].moves += Dice;
                    int X = P1.Location.X;
                    int Y = P1.Location.Y;
                    if (Players[2].Pieces[0].moves <= 76)
                    {
                        Move(ref X, ref Y);
                        P1.Location = new Point(X, Y);             //total 76
                        PlayMoves();
                    }
                    else
                    {
                        int Extra = Players[2].Pieces[0].moves - 76;

                        if (Players[2].Pieces[0].moves > 82)
                        {
                            Players[2].Pieces[0].moves -= Dice;
                            MessageBox.Show("Extra Move");
                            if (AllHome2(0) && Dice != 6)
                                PlayMoves();
                            return;
                        }

                        Dice -= Extra;
                        if (Dice > 0)
                        {
                            Move(ref X, ref Y);
                            P1.Location = new Point(X, Y);             //total 76
                        }
                        else
                        {
                            Extra += Dice;
                        }

                        MoveIn(ref X, ref Y, Extra);
                        P1.Location = new Point(X, Y);
                        PlayMoves();

                        if (Players[2].Pieces[0].moves == 82)
                        {
                            Players[2].Pieces[0].In = true;
                            P1.Visible = false;
                        }
                    }
                }
                else
                {
                    if (Dice == 6)
                    {
                        int X = Players[2].StartX;
                        int Y = Players[2].StartY;
                        Players[2].Pieces[0].Home = false;
                        P1.Location = new Point(X, Y);
                        PlayMoves();
                    }
                }
            }
            else
                MessageBox.Show("Its Player " + (Turn + 1).ToString() + " Turn");
        }

        private void P2_Click(object sender, EventArgs e)
        {
            //if (Turn + 1 == Players[2].ID)
            //{
            //    if (Players[2].Pieces[1].Home == false)
            //    {

            //        Players[2].Pieces[1].moves += Dice;
            //        int X = P2.Location.X;
            //        int Y = P2.Location.Y;
            //        Move(ref X, ref Y);
            //        P2.Location = new Point(X, Y);
            //        PlayMoves();
            //    }
            //    else
            //    {

            //        if (Dice == 6)
            //        {
            //            int X = Players[2].StartX;
            //            int Y = Players[2].StartY;
            //            Players[2].Pieces[1].Home = false;
            //            P2.Location = new Point(X, Y);
            //            PlayMoves();
            //        }
            //    }
            //}
            //else
            //    MessageBox.Show("Its Player " + (Turn + 1).ToString() + " Turn");

            if (Turn + 1 == Players[2].ID)
            {
                if (Players[2].Pieces[1].Home == false)
                {
                    Players[2].Pieces[1].moves += Dice;
                    int X = P2.Location.X;
                    int Y = P2.Location.Y;
                    if (Players[2].Pieces[1].moves <= 76)
                    {
                        Move(ref X, ref Y);
                        P2.Location = new Point(X, Y);             //total 76
                        PlayMoves();
                    }
                    else
                    {
                        int Extra = Players[2].Pieces[1].moves - 76;


                        if (Players[2].Pieces[1].moves > 82)
                        {
                            Players[2].Pieces[1].moves -= Dice;
                            MessageBox.Show("Extra Move");
                            if (AllHome2(1) && Dice != 6)
                                PlayMoves();
                            return;
                        }

                        Dice -= Extra;
                        if (Dice > 0)
                        {
                            Move(ref X, ref Y);
                            P2.Location = new Point(X, Y);             //total 76
                        }
                        else
                        {
                            Extra += Dice;
                        }

                        MoveIn(ref X, ref Y, Extra);
                        P2.Location = new Point(X, Y);
                        PlayMoves();

                        if (Players[2].Pieces[1].moves == 82)
                        {
                            Players[2].Pieces[1].In = true;
                            P2.Visible = false;
                        }
                    }
                }
                else
                {
                    if (Dice == 6)
                    {
                        int X = Players[2].StartX;
                        int Y = Players[2].StartY;
                        Players[2].Pieces[1].Home = false;
                        P2.Location = new Point(X, Y);
                        PlayMoves();
                    }
                }
            }
            else
                MessageBox.Show("Its Player " + (Turn + 1).ToString() + " Turn");
        }

        private void P3_Click(object sender, EventArgs e)
        {
            //if (Turn + 1 == Players[2].ID)
            //{
            //    if (Players[2].Pieces[2].Home == false)
            //    {

            //        Players[2].Pieces[2].moves += Dice;
            //        int X = P3.Location.X;
            //        int Y = P3.Location.Y;
            //        Move(ref X, ref Y);
            //        P3.Location = new Point(X, Y);
            //        PlayMoves();
            //    }
            //    else
            //    {

            //        if (Dice == 6)
            //        {
            //            int X = Players[2].StartX;
            //            int Y = Players[2].StartY;
            //            Players[2].Pieces[2].Home = false;
            //            P3.Location = new Point(X, Y);
            //            PlayMoves();
            //        }
            //    }
            //}
            //else
            //    MessageBox.Show("Its Player " + (Turn + 1).ToString() + " Turn");

            if (Turn + 1 == Players[2].ID)
            {
                if (Players[2].Pieces[2].Home == false)
                {
                    Players[2].Pieces[2].moves += Dice;
                    int X = P3.Location.X;
                    int Y = P3.Location.Y;
                    if (Players[2].Pieces[2].moves <= 76)
                    {
                        Move(ref X, ref Y);
                        P3.Location = new Point(X, Y);             //total 76
                        PlayMoves();
                    }
                    else
                    {
                        int Extra = Players[2].Pieces[2].moves - 76;

                        if (Players[2].Pieces[2].moves > 82)
                        {
                            Players[2].Pieces[2].moves -= Dice;
                            MessageBox.Show("Extra Move");
                            if (AllHome2(2) && Dice != 6)
                                PlayMoves();
                            return;
                        }

                        Dice -= Extra;
                        if (Dice > 0)
                        {
                            Move(ref X, ref Y);
                            P3.Location = new Point(X, Y);             //total 76
                        }
                        else
                        {
                            Extra += Dice;
                        }

                        MoveIn(ref X, ref Y, Extra);
                        P3.Location = new Point(X, Y);
                        PlayMoves();

                        if (Players[2].Pieces[2].moves == 82)
                        {
                            Players[2].Pieces[2].In = true;
                            P3.Visible = false;
                        }
                    }
                }
                else
                {
                    if (Dice == 6)
                    {
                        int X = Players[2].StartX;
                        int Y = Players[2].StartY;
                        Players[2].Pieces[2].Home = false;
                        P3.Location = new Point(X, Y);
                        PlayMoves();
                    }
                }
            }
            else
                MessageBox.Show("Its Player " + (Turn + 1).ToString() + " Turn");
        }

        private void P4_Click(object sender, EventArgs e)
        {
            //if (Turn + 1 == Players[2].ID)
            //{
            //    if (Players[2].Pieces[3].Home == false)
            //    {

            //        Players[2].Pieces[3].moves += Dice;
            //        int X = P4.Location.X;
            //        int Y = P4.Location.Y;
            //        Move(ref X, ref Y);
            //        P4.Location = new Point(X, Y);
            //        PlayMoves();
            //    }
            //    else
            //    {

            //        if (Dice == 6)
            //        {
            //            int X = Players[2].StartX;
            //            int Y = Players[2].StartY;
            //            Players[2].Pieces[3].Home = false;
            //            P4.Location = new Point(X, Y);
            //            PlayMoves();
            //        }
            //    }
            //}
            //else
            //    MessageBox.Show("Its Player " + (Turn + 1).ToString() + " Turn");

            if (Turn + 1 == Players[2].ID)
            {
                if (Players[2].Pieces[3].Home == false)
                {
                    Players[2].Pieces[3].moves += Dice;
                    int X = P4.Location.X;
                    int Y = P4.Location.Y;
                    if (Players[2].Pieces[3].moves <= 76)
                    {
                        Move(ref X, ref Y);
                        P4.Location = new Point(X, Y);             //total 76
                        PlayMoves();
                    }
                    else
                    {
                        int Extra = Players[2].Pieces[3].moves - 76;

                        if (Players[2].Pieces[3].moves > 82)
                        {
                            Players[2].Pieces[3].moves -= Dice;
                            MessageBox.Show("Extra Move");
                            if (AllHome2(3) && Dice != 6)
                                PlayMoves();
                            return;
                        }

                        Dice -= Extra;
                        if (Dice > 0)
                        {
                            Move(ref X, ref Y);
                            P4.Location = new Point(X, Y);             //total 76
                        }
                        else
                        {
                            Extra += Dice;
                        }

                        MoveIn(ref X, ref Y, Extra);
                        P4.Location = new Point(X, Y);
                        PlayMoves();

                        if (Players[2].Pieces[3].moves == 82)
                        {
                            Players[2].Pieces[3].In = true;
                            P4.Visible = false;
                        }
                    }
                }
                else
                {
                    if (Dice == 6)
                    {
                        int X = Players[2].StartX;
                        int Y = Players[2].StartY;
                        Players[2].Pieces[3].Home = false;
                        P4.Location = new Point(X, Y);
                        PlayMoves();
                    }
                }
            }
            else
                MessageBox.Show("Its Player " + (Turn + 1).ToString() + " Turn");
        }

        private void H2P_Click(object sender, EventArgs e)
        {
            if (Restart)
            {
               
                Turn = 0;

                Restart = false;

                B1.Visible = true;
                B2.Visible = true;
                B3.Visible = true;
                B4.Visible = true;

                Y1.Visible = true;
                Y2.Visible = true;
                Y3.Visible = true;
                Y4.Visible = true;
                Dice1.BackColor = Color.SkyBlue;
                NOP = 2;
            }
            else
                MessageBox.Show("Restart the Game Plz...!!!");
           
        }

        private void H3P_Click(object sender, EventArgs e)
        {
            if (Restart)
            {
               
                Turn = 0;

                H2P_Click(sender, e);

                P1.Visible = true;
                P2.Visible = true;
                P3.Visible = true;
                P4.Visible = true;

                NOP = 3;
            }
            else
                MessageBox.Show("Restart the Game Plz...!!!");
        }

        private void Quit_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void H4P_Click(object sender, EventArgs e)
        {
            if (Restart)
            {
                
                Turn = 0;

                H3P_Click(sender, e);

                R1.Visible = true;
                R2.Visible = true;
                R3.Visible = true;
                R4.Visible = true;

                NOP = 4;
            }
            else
                MessageBox.Show("Restart the Game Plz...!!!");
           
        }

        private void H5P_Click(object sender, EventArgs e)
        {
            if (Restart)
            {
               
                Turn = 0;

                H4P_Click(sender, e);

                G1.Visible = true;
                G2.Visible = true;
                G3.Visible = true;
                G4.Visible = true;

                NOP = 5;
            }
            else
                MessageBox.Show("Restart the Game Plz...!!!");
        }

        private void H6P_Click(object sender, EventArgs e)
        {
            if (Restart)
            {
                Turn = 0;

                H5P_Click(sender, e);

                O1.Visible = true;
                O2.Visible = true;
                O3.Visible = true;
                O4.Visible = true;

                NOP = 6;
            }
            else
                MessageBox.Show("Restart the Game Plz...!!!");
        }

        private void RestartGame_Click(object sender, EventArgs e)
        {
            Again = true;
            DidMove = true;
            Dice = 0;
            Turn = 0;
            NOP = 0;
            Restart = true;
            DiceDisplay.Text = " ";
            WinIndex = 0;

            for(int i = 0; i<6; i++)
            {
                for(int j = 0; j<4; j++)
                {
                    Players[i].Pieces[j].moves = 0;
                    Players[i].Pieces[j].Home = true;
                    Players[i].Pieces[j].Safe = true;
                    Players[i].Pieces[j].In = false;
                }
            }

            int x = 0, y = 0;
            Players[0].Pieces[0].Reset(ref x, ref y);
            B1.Location = new Point(x, y);
            Players[0].Pieces[1].Reset(ref x, ref y);
            B2.Location = new Point(x, y);
            Players[0].Pieces[2].Reset(ref x, ref y);
            B3.Location = new Point(x, y);
            Players[0].Pieces[3].Reset(ref x, ref y);
            B4.Location = new Point(x, y);

            Players[1].Pieces[0].Reset(ref x, ref y);
            Y1.Location = new Point(x, y);
            Players[1].Pieces[1].Reset(ref x, ref y);
            Y2.Location = new Point(x, y);
            Players[1].Pieces[2].Reset(ref x, ref y);
            Y3.Location = new Point(x, y);
            Players[1].Pieces[3].Reset(ref x, ref y);
            Y4.Location = new Point(x, y);

            Players[2].Pieces[0].Reset(ref x, ref y);
            P1.Location = new Point(x, y);
            Players[2].Pieces[1].Reset(ref x, ref y);
            P2.Location = new Point(x, y);
            Players[2].Pieces[2].Reset(ref x, ref y);
            P3.Location = new Point(x, y);
            Players[2].Pieces[3].Reset(ref x, ref y);
            P4.Location = new Point(x, y);

            Players[3].Pieces[0].Reset(ref x, ref y);
            R1.Location = new Point(x, y);
            Players[3].Pieces[1].Reset(ref x, ref y);
            R2.Location = new Point(x, y);
            Players[3].Pieces[2].Reset(ref x, ref y);
            R3.Location = new Point(x, y);
            Players[3].Pieces[3].Reset(ref x, ref y);
            R4.Location = new Point(x, y);

            Players[4].Pieces[0].Reset(ref x, ref y);
            G1.Location = new Point(x, y);
            Players[4].Pieces[1].Reset(ref x, ref y);
            G2.Location = new Point(x, y);
            Players[4].Pieces[2].Reset(ref x, ref y);
            G3.Location = new Point(x, y);
            Players[4].Pieces[3].Reset(ref x, ref y);
            G4.Location = new Point(x, y);

            Players[5].Pieces[0].Reset(ref x, ref y);
            O1.Location = new Point(x, y);
            Players[5].Pieces[1].Reset(ref x, ref y);
            O2.Location = new Point(x, y);
            Players[5].Pieces[2].Reset(ref x, ref y);
            O3.Location = new Point(x, y);
            Players[5].Pieces[3].Reset(ref x, ref y);
            O4.Location = new Point(x, y);

            B1.Visible = false;
            B2.Visible = false;
            B3.Visible = false;
            B4.Visible = false;

            Y1.Visible = false;
            Y2.Visible = false;
            Y3.Visible = false;
            Y4.Visible = false;

            P1.Visible = false;
            P2.Visible = false;
            P3.Visible = false;
            P4.Visible = false;

            R1.Visible = false;
            R2.Visible = false;
            R3.Visible = false;
            R4.Visible = false;

            G1.Visible = false;
            G2.Visible = false;
            G3.Visible = false;
            G4.Visible = false;

            O1.Visible = false;
            O2.Visible = false;
            O3.Visible = false;
            O4.Visible = false;
        }

        private void Dice1_TextChanged(object sender, EventArgs e)
        {

        }

    }
}
