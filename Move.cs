using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class Move : MonoBehaviour
{
    // constants
    private const int PLAYER1 = 1;
    private const int PLAYER2 = 2;
    private const int WIDTH = 3;
    private const int MAXGRID = 8;

    // Start is called before the first frame update
    public Button gridSpaceBtn;

    // array to store the active buttons
    public static Button[] gridSpaceActive = new Button[10];

    public static int moveNum = 0;
    private Text playerSymText;
    private static int playerNum = 1;
    private string curButTag;
    private string curButText;
    private int ctr = 1;
    private int winner = -1;

    void Start()
    {
        // player move button
        Button btn = gridSpaceBtn.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);

        // start the game 
        FindObjectOfType<GameManager>().Game();

        FindObjectOfType<GameManager>().endGametext.enabled = false;
        FindObjectOfType<GameManager>().restartButton.gameObject.SetActive(false);
        FindObjectOfType<GameManager>().winnerText.enabled = false;
    }


    void TaskOnClick()
    {
        moveNum++;

        if (playerNum == PLAYER1)
        {
            SetText("X");
            playerNum = PLAYER2;
        }
        else
        {
            SetText("O");
            playerNum = PLAYER1;
        }

        int gridSpaceNum = Int32.Parse(gridSpaceBtn.tag);

        gridSpaceActive[gridSpaceNum] = gridSpaceBtn;
        Checkwinner(gridSpaceActive, gridSpaceNum);

        // prevents reclicking of the button
        gridSpaceBtn.interactable = false;

        if (winner == PLAYER1 || winner == PLAYER2 || moveNum == 9)
        {
            FindObjectOfType<GameManager>().EndGame(winner);
        }
    }

    private void SetText(string text)
    {
        playerSymText = gridSpaceBtn.GetComponentInChildren<Text>();
        playerSymText.text = text;
    }

    private void Checkwinner(Button[] moveBtn, int gridNum)
    {
        curButTag = gridSpaceBtn.tag;
        curButText = gridSpaceBtn.GetComponentInChildren<Text>().text;

        int row = 0;
        if (gridNum > 5)
        {
            row = 2;
        }
        else if (gridNum >= 3)
        {
            row = 1;
        }

        else
        {
            row = 0;
        }
        int col = gridNum - (row * 3);

        for (int i = 0; i <= WIDTH; i++)
        {
            CheckWinner(moveBtn, curButTag, curButText, row, col, i);
            PossibleWinner();
            ctr = 1;
        }
    }

    private void PossibleWinner()
    {
        if (ctr == 3)
        {
            if (curButText == "X")
            {
                winner = PLAYER1;
            }
            else
            {
                winner = PLAYER2;
            }
            return;
        }
    }
    private void CheckWinner(Button[] moveBtn, string tag, string curtext, int row, int col, int dir)
    {
        int loc = 0;
        int loc2 = 0;
        for (int i = 1; i <= WIDTH - 1; i++)
        {
            // diagonally left 
            if (dir == 0)
            {
                if (row - i >= 0 && row - i <= (WIDTH - 1) && col + i >= 0 && col + i <= (WIDTH - 1))
                {
                    loc = (row - i) * WIDTH + (col + i);
                    Checkloc(loc, moveBtn, curtext);
                }
                if (row + i >= 0 && row + i <= (WIDTH - 1) && col - i >= 0 && col - i <= (WIDTH - 1))
                {
                    loc2 = (row + i) * WIDTH + (col - i);
                    Checkloc(loc2, moveBtn, curtext);
                }
            }
            // diagonally right
            else if (dir == 1)
            {
                if (row + i >= 0 && row + i <= (WIDTH - 1) && col + i >= 0 && col + i <= (WIDTH - 1))
                {
                    loc = ((row + i) * WIDTH) + (col + i);
                    Checkloc(loc, moveBtn, curtext);
                }
                if (row - i >= 0 && row - i <= (WIDTH - 1) && col - i >= 0 && col - i <= (WIDTH - 1))
                {
                    loc2 = ((row - i) * WIDTH) + (col - i);
                    Checkloc(loc2, moveBtn, curtext);
                }
            }

            // vertical
            else if (dir == 2)
            {
                if (row + i >= 0 && row + i <= (WIDTH - 1))
                {
                    loc = (row + i) * WIDTH + col;
                    Checkloc(loc, moveBtn, curtext);
                }
                if (row - i >= 0 && row - i <= (WIDTH - 1))
                {
                    loc2 = (row - i) * WIDTH + col;
                    Checkloc(loc2, moveBtn, curtext);

                }
            }
            // horizontal
            else if (dir == 3)
            {
                if (col + i >= 0 && col + i <= (WIDTH - 1))
                {
                    loc = row * WIDTH + (col + i);
                    Checkloc(loc, moveBtn, curtext);
                }
                if (col - i >= 0 && col - i <= (WIDTH - 1))
                {
                    loc2 = row * WIDTH + (col - i);
                    Checkloc(loc2, moveBtn, curtext);

                }
            }

        }
    }


    private void Checkloc(int loc, Button[] moves, string curtext)
    {
        if (loc >= 0 && loc <= MAXGRID)
        {
            if (moves[loc])
            {
                String ButText = moves[loc].GetComponentInChildren<Text>().text;
                if (ButText == curtext)
                {
                    ctr++;
                }
            }
        }
    }
}
