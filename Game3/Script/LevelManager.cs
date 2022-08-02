using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{ 
    public int levelDuration = 40;
    public int desiredScore;
    float countDown;
    public static bool isGameOver = false;
    public AudioClip loseSFX;
    public AudioClip winSFX;
    public Text timerText;
    public Text statusText;
    public Text scoreText;
    public static int score;
    public static int totalScore;
    public static List<string> sceneNames = new List<string>() {"Level1", "Level2", "Level3"};
    public static int currentLevelIndex;
    string currentLevel = sceneNames[currentLevelIndex];
    public static int levelNum = 2;
    private void Awake() {
        currentLevel = SceneManager.GetActiveScene().name;
        isGameOver = false;
        score = 0;
    }
    // Start is called before the first frame update
    void Start()
    {
        countDown = levelDuration;
        timerText.text = "Time: " + ((int)countDown).ToString();
        scoreText.text = "Score: " + score.ToString();
        statusText.enabled = false; 
    }

    // Update is called once per frame
    void Update()
    {
        if(!isGameOver) {
            if(countDown > 0) {
                if(score >= desiredScore) {
                    isGameOver = true;
                    LevelBeat();
                }
                else {
                    countDown -= Time.deltaTime;
                }
            }
            else {
                countDown = 0.0f;
                isGameOver = true;
                if(score >= desiredScore) {
                    LevelBeat();
                }   
                else {
                    LevelLost();
                }
            }
            scoreText.text = "Score: " + score.ToString();
        }
        else {
            scoreText.text = "Total Score: " + totalScore.ToString();
        }
    }
    private void FixedUpdate() {
        timerText.text = "Time: " + ((int)countDown).ToString();
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
        SetGameOverStatus("You Lost!", loseSFX, Color.red);
        Camera.main.GetComponent<AudioSource>().pitch = 1;
        Invoke("LoadCurrentLevel", 2);
    }
    public void LevelBeat()
    {
        totalScore += score;
        if(currentLevelIndex < 2) {
            SetGameOverStatus("You Win!", winSFX, Color.green);
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
