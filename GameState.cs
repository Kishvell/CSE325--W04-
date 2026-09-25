namespace W04;

public class GameState
{
    public enum WinState
    {
        NoWinner,
        Player1_Wins,
        Player2_Wins,
        Tie
    }

    public int[] Board { get; private set; } = new int[42];
    public int PlayerTurn { get; private set; } = 1;
    public int CurrentTurnNumber { get; private set; } = 1;

    public WinState CheckForWin()
    {
        int checkBoard(int row, int col) => Board[row * 7 + col];

        // Horizontal check
        for (int r = 0; r < 6; r++)
        {
            for (int c = 0; c < 4; c++)
            {
                int val = checkBoard(r, c);
                if (val != 0 && val == checkBoard(r, c + 1) && val == checkBoard(r, c + 2) && val == checkBoard(r, c + 3))
                    return val == 1 ? WinState.Player1_Wins : WinState.Player2_Wins;
            }
        }

        // Vertical check
        for (int r = 0; r < 3; r++)
        {
            for (int c = 0; c < 7; c++)
            {
                int val = checkBoard(r, c);
                if (val != 0 && val == checkBoard(r + 1, c) && val == checkBoard(r + 2, c) && val == checkBoard(r + 3, c))
                    return val == 1 ? WinState.Player1_Wins : WinState.Player2_Wins;
            }
        }

        // Diagonal (bottom-left to top-right)
        for (int r = 3; r < 6; r++)
        {
            for (int c = 0; c < 4; c++)
            {
                int val = checkBoard(r, c);
                if (val != 0 && val == checkBoard(r - 1, c + 1) && val == checkBoard(r - 2, c + 2) && val == checkBoard(r - 3, c + 3))
                    return val == 1 ? WinState.Player1_Wins : WinState.Player2_Wins;
            }
        }

        // Diagonal (top-left to bottom-right)
        for (int r = 0; r < 3; r++)
        {
            for (int c = 0; c < 4; c++)
            {
                int val = checkBoard(r, c);
                if (val != 0 && val == checkBoard(r + 1, c + 1) && val == checkBoard(r + 2, c + 2) && val == checkBoard(r + 3, c + 3))
                    return val == 1 ? WinState.Player1_Wins : WinState.Player2_Wins;
            }
        }

        if (Board.All(cell => cell != 0)) return WinState.Tie;

        return WinState.NoWinner;
    }

    public int PlayPiece(int col)
    {
        for (int row = 5; row >= 0; row--)
        {
            int index = row * 7 + col;
            if (Board[index] == 0)
            {
                Board[index] = PlayerTurn;
                int landedRow = row;
                PlayerTurn = PlayerTurn == 1 ? 2 : 1;
                CurrentTurnNumber++;
                return landedRow;
            }
        }
        return -1;
    }

    public void Reset()
    {
        Board = new int[42];
        PlayerTurn = 1;
        CurrentTurnNumber = 1;
    }
}