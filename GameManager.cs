using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // constants
    private const int PLAYER1 = 1;
    private const int PLAYER2 = 2;
    private const float RESTARTDELAY = 0.5f;

    bool gameHasEnded = false;
    public Text winnerText;
    public Text endGametext;
    public Button startButton;
    public Button restartButton;
    private static bool started = false;
    public Button[] buttons;

    public void Game()
    {
        Button start = startButton.GetComponent<Button>();
        start.onClick.AddListener(StartGame);

        if (started)
        {
            start.gameObject.SetActive(false);
        }

    }

    void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        started = true;
    }

    public void EndGame(int winner)
    {

        if (gameHasEnded == false)
        {
            if (winner == PLAYER1)
            {
                winnerText.text = "Player 1 wins!";
            }
            else if (winner == PLAYER2)
            {
                winnerText.text = "Player 2 wins!";
            }
            else
            {
                winnerText.text = "Tie";
            }
            Invoke("restart", RESTARTDELAY);
        }

    }
    void restart()
    {
        for (int i = 0; i <= 8; i++)
        {
            buttons[i].interactable = false;
            buttons[i].GetComponentInChildren<Text>().enabled = false;
        }

        endGametext.enabled = true;
        gameHasEnded = true;

        winnerText.enabled = true;
        restartButton.gameObject.SetActive(true);

        Button restart = restartButton.GetComponent<Button>();
        restart.onClick.AddListener(StartGame);

        if (restart.interactable == true)
        {
            // because its static it can be accessed directly by the class
            Move.moveNum = 0;
        }
    }
}
