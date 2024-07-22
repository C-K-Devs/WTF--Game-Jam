using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainMenuController : MonoBehaviour
{
    public GameObject buttonPanel;
    public GameObject settingsPanel;
    public void PlayGame()
    {
        SceneManager.LoadScene("MainGameScene");
    }

    public void OpenSettings()
    {
        buttonPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void Back()
    {
        settingsPanel.SetActive(false);
        buttonPanel.SetActive(true);
    }

    public void QuitToMainScreen()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    //volume slider need to be implemented that controlls master volume
}
