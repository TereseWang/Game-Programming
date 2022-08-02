using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialWindow : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject pauseMenu;
    public static bool isGamePaused = true;
    void Start()
    {
        PauseGame();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            if(isGamePaused) {
                ResumeGame();
            }
            else {
                PauseGame();
            }
        }
    }
   void PauseGame() {
        isGamePaused = true;
        Time.timeScale = 0.0f;
        pauseMenu.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ResumeGame() {
        isGamePaused = false;
        Time.timeScale = 1.0f;
        pauseMenu.SetActive(false);
    }
}
