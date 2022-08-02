using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static int pickupCount = 0;    
    public int levelDuration = 40;
    public float countDown;
    public static bool isGameOver = false;
    public AudioClip loseSFX;
    public AudioClip winSFX;
    public Text timerText;
    public Text statusText;
    public Text scoreText;
    public static int score;
    public static int totalScore;
    public static List<string> sceneNames = new List<string>() {"level1", "level2", "level3"};
    public static int currentLevelIndex;
    string currentLevel = sceneNames[currentLevelIndex];
    public static int levelNum = 2;
    private void Awake() {
        pickupCount = 0;
        currentLevel = SceneManager.GetActiveScene().name;
        isGameOver = false;
        score = 0;
    }
    void Start()
    {
        countDown = levelDuration;
        SetTimerText();
        scoreText.text = "Score: " + score.ToString();
        statusText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isGameOver) {
            if(countDown > 0) {
                countDown -= Time.deltaTime;
            }
            else {
                countDown = 0.0f;
                isGameOver = true;
                LevelLost();
            }
            SetTimerText();
            scoreText.text = "Score: " + score.ToString();
        }
        else {
            scoreText.text = "Total Score: " + totalScore.ToString();
        }
    }

    void SetTimerText()
    {
        timerText.text = countDown.ToString();
    }

    void SetGameOverStatus(string gameTextMessage, AudioClip statusSFX, Color textColor){
        isGameOver = true;
        statusText.text = gameTextMessage;
        statusText.color = textColor;
        statusText.enabled = true;
        AudioSource.PlayClipAtPoint(statusSFX, Camera.main.transform.position);
    }
    public void LevelLost()
    {
        SetGameOverStatus("You Are Starving!", loseSFX, Color.red);
        Camera.main.GetComponent<AudioSource>().pitch = 1;
        Invoke("LoadCurrentLevel", 2);
    }

    public void LevelBeat()
    {
        totalScore += score;
        if(currentLevelIndex < 2) {
            SetGameOverStatus("You Got Enough Food!", winSFX, Color.green);
            currentLevelIndex++;
            currentLevel = sceneNames[currentLevelIndex];
            Invoke("LoadNextLevel", 2);
        }
        else {
            SetGameOverStatus("You Win All Levels!", winSFX, Color.green);
        }
    }
    void LoadCurrentLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    void LoadNextLevel() {
        SceneManager.LoadScene(currentLevel);
    }
}
