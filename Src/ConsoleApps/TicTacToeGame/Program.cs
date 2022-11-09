using System.Diagnostics;

string[,] board =
{
    { "1", "2", "3" },
    { "4", "5", "6" },
    { "7", "8", "9" }
};

int currentPlayer = 0;

bool gameState = false;

void PrintBoard()
{
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine("     |     |");
    Console.WriteLine("  {0}  |  {1}  |  {2}", board[0, 0], board[0, 1], board[0, 2]);
    Console.WriteLine("     |     |");
    Console.WriteLine("-----------------");
    Console.WriteLine("     |     |");
    Console.WriteLine("  {0}  |  {1}  |  {2}", board[1, 0], board[1, 1], board[1, 2]);
    Console.WriteLine("     |     |");
    Console.WriteLine("-----------------");
    Console.WriteLine("     |     |");
    Console.WriteLine("  {0}  |  {1}  |  {2}", board[2, 0], board[2, 1], board[2, 2]);
    Console.WriteLine("     |     |\n");
}

void PlayGame()
{
    while (!gameState)
    {
        PrintBoard();
        UserMove();
        CheckForWin();
        if (!gameState) { currentPlayer = currentPlayer == 0 ? 1 : 0; }
    }

    PrintBoard();

    if (!CheckForDraw())
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("Congratulations player {0}. ");
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("type \"n\" to quit or just press return to play again.\n", currentPlayer + 1);
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.Write("Draw! ");
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("type \"n\" to quit or just press return to play again.\n");
    }

    string playAgain = Console.ReadLine();

    playAgain = playAgain.ToLower();

    if (playAgain != "n")
    {
        ResetGame();

        PlayGame();
    }
}

void UserMove()
{
    Console.ForegroundColor = ConsoleColor.White;
    Console.Write("Player {0}'s Turn. Choose a spot: ", currentPlayer + 1);
    GetUserInput();
}

void GetUserInput()
{
    string place = Console.ReadLine();
    int placeInt = CheckRange(place);

    int[] boardPlace = GetPlace(placeInt);

    Debug.Write(board[boardPlace[0], boardPlace[1]]);

    if (board[boardPlace[0], boardPlace[1]] != "X" && board[boardPlace[0], boardPlace[1]] != "O") { board[boardPlace[0], boardPlace[1]] = currentPlayer == 0 ? "X" : "O"; }
    else
    {
        Console.Clear();
        PrintBoard();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("Piece already taken, pick again! ");
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("Player {0}'s Turn. Choose a spot: \n", currentPlayer + 1);
        GetUserInput();
    }
}

int CheckRange(string piece)
{
    int place = CheckValidNumber(piece);

    while (place > 9 || place < 1)
    {
        Console.Clear();
        PrintBoard();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("You entered a incorrect place number, try Again! ");
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("Player {0}'s Turn. Choose a spot: \n", currentPlayer + 1);
        piece = Console.ReadLine();
        place = CheckValidNumber(piece);
    }

    return place;
}

int CheckValidNumber(string piece)
{
    int pieceInt;

    while (!int.TryParse(piece, out pieceInt))
    {
        Console.Clear();
        PrintBoard();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("You did not enter a valid number, try Again! ");
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("Player {0}'s Turn. Choose a spot: \n", currentPlayer + 1);
        piece = Console.ReadLine();
    }

    return pieceInt;
}

int[] GetPlace(int place)
{
    return place switch
    {
        1 => new int[] { 0, 0 },
        2 => new int[] { 0, 1 },
        3 => new int[] { 0, 2 },
        4 => new int[] { 1, 0 },
        5 => new int[] { 1, 1 },
        6 => new int[] { 1, 2 },
        7 => new int[] { 2, 0 },
        8 => new int[] { 2, 1 },
        9 => new int[] { 2, 2 },
        _ => new int[] { -1, -1 },
    };
}

void CheckForWin()
{
    string piece = currentPlayer == 0 ? "X" : "O";

    Debug.WriteLine(" {0}", piece);

    if (board[0, 0] == piece && board[0, 1] == piece && board[0, 2] == piece ||
        board[1, 0] == piece && board[1, 1] == piece && board[1, 2] == piece ||
        board[2, 0] == piece && board[2, 1] == piece && board[2, 2] == piece ||
        board[0, 0] == piece && board[1, 0] == piece && board[2, 0] == piece ||
        board[0, 1] == piece && board[1, 1] == piece && board[2, 1] == piece ||
        board[0, 2] == piece && board[1, 2] == piece && board[2, 2] == piece ||
        board[0, 0] == piece && board[1, 1] == piece && board[2, 2] == piece ||
        board[0, 2] == piece && board[1, 1] == piece && board[2, 0] == piece)
    {
        Debug.WriteLine("Winner declared");
        gameState = true;
    }
    else if (CheckForDraw())
    {
        gameState = true;
    }
    else
    {
        Debug.WriteLine("Keep playing!");
        gameState = false;
    }
}

bool CheckForDraw()
{
    int count = 0;

    foreach (string spot in board)
    {
        if (spot == "X" || spot == "O") count++;
    }

    return count == 9;
}

void ResetGame()
{
    string[,] boardTemp =
    {
        { "1", "2", "3" },
        { "4", "5", "6" },
        { "7", "8", "9" }
    };

    board = boardTemp;

    currentPlayer = 0;

    gameState = false;
}

PlayGame();