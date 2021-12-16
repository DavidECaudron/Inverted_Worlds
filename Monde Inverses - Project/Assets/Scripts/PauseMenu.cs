using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private void OnEnable()
    {
        Time.timeScale = 0;
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
    }

    public void Resume()
    {
        gameObject.SetActive(false);
    }

    public void RestartLevel()
    {
        LevelLoader.instance.LoadScene(SceneManager.GetActiveScene().name);
        Resume();
    }

    public void MainMenu()
    {
        LevelLoader.instance.LoadScene("Menu");
        Resume();
    }

}
