using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager inst;

    public GameObject ringPrefab;
    private List<GameObject> ringsList = new List<GameObject>();

    public Player player;

    public GameObject gameCanvas;
    public Text gameOverText;

    public Text scoreText;
    public Text highScoreText;

    public static int score = 0;
    public static int highScore = 0;

    private void Awake()
    {
        if (inst != null)
        {
            Destroy(this);
        }
        inst = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        player.disablePlayer(true);
        gameCanvas.SetActive(true);
        showHighScore();
    }


    // Play button
    public void onPlayBtnClick()
    {
        gameCanvas.SetActive(false);
        player.disablePlayer(false);
        startCreatingRings();
        score = 0;
        updateScore();
    }

    void startCreatingRings()
    {
        InvokeRepeating("CreateRings", 0.5f, 3f);
    }

    void stopCreatingRings()
    {
        CancelInvoke();
    }

    // create Rings
    void CreateRings()
    {
        GameObject g = getRing();
        g.SetActive(true);
        setRingPosition(g);
    }

    // get last deactivated ring or instatintiate new Ring
    private GameObject getRing()
    {
        if (ringsList.Count == 0)
        {
            ringsList.Add(Instantiate(ringPrefab));
        }
        else
        {
            if (ringsList[0].activeSelf)
            {
                ringsList.Add(Instantiate(ringPrefab));
            }
            else
            {
                GameObject g = ringsList[0];
                ringsList.RemoveAt(0);
                ringsList.Add(g);
            }
        }

        return ringsList[ringsList.Count - 1];
    }

    // assign random position to ring
    private void setRingPosition(GameObject ring)
    {
        ring.transform.localPosition = new Vector3(14f, Random.Range(-4.2f, 4.2f), -1);
    }

    //update score text
    public void updateScore()
    {
        scoreText.text ="SCORE  :  "+ score.ToString();
        showHighScore();
    }


    // display and update highscore
    public void showHighScore()
    {
        if (PlayerPrefs.HasKey("HighScore"))
        {
            highScore = PlayerPrefs.GetInt("HighScore");
            gameOverText.text = "Welcome Back";
        }
        else
        {
            gameOverText.text = "Welcome";
        }

        if (score > highScore)
        {
            highScore = score;
        }

        PlayerPrefs.SetInt("HighScore", highScore);
        PlayerPrefs.Save();

        highScoreText.text = "HIGH SCORE  :  " + highScore.ToString();
    }

    public void gameOver()
    {
        gameOverText.text = "Game Over";
        gameCanvas.SetActive(true);
        player.disablePlayer(true);
        stopCreatingRings();
    }
}
