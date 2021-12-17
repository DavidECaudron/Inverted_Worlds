using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] AudioClip _clickSound;
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
        AudioManager.instance.PlayClipAt(_clickSound, transform.position);
        gameObject.SetActive(false);
    }

    public void RestartLevel()
    {
        AudioManager.instance.PlayClipAt(_clickSound, transform.position);
        LevelLoader.instance.LoadScene(SceneManager.GetActiveScene().name);
        Resume();
    }

    public void MainMenu()
    {
        AudioManager.instance.PlayClipAt(_clickSound, transform.position);
        LevelLoader.instance.LoadScene("Menu");
        Resume();
    }

}
