using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Boulder_Dash
{
    public enum state
    {
        EMPTY,
        PLAYER,
        WALL,
        SUPERWALL,
        GROUND,
        BOULDER,
        DIAMOND,
        BUTTERFLY,
        GHOST,
        EXIT
    }

    public class Board
    {
        public state[,] board;

        public List<Diamond> Diamonds = new List<Diamond>();
        public List<Boulder> Boulders = new List<Boulder>();

        public Butterfly motyl;
        public Ghost duch;

        public int ileDiamentów = 0;
        public int life = 3;
        public int time = 60;
        
        public Board(int x, int y)
        {
            board = new state[x, y];
            generate();
        }

        public void generate() //generuje mape
        {
            Random losuj = new Random();

            ileDiamentów = 0;
            time = 60;

            Diamonds.Clear();
            Boulders.Clear();

            for (int i=0;i<board.GetLength(0);i++)
            {
                for(int j=0;j<board.GetLength(1);j++)
                {
                    if (i == 0 || i == board.GetLength(0) - 1)
                        board[i, j] = state.SUPERWALL;
                    else if (j == 0 || j == board.GetLength(1) - 1)
                        board[i, j] = state.SUPERWALL;
                    else
                        board[i, j] = state.GROUND;
                }
            }


            int x = losuj.Next(1, board.GetLength(0) / 2 - (board.GetLength(0) / 2) / 2);
            int y = losuj.Next(3, board.GetLength(1) / 2 - 2);
            for(int i=0;i<5;i++)
                board[x + i, y] = state.WALL;

            x = losuj.Next(board.GetLength(0) / 2 + 2, board.GetLength(0) - 6);
            y = losuj.Next(board.GetLength(1) / 2 + 2, board.GetLength(1) - 2);
            for (int i = 0; i < 5; i++)
                board[x + i, y] = state.WALL;

            x = losuj.Next(board.GetLength(0) / 2 + 2, board.GetLength(0) - 2);
            y = losuj.Next(2, board.GetLength(1)/2 - 6);
            for (int i = 0; i < 5; i++)
                board[x , y + i] = state.WALL;

            x = losuj.Next(2, board.GetLength(0) / 2 - (board.GetLength(0) / 2) / 2);
            y = losuj.Next(board.GetLength(1) / 2 + 2, board.GetLength(1) - 6);
            for (int i = 0; i < 5; i++)
                board[x, y + i] = state.WALL;

            board[1, 1] = state.PLAYER;
            board[board.GetLength(0) - 2, board.GetLength(1) - 2] = state.EXIT;

            bool test = false;
            while(test == false)
            {
                int ile = 0;
                x = losuj.Next(2, board.GetLength(0) - 5);
                y = losuj.Next(2, board.GetLength(1) - 5);
                for (int i = 0; i < 3; i++)
                    for (int j = 0; j < 3; j++)
                        if (board[x + i, y + j] == state.GROUND)
                            ile += 1;

                if (ile == 9)
                    test = true;       
            }

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (i == 1 && j == 1)
                        continue;
                    else
                        board[x + i, y + j] = state.EMPTY;
                }
            }

            board[x, y] = state.BUTTERFLY;
            motyl = new Butterfly(x, y);


            test = false;
            while (test == false)
            {
                int ile = 0;
                x = losuj.Next(2, board.GetLength(0) - 5);
                y = losuj.Next(2, board.GetLength(1) - 5);
                for (int i = 0; i < 3; i++)
                    for (int j = 0; j < 3; j++)
                        if (board[x + i, y + j] == state.GROUND)
                            ile += 1;

                if (ile == 9)
                    test = true;
            }

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (i == 1 && j == 1)
                        continue;
                    else
                        board[x + i, y + j] = state.EMPTY;
                }
            }

            board[x, y] = state.GHOST;
            duch = new Ghost(x, y);


            for (int i=0;i<30;i++)
            {
                while (board[x,y] != state.GROUND)
                {
                    x = losuj.Next(2, board.GetLength(0) - 3);
                    y = losuj.Next(2, board.GetLength(1) - 3);
                }

                Boulders.Add(new Boulder(x,y));
                board[x, y] = state.BOULDER;
            }

            for (int i = 0; i < 15; i++)
            {
                while (board[x, y] != state.GROUND)
                {
                    x = losuj.Next(1, board.GetLength(0) - 2);
                    y = losuj.Next(1, board.GetLength(1) - 2);
                }

                Diamonds.Add(new Diamond(x, y));
                board[x, y] = state.DIAMOND;
            }

            
        }

        public void PlayerOnTheBoard(int x, int y) //umiescza gracza na planszy
        {
            board[x, y] = state.PLAYER;
        }

        public void PlayerDeadOnTheBoard()
        {
            life -= 1;
            board[1, 1] = state.PLAYER;
        } 

        public void ShowBoard(Graphics g) //pokazuje mape na ekranie
        {
            Image głaz = Image.FromFile(@"tiles\boulder.png");
            Image diament = Image.FromFile(@"tiles\diamond.png");
            Image ściana = Image.FromFile(@"tiles\wall.png");
            Image superściana = Image.FromFile(@"tiles\superwall.png");
            Image gracz = Image.FromFile(@"tiles\player.png");
            Image ziemia = Image.FromFile(@"tiles\ground.png");
            Image pusto = Image.FromFile(@"tiles\empty.png");
            Image duch = Image.FromFile(@"tiles\ghost.png");
            Image motyl = Image.FromFile(@"tiles\butterfly.png");
            Image otwarte = Image.FromFile(@"tiles\openexit.png");
            Image zamknięte = Image.FromFile(@"tiles\closedexit.png");

            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[j, i] == state.GROUND)
                        g.DrawImage(ziemia, i * 50, j * 40, 50, 40);
                    if (board[j, i] == state.SUPERWALL)
                        g.DrawImage(superściana, i * 50, j * 40, 50, 40);
                    if (board[j, i] == state.WALL)
                        g.DrawImage(ściana, i * 50, j * 40, 50, 40);
                    if (board[j, i] == state.EXIT)
                    {
                        if (ileDiamentów >= 10)
                            g.DrawImage(otwarte, i * 50, j * 40, 50, 40);
                        else
                            g.DrawImage(zamknięte, i * 50, j * 40, 50, 40);
                    }
                    if (board[j, i] == state.PLAYER)
                        g.DrawImage(gracz, i * 50, j * 40, 50, 40);
                    if (board[j, i] == state.EMPTY)
                        g.DrawImage(pusto, i * 50, j * 40, 50, 40);
                    if (board[j, i] == state.BOULDER)
                        g.DrawImage(głaz, i * 50, j * 40, 50, 40);
                    if (board[j, i] == state.DIAMOND)
                        g.DrawImage(diament, i * 50, j * 40, 50, 40);
                    if (board[j, i] == state.BUTTERFLY)
                        g.DrawImage(motyl, i * 50, j * 40, 50, 40);
                    if (board[j, i] == state.GHOST)
                        g.DrawImage(duch, i * 50, j * 40, 50, 40);
                }
            }
        }
    }
}
