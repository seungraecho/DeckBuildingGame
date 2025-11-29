using UnityEngine;

public class GamePauseSystem : MonoBehaviour
{
    public GameObject PausePanel;
    public GameObject RestartPanel;
    public GameObject QuitPanel;
    public Startmenu startmenu;
    public bool isGamePaused = false;
    void Start()
    {
        PausePanel.SetActive(false);
        RestartPanel.SetActive(false);
        QuitPanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (RestartPanel.activeSelf) { CancelRestart(); }
            else if(QuitPanel.activeSelf) { CancelQuit(); }
            else if (isGamePaused) { ResumeGame(); }
            else { PauseGame(); }
        }
    }

    void PauseGame()
    {
        Time.timeScale = 0;
        PausePanel.SetActive(true);
        isGamePaused = true;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        PausePanel.SetActive(false);
        isGamePaused = false;
    }

    public void SetRestart()
    {
        RestartPanel.SetActive(true);
    }

    public void CancelRestart()
    {
        RestartPanel.SetActive(false);
    }

    public void DoRestart()
    {
        Time.timeScale = 1;
        ResumeGame();
        CancelRestart();
        startmenu.inGame = false;
        startmenu.LoadInGameScene(true);
    }

    public void SetQuit()
    {
        QuitPanel.SetActive(true);
    }

    public void CancelQuit()
    {
        QuitPanel.SetActive(false);
    }

    public void DoTitleQuit()
    {
        Time.timeScale = 1;
        ResumeGame();
        CancelQuit();
        startmenu.GameQuit(true);
    }

    public void DoGameQuit()
    {
        Time.timeScale = 1;
        ResumeGame();
        CancelQuit();
        startmenu.inGame = false;
        startmenu.GameQuit(true);
    }
}
