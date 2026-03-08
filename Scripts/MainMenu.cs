using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void OnLevel1ButtonPressed()
    {
        SceneManager.LoadScene("Level1"); 
    }

    public void OnExitToDesktopButtonPressed()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #endif
                Application.Quit();
    }
    public void OnLevel2ButtonPressed()
    {
        SceneManager.LoadScene("Level2"); 
    }
}
