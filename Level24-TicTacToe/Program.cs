var game = new TicTacToeGame();
game.RunGame();

public class TicTacToeGame
{
     public void RunGame()
    {
        Board board = new Board();
        Player player1 = new Player('X');
        Player player2 = new Player('O');
        int round = 0;
        int currentPlayer = 1;//1 represents player1('X'), 2 represents player2('O')        

        while(round < 9)
        {
            if(currentPlayer == 1)
            {               
                var play = player1.PickPosition(board);
                board.UpdateBoard(play[0], play[1], player1.Symbol);
                Console.Clear();
                board.DrawBoard();
                if (round>=4 && HasWon(board, player1.Symbol))
                {
                    Console.WriteLine($"Player {player1.Symbol} has Won the game");
                    return;
                }
                
            }
            else
            {
                var play = player2.PickPosition(board);
                board.UpdateBoard(play[0], play[1], player2.Symbol);
                Console.Clear();
                board.DrawBoard();
                if (round >= 4 && HasWon(board, player2.Symbol))
                {
                    Console.WriteLine($"Player {player2.Symbol} has Won the game");
                    return;
                }
            }
            round++;
            currentPlayer = currentPlayer == 1 ? 2 : 1;//change the player after the current round
        }

        Console.WriteLine("\nDraw!");
    }

    public bool HasWon(Board board, char symb)
    {
        var fields = board.Fields;
        //check diagonals
        if (symb == fields[0, 0]  && fields[0, 0] == fields[1, 1] && fields[1, 1] == fields[2, 2] || //diagonal
            symb == fields[0, 2]  && fields[0, 2] == fields[1, 1] && fields[1, 1] == fields[2, 0] || //diagonal
            symb == fields[0, 0]  && fields[0, 0] == fields[0, 1] && fields[0, 1] == fields[0, 2] || //row
            symb == fields[1, 0]  && fields[1, 0] == fields[1, 1] && fields[1, 1] == fields[1, 2] || //row
            symb == fields[2, 0]  && fields[2, 0] == fields[2, 1] && fields[2, 1] == fields[2, 2] || //row
            symb == fields[0, 0]  && fields[0, 0] == fields[1, 0] && fields[1, 0] == fields[2, 0] || //column
            symb == fields[0, 1]  && fields[0, 1] == fields[1, 1] && fields[1, 1] == fields[2, 1] || //column
            symb == fields[0, 2]  && fields[0, 2] == fields[1, 2] && fields[1, 2] == fields[2, 2])   //column
        {
            return true;
        }

        else return false;
    }
}

public class Player
{
    private readonly char _symbol;

    public Player(char symbol)
    {
        if(symbol == 'X' ||  symbol == 'O')
            _symbol = symbol;        
    }
    public char Symbol => _symbol;

    public int[] PickPosition(Board board)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine($"It's {_symbol} turn! ");
            Console.WriteLine("Enter the position of the board that you wanna play. ");
            Console.WriteLine("Remember: Use the numpad key as the positions of the board ");
            board.DrawBoard();
            ConsoleKey key = Console.ReadKey().Key;

            int[] pos = key switch
            {
                ConsoleKey.NumPad1 => new int[] { 2, 0 },
                ConsoleKey.NumPad2 => new int[] { 2, 1 },
                ConsoleKey.NumPad3 => new int[] { 2, 2 },
                ConsoleKey.NumPad4 => new int[] { 1, 0 },
                ConsoleKey.NumPad5 => new int[] { 1, 1 },
                ConsoleKey.NumPad6 => new int[] { 1, 2 },
                ConsoleKey.NumPad7 => new int[] { 0, 0 },
                ConsoleKey.NumPad8 => new int[] { 0, 1 },
                ConsoleKey.NumPad9 => new int[] { 0, 2 },
            };

            if (board.PositionIsEmpty(pos[0], pos[1]))
            {
                return pos;
            }
            else
            {
                Console.WriteLine("\nPosition already choosen! Try another one.");
                Thread.Sleep(500);
            }

        }
    }
}


public class Board
{
    private readonly char[,] _fields = new char[3, 3];

    public Board()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                //Initialize the multidimensional array with whitespace
                _fields[i, j] = ' ';
            }
        }
    }
    public void DrawBoard()
    {        
        Console.WriteLine($" {_fields[0, 0]}  | {_fields[0, 1]}  | {_fields[0, 2]}");
        Console.WriteLine("----+----+----");
        Console.WriteLine($" {_fields[1, 0]}  | {_fields[1, 1]}  | {_fields[1, 2]}");
        Console.WriteLine("----+----+----");
        Console.WriteLine($" {_fields[2, 0]}  | {_fields[2, 1]}  | {_fields[2, 2]}");
    }
    public void UpdateBoard(int row, int col, char symb)
    {
        _fields[row, col] = symb;
    }

    public char[,] Fields => _fields;
    public bool PositionIsEmpty(int row, int col) => _fields[row, col] != 'X' && _fields[row, col] != 'O';
}

