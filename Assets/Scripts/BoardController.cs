using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Linq;
using UnityEngine;

public class BoardController : MonoBehaviour
{
    // 0 means open slot, 1 means naught, 2 means cross
    private int[,] boardStatus = new int[3, 3] { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };
    private PlacementController[] childScripts;
    public bool playerTeamCross = true;
    private bool playerTurn = true;
    private bool aiMadeTurn = false;
    private bool gameWon = false;
    private bool gameLost = false;
    private bool gameDraw = false;
    private bool gameOver = false;
    private int playerTeamID;
    private int aiTeamID;
    private int turnCount = 0;

    // Start is called before the first frame update
    private void Start()
    {
        childScripts = GetComponentsInChildren<PlacementController>();
        clearBoard();
        if (playerTeamCross)
        {
            playerTeamID = 2;
            aiTeamID = 1;
        }
        else
        {
            playerTeamID = 1;
            aiTeamID = 2;
        }
        if (!playerTeamCross)
        {
            endPlayerTurn();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("EscMenu");
        }
    }

    private void endPlayerTurn() 
    {
        if (!gameOver)
        {
            makeAIMove();
            checkGameOver();
            turnCount++;
        } 
    }

    private void makeAIMove()
    {
        int aiMove = Random.Range(0, 9);
        int aiMoveX = Mathf.FloorToInt(aiMove / 3);
        int aiMoveY = aiMove % 3;
        while (aiMadeTurn == false)
        {
            if (spotIsOpen(aiMoveX, aiMoveY))
            {
                childScripts[aiMove].placeAIObject();
                aiMadeTurn = true;
            } else
            {
                aiMove = Random.Range(0, 9);
                aiMoveX = Mathf.FloorToInt(aiMove / 3);
                aiMoveY = aiMove % 3;
            }
        }
        boardStatus[aiMoveX, aiMoveY] = aiTeamID;
        aiMadeTurn = false;
        playerTurn = true;
    }

    public void makePlayerTurn(int xCoord, int yCoord)
    {
        if (!gameOver)
        {
            boardStatus[xCoord, yCoord] = playerTeamID;
            playerTurn = false;
            checkGameOver();
            endPlayerTurn();
        }
    }

    public bool spotIsOpen(int x, int y)
    {
        if (boardStatus[x, y] == 0)
        {
            return true;
        } else
        {
            return false;
        }
    }

    private void clearBoard()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                boardStatus[i, j] = 0;
            }
        }
    }

    public bool getPlayerTeamCross()
    {
        return playerTeamCross;
    }

    public bool isPlayerTurn()
    {
        return playerTurn;
    }

    public int getTurnCount()
    {
        return turnCount;
    }

    public void setIsPlayerTurn(bool isPlayerTurn)
    {
        this.playerTurn = isPlayerTurn;
    }

    public void fillSpot(int teamId, int xCoord, int yCoord)
    {
        boardStatus[xCoord, yCoord] = teamId;
    }

    private void checkGameOver()
    {
        gameDraw = true;
        gameOver = true;
        for (int i = 0; i < 3; i++)
        {
            int[] currRow = new int[3];
            int[] currCol = new int[3];
            for (int j = 0; j < 3; j++)
            {
                currRow[j] = boardStatus[j, i];
                currCol[j] = boardStatus[i, j];
            }
            if (currCol.SequenceEqual(new int[] { playerTeamID, playerTeamID, playerTeamID }) 
                || currRow.SequenceEqual(new int[] { playerTeamID, playerTeamID, playerTeamID })
                || diagonals(playerTeamID))
            {
                gameWon = true;
            }
            if (currCol.SequenceEqual(new int[] { aiTeamID, aiTeamID, aiTeamID })
                || currRow.SequenceEqual(new int[] { aiTeamID, aiTeamID, aiTeamID })
                || diagonals(aiTeamID))
            {
                gameLost = true;
            }
        }
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (boardStatus[i, j] == 0)
                {
                    gameDraw = false;
                    gameOver = false;
                }
            }
        }

        if (gameWon || gameLost)
        {
            gameOver = true;
            gameDraw = false;
        }

        if (gameWon)
        {
            Debug.Log("Game Won!");
        }
        if (gameLost)
        {
            Debug.Log("Game Lost!");
        }
        if (gameDraw)
        {
            Debug.Log("Game Draw!");
        }
    }

    private bool diagonals(int teamID)
    {
        if ((boardStatus[0, 0] == teamID
            && boardStatus[1,1] == teamID
            && boardStatus[2,2] == teamID)
            || (boardStatus[0, 2] == teamID
            && boardStatus[1, 1] == teamID
            && boardStatus[2, 0] == teamID))
        {
            return true;
        } else
        {
            return false;
        }
    }
}